using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS {
    struct ExploredNode : IComparable<ExploredNode>{
        int priority;
        IntPosition position;

        public ExploredNode(int pri, IntPosition pos){
            priority = pri;
            position = pos; 
        }
        public int CompareTo(ExploredNode other){
            return priority - other.priority;
        }
        internal IntPosition Position {
            get { return position; }
            set { position = value; }
        }
    }
}
