using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RTS {
    struct IntPosition {
        int xPos;
        int yPos;
        public IntPosition(int x, int y){
            xPos = x;
            yPos = y;
        }
        public int XPos{
            get { return xPos; }
            set { xPos = value; }
        }
        public int YPos{
            get { return yPos; }
            set { yPos = value; }
        }
        public override bool Equals(object obj) {
            if (obj.GetType().IsAssignableFrom(this.GetType())){
                IntPosition ip = (IntPosition) obj;
                if (ip.XPos == XPos && ip.yPos == YPos){
                    return true;
                } else {
                    return false;
                }
            }
            return base.Equals(obj);
        }
    }
    class Controller {

        public static IntPosition getDirectionValues(RotationAngle angle){
            if (angle == RotationAngle.East){
                return new IntPosition(1, 0);
            }
            if (angle == RotationAngle.North){
                return new IntPosition(0, -1);
            }
            if (angle == RotationAngle.NorthEast){
                return new IntPosition(1, -1);
            }
            if (angle == RotationAngle.NorthWest){
                return new IntPosition(-1, -1);
            }
            if (angle == RotationAngle.South){
                return new IntPosition(0, 1);
            }
            if (angle == RotationAngle.SouthEast){
                return new IntPosition(1, 1);
            }
            if (angle == RotationAngle.SouthWest){
                return new IntPosition(-1, 1);
            }
            if (angle == RotationAngle.West){
                return new IntPosition(-1, 0);
            }
            return new IntPosition(0, 0);
        }

        public static bool diagonal(RotationAngle angle){
            if (getDirectionValues(angle).XPos != 0 && getDirectionValues(angle).YPos != 0){
                return true;
            }
            return false;
        }
        /*
        internal static List<Edge> getShortestPath(IntPosition start, IntPosition goal) {
           
        }*/
        public static RotationAngle getDirection(IntPosition position, IntPosition goal){
            if (goal.XPos > position.XPos && goal.YPos > position.YPos){
                return RotationAngle.SouthEast;
            } else if (goal.XPos > position.XPos && goal.YPos < position.YPos){
                return RotationAngle.NorthEast;
            } else if (goal.XPos < position.XPos && goal.YPos < position.YPos){
                return RotationAngle.NorthWest;
            } else if (goal.XPos < position.XPos && goal.YPos > position.YPos){
                return RotationAngle.SouthWest;
            } else if (goal.XPos == position.XPos && goal.YPos < position.YPos){
                return RotationAngle.North;
            } else if (goal.XPos == position.XPos && goal.YPos > position.YPos){
                return RotationAngle.South;
            } else if (goal.XPos > position.XPos && goal.YPos == position.YPos){
                return RotationAngle.East;
            } else if (goal.XPos < position.XPos && goal.YPos == position.YPos){
                return RotationAngle.West;
            }
            return RotationAngle.North;
        }
    }
}

