using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RTS {
    public class Editor: Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Point focusPoint = new Point(-32,-32);

        Vector2 size = new Vector2(64,64);
        Dictionary<int, Color> playerColors;
        Dictionary<int, Player> players;
        Player activePlayer;

        public Editor() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            loadTileTextures();
            loadUnitTextures();
            loadMiscTextures();
            loadStartLocationTextures();

            Map.initTileSet();
            Map.initSmoothDirectory();
            
            Map.createMap(size);
            
            Map.generateSmoothMap();

            UnitLibrary.initLibrary();

            setupPlayers();

            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            base.Initialize();

        }

        private void setupPlayers() {
            setupPlayerColors();
            players = new Dictionary<int, Player>();
            players.Add(1,new Player());
            activePlayer = players[1];
        }

        private void setupPlayerColors() {
            playerColors = new Dictionary<int, Color>();
            playerColors.Add(1,Color.Red);
            playerColors.Add(2,Color.Blue);
        }
        
        private void loadStartLocationTextures() {
            Textures.redStartLocation = Content.Load<Texture2D>("units/human/startLocations/startLocationRed");
        }

        private void loadMiscTextures() {
            Textures.humanCursor = Content.Load<Texture2D>("cursor/cursor");
            Textures.humanBar = Content.Load<Texture2D>("humanBar");
            Textures.humanBarUp = Content.Load<Texture2D>("humanBarUp");
            Textures.selectionTile = Content.Load<Texture2D>("selectionTile");
        }

        private void loadUnitTextures() {
            Textures.footmanIdle1 = Content.Load<Texture2D>("units/human/footman/idle1Red");
            Textures.footmanIdle1Sel = Content.Load<Texture2D>("units/human/footman/idle1RedSel");
            Textures.footmanIdle2 = Content.Load<Texture2D>("units/human/footman/idle2Red");
            Textures.footmanIdle2Sel = Content.Load<Texture2D>("units/human/footman/idle2RedSel");
            Textures.footmanIdle3 = Content.Load<Texture2D>("units/human/footman/idle3Red");
            Textures.footmanIdle3Sel = Content.Load<Texture2D>("units/human/footman/idle3RedSel");
            Textures.footmanIdle4 = Content.Load<Texture2D>("units/human/footman/idle4Red");
            Textures.footmanIdle4Sel = Content.Load<Texture2D>("units/human/footman/idle4RedSel");
            Textures.footmanIdle6 = Content.Load<Texture2D>("units/human/footman/idle6Red");
            Textures.footmanIdle6Sel = Content.Load<Texture2D>("units/human/footman/idle6RedSel");
            Textures.footmanIdle7 = Content.Load<Texture2D>("units/human/footman/idle7Red");
            Textures.footmanIdle7Sel = Content.Load<Texture2D>("units/human/footman/idle7RedSel");
            Textures.footmanIdle8 = Content.Load<Texture2D>("units/human/footman/idle8Red");
            Textures.footmanIdle8Sel = Content.Load<Texture2D>("units/human/footman/idle8RedSel");
            Textures.footmanIdle9 = Content.Load<Texture2D>("units/human/footman/idle9Red");
            Textures.footmanIdle9Sel = Content.Load<Texture2D>("units/human/footman/idle9RedSel");
        }

        private void loadTileTextures() {
            //Textures.setPx(Content.Load<Texture2D>("px"));
            Textures.fog = Content.Load<Texture2D>("tiles/fog/fog");

                Textures.bound = Content.Load<Texture2D>("bound");
                Textures.grass = Content.Load<Texture2D>("tiles/grass/grass");
                    Textures.grass11 = Content.Load<Texture2D>("tiles/grass/grass11");
                Textures.dirt = Content.Load<Texture2D>("tiles/dirt/dirt");
                    Textures.dirt200 = Content.Load<Texture2D>("tiles/dirt/dirt200");
                    Textures.dirt2grass = Content.Load<Texture2D>("tiles/dirt/dirt8grass");
                        Textures.dirt2grass2 = Content.Load<Texture2D>("tiles/dirt/dirt8grass2");
                    Textures.dirt4grass = Content.Load<Texture2D>("tiles/dirt/dirt4grass");
                        Textures.dirt4grass2 = Content.Load<Texture2D>("tiles/dirt/dirt4grass2");
                    Textures.dirt6grass = Content.Load<Texture2D>("tiles/dirt/dirt6grass");
                        Textures.dirt6grass2 = Content.Load<Texture2D>("tiles/dirt/dirt6grass2");
                    Textures.dirt8grass = Content.Load<Texture2D>("tiles/dirt/dirt2grass");
                        Textures.dirt8grass2 = Content.Load<Texture2D>("tiles/dirt/dirt2grass2");
                    Textures.dirt286 = Content.Load<Texture2D>("tiles/dirt/dirt86");
                    Textures.dirt284 = Content.Load<Texture2D>("tiles/dirt/dirt84");
                    Textures.dirt224 = Content.Load<Texture2D>("tiles/dirt/dirt24");
                    Textures.dirt226 = Content.Load<Texture2D>("tiles/dirt/dirt26");

                    Textures.dirt27 = Content.Load<Texture2D>("tiles/dirt/dirt7");
                    Textures.dirt29 = Content.Load<Texture2D>("tiles/dirt/dirt9");
                    Textures.dirt21 = Content.Load<Texture2D>("tiles/dirt/dirt1");
                    Textures.dirt23 = Content.Load<Texture2D>("tiles/dirt/dirt3");

                    Textures.dirt19 = Content.Load<Texture2D>("tiles/dirt/dirt19");
                    Textures.dirt37 = Content.Load<Texture2D>("tiles/dirt/dirt37");

                Textures.forest = Content.Load<Texture2D>("tiles/forest/forest");
                    Textures.forest1 = Content.Load<Texture2D>("tiles/forest/forest1");
                    Textures.forest2 = Content.Load<Texture2D>("tiles/forest/forest2");
                    Textures.forest3 = Content.Load<Texture2D>("tiles/forest/forest3");
                    Textures.forest4 = Content.Load<Texture2D>("tiles/forest/forest4");
                    Textures.forest6 = Content.Load<Texture2D>("tiles/forest/forest6");
                    Textures.forest7 = Content.Load<Texture2D>("tiles/forest/forest7");
                    Textures.forest8 = Content.Load<Texture2D>("tiles/forest/forest8");
                    Textures.forest9 = Content.Load<Texture2D>("tiles/forest/forest9");

                    Textures.forest24 = Content.Load<Texture2D>("tiles/forest/forest24");
                    Textures.forest26 = Content.Load<Texture2D>("tiles/forest/forest26");
                    Textures.forest84 = Content.Load<Texture2D>("tiles/forest/forest84");
                    Textures.forest86 = Content.Load<Texture2D>("tiles/forest/forest86");

                Textures.water = Content.Load<Texture2D>("tiles/water/water");
                    Textures.water1 = Content.Load<Texture2D>("tiles/water/water1");
                    Textures.water2 = Content.Load<Texture2D>("tiles/water/water2");
                    Textures.water3 = Content.Load<Texture2D>("tiles/water/water3");
                    Textures.water4 = Content.Load<Texture2D>("tiles/water/water4");
                    Textures.water6 = Content.Load<Texture2D>("tiles/water/water6");
                    Textures.water7 = Content.Load<Texture2D>("tiles/water/water7");
                    Textures.water8 = Content.Load<Texture2D>("tiles/water/water8");
                    Textures.water9 = Content.Load<Texture2D>("tiles/water/water9");

                    Textures.water24 = Content.Load<Texture2D>("tiles/water/water24");
                    Textures.water26 = Content.Load<Texture2D>("tiles/water/water26");
                    Textures.water84 = Content.Load<Texture2D>("tiles/water/water84");
                    Textures.water86 = Content.Load<Texture2D>("tiles/water/water86");
        }

        protected override void LoadContent() {

            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent() {

        }

        static int mousePressedX = -1;
        static int mousePressedY = -1;
        static int brushSize = 1;
        static TileType tileBrush = TileType.Grass;
        static UnitType unitBrush = UnitType.Footman;
        BrushType brushType = BrushType.Tiles;
        
        protected override void Update(GameTime gameTime) {
            if (this.IsActive){
                int mapWidth = (Map.getMap().GetLength(1)-2)*32;
                int mapHeight = (Map.getMap().GetLength(0)-2)*32;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                    if (brushType == BrushType.Units){
                        if (mousePressedX != (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32 || mousePressedY != (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32) {
                            mousePressedX = (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32;
                            mousePressedY = (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32;
                            Map.addUnit(unitBrush, (focusPoint.X*-1)/32+Mouse.GetState().X/32, (focusPoint.Y*-1)/32+Mouse.GetState().Y/32, activePlayer);
                        }
                    } else if (brushType == BrushType.Tiles){
                        if (mousePressedX != (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32 || mousePressedY != (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32) {
                            mousePressedX = (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32;
                            mousePressedY = (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32;
                            Map.paintTile(tileBrush, (focusPoint.X*-1)/32+Mouse.GetState().X/32, (focusPoint.Y*-1)/32+Mouse.GetState().Y/32, brushSize);
                        }
                    } else if (brushType == BrushType.StartLocations){
                        if (mousePressedX != (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32 || mousePressedY != (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32) {
                            mousePressedX = (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32;
                            mousePressedY = (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32;
                            Map.setStartLocation((focusPoint.X*-1)/32+Mouse.GetState().X/32, (focusPoint.Y*-1)/32+Mouse.GetState().Y/32, activePlayer);
                        }
                    }
                } else {
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                        this.Exit();
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.D3)) {
                        brushSize = 3;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.D1)) {
                        brushSize = 1;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                        tileBrush = TileType.Dirt;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.G)) {
                        tileBrush = TileType.Grass;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.F)) {
                        tileBrush = TileType.Forest;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                        tileBrush = TileType.Water;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Q)) {
                        unitBrush = UnitType.Footman;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.F10)) {
                        saveToFile("myMap");
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.F2)) {
                        brushType = BrushType.Units;
                        mousePressedX = -1;
                        mousePressedY = -1;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.F1)) {
                        brushType = BrushType.Tiles;
                        mousePressedX = -1;
                        mousePressedY = -1;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.F4)) {
                        brushType = BrushType.StartLocations;
                        mousePressedX = -1;
                        mousePressedY = -1;
                    }


                    if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                        mousePressedX = (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32;
                        mousePressedY = (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32;
                        if (focusPoint.X >= mapWidth * (-1) + graphics.PreferredBackBufferWidth) {
                            focusPoint.X -= 32;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                        mousePressedX = (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32;
                        mousePressedY = (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32;
                        if (focusPoint.X + 32 < 0) {
                            focusPoint.X += 32;
                        }
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                        mousePressedX = (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32;
                        mousePressedY = (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32;
                        if (focusPoint.Y + 32 < 0) {
                            focusPoint.Y += 32;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                        mousePressedX = (focusPoint.X * -1) / 32 + Mouse.GetState().X / 32;
                        mousePressedY = (focusPoint.Y * -1) / 32 + Mouse.GetState().Y / 32;
                        if (focusPoint.Y - 32 > mapHeight * (-1) + graphics.PreferredBackBufferHeight) {
                            focusPoint.Y -= 32;
                        }
                    }
                }
            }
            base.Update(gameTime);
        }

        private void saveToFile(string fileName) {

            bool locationsSet = true;
            foreach(Player p in players.Values){
                if (p.StartLocation == null){
                    locationsSet = false;
                }
            }

            if (locationsSet){
                // create a writer and open the file
                TextWriter tw = new StreamWriter(fileName+".map");

                // Write date
                tw.WriteLine(DateTime.Now);

                // Write players
                tw.WriteLine("Players:");
                tw.WriteLine(players.Count);
                for (int a = 1; a <= players.Count; a++) {
                    Player p = players[a];
                    String s = p.StartLocation.X + "," + p.StartLocation.Y + "," + p.Race;
                    tw.WriteLine(s);
                }

                // Write tiles
                tw.WriteLine("Map:");
                tw.WriteLine(Map.getMap().GetLength(0));
                tw.WriteLine(Map.getMap().GetLength(1));
                for (int y = 0; y < Map.getMap().GetLength(0); y++) {
                    String s = "";
                    for (int x = 0; x < Map.getMap().GetLength(1); x++) {
                        s += ""+Map.getMap()[y, x];
                        if (x!=Map.getMap().GetLength(1)-1){
                            s += ",";
                        }
                    }
                    tw.WriteLine(s);
                }

                // Write units
                tw.WriteLine("Units:");
                for (int y = 0; y < Map.getMap().GetLength(0); y++) {
                    String s = "";
                    for (int x = 0; x < Map.getMap().GetLength(1); x++) {
                        int id = Map.getUnitMap()[y, x];
                        if (id!=0){
                            s += ""+(int)UnitLibrary.getUnitIDs()[id].UnitType;
                        } else {
                            s += 0;
                        }
                        if (x!=Map.getUnitMap().GetLength(1)-1){
                            s += ",";
                        }
                    }
                    tw.WriteLine(s);
                }
                tw.WriteLine("end");
                // close the stream
                tw.Close();
            }
        }

        protected override void Draw(GameTime gameTime) {
            
            spriteBatch.Begin();
            drawMap();
            drawUnits();
            drawStartLocations();
            if (this.IsActive){
                drawBrush();
            }
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void drawStartLocations() {
            int i = 0;
            foreach(Player p in players.Values){
                i++;
                if (p.StartLocation != null){
                    //spriteBatch.Draw(Textures.startlocation(playerColors[i]),new Rectangle((int)(p.StartLocation.X * 32) + focusPoint.X, (int)(p.StartLocation.Y * 32) + focusPoint.Y, 32, 32),Color.White);
                }
            }
        }

        private void drawUnits() {
            for (int y = 0; y < Map.getUnitMap().GetLength(0); y++) {
                for (int x = 0; x < Map.getUnitMap().GetLength(1); x++) {
                    if (Map.getUnitMap()[y,x] != 0){
                        Unit unit = UnitLibrary.getUnitIDs()[Map.getUnitMap()[y,x]];
                        UnitClass uClass = UnitLibrary.getDictionary()[unit.UnitType];
                        RotationAngle rotation = unit.Rotation;
                        Texture2D tex = uClass.UnitTextures.getIdleTextures()[rotation];
                        spriteBatch.Draw(tex, new Rectangle((int)(x * 32) + focusPoint.X - 16, (int)(y * 32) + focusPoint.Y - 16, 64, 64), Color.White);
                    }
                }
            }
        }

        private void drawBrush() {
            int xa = Mouse.GetState().X;
            int ya = Mouse.GetState().Y;

            if (brushSize == 1 || brushType != BrushType.Tiles) {
                spriteBatch.Draw(Textures.dirt, new Rectangle(((int)(((xa)) / 32)) * 32, ((int)((ya) / 32)) * 32, 32, 32), new Color(0, 0, 0, 0.1f));
            } else if (brushSize == 3) {
                spriteBatch.Draw(Textures.dirt, new Rectangle(((int)(((xa-32)) / 32)) * 32, ((int)((ya-32) / 32)) * 32, 96, 96), new Color(0, 0, 0, 0.1f));
            }
            
        }

        private void drawMap() {
            for (int y = 0; y < Map.getMap().GetLength(0); y++) {
                for (int x = 0; x < Map.getMap().GetLength(1); x++) {
                    spriteBatch.Draw(Map.getTileSet()[Map.getSmoothMap()[y, x]], new Rectangle((int)(x*32)+focusPoint.X,(int)(y*32)+focusPoint.Y,32,32), Color.White);
                }
            }
        }
    }
}
