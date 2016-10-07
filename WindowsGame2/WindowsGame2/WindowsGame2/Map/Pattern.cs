using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS {
    class Pattern {
        int[,] pattern;

        public Pattern (int[,] p){
            pattern = p;
        }

        public int[,] getPattern(){
            return pattern;
        }

        public bool equals(Pattern p) {
            bool equal = true;
            if (p==null){
                return false;
            }
            for(int y=0; y<=2;y++){
                for(int x=0; x<=2;x++){
                    if (p.getPattern()[y,x]!=pattern[y,x]){
                        equal = false;
                    }
                }
            }
            return equal;
        }
    }
}
