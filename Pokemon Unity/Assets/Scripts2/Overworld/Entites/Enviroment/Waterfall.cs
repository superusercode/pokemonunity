﻿using System;
using System.Collections.Generic;
using System.Linq;
using PokemonUnity.Pokemon;

namespace PokemonUnity.Overworld.Entity.Environment
{
public class Waterfall : Entity
{
    private static Dictionary<string, Texture2D> WaterfallTexturesTemp = new Dictionary<string, Texture2D>();
    private static Dictionary<string, Texture2D> WaterTexturesTemp = new Dictionary<string, Texture2D>();

    private string waterFallTextureName = "";
    private string waterTextureName = "";

    private Animation WaterAnimation;
    private Rectangle currentRectangle = new Rectangle(0, 0, 0, 0);

    public static void ClearAnimationResources()
    {
        WaterfallTexturesTemp.Clear();
        WaterTexturesTemp.Clear();
    }

    public override void Initialize()
    {
        base.Initialize();

        WaterAnimation = new Animation(TextureManager.GetTexture(@"Textures\Routes"), 1, 3, 16, 16, 9, 13, 0);

        CreateWaterTextureTemp();
    }

    private void CreateWaterTextureTemp()
    {
        if (Core.GameOptions.GraphicStyle == 1)
        {
            List<string> textureData = this.AdditionalValue.Split(System.Convert.ToChar(",")).ToList();

            if (textureData.Count >= 5)
            {
                Rectangle r = new Rectangle(System.Convert.ToInt32(textureData[1]), System.Convert.ToInt32(textureData[2]), System.Convert.ToInt32(textureData[3]), System.Convert.ToInt32(textureData[4]));
                string texturePath = textureData[0];
                this.waterFallTextureName = AdditionalValue;
                if (Waterfall.WaterfallTexturesTemp.ContainsKey(AdditionalValue + "_0") == false)
                {
                    Waterfall.WaterfallTexturesTemp.Add(AdditionalValue + "_0", TextureManager.GetTexture(texturePath, new Rectangle(r.X, r.Y, r.Width, r.Height)));
                    Waterfall.WaterfallTexturesTemp.Add(AdditionalValue + "_1", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width, r.Y, r.Width, r.Height)));
                    Waterfall.WaterfallTexturesTemp.Add(AdditionalValue + "_2", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 2, r.Y, r.Width, r.Height)));
                }
            }
            else if (Waterfall.WaterfallTexturesTemp.ContainsKey("_0") == false)
            {
                Waterfall.WaterfallTexturesTemp.Add("_0", TextureManager.GetTexture("Routes", new Rectangle(0, 192, 16, 16)));
                Waterfall.WaterfallTexturesTemp.Add("_1", TextureManager.GetTexture("Routes", new Rectangle(16, 192, 16, 16)));
                Waterfall.WaterfallTexturesTemp.Add("_2", TextureManager.GetTexture("Routes", new Rectangle(32, 192, 16, 16)));
            }

            if (textureData.Count >= 10)
            {
                Rectangle r = new Rectangle(System.Convert.ToInt32(textureData[6]), System.Convert.ToInt32(textureData[7]), System.Convert.ToInt32(textureData[8]), System.Convert.ToInt32(textureData[9]));
                string texturePath = textureData[5];
                this.waterTextureName = AdditionalValue;
                if (Waterfall.WaterTexturesTemp.ContainsKey(AdditionalValue + "_0") == false)
                {
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_0", TextureManager.GetTexture(texturePath, new Rectangle(r.X, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_1", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_2", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 2, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_3", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 3, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_4", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 4, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_5", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 5, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_6", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 6, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_7", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 7, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_8", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 8, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_9", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 9, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_10", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 10, r.Y, r.Width, r.Height)));
                    Waterfall.WaterTexturesTemp.Add(AdditionalValue + "_11", TextureManager.GetTexture(texturePath, new Rectangle(r.X + r.Width * 11, r.Y, r.Width, r.Height)));
                }
            }
            else if (Waterfall.WaterTexturesTemp.ContainsKey("_0") == false)
            {
                Waterfall.WaterTexturesTemp.Add("_0", TextureManager.GetTexture("Routes", new Rectangle(0, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_1", TextureManager.GetTexture("Routes", new Rectangle(20, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_2", TextureManager.GetTexture("Routes", new Rectangle(40, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_3", TextureManager.GetTexture("Routes", new Rectangle(60, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_4", TextureManager.GetTexture("Routes", new Rectangle(80, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_5", TextureManager.GetTexture("Routes", new Rectangle(100, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_6", TextureManager.GetTexture("Routes", new Rectangle(120, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_7", TextureManager.GetTexture("Routes", new Rectangle(140, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_8", TextureManager.GetTexture("Routes", new Rectangle(160, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_9", TextureManager.GetTexture("Routes", new Rectangle(180, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_10", TextureManager.GetTexture("Routes", new Rectangle(200, 220, 20, 20)));
                Waterfall.WaterTexturesTemp.Add("_11", TextureManager.GetTexture("Routes", new Rectangle(220, 220, 20, 20)));
            }
        }
    }

    public override void UpdateEntity()
    {
        if (!WaterAnimation == null)
        {
            WaterAnimation.Update(0.01);
            if (currentRectangle != WaterAnimation.TextureRectangle)
            {
                ChangeTexture();

                currentRectangle = WaterAnimation.TextureRectangle;
            }
        }

        base.UpdateEntity();
    }

    private void ChangeTexture()
    {
        if (Core.GameOptions.GraphicStyle == 1)
        {
            if (WaterfallTexturesTemp.Count == 0 | WaterTexturesTemp.Count == 0)
            {
                ClearAnimationResources();
                CreateWaterTextureTemp();
            }

            switch (WaterAnimation.CurrentColumn)
            {
                case 0:
                    {
                        this.Textures(0) = Waterfall.WaterfallTexturesTemp[waterFallTextureName + "_0"];
                        break;
                    }
                case 1:
                    {
                        this.Textures(0) = Waterfall.WaterfallTexturesTemp[waterFallTextureName + "_1"];
                        break;
                    }
                case 2:
                    {
                        this.Textures(0) = Waterfall.WaterfallTexturesTemp[waterFallTextureName + "_2"];
                        break;
                    }
            }
            switch (this.Rotation.Y)
            {
                case 0:
                case (object)MathHelper.TwoPi:
                    {
                        switch (WaterAnimation.CurrentColumn)
                        {
                            case 0:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_0"];
                                    break;
                                }
                            case 1:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_1"];
                                    break;
                                }
                            case 2:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_2"];
                                    break;
                                }
                        }

                        break;
                    }
                case (object)MathHelper.Pi * 0.5F:
                    {
                        switch (WaterAnimation.CurrentColumn)
                        {
                            case 0:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_3"];
                                    break;
                                }
                            case 1:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_4"];
                                    break;
                                }
                            case 2:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_5"];
                                    break;
                                }
                        }

                        break;
                    }
                case (object)MathHelper.Pi:
                    {
                        switch (WaterAnimation.CurrentColumn)
                        {
                            case 0:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_6"];
                                    break;
                                }

                            case 1:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_7"];
                                    break;
                                }

                            case 2:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_8"];
                                    break;
                                }
                        }

                        break;
                    }
                case (object)MathHelper.Pi * 1.5:
                    {
                        switch (WaterAnimation.CurrentColumn)
                        {
                            case 0:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_9"];
                                    break;
                                }

                            case 1:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_10"];
                                    break;
                                }

                            case 2:
                                {
                                    this.Textures(1) = Waterfall.WaterTexturesTemp[waterTextureName + "_11"];
                                    break;
                                }
                        }

                        break;
                    }
            }
        }
    }

    public override void Render()
    {
        this.Draw(this.Model, Textures, false);
    }

    private Pokemon ReturnWaterFallPokemonName()
    {
        foreach (Pokemon p in Core.Player.Pokemons)
        {
            if (p.isEgg == false)
            {
                foreach (BattleSystem.Attack a in p.Attacks)
                {
                    if (a.Name.ToLower() == "waterfall")
                        return p;
                }
            }
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public override void WalkOntoFunction()
    {
        if (this.ActionValue == 1)
            return;

        bool isOnTop = true;
        Vector3 OnTopcheckPosition = new Vector3(this.Position.X, this.Position.Y + 1, this.Position.Z);
        Entity Oe = GetEntity(Screen.Level.Entities, OnTopcheckPosition, true, new System.Type[]
        {
            typeof(Waterfall)
        });
        if (Oe != null)
        {
            if (Oe.EntityID == "Waterfall")
                isOnTop = false;
        }

        if (isOnTop == true)
        {
            string s = "";

            int Steps = 0;
            if (Screen.Level.Surfing == false)
                Steps = 1;

            Vector3 checkPosition = new Vector3(this.Position.X, this.Position.Y - 1, this.Position.Z);
            bool foundSteps = true;
            while (foundSteps == true)
            {
                Entity e = GetEntity(Screen.Level.Entities, checkPosition, true, new System.Type[]
                {
                    typeof(Waterfall)
                });
                if (e != null)
                {
                    if (e.EntityID == "Waterfall")
                    {
                        Steps += 1;
                        checkPosition.Y -= 1;
                    }
                    else
                        foundSteps = false;
                }
                else
                    foundSteps = false;
            }

            s = "version=2" + Environment.NewLine + "@pokemon.hide" + Environment.NewLine + "@player.move(2)" + Environment.NewLine + "@player.setmovement(0,-1,0)" + Environment.NewLine + "@pokemon.hide" + Environment.NewLine + "@player.move(" + Steps + ")" + Environment.NewLine + "@pokemon.hide" + Environment.NewLine + ":end";

            (OverworldScreen)Core.CurrentScreen.ActionScript.StartScript(s, 2);
        }
    }

    public override bool WalkAgainstFunction()
    {
        if (this.ActionValue == 1)
            return this.Collision;

        Pokemon p = ReturnWaterFallPokemonName();
        if (Badge.CanUseHMMove(Badge.HMMoves.Waterfall) == true & p != null | GameController.IS_DEBUG_ACTIVE == true | Core.Player.SandBoxMode == true)
        {
            string s = "";

            string pName = "";
            int pNumber = 1;
            if (p != null)
            {
                pName = p.GetDisplayName();
                pNumber = p.Number;
            }

            int Steps = 1;
            if (Screen.Level.Surfing == false)
                Steps = 0;

            Vector3 checkPosition = new Vector3(this.Position.X, this.Position.Y + 1, this.Position.Z);
            bool foundSteps = true;
            while (foundSteps == true)
            {
                Entity e = GetEntity(Screen.Level.Entities, checkPosition, true, new System.Type[]
                {
                    typeof(Waterfall)
                });
                if (e != null)
                {
                    if (e.EntityID == "Waterfall")
                    {
                        Steps += 1;
                        checkPosition.Y += 1;
                    }
                    else
                        foundSteps = false;
                }
                else
                    foundSteps = false;
            }

            Screen.Camera.PlannedMovement = new Vector3(0, 1, 0);

            s = "version=2" + Environment.NewLine + "@pokemon.cry(" + pNumber + ")" + Environment.NewLine + "@sound.play(select)" + Environment.NewLine + "@text.show(" + pName + " used~Waterfall.)" + Environment.NewLine + "@player.move(" + Steps + ")" + Environment.NewLine + "@pokemon.hide" + Environment.NewLine + "@player.move(2)" + Environment.NewLine + "@pokemon.hide" + Environment.NewLine + ":end";

            PlayerStatistics.Track("Waterfall used", 1);
            (OverworldScreen)Core.CurrentScreen.ActionScript.StartScript(s, 2);

            return false;
        }

        if (this.Collision == true)
            return false;
        else
            return true;
    }
}
}