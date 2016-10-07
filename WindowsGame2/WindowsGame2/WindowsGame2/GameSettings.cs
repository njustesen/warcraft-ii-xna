using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS {
    class GameSettings {
        static float gameSpeed = 1.8f;

        public static float GameSpeed {
            get { return gameSpeed; }
            set { gameSpeed = value; }
        }
    }
}
