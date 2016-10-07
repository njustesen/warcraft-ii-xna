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
    public class Game: Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Point focusPoint = new Point(-32,-32);
        Dictionary<int, Color> playerColors;
        Dictionary<int, Player> players;
        List<Unit> unitSelection;

        int mapWidth;
        int mapHeight;

        public Game() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            loadTileTextures();
            loadUnitTextures();
            loadMiscTextures();
            loadStartLocationTextures();
            setupPlayerColors();

            Map.initTileSet();
            Map.initUnitSet();
            Map.initSmoothDirectory();

            UnitLibrary.initLibrary();

            players = new Dictionary<int, Player>();
            loadMap("myMap");
            
            Map.generateSmoothMap();
            unitSelection = new List<Unit>();

            mapWidth = (Map.getMap().GetLength(1)-2)*32;
            mapHeight = (Map.getMap().GetLength(0)-2)*32;

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;

            graphics.ApplyChanges();

            centralizeFocus();
            //Map.initFogMap();
            Map.initWhosLooking();

            base.Initialize();

        }

        private void setupPlayerColors() {

            playerColors = new Dictionary<int, Color>();
            playerColors.Add(1,Color.Red);
            playerColors.Add(2,Color.Blue);
        }

        private void loadStartLocationTextures() {
            Textures.redStartLocation = Content.Load<Texture2D>("units/human/startLocations/startLocationRed");
        }

        

        private void centralizeFocus() {
            int slX = players[1].StartLocation.X;
            int slY = players[1].StartLocation.Y;

            focusPoint.X = ((slX - (graphics.PreferredBackBufferWidth/32) / 2)*32)*(-1);
            focusPoint.Y = ((slY - (graphics.PreferredBackBufferHeight/32) / 2)*32)*(-1);
            
            if (focusPoint.Y-32 < mapHeight * (-1) + graphics.PreferredBackBufferHeight) {
                focusPoint.Y = mapHeight * (-1) + graphics.PreferredBackBufferHeight-32;
            }
            
            if (focusPoint.Y > -32) {
                focusPoint.Y = -32;
            }

            if (focusPoint.X-32 < mapWidth * (-1) + graphics.PreferredBackBufferWidth) {
                focusPoint.X = mapWidth * (-1) + graphics.PreferredBackBufferWidth-32;
            }

            if (focusPoint.X > -32) {
                focusPoint.X =-32;
            }
        }

        private void loadMap(string fileName) {

            int height;
            int width;
            
            string file = fileName += ".map";

            StreamReader SR;
            string S;
            SR=File.OpenText(file);

            // Skip dateline
            S=SR.ReadLine();

            // Skip headerline
            S=SR.ReadLine();

            // read players
            S=SR.ReadLine();
            int numOfPlayers = Convert.ToInt32(S);
            for (int i = 1; i<=numOfPlayers;i++){
                S=SR.ReadLine();
                StartLocation sl = new StartLocation(Convert.ToInt32(S.Split(',')[0]),Convert.ToInt32(S.Split(',')[1]));
                Race race;
                if (S.Split(',')[2] == "Human"){
                    race = Race.Human;
                } else {
                    race = Race.Orcs;
                }
                players.Add(i, new Player(race,sl));
            }

            // Skip headerline
            S=SR.ReadLine();

            // read map size
            height=Convert.ToInt32(SR.ReadLine());
            width=Convert.ToInt32(SR.ReadLine());

            // read tiles
            Map.setMap(new int[height,width]);
            int y = 0;
            S=SR.ReadLine();
            while(!S.Equals("Units:")){
                addWidth(S,y);
                y++;
                S=SR.ReadLine();
            }

            // read units
            S=SR.ReadLine();
            Map.setUnitMap(new int[height,width]);
            y = 0;
            while(!S.Equals("end")){
                addUnitWidth(S,y);
                S=SR.ReadLine();
                y++;
            }

            SR.Close();
        }

        private void addWidth(string str, int y) {
            string[] words = str.Split(',');
            int x = 0;
            foreach(string s in words){
                Map.getMap()[y, x] = Convert.ToInt32(s);
                x++;
            }
        }

        private void addUnitWidth(string str, int y) {
            string[] words = str.Split(',');
            int x = 0;
            foreach(string s in words){
                int unitNumber = Convert.ToInt32(s);
                if (unitNumber != 0){
                    int id = UnitLibrary.getNextID();
                    Unit u = new Unit(id, new IntPosition(x, y), Map.getUnitSet()[unitNumber], players[1]);
                    UnitLibrary.addUnit(u);
                    UnitLibrary.getActors().Add(u);
                    Map.getUnitMap()[y, x] = id;
                }
                x++;
            }
        }
        

        private void loadMiscTextures() {
            Textures.humanCursor = Content.Load<Texture2D>("cursor/cursor");
            Textures.humanBar = Content.Load<Texture2D>("humanBar");
            Textures.humanBarUp = Content.Load<Texture2D>("humanBarUp");
            Textures.selectionTile = Content.Load<Texture2D>("selectionTile");
        }

        private void loadUnitTextures() {
            Textures.footmanIdle1 = Content.Load<Texture2D>("units/human/footman/Idle/FootmanIdle1");
            Textures.footmanIdle1Sel = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle1Sel");
            Textures.footmanIdle2 = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle2");
            Textures.footmanIdle2Sel = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle2Sel");
            Textures.footmanIdle3 = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle3");
            Textures.footmanIdle3Sel = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle3Sel");
            Textures.footmanIdle4 = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle4");
            Textures.footmanIdle4Sel = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle4Sel");
            Textures.footmanIdle6 = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle6");
            Textures.footmanIdle6Sel = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle6Sel");
            Textures.footmanIdle7 = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle7");
            Textures.footmanIdle7Sel = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle7Sel");
            Textures.footmanIdle8 = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle8");
            Textures.footmanIdle8Sel = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle8Sel");
            Textures.footmanIdle9 = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle9");
            Textures.footmanIdle9Sel = Content.Load<Texture2D>("units/human/footman/idle/FootmanIdle9Sel");
            
            Textures.footmanMoving1A = Content.Load<Texture2D>("units/human/footman/moving/southWest/FootmanMoving1A");
            Textures.footmanMoving1B = Content.Load<Texture2D>("units/human/footman/moving/southWest/FootmanMoving1B");
            Textures.footmanMoving1C = Content.Load<Texture2D>("units/human/footman/moving/southWest/FootmanMoving1C");
            Textures.footmanMoving1D = Content.Load<Texture2D>("units/human/footman/moving/southWest/FootmanMoving1D");
            
            Textures.footmanMoving2A = Content.Load<Texture2D>("units/human/footman/moving/south/FootmanMoving2A");
            Textures.footmanMoving2B = Content.Load<Texture2D>("units/human/footman/moving/south/FootmanMoving2B");
            Textures.footmanMoving2C = Content.Load<Texture2D>("units/human/footman/moving/south/FootmanMoving2C");
            Textures.footmanMoving2D = Content.Load<Texture2D>("units/human/footman/moving/south/FootmanMoving2D");
            
            Textures.footmanMoving3A = Content.Load<Texture2D>("units/human/footman/moving/southEast/FootmanMoving3A");
            Textures.footmanMoving3B = Content.Load<Texture2D>("units/human/footman/moving/southEast/FootmanMoving3B");
            Textures.footmanMoving3C = Content.Load<Texture2D>("units/human/footman/moving/southEast/FootmanMoving3C");
            Textures.footmanMoving3D = Content.Load<Texture2D>("units/human/footman/moving/southEast/FootmanMoving3D");
            
            Textures.footmanMoving4A = Content.Load<Texture2D>("units/human/footman/moving/west/FootmanMoving4A");
            Textures.footmanMoving4B = Content.Load<Texture2D>("units/human/footman/moving/west/FootmanMoving4B");
            Textures.footmanMoving4C = Content.Load<Texture2D>("units/human/footman/moving/west/FootmanMoving4C");
            Textures.footmanMoving4D = Content.Load<Texture2D>("units/human/footman/moving/west/FootmanMoving4D");
            
            Textures.footmanMoving6A = Content.Load<Texture2D>("units/human/footman/moving/east/FootmanMoving6A");
            Textures.footmanMoving6B = Content.Load<Texture2D>("units/human/footman/moving/east/FootmanMoving6B");
            Textures.footmanMoving6C = Content.Load<Texture2D>("units/human/footman/moving/east/FootmanMoving6C");
            Textures.footmanMoving6D = Content.Load<Texture2D>("units/human/footman/moving/east/FootmanMoving6D");
            
            Textures.footmanMoving7A = Content.Load<Texture2D>("units/human/footman/moving/northWest/FootmanMoving7A");
            Textures.footmanMoving7B = Content.Load<Texture2D>("units/human/footman/moving/northWest/FootmanMoving7B");
            Textures.footmanMoving7C = Content.Load<Texture2D>("units/human/footman/moving/northWest/FootmanMoving7C");
            Textures.footmanMoving7D = Content.Load<Texture2D>("units/human/footman/moving/northWest/FootmanMoving7D");
            
            Textures.footmanMoving8A = Content.Load<Texture2D>("units/human/footman/moving/north/FootmanMoving8A");
            Textures.footmanMoving8B = Content.Load<Texture2D>("units/human/footman/moving/north/FootmanMoving8B");
            Textures.footmanMoving8C = Content.Load<Texture2D>("units/human/footman/moving/north/FootmanMoving8C");
            Textures.footmanMoving8D = Content.Load<Texture2D>("units/human/footman/moving/north/FootmanMoving8D");        
            
            Textures.footmanMoving9A = Content.Load<Texture2D>("units/human/footman/moving/northEast/FootmanMoving9A");
            Textures.footmanMoving9B = Content.Load<Texture2D>("units/human/footman/moving/northEast/FootmanMoving9B");
            Textures.footmanMoving9C = Content.Load<Texture2D>("units/human/footman/moving/northEast/FootmanMoving9C");
            Textures.footmanMoving9D = Content.Load<Texture2D>("units/human/footman/moving/northEast/FootmanMoving9D");
            
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

        bool mouseDown = false;
        Vector2 mousePointA = new Vector2();
        Vector2 mousePointB = new Vector2();
        protected override void Update(GameTime gameTime) {
            foreach (Actor a in UnitLibrary.getActors()){
                a.act(gameTime);
            }
            rotateIdleUnits(gameTime);

            if (this.IsActive){
                if (Mouse.GetState().RightButton == ButtonState.Pressed) {
                    int x = (int)((Mouse.GetState().X)/32) + (focusPoint.X/32)*(-1);
                    int y = (int)((Mouse.GetState().Y)/32) + (focusPoint.Y/32)*(-1);

                    foreach(Unit u in unitSelection){
                        u.moveTo(new IntPosition(x,y));
                    }
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                    if (!mouseDown){
                        mouseDown = true;
                        mousePointA.X = Mouse.GetState().X;
                        mousePointA.Y = Mouse.GetState().Y;
                        mousePointB.X = Mouse.GetState().X;
                        mousePointB.Y = Mouse.GetState().Y;
                    } else {
                        mouseDown = true;
                        mousePointB.X = Mouse.GetState().X;
                        mousePointB.Y = Mouse.GetState().Y;
                    }
                } else {
                    if (mouseDown){
                        calculateSelection();
                        mouseDown = false;
                        mousePointA = new Vector2();
                        mousePointB = new Vector2();
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape)){
                    this.Exit();
                }

                if (!mouseDown){
                    if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                        if (focusPoint.X >= mapWidth * (-1) + graphics.PreferredBackBufferWidth) {
                            focusPoint.X -= 32;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                        if (focusPoint.X + 32 < 0) {
                            focusPoint.X += 32;
                        }
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                        if (focusPoint.Y + 32 < 0) {
                            focusPoint.Y += 32;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                        if (focusPoint.Y - 32 > mapHeight * (-1) + graphics.PreferredBackBufferHeight) {
                            focusPoint.Y -= 32;
                        }
                    }
                }
            }
            
            base.Update(gameTime);
        }

        int timeSinceLastRotation = 0;
        private void rotateIdleUnits(GameTime gameTime) {
            timeSinceLastRotation += 1+gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastRotation > 300-UnitLibrary.getUnits().Count){
                bool rotate = false;
                Random random = new Random();
                if (random.Next(1,gameTime.ElapsedGameTime.Milliseconds+10010+(int)(UnitLibrary.getUnits().Count*50))>10000){
                    rotate = true;
                }
                if (rotate){
                    int rotationNumber = random.Next(1,9);
                
                    int index = random.Next(0,UnitLibrary.getUnits().Count);
                    Unit u = UnitLibrary.getUnits()[index];
                    if (!u.Moving){
                        u.Rotation = getNewRotation(u.Rotation);
                    }
                }
            }
        }

        int seed = DateTime.Now.Millisecond;
        private RotationAngle getNewRotation(RotationAngle rotation) {
            bool turnCW = true;
            Random random = new Random(seed);
            seed = random.Next(Int32.MinValue, Int32.MaxValue);
            if (random.Next(1, 4)==3){
                turnCW = false;
            }
            if (rotation==RotationAngle.North){
                if (turnCW){
                    return RotationAngle.NorthEast;
                } else {
                    return RotationAngle.NorthWest;
                }
            } else if (rotation==RotationAngle.NorthEast){
                if (turnCW){
                    return RotationAngle.East;
                } else {
                    return RotationAngle.North;
                }
            } else if (rotation==RotationAngle.East){
                if (turnCW){
                    return RotationAngle.SouthEast;
                } else {
                    return RotationAngle.NorthEast;
                }
            } else if (rotation==RotationAngle.SouthEast){
                if (turnCW){
                    return RotationAngle.South;
                } else {
                    return RotationAngle.East;
                }
            } else if (rotation==RotationAngle.South){
                if (turnCW){
                    return RotationAngle.SouthWest;
                } else {
                    return RotationAngle.SouthEast;
                }
            } else if (rotation==RotationAngle.SouthWest){
                if (turnCW){
                    return RotationAngle.West;
                } else {
                    return RotationAngle.South;
                }
            } else if (rotation==RotationAngle.West){
                if (turnCW){
                    return RotationAngle.NorthWest;
                } else {
                    return RotationAngle.SouthWest;
                }
            } else if (rotation==RotationAngle.NorthWest){
                if (turnCW){
                    return RotationAngle.North;
                } else {
                    return RotationAngle.West;
                }
            }
            return RotationAngle.North;
        }

        private void calculateSelection() {

            if (!(Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))){
                unitSelection.Clear();
            }
            if (mousePointA.X>mousePointB.X){
                int d = (int)mousePointB.X;
                mousePointB.X = mousePointA.X;
                mousePointA.X = d;
            }
            if (mousePointA.Y>mousePointB.Y){
                int d = (int)mousePointB.Y;
                mousePointB.Y = mousePointA.Y;
                mousePointA.Y = d;
            }
                
            int tileXfrom = (int)((mousePointA.X+32)/32) + (focusPoint.X/32)*(-1);
            int tileYfrom = (int)((mousePointA.Y)/32) + (focusPoint.Y/32)*(-1);
            int tileXto = (int)((mousePointB.X+64)/32) + (focusPoint.X/32)*(-1);
            int tileYto = (int)((mousePointB.Y+32)/32) + (focusPoint.Y/32)*(-1);

            getUnitsToSelection(tileXfrom, tileYfrom, tileXto, tileYto, players[1]);
        }

        private void getUnitsToSelection(int tileXfrom, int tileYfrom, int tileXto, int tileYto, Player player) {
            for (int y = tileYfrom; y < tileYto; y++) {
                for (int x = tileXfrom; x < tileXto; x++) {
                    try {
                        if (Map.getUnitMap()[y,x]!=0){
                        //if (Map.getUnitMap()[y,x]!=0 && UnitLibrary.getUnitIDs()[2].Owner == player){
                            unitSelection.Add(UnitLibrary.getUnitIDs()[Map.getUnitMap()[y,x]]);
                        }
                    } catch (Exception e) {

                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime) {
            graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            drawMap();
            drawUnits();
            drawStartLocations();
            //drawFog();
            
            drawBar();
            drawCursor();
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void drawBar() {
            spriteBatch.Draw(Textures.humanBar, new Vector2(0,graphics.PreferredBackBufferHeight-Textures.humanBar.Height), Color.White);
            spriteBatch.Draw(Textures.humanBarUp, new Vector2(0,0), Color.White);
        }

        private void drawCursor() {
            spriteBatch.Draw(Textures.humanCursor, new Vector2(Mouse.GetState().X,Mouse.GetState().Y), Color.White);
            Rectangle r = new Rectangle();
            if (!mousePointA.Equals(mousePointB)){
                if (mousePointA.X >= mousePointB.X && mousePointA.Y >= mousePointB.Y){
                    r = new Rectangle((int)(mousePointA.X), (int)(mousePointA.Y), (int)(mousePointB.X - mousePointA.X), (int)(mousePointB.Y - mousePointA.Y));
                } else if (mousePointA.X >= mousePointB.X && mousePointA.Y <= mousePointB.Y){
                    r = new Rectangle((int)(mousePointB.X), (int)(mousePointA.Y), (int)(mousePointA.X - mousePointB.X), (int)(mousePointB.Y - mousePointA.Y));
                } else if (mousePointA.X <= mousePointB.X && mousePointA.Y <= mousePointB.Y){
                    r = new Rectangle((int)(mousePointA.X), (int)(mousePointA.Y), (int)(mousePointB.X - mousePointA.X), (int)(mousePointB.Y - mousePointA.Y));
                } else if (mousePointA.X <= mousePointB.X && mousePointA.Y >= mousePointB.Y){
                    r = new Rectangle((int)(mousePointB.X), (int)(mousePointA.Y), (int)(mousePointA.X - mousePointB.X), (int)(mousePointB.Y - mousePointA.Y));
                }
            } else {
                r = new Rectangle((int)mousePointA.X, (int)mousePointA.Y,1,1);
            }
            
            spriteBatch.Draw(Textures.selectionTile, r, new Color(255,255,255,1.0f));
        }

        private void drawFog() {
           for (int y = 0; y < Map.getFogMap().GetLength(0); y++) {
                for (int x = 0; x < Map.getFogMap().GetLength(1); x++) {
                    if (Map.getFogMap()[y,x] == 1){

                    }
                }
            }
        }

        private void drawStartLocations() {
            int i = 0;
            foreach(Player p in players.Values){
                i++;
                if (p.StartLocation != null){
                    //spriteBatch.Draw(Textures.getStartlocation(playerColors[i]),new Rectangle((int)(p.StartLocation.X * 32) + focusPoint.X, (int)(p.StartLocation.Y * 32) + focusPoint.Y, 32, 32),Color.White);
                }
            }
        }

        private void drawUnits() {
            for (int y = 0; y < Map.getUnitMap().GetLength(0); y++) {
                for (int x = 0; x < Map.getUnitMap().GetLength(1); x++) {
                    if (Map.getUnitMap()[y,x] > 0){
                        Unit unit = UnitLibrary.getUnitIDs()[Map.getUnitMap()[y,x]];
                        UnitClass uClass = UnitLibrary.getDictionary()[unit.UnitType];
                        RotationAngle rotation = unit.Rotation;
                        Texture2D tex = getTexture(unit);
                        IntPosition offset = getOffset(unit);
                        if (unitSelection.Contains(unit)){
                            spriteBatch.Draw(
                                tex, 
                                new Rectangle(
                                    x * 32 + focusPoint.X - 16 + offset.XPos, 
                                    y * 32 + focusPoint.Y - 16 + offset.YPos, 
                                    64, 
                                    64),
                                    new Color(150,150,255));
                        } else {
                            spriteBatch.Draw(
                                tex, 
                                new Rectangle(
                                    x * 32 + focusPoint.X - 16 + offset.XPos, 
                                    y * 32 + focusPoint.Y - 16 + offset.YPos, 
                                    64, 
                                    64),
                                    new Color(255,255,255));
                        }
                    }
                }
            }
        }

        private IntPosition getOffset(Unit unit) {
            UnitClass uc = UnitLibrary.getDictionary()[unit.UnitType];
            if (unit.Moving){
                int phase = 1 + unit.getWalkingPhase();
                float factor = 1.0f;
                if (Controller.diagonal(unit.Rotation)){
                    factor = 1.414f;
                }
                return new IntPosition(
                    (int)((32/(4*factor)/factor*(phase))*Controller.getDirectionValues(unit.Rotation).XPos),
                    (int)((32/(4*factor)/factor*(phase)+2)*Controller.getDirectionValues(unit.Rotation).YPos));
            } else {
                return new IntPosition(0, 0);
            }
        }

        private Texture2D getTexture(Unit unit) {
            UnitClass uc = UnitLibrary.getDictionary()[unit.UnitType];
            if (unit.Moving){
                int phase = unit.getWalkingPhase();
                return uc.UnitTextures.getWalkingTextures()[unit.Rotation][Math.Min(phase,6)];
            } else {
                return uc.UnitTextures.getIdleTextures()[unit.Rotation];
            }
        }

        private void drawMap() {
            for (int y = 0; y < Map.getMap().GetLength(0); y++) {
                for (int x = 0; x < Map.getMap().GetLength(1); x++) {
                    if (Map.getWhosLooking()[y,x].Count != 0){
                        spriteBatch.Draw(Map.getTileSet()[Map.getSmoothMap()[y, x]], new Rectangle((int)(x*32)+focusPoint.X,(int)(y*32)+focusPoint.Y,32,32), new Color(255,255,255,(byte)255));
                    } else {
                        spriteBatch.Draw(Map.getTileSet()[Map.getSmoothMap()[y, x]], new Rectangle((int)(x*32)+focusPoint.X,(int)(y*32)+focusPoint.Y,32,32), Color.White * .33f);
                    }
                }
            }
        }
    }
}
