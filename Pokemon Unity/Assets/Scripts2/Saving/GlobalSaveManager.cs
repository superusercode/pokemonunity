﻿using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

namespace PokemonUnity.Saving
{
    using PokemonUnity.Pokemon;
    using PokemonUnity.Item;
    using PokemonUnity.Saving.Location;

    public static class GlobalSaveManager
    {
        private const string BuildVersion = "0.0.1";

        //If UseAppdata = true, Pokemon Unity will save the save files into %AppData%/Roaming/Pokemon Unity/Saves
        //If UseAppdata = false, Pokemon Unity will save the save files into Assets/Saves
        private const bool UseAppdate = false;
        private static string saveLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\Pokemon Unity\Saves\";

        private static UnityEngine.GameObject Player;
        private static List<SaveEvent> EventSaves = new List<SaveEvent>();

        public static void RegisterPlayer(UnityEngine.GameObject player)
        {
            Player = player;
            UnityEngine.Debug.Log("Registered Player.");
        }

        /// <summary>
        /// Registers an event into the EventSaves List<>
        /// </summary>
        /// <param name="customEvent">The event that needs to be registered</param>
        public static void RegisterEvent(SaveEvent customEvent)
        {
            EventSaves.Add(customEvent);
            EventSaves = EventSaves.OrderBy(x => x.EventTime).ToList();
            UnityEngine.Debug.Log(customEvent.ToString());
        }

        /// <summary>
        /// Get's the relevant CustomSaveEvents for the current Scene.
        /// </summary>
        /// <param name="sceneIndex">The scene index that the Player is currently on.</param>
        /// <returns></returns>
        public static List<SaveEvent> GetRelaventSaveData(int sceneIndex)
        {
            return EventSaves.Where(x => x.SceneIndex == sceneIndex).ToList();
        }

        /// <summary>
        /// Saves the game using the standard data.
        /// </summary>
        public static void Save(string saveName)
        {
            if (!UseAppdate)
            {
                saveLocation = UnityEngine.Application.dataPath + "/Saves/";
            }

            Pokemon[] Party = GameVariables.playerTrainer.Trainer.Party;
            Pokemon[,] PC = GameVariables.PC_Poke;
            List<Items> PlayerBag = GameVariables.Bag_Items;

            SaveData DataToSave = new SaveData(
                saveName,
                SceneManager.GetActiveScene().buildIndex,
                GameVariables.playerTrainer.Trainer.Name,
                GameVariables.playerTrainer.TrainerID, GameVariables.playerTrainer.SecretID,
                GameVariables.playerTrainer.isMale, //GameVariables.playerTrainer.GymBadges,
                GameVariables.playerTrainer.playerPokedex,
                GameVariables.playerTrainer.playerTime,
                Player.transform.position, Player.GetComponent<Player>().playerDirection,
                Player.transform.Find("Follower").transform.position, Player.GetComponent<FollowerMovement>().direction,
                Party, PC, PlayerBag,
                EventSaves
                );

            SerializeAndCreateSaveFile(DataToSave);
        }

        /// <summary>
        /// Saves the game using a given SaveData class.
        /// </summary>
        /// <param name="saveData">The SaveData containing the data that needs to be saved.</param>
        public static void Save(SaveData saveData)
        {
            SerializeAndCreateSaveFile(saveData);
        }

        private static void SerializeAndCreateSaveFile(SaveData saveData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                int saveAmount = Directory.GetFiles(saveLocation, "*pku", SearchOption.TopDirectoryOnly).Length;

                if (saveAmount < 0)
                    saveAmount = 0;

                FileStream file = File.Open(saveLocation + @"Save" + saveAmount.ToString() + ".pku", FileMode.OpenOrCreate, FileAccess.Write);
                bf.Serialize(file, saveData);
                file.Close();
                UnityEngine.Debug.Log("Save file created.");
            }
            catch (Exception)
            {
                UnityEngine.Debug.Log("Pokemon Unity save directory does not exist, creating new one...");
                Directory.CreateDirectory(saveLocation.Substring(0, saveLocation.Length - 1));
                UnityEngine.Debug.Log("Trying to save again...");

                FileStream file = File.Open(saveLocation + @"Save" + (Directory.GetFiles(saveLocation, "*pku", SearchOption.TopDirectoryOnly).Length).ToString() + ".pku", FileMode.OpenOrCreate, FileAccess.Write);
                bf.Serialize(file, saveData);
                file.Close();

                UnityEngine.Debug.Log("Save file created.");
            }
        }

