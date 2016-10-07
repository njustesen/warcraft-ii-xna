using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS {
    class Player {
        StartLocation startLocation;

        internal StartLocation StartLocation {
            get { return startLocation; }
            set { startLocation = value; }
        }
        Race race;

        internal Race Race {
            get { return race; }
            set { race = value; }
        }
        string name;

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public Player(){
            race = Race.Human;
        }

        public Player(Race irace, StartLocation istartLocation){
            race = irace;
            startLocation = istartLocation;
        }

    }
}
