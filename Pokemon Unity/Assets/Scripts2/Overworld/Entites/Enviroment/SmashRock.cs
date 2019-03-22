﻿using PokemonUnity.Pokemon;
using System;
using System.Collections.Generic;

namespace PokemonUnity.Overworld.Entity.Environment
{
public class SmashRock : Entity
{
    public override void ClickFunction()
    {
        if (Screen.TextBox.Showing == false)
        {
            string pName = "";

            foreach (Pokemon p in Core.Player.Pokemons)
            {
                if (p.isEgg == false)
                {
                    foreach (BattleSystem.Attack a in p.Attacks)
                    {
                        if (a.Name.ToLower() == ("Rock Smash").ToLower())
                        {
                            pName = p.GetDisplayName();
                            break;
                        }
                    }
                }

                if (pName != "")
                    break;
            }

            string text = "This rock looks like~it can be broken!";

            if (pName != "" | GameController.IS_DEBUG_ACTIVE == true | Core.Player.SandBoxMode == true)
                text += "~Do you want to~use Rock Smash?%Yes|No%";

            Screen.TextBox.Show(text, this);
            SoundManager.PlaySound("select");
        }
    }

    public override void ResultFunction(int Result)
    {
        if (Result == 0)
        {
            string pName = "";

            foreach (Pokemon p in Core.Player.Pokemons)
            {
                if (p.isEgg == false)
                {
                    foreach (BattleSystem.Attack a in p.Attacks)
                    {
                        if (a.Name.ToLower() == ("Rock Smash").ToLower())
                        {
                            pName = p.GetDisplayName();
                            break;
                        }
                    }
                }

                if (pName != "")
                    break;
            }

            Pokemon spawnedPokemon = null/* TODO Change to default(_) if this is not a reference type */;
            if (Core.Random.Next(0, 100) < 20)
            {
                spawnedPokemon = Spawner.GetPokemon(Screen.Level.LevelFile, Spawner.EncounterMethods.RockSmash, false);
                if (spawnedPokemon == null)
                {
                    string s = "version=2" + Environment.NewLine + "@text.show(" + pName + " used~Rock Smash!)" + Environment.NewLine + "@sound.play(destroy)" + Environment.NewLine + ":end";
                    (OverworldScreen)Core.CurrentScreen.ActionScript.StartScript(s, 2);
                }
                else
                {
                    string s = "version=2" + Environment.NewLine + "@text.show(" + pName + " used~Rock Smash!)" + Environment.NewLine + "@sound.play(destroy)" + Environment.NewLine + "@level.update" + Environment.NewLine + "@text.show(A wild Pokémon~appeared!)" + Environment.NewLine + "@battle.wild(" + spawnedPokemon.Number + "," + spawnedPokemon.Level + ")" + Environment.NewLine + ":end";
                    (OverworldScreen)Core.CurrentScreen.ActionScript.StartScript(s, 2);
                }
            }
            else if (Core.Random.Next(0, 100) < 20)
            {
                int ItemID = GetItemID();
                string s = "version=2" + Environment.NewLine + "@text.show(" + pName + " used~Rock Smash!)" + Environment.NewLine + "@sound.play(destroy)" + Environment.NewLine + "@level.update" + Environment.NewLine + "@item.give(" + ItemID + ",1)" + Environment.NewLine + "@item.messagegive(" + ItemID + ",1)" + Environment.NewLine + ":end";
                (OverworldScreen)Core.CurrentScreen.ActionScript.StartScript(s, 2);
            }
            else
            {
                string s = "version=2" + Environment.NewLine + "@text.show(" + pName + " used~Rock Smash!)" + Environment.NewLine + "@sound.play(destroy)" + Environment.NewLine + ":end";
                (OverworldScreen)Core.CurrentScreen.ActionScript.StartScript(s, 2);
            }
            PlayerStatistics.Track("Rock Smash used", 1);

            this.CanBeRemoved = true;
        }
    }

    private int GetItemID()
    {
        List<ItemContainer> MatchingContainers = new List<ItemContainer>();
        List<int> Chances = new List<int>();
        foreach (ItemContainer c in ItemContainerlist)
        {
            if (c.MapFile.ToLower() == Screen.Level.LevelFile.ToLower())
            {
                MatchingContainers.Add(c);
                Chances.Add(c.Chance);
            }
        }
        if (MatchingContainers.Count == 0)
            return 190;

        return MatchingContainers[GetRandomChance(Chances)].ItemID;
    }

    private class ItemContainer
    {
        public int ItemID = 190;
        public int Chance = 0;
        public string MapFile = "";

        public ItemContainer(string MapFile, string Data)
        {
            this.MapFile = MapFile;
            // {ID,Chance}
            Data = Data.Remove(Data.Length - 1, 1).Remove(0, 1);
            string[] DataArray = Data.Split(System.Convert.ToChar(","));
            this.ItemID = System.Convert.ToInt32(DataArray[0]);
            this.Chance = System.Convert.ToInt32(DataArray[1]);
        }
    }

    private static List<ItemContainer> ItemContainerlist = new List<ItemContainer>();

    public static void Load()
    {
        ItemContainerlist.Clear();
        string File = GameModeManager.GetContentFilePath(@"Data\smashrockitems.dat");
        if (System.IO.File.Exists(File) == true)
        {
            System.Security.FileValidation.CheckFileValid(File, false, "SmashRock.vb");
            string[] data = System.IO.File.ReadAllLines(File);
            foreach (string line in data)
            {
                string[] Linedata = line.Split(System.Convert.ToChar("|"));
                string Mapfile = Linedata[0];
                for (var i = 1; i <= Linedata.Length - 1; i++)
                    ItemContainerlist.Add(new ItemContainer(Mapfile, Linedata[i]));
            }
        }
    }

    public override void UpdateEntity()
    {
        base.UpdateEntity();

        if (Rotation.Y != Screen.Camera.Yaw)
        {
            this.Rotation.Y = Screen.Camera.Yaw;
            this.CreatedWorld = false;
        }
    }

    public override void Render()
    {
        this.Draw(this.Model, this.Textures, false);
    }
}
}