        /// <summary>
        /// Loads the Save file that corresponeds with the saveInex.
        /// </summary>
        /// <param name="saveIndex">The index of the save (starting from 0, FE: "Save0.pku")</param>
        public static void Load(int saveIndex)
        {
            if (!UseAppdate)
            {
                saveLocation = UnityEngine.Application.dataPath + "/Saves/";
                if (!Directory.Exists(saveLocation.Substring(0, saveLocation.Length - 1)))
                {
                    Directory.CreateDirectory(saveLocation.Substring(0, saveLocation.Length - 1));
                }
            }

            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                FileStream file = File.Open(saveLocation + "Save" + saveIndex.ToString() + ".pku", FileMode.Open, FileAccess.Read);
                SaveData DataToLoad = (SaveData)bf.Deserialize(file);

                if (null != DataToLoad)
                {

                    //EventSaves contains all the Events that the Player has encountered
                    EventSaves = DataToLoad.EventList;

                    if (SceneManager.GetActiveScene().buildIndex != DataToLoad.ActiveScene)
                    {
                        SceneManager.LoadScene(DataToLoad.ActiveScene);
                    }
                    else
                    {
                        //Loads the Trainer's Party into the GameVariables
                        for (int i = 0; i < DataToLoad.PlayerParty.Length; i++)
                        {
                            //The Trainer.Party is `private set;`, so we just loop through it and set it individually
                            GameVariables.playerTrainer.Trainer.Party[i] = DataToLoad.PlayerParty[i];
                        }
                        GameVariables.PC_Poke = DataToLoad.PC;

                        //Loads the Bag (containing the Items that the player owns) into the GameVariables
                        GameVariables.Bag_Items = DataToLoad.PlayerBag;


                        //Loading Player
                        UnityEngine.GameObject Player = UnityEngine.GameObject.FindGameObjectWithTag("Player");
                        Player.transform.position = DataToLoad.PlayerPosition;
                        Player.GetComponent<PlayerMovement>().direction = DataToLoad.PlayerDirection;
                        //Loading Follower
                        UnityEngine.GameObject Follower = Player.transform.Find("Follower").gameObject;
                        Follower.transform.position = DataToLoad.FollowerPosition;
                        Follower.GetComponent<FollowerMovement>().direction = DataToLoad.FollowerDirection;

                        EventSaves = EventSaves.OrderBy(x => x.EventTime).ToList();
                    }
                }

                file.Dispose();
            }
            catch (FileNotFoundException)
            {
                UnityEngine.Debug.Log("Couldn't find \"Save" + saveIndex + ".pku\".");
            }
        }

        /// <summary>
        /// Gets the amount of save files that's specified in Amount.
        /// </summary>
        /// <param name="Amount">The amount of save files that needs to be loaded (0 for all the save files).</param>
        /// <returns>A List containing the relevant amount of save files.</returns>
        static List<SaveData> GetSaves(int Amount)
        {
            List<SaveData> saveFiles = new List<SaveData>();
            foreach (string file in Directory.GetFiles(saveLocation))
            {
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    if (Path.GetExtension(file) == "pku")
                    {
                        using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                            try
                            {
                                SaveData saveData = (SaveData)bf.Deserialize(fileStream);
                                if (saveData.BuildVersion == BuildVersion)
                                {
                                    saveFiles.Add(saveData);
                                }
                                else
                                {
                                    //Try to convert the created file into the current version
                                }
                            }
                            catch (Exception e)
                            {
                                UnityEngine.Debug.Log(e.ToString());
                            }
                        }
                    }
                }
                catch (IOException e)
                {
                    throw e;
                }
            }

            saveFiles = saveFiles.OrderBy(x => x.TimeCreated).ToList();
            if (Amount == 0 || saveFiles.Count < Amount)
            {
                return saveFiles;
            }
            else
            {
                return saveFiles.Take(Amount).ToList();
            }
        }

        public static string GetBuildVersion()
        {
            return BuildVersion;
        }
    }
}