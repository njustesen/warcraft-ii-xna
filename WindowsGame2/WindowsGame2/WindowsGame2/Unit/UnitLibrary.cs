using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RTS {
    class UnitLibrary {
        static Dictionary<UnitType,UnitClass> unitDictionary;
        static Dictionary<int,Unit> unitIDs;
        static List<Unit> units;
        static List<Actor> actors;
        static int lastID;

        public static Dictionary<UnitType,UnitClass> getDictionary(){
            return unitDictionary;
        }

        public static List<Actor> getActors(){
            return actors;
        }

        public static Dictionary<int,Unit> getUnitIDs(){
            return unitIDs;
        }

        public static List<Unit> getUnits(){
            return units;
        }

        public static void initLibrary(){
            lastID = 0;
            unitDictionary = new Dictionary<UnitType, UnitClass>();
            unitIDs = new Dictionary<int, Unit>();
            units = new List<Unit>();
            actors = new List<Actor>();
            createFootman();
        }

        private static void createFootman() {
            List<DamageType> strongTo = new List<DamageType>();
            strongTo.Add(DamageType.Pierce);
            strongTo.Add(DamageType.Blade);
            List<Ability> abilities = new List<Ability>();
            abilities.Add(Ability.Attack);

            Dictionary<RotationAngle,Texture2D> idleTextures = new Dictionary<RotationAngle,Texture2D>();
            idleTextures.Add(RotationAngle.East, Textures.footmanIdle6);
            idleTextures.Add(RotationAngle.NorthWest, Textures.footmanIdle7);
            idleTextures.Add(RotationAngle.North, Textures.footmanIdle8);
            idleTextures.Add(RotationAngle.NorthEast, Textures.footmanIdle9);
            idleTextures.Add(RotationAngle.South, Textures.footmanIdle2);
            idleTextures.Add(RotationAngle.SouthEast, Textures.footmanIdle3);
            idleTextures.Add(RotationAngle.SouthWest, Textures.footmanIdle1);
            idleTextures.Add(RotationAngle.West, Textures.footmanIdle4);

            Dictionary<RotationAngle,List<Texture2D>> walkingTextures = new Dictionary<RotationAngle,List<Texture2D>>();
            List<Texture2D> eastWalking = new List<Texture2D>();
                eastWalking.Add(Textures.footmanMoving6A);
                eastWalking.Add(Textures.footmanMoving6B);
                eastWalking.Add(Textures.footmanMoving6C);
                eastWalking.Add(Textures.footmanMoving6D);
            List<Texture2D> northWalking = new List<Texture2D>();
                northWalking.Add(Textures.footmanMoving8A);
                northWalking.Add(Textures.footmanMoving8B);
                northWalking.Add(Textures.footmanMoving8C);
                northWalking.Add(Textures.footmanMoving8D);
            List<Texture2D> westWalking = new List<Texture2D>();
                westWalking.Add(Textures.footmanMoving4A);
                westWalking.Add(Textures.footmanMoving4B);
                westWalking.Add(Textures.footmanMoving4C);
                westWalking.Add(Textures.footmanMoving4D);
            List<Texture2D> southWalking = new List<Texture2D>();
                southWalking.Add(Textures.footmanMoving2A);
                southWalking.Add(Textures.footmanMoving2B);
                southWalking.Add(Textures.footmanMoving2C);
                southWalking.Add(Textures.footmanMoving2D);
            List<Texture2D> northEastWalking = new List<Texture2D>();
                northEastWalking.Add(Textures.footmanMoving9A);
                northEastWalking.Add(Textures.footmanMoving9B);
                northEastWalking.Add(Textures.footmanMoving9C);
                northEastWalking.Add(Textures.footmanMoving9D);
                northEastWalking.Add(Textures.footmanMoving9A);
                northEastWalking.Add(Textures.footmanMoving9B);
                northEastWalking.Add(Textures.footmanMoving9C);
                northEastWalking.Add(Textures.footmanMoving9D);
            List<Texture2D> northWestWalking = new List<Texture2D>();
                northWestWalking.Add(Textures.footmanMoving7A);
                northWestWalking.Add(Textures.footmanMoving7B);
                northWestWalking.Add(Textures.footmanMoving7C);
                northWestWalking.Add(Textures.footmanMoving7D);
                northWestWalking.Add(Textures.footmanMoving7A);
                northWestWalking.Add(Textures.footmanMoving7B);
                northWestWalking.Add(Textures.footmanMoving7C);
            List<Texture2D> southWestWalking = new List<Texture2D>();
                southWestWalking.Add(Textures.footmanMoving1A);
                southWestWalking.Add(Textures.footmanMoving1B);
                southWestWalking.Add(Textures.footmanMoving1C);
                southWestWalking.Add(Textures.footmanMoving1D);
                southWestWalking.Add(Textures.footmanMoving1A);
                southWestWalking.Add(Textures.footmanMoving1B);
                southWestWalking.Add(Textures.footmanMoving1C);
            List<Texture2D> southEastWalking = new List<Texture2D>();
                southEastWalking.Add(Textures.footmanMoving3A);
                southEastWalking.Add(Textures.footmanMoving3B);
                southEastWalking.Add(Textures.footmanMoving3C);
                southEastWalking.Add(Textures.footmanMoving3D);
                southEastWalking.Add(Textures.footmanMoving3A);
                southEastWalking.Add(Textures.footmanMoving3B);
                southEastWalking.Add(Textures.footmanMoving3C);
            walkingTextures.Add(RotationAngle.East, eastWalking);
            walkingTextures.Add(RotationAngle.North, northWalking);
            walkingTextures.Add(RotationAngle.West, westWalking);
            walkingTextures.Add(RotationAngle.South, southWalking);
            walkingTextures.Add(RotationAngle.NorthEast, northEastWalking);
            walkingTextures.Add(RotationAngle.NorthWest, northWestWalking);
            walkingTextures.Add(RotationAngle.SouthEast, southEastWalking);
            walkingTextures.Add(RotationAngle.SouthWest, southWestWalking);

            UnitTextures unitTextures = new UnitTextures(
                null, 
                idleTextures, 
                walkingTextures,
                null, 
                null
            );

            unitDictionary.Add(UnitType.Footman,
                new UnitClass(
                    60,
                    4,
                    10,
                    DamageType.Blade,
                    1,
                    5,
                    300,
                    0,
                    1,
                    new List<DamageType>(),
                    strongTo, 
                    abilities, 
                    new List<Spell>(),
                    unitTextures
                )
            );
        }

        internal static int getNextID() {
            lastID++;
            return lastID;
        }

        internal static void addUnit(Unit unit) {
            unitIDs.Add(unit.Id, unit);
            units.Add(unit);
        }

        internal static void removeUnit(Unit unit) {
            unitIDs.Remove(unit.Id);
            units.Remove(unit);
        }
    }
}
