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
    class StartLocation{
        int x;

        public int X {
            get { return x; }
            set { x = value; }
        }
        int y;

        public int Y {
            get { return y; }
            set { y = value; }
        }

        public StartLocation(int xx, int yy){
            x = xx;
            y = yy;
        }
    }
}
