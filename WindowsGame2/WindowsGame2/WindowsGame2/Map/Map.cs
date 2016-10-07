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
    class Map {
        static int[,] map;
        static int[,] fogMap;
        static int[,] unitMap;
        static int[,] smoothMap;
        static List<Unit>[,] whosLooking;

        static Dictionary<int, Texture2D> tileSet = new Dictionary<int, Texture2D>();
        static Dictionary<int, bool> walkableTiles;

        public static void initTileSet(){
            tileSet.Add(0, Textures.bound);
            tileSet.Add(1, Textures.grass);
                tileSet.Add(11, Textures.grass11);
            tileSet.Add(2, Textures.dirt);
                tileSet.Add(200, Textures.dirt200);

                tileSet.Add(22, Textures.dirt2grass);
                    tileSet.Add(220, Textures.dirt2grass2);
                tileSet.Add(24, Textures.dirt4grass);
                    tileSet.Add(240, Textures.dirt4grass2);
                tileSet.Add(26, Textures.dirt6grass);
                    tileSet.Add(260, Textures.dirt6grass2);
                tileSet.Add(28, Textures.dirt8grass);
                    tileSet.Add(280, Textures.dirt8grass2);

                tileSet.Add(286, Textures.dirt286);
                tileSet.Add(284, Textures.dirt284);
                tileSet.Add(224, Textures.dirt224);
                tileSet.Add(226, Textures.dirt226);

                tileSet.Add(21, Textures.dirt21);
                tileSet.Add(23, Textures.dirt23);
                tileSet.Add(27, Textures.dirt27);
                tileSet.Add(29, Textures.dirt29);

                tileSet.Add(219, Textures.dirt19);
                tileSet.Add(237, Textures.dirt37);

            tileSet.Add(3, Textures.forest);
                tileSet.Add(31, Textures.forest1);
                tileSet.Add(32, Textures.forest2);
                tileSet.Add(33, Textures.forest3);
                tileSet.Add(34, Textures.forest4);
                tileSet.Add(35, Textures.forest5);
                tileSet.Add(36, Textures.forest6);
                tileSet.Add(37, Textures.forest7);
                tileSet.Add(38, Textures.forest8);
                tileSet.Add(39, Textures.forest9);
                tileSet.Add(324, Textures.forest24);
                tileSet.Add(326, Textures.forest26);
                tileSet.Add(384, Textures.forest84);
                tileSet.Add(386, Textures.forest86);

                tileSet.Add(337, Textures.forest);
                tileSet.Add(319, Textures.forest);
            
            tileSet.Add(4, Textures.water);
                tileSet.Add(41, Textures.water1);
                tileSet.Add(42, Textures.water2);
                tileSet.Add(43, Textures.water3);
                tileSet.Add(44, Textures.water4);
                tileSet.Add(45, Textures.water5);
                tileSet.Add(46, Textures.water6);
                tileSet.Add(47, Textures.water7);
                tileSet.Add(48, Textures.water8);
                tileSet.Add(49, Textures.water9);
                tileSet.Add(424, Textures.water24);
                tileSet.Add(426, Textures.water26);
                tileSet.Add(484, Textures.water84);
                tileSet.Add(486, Textures.water86);

                tileSet.Add(437, Textures.water);
                tileSet.Add(419, Textures.water);

            walkableTiles = new Dictionary<int, bool>();

            walkableTiles.Add(1, true);
            walkableTiles.Add(2, true);
            walkableTiles.Add(3, false);
            walkableTiles.Add(4, false);

        }

        static Dictionary<int, UnitType> unitSet = new Dictionary<int, UnitType>();

        public static void initUnitSet(){
            unitSet.Add(12, UnitType.Footman);
            unitSet.Add(22, UnitType.Grunt);
        }

        
        static Dictionary<Pattern,int> smoothDirectory = new Dictionary<Pattern,int>();

        public static void initSmoothDirectory(){

            // DIRT TO GRASS

                // None
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                // Singles
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            27);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            28);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            29);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            24);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            26);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            21);
            
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            22);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            23);

                // doubles
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            28);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            24);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            26);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            22);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            237);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            28);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            284);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            286);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            28);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            28);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            24);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            26);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            219);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            22);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            24);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            224);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            24);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            26);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            226);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            26);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            22);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            22);

                // Triples
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            28);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            284);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            286);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            28);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            28);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            24);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            26);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            22);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            24);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            224);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            24);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            26);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            226);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            26);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            22);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            22);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            284);


                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            286);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            28);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            28);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            284);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            284);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            286);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            286);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            28);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            24);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            224);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            24);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            26);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            226);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            26);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            22);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            22);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            224);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            24);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            224);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            226);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            26);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            226);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            22);

                // Quadro
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            284);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            286);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            28);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            28);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            284);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            284);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            286);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            286);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            28);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            24);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            224);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            24);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            26);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            226);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            26);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            22);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            224);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            24);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            224);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            226);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            226);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            22);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            284);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            284);
            
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            286);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            224);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            24);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            224);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            226);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            26);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            226);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            22);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            24);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            224);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            226);

                // Penta
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,1},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,2},
                                                            {1,2,2}}
                                                            ),
                                                            284);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,2},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,2},
                                                            {2,2,1}}
                                                            ),
                                                            284);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            286);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            286);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            284);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            286);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            224);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            24);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            224);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            226);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            26);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            28);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            226);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            224);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {2,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            226);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            284);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            286);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            248);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            286);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {2,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            224);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {2,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            226);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {1,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //Six
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,1},
                                                            {1,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,1},
                                                            {2,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,1},
                                                            {2,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,2},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,2},
                                                            {1,2,1}}
                                                            ),
                                                            284);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,2},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            286);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {2,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            224);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {2,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            226);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,2},
                                                            {1,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {2,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,2},
                                                            {1,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,1},
                                                            {1,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //seven
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,1},
                                                            {1,1,2}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,1},
                                                            {1,2,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,1},
                                                            {2,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,2,2},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {2,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,2},
                                                            {1,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,2,1},
                                                            {1,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,1,1},
                                                            {1,2,1},
                                                            {1,1,1}}
                                                            ),
                                                            2);

            // DIRT TO WATER

                // None
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                // Singles
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);
            
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                // doubles
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                // Triples
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);


                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                // Quadro
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,2},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);
            
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                // Penta
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,4},
                                                            {2,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,2},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,2},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,2},
                                                            {2,2,4}}
                                                            ),
                                                            2);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //Six
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,4},
                                                            {4,2,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,4},
                                                            {2,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,4},
                                                            {2,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,2},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,2},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,2},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //seven
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,4},
                                                            {4,4,2}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,4},
                                                            {2,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,2,2},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,2,4},
                                                            {4,4,4}}
                                                            ),
                                                            2);


            // FOREST
                
                // None
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                // Singles
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            37);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            38);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            39);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            34);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            36);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            31);
            
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            32);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            33);

                // doubles
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            38);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            34);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            36);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            32);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            337);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            38);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            384);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            386);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            38);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            38);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            34);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            36);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            319);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            32);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            34);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            324);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            34);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            36);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            326);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            36);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            32);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            32);

                // Triples
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            38);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            384);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            386);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            38);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            38);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            34);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            36);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            32);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            34);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            324);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            34);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            36);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            326);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            36);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            32);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            32);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            384);


                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            386);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            38);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            38);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            384);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            384);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            386);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            386);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            38);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            34);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            324);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            34);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            36);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            326);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            36);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            32);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            32);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            324);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            34);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            324);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            326);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            36);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            326);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            32);

                // Quadro
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,3},
                                                            {3,3,3}}
                                                            ),
                                                            384);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            386);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            38);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            38);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            384);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            384);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            386);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            386);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            38);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            34);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            324);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            34);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            36);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            326);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            36);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            32);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            324);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            34);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            324);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            326);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            326);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            32);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            384);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            384);
            
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            386);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            324);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            34);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            324);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            326);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            36);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            326);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            32);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            34);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            324);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {3,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            326);

                // Penta
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,1},
                                                            {3,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,3},
                                                            {1,3,3}}
                                                            ),
                                                            384);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,3},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,3},
                                                            {3,3,1}}
                                                            ),
                                                            384);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            386);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            386);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            384);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            386);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            324);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            34);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            324);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            326);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            36);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            38);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            326);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            324);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {3,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            326);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            384);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            386);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            348);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            386);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {3,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            324);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {3,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            326);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,3},
                                                            {1,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //Six
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,1},
                                                            {1,3,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,1},
                                                            {3,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,1},
                                                            {3,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,3},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,3},
                                                            {1,3,1}}
                                                            ),
                                                            384);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,3},
                                                            {3,1,1}}
                                                            ),
                                                            23);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            386);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {3,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            324);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {3,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            326);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,3},
                                                            {1,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {3,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,3},
                                                            {1,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,3,1},
                                                            {1,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //seven
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,1},
                                                            {1,1,3}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,1},
                                                            {1,3,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,1},
                                                            {3,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {1,3,3},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,1},
                                                            {3,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,1,3},
                                                            {1,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{1,3,1},
                                                            {1,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{3,1,1},
                                                            {1,3,1},
                                                            {1,1,1}}
                                                            ),
                                                            3);

            // WATER
                
                // None
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                // Singles
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            47);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            48);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            49);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            44);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            46);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            41);
            
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            42);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            43);

                // doubles
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            48);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            44);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            46);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            42);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            437);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            48);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            484);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            486);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            48);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            48);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            44);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            46);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            42);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            44);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            424);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            44);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            46);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            426);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            46);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            42);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            42);

                // Triples
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            48);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            484);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            486);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            48);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            48);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            44);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            46);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            42);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            44);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            424);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            44);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            46);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            426);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            46);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            42);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            42);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            484);


                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            486);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            48);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            48);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            484);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            2);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            484);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            486);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            486);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            48);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            44);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            424);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            44);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            46);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            426);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            46);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            42);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            42);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            424);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            44);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            424);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            426);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            46);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            426);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            42);

                // Quadro
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,4},
                                                            {4,4,4}}
                                                            ),
                                                            484);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            486);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            48);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            48);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            484);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            484);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            486);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            486);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            48);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            44);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            424);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            44);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            46);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            426);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            46);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            42);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            424);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            44);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            424);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            426);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            426);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            42);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            484);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            484);
            
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            486);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            424);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            44);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            424);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            426);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            46);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            426);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            42);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            44);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            424);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {4,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            426);

                // Penta
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,2},
                                                            {4,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,4},
                                                            {2,4,4}}
                                                            ),
                                                            484);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,4},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,4},
                                                            {4,4,2}}
                                                            ),
                                                            484);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            486);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            486);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            484);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            486);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            424);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            44);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            424);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            426);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            46);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            48);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            426);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            424);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {4,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            426);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            484);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            486);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            448);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            486);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {4,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            424);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {4,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            426);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,4},
                                                            {2,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //Six
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,2},
                                                            {2,4,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,2},
                                                            {4,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,2},
                                                            {4,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,4},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,4},
                                                            {2,4,2}}
                                                            ),
                                                            484);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,4},
                                                            {4,2,2}}
                                                            ),
                                                            24);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);
                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            486);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {4,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            424);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {4,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            426);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,4},
                                                            {2,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {4,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,4},
                                                            {2,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,4,2},
                                                            {2,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //seven
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,2},
                                                            {2,2,4}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,2},
                                                            {2,4,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,2},
                                                            {4,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {2,4,4},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,2},
                                                            {4,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,2,4},
                                                            {2,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{2,4,2},
                                                            {2,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);

                //--
                smoothDirectory.Add(new Pattern(new int[,] {{4,2,2},
                                                            {2,4,2},
                                                            {2,2,2}}
                                                            ),
                                                            4);


        }

        public static void generateSmoothMap(){
            smoothMap = new int[map.GetLength(0),map.GetLength(1)];

            for (int y = 0; y < map.GetLength(0); y++) {
                for (int x = 0; x < map.GetLength(1); x++) {
                    smoothMap[y,x] = map[y,x];
                }
            }

            for (int y = 1; y < map.GetLength(0)-1; y++) {
                for (int x = 1; x < map.GetLength(1)-1; x++) {
                    smoothMap[y,x] = vareity(smoothenMap(map[y,x], createSurroundings(y,x)));
                }
            }
        }

        private static int seed = 0;
        private static int vareity(int p) {
            Random random = new Random(seed);
            int randomNumber = random.Next(1,99);
            seed += random.Next(1,randomNumber);
            if (p==1 && randomNumber%2==1){
                return 11;
            }
            if (p==2 && randomNumber%2==1){
                return 200;
            }
            if (p==22 && randomNumber%2==1){
                return 220;
            }
            if (p==24 && randomNumber%2==1){
                return 240;
            }
            if (p==26 && randomNumber%2==1){
                return 260;
            }
            if (p==28 && randomNumber%2==1){
                return 280;
            }
            return p;
        }

        private static int smoothenMap(int type, Pattern p) {
            if (type==1){
                return 1;
            }
            if (type==2 || type==3 || type==4){
                return knownPattern(p);
            }
            return 2;
        }

        private static int knownPattern(Pattern p){
            foreach (Pattern k in smoothDirectory.Keys){
                if (k.equals(p)){
                    return smoothDirectory[k];
                }
            }
            return 1;
        }

        private static Pattern createSurroundings(int y, int x) {
            if ((x < 1 || y < 1) || (x+1 >= map.GetLength(1) || y+1 >= map.GetLength(0))) {
                return null;
            } else {
                int[,] surroundings = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
                surroundings[0, 0] = map[y - 1, x - 1];
                surroundings[0, 1] = map[y - 1, x];
                surroundings[0, 2] = map[y - 1, x + 1];
                surroundings[1, 0] = map[y, x - 1];
                surroundings[1, 1] = map[y, x];
                surroundings[1, 2] = map[y, x + 1];
                surroundings[2, 0] = map[y + 1, x - 1];
                surroundings[2, 1] = map[y + 1, x];
                surroundings[2, 2] = map[y + 1, x + 1];

                return new Pattern(surroundings);
            }
        }

        public static int[,] getSmoothMap(){
            return smoothMap;
        }

        public static int[,] getMap(){
            return map;
        }

        public static int[,] getFogMap(){
            return fogMap;
        }

        public static int[,] getUnitMap(){
            return unitMap;
        }

        public static List<Unit>[,] getWhosLooking(){
            return whosLooking;
        }

        public static Dictionary<int, Texture2D> getTileSet(){
            return tileSet;
        }

        public static Dictionary<int, UnitType> getUnitSet(){
            return unitSet;
        }

        internal static void paintTile(TileType tileType, int x, int y, int brushSize) {
            if (tileType == TileType.Dirt) {
                if (brushSize == 3) {
                    clearForDirt(x - 2, y - 2);
                    clearForDirt(x - 1, y - 2);
                    clearForDirt(x, y - 2);
                    clearForDirt(x + 1, y - 2);
                    clearForDirt(x + 2, y - 2);

                    clearForDirt(x - 2, y - 1);
                    clearForDirt(x + 2, y - 1);

                    clearForDirt(x - 2, y);
                    clearForDirt(x + 2, y);

                    clearForDirt(x - 2, y + 1);
                    clearForDirt(x + 2, y + 1);

                    clearForDirt(x - 2, y + 2);
                    clearForDirt(x - 1, y + 2);
                    clearForDirt(x, y + 2);
                    clearForDirt(x + 1, y + 2);
                    clearForDirt(x + 2, y + 2);

                    addTile(2, x - 1, y - 1);
                    addTile(2, x, y - 1);
                    addTile(2, x + 1, y - 1);
                    addTile(2, x + 1, y);
                    addTile(2, x - 1, y);
                    addTile(2, x + 1, y + 1);
                    addTile(2, x, y + 1);
                    addTile(2, x - 1, y + 1);
                } else if (brushSize == 1) {
                    clearForDirt(x - 1, y - 1);
                    clearForDirt(x, y - 1);
                    clearForDirt(x + 1, y - 1);

                    clearForDirt(x - 1, y);
                    clearForDirt(x + 1, y);

                    clearForDirt(x - 1, y + 1);
                    clearForDirt(x, y + 1);
                    clearForDirt(x + 1, y + 1);
                }
                addTile(2, x, y);

            } else if (tileType == TileType.Grass) {
                if (brushSize == 3) {
                    addTile(1, x - 1, y - 1);
                    addTile(1, x, y - 1);
                    addTile(1, x + 1, y - 1);
                    addTile(1, x + 1, y);
                    addTile(1, x - 1, y);
                    addTile(1, x + 1, y + 1);
                    addTile(1, x, y + 1);
                    addTile(1, x - 1, y + 1);
                }
                addTile(1, x, y);
            } else if (tileType == TileType.Forest) {
                if (brushSize == 3) {

                    clearForForest(x - 2, y - 2);
                    clearForForest(x - 1, y - 2);
                    clearForForest(x, y - 2);
                    clearForForest(x + 1, y - 2);
                    clearForForest(x + 2, y - 2);

                    clearForForest(x - 2, y - 1);
                    clearForForest(x + 2, y - 1);

                    clearForForest(x - 2, y);
                    clearForForest(x + 2, y);

                    clearForForest(x - 2, y + 1);
                    clearForForest(x + 2, y + 1);

                    clearForForest(x - 2, y + 2);
                    clearForForest(x - 1, y + 2);
                    clearForForest(x, y + 2);
                    clearForForest(x + 1, y + 2);
                    clearForForest(x + 2, y + 2);
                    
                    addTile(3, x - 1, y - 1);
                    addTile(3, x, y - 1);
                    addTile(3, x + 1, y - 1);
                    addTile(3, x + 1, y);
                    addTile(3, x - 1, y);
                    addTile(3, x + 1, y + 1);
                    addTile(3, x, y + 1);
                    addTile(3, x - 1, y + 1);
                    
                } else if (brushSize == 1) {
                    clearForForest(x - 1, y - 1);
                    clearForForest(x, y - 1);
                    clearForForest(x + 1, y - 1);

                    clearForForest(x - 1, y);
                    clearForForest(x + 1, y);

                    clearForForest(x - 1, y + 1);
                    clearForForest(x, y + 1);
                    clearForForest(x + 1, y + 1);
                }
                addTile(3, x, y);
            }  else if (tileType == TileType.Water) {
                if (brushSize == 3) {

                    clearForWater(x - 3, y - 3);
                    clearForWater(x - 2, y - 3);
                    clearForWater(x - 1, y - 3);
                    clearForWater(x - 0, y - 3);
                    clearForWater(x + 1, y - 3);
                    clearForWater(x + 2, y - 3);
                    clearForWater(x + 3, y - 3);

                    clearForWater(x - 3, y - 2);
                    clearForWater(x + 3, y - 2);

                    clearForWater(x - 3, y - 1);
                    clearForWater(x + 3, y - 1);

                    clearForWater(x - 3, y - 0);
                    clearForWater(x + 3, y - 0);

                    clearForWater(x - 3, y + 1);
                    clearForWater(x + 3, y + 1);

                    clearForWater(x - 3, y + 2);
                    clearForWater(x + 3, y + 2);

                    clearForWater(x - 3, y + 3);
                    clearForWater(x + 3, y + 3);

                    clearForWater(x - 3, y + 3);
                    clearForWater(x - 2, y + 3);
                    clearForWater(x - 1, y + 3);
                    clearForWater(x - 0, y + 3);
                    clearForWater(x + 1, y + 3);
                    clearForWater(x + 2, y + 3);
                    clearForWater(x + 3, y + 3);

                    clearForWater(x - 2, y - 2);
                    clearForWater(x - 1, y - 2);
                    clearForWater(x, y - 2);
                    clearForWater(x + 1, y - 2);
                    clearForWater(x + 2, y - 2);

                    clearForWater(x - 2, y - 1);
                    clearForWater(x + 2, y - 1);

                    clearForWater(x - 2, y);
                    clearForWater(x + 2, y);

                    clearForWater(x - 2, y + 1);
                    clearForWater(x + 2, y + 1);

                    clearForWater(x - 2, y + 2);
                    clearForWater(x - 1, y + 2);
                    clearForWater(x, y + 2);
                    clearForWater(x + 1, y + 2);
                    clearForWater(x + 2, y + 2);
                    
                    addTile(4, x - 1, y - 1);
                    addTile(4, x, y - 1);
                    addTile(4, x + 1, y - 1);
                    addTile(4, x + 1, y);
                    addTile(4, x - 1, y);
                    addTile(4, x + 1, y + 1);
                    addTile(4, x, y + 1);
                    addTile(4, x - 1, y + 1);
                    
                } else if (brushSize == 1) {
                    clearForWater(x - 1, y - 1);
                    clearForWater(x, y - 1);
                    clearForWater(x + 1, y - 1);

                    clearForWater(x - 1, y);
                    clearForWater(x + 1, y);

                    clearForWater(x - 1, y + 1);
                    clearForWater(x, y + 1);
                    clearForWater(x + 1, y + 1);
                }
                addTile(4, x, y);
            }
            updateSmoothMap(x, y, 5);
        }

        internal static void addUnit(UnitType unitType, int x, int y, Player player){
            if (onMap(x,y)){
                if (walkableTiles[map[y,x]]){
                    int id = UnitLibrary.getNextID();
                    UnitLibrary.addUnit(new Unit(id, new IntPosition(x,y), unitType, player));
                    unitMap[y,x] = id;
                }
            }
        }
        
        internal static void setStartLocation(int x, int y, Player activePlayer) {
            if (onMap(x,y)){
                if (walkableTiles[map[y,x]]){
                    activePlayer.StartLocation = new StartLocation(x,y);
                }
            }
        }

        private static void clearForWater(int x, int y) {
            if (onMap(x,y)) {
                if (map[y,x] != 4){
                    addTile(2, x, y);
                }
            }
        }

        private static void clearForForest(int x, int y) {
            if (onMap(x,y)) {
                if (map[y,x] != 3){
                    addTile(1, x, y);
                }
            }
        }

        private static void clearForDirt(int x, int y) {
            if (onMap(x,y)) {
                if (map[y,x] != 2){
                    if (map[y,x] == 4){
                        addTile(4, x, y);
                    } else {
                        addTile(1, x, y);
                    }
                }
            }
        }

        public static void addTile(int mapValue, int x, int y) {
            if (onMap(x,y)) {
                map[y, x] = mapValue;
                if (!walkableTiles[mapValue]){
                    unitMap[y, x] = 0;
                }
            }
        }

        private static bool onMap(int x, int y) {
            if (!((x < 0 || y < 0) || (x >= map.GetLength(1) || y >= map.GetLength(0)))) {
                return true;
            }
            return false;
        }

        private static void updateSmoothMap(int x, int y, int area) {
            for (int ya = y - area; ya < y + area - 1; ya++) {
                for (int xa = x - area; xa < x + area - 1; xa++) {
                    if (onMap(xa,ya)) {
                        smoothMap[ya, xa] = map[ya, xa];
                    }
                }
            }
            
            for (int ya = y - area; ya < y + area - 1; ya++) {
                for (int xa = x - area; xa < x + area - 1; xa++) {
                    if (onMap(xa,ya)) {
                        smoothMap[ya, xa] = vareity(smoothenMap(map[ya, xa], createSurroundings(ya, xa)));
                    }
                }
            }
            
        }

        internal static void createMap(Vector2 size) {
            map = new int[(int)size.Y, (int)size.X];
            unitMap = new int[(int)size.Y, (int)size.X];
            for (int y = 0; y < map.GetLength(0); y++) {
                for (int x = 0; x < map.GetLength(1); x++) {
                    map[y, x] = 1;
                    unitMap[y, x] = 0;
                }
            }
        }

        internal static void initFogMap() {
            fogMap = new int[map.GetLength(0), map.GetLength(1)];
            for (int y = 0; y < fogMap.GetLength(0); y++) {
                for (int x = 0; x < fogMap.GetLength(1); x++) {
                    fogMap[y, x] = 1;
                }
            }
            foreach(Unit u in UnitLibrary.getUnits()){
                //addSight((int)u.Position.X, (int)u.Position.Y, u, (float)UnitLibrary.getDictionary()[u.UnitType].Sight);
            }
        }

        internal static void initWhosLooking() {
            whosLooking = new List<Unit>[map.GetLength(0), map.GetLength(1)];
            for (int y = 0; y < whosLooking.GetLength(0); y++) {
                for (int x = 0; x < whosLooking.GetLength(1); x++) {
                    whosLooking[y, x] = new List<Unit>(20);
                }
            }
            foreach(Unit u in UnitLibrary.getUnits()){
                addSight((int)u.Position.XPos, (int)u.Position.YPos, u, (float)UnitLibrary.getDictionary()[u.UnitType].Sight);
            }
        }

        private static void addSight(int x, int y, Unit unit, float sight) {
            if (sight+1>0 && !((x < 0 || y < 0) || (x >= map.GetLength(1) || y >= map.GetLength(0)))){
                whosLooking[y, x].Add(unit);
                addSight(x-1, y-1, unit, sight-1.25f);
                addSight(x, y-1, unit, sight-1);
                addSight(x+1, y-1, unit, sight-1.25f);

                addSight(x-1, y, unit, sight-1);
                addSight(x, y, unit, sight-1);
                addSight(x+1, y, unit, sight-1);

                addSight(x-1, y+1, unit, sight-1.25f);
                addSight(x, y+1, unit, sight-1);
                addSight(x+1, y+1, unit, sight-1.25f);
            }
        }

        internal static void setMap(int[,] array) {
            map = array;
        }

        internal static void setUnitMap(int[,] array) {
            unitMap = array;
        }

        internal static void updateUnitFog(Unit unit) {
            /*
            for (int y = unit.Position.YPos - 10; y < unit.Position.YPos + 10; y++ ) {
                for (int x = unit.Position.XPos - 10; x < unit.Position.XPos + 10; x++ ) {
                    if (onMap(y,x)){
                        whosLooking[y, x].Remove(unit);
                    }
                }
            }*/
            addSight(unit.Position.XPos, unit.Position.YPos, unit, UnitLibrary.getDictionary()[unit.UnitType].Sight);
        }

        internal static IEnumerable<IntPosition> getAdjecentSquares(IntPosition pos) {
            List<IntPosition> squares = new List<IntPosition>();
            if (onMap(pos.XPos+1, pos.YPos-1)){
                squares.Add(new IntPosition(pos.XPos+1, pos.YPos-1));
            }
            if (onMap(pos.XPos, pos.YPos-1)){
                squares.Add(new IntPosition(pos.XPos, pos.YPos-1));
            }
            if (onMap(pos.XPos-1, pos.YPos-1)){
                squares.Add(new IntPosition(pos.XPos-1, pos.YPos-1));
            }
            if (onMap(pos.XPos+1, pos.YPos)){
                squares.Add(new IntPosition(pos.XPos+1, pos.YPos));
            }
            if (onMap(pos.XPos-1, pos.YPos)){
                squares.Add(new IntPosition(pos.XPos-1, pos.YPos));
            }
            if (onMap(pos.XPos+1, pos.YPos+1)){
                squares.Add(new IntPosition(pos.XPos+1, pos.YPos+1));
            }
            if (onMap(pos.XPos, pos.YPos+1)){
                squares.Add(new IntPosition(pos.XPos, pos.YPos+1));
            }
            if (onMap(pos.XPos-1, pos.YPos+1)){
                squares.Add(new IntPosition(pos.XPos-1, pos.YPos+1));
            }
            return squares;
        }

        internal static bool isWalkable(IntPosition pos) {
            if (map[pos.YPos,pos.XPos] == 1 || map[pos.YPos,pos.XPos] == 2){
                return true;
            }
            return false;
        }
    }
}
