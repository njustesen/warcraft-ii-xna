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
    class Unit : Actor {
        static int lastID = 0;
        static int movementPoints = 10;
        Player owner;
        UnitType unitType;
        RotationAngle rotation;
        bool idle;
        int HP;
        int id;
        IntPosition position;
        bool moving = false;
        double movingProcess = 0;

        public Unit (int i, IntPosition pos, UnitType type, Player owner){
            id = i;
            position = pos;
            idle = true;
            unitType = type;
            rotation = getRandomRotation();
            HP = UnitLibrary.getDictionary()[unitType].HP;
        }

        public Unit (int i, IntPosition pos, UnitType type, Player owner, RotationAngle rot){
            id = i;
            position = pos;
            idle = true;
            unitType = type;
            rotation = rot;
            HP = UnitLibrary.getDictionary()[unitType].HP;
        }

        private static int seed = 0;
        private RotationAngle getRandomRotation() {
            Random random = new Random(seed);
            int randomNumber = random.Next(1,81);
            seed += random.Next(1,randomNumber);
            if (randomNumber<=10){
                return RotationAngle.East;
            } else if (randomNumber<=20){
                return RotationAngle.NorthWest;
            } else if (randomNumber<=30){
                return RotationAngle.North;
            } else if (randomNumber<=40){
                return RotationAngle.NorthEast;
            } else if (randomNumber<=50){
                return RotationAngle.South;
            } else if (randomNumber<=60){
                return RotationAngle.SouthEast;
            } else if (randomNumber<=70){
                return RotationAngle.SouthWest;
            }
            return RotationAngle.West;
        }

        public void act(GameTime gameTime){
            if (moving){
                movingProcess += UnitLibrary.getDictionary()[unitType].Speed*(gameTime.ElapsedGameTime.TotalSeconds*5*GameSettings.GameSpeed);
                float factor = 1.0f;
                if (Controller.diagonal(rotation)){
                    factor = 1.414f;
                }
                if (movingProcess >= movementPoints*factor){
                    if (Map.getUnitMap()[(int)position.YPos+Controller.getDirectionValues(rotation).YPos, (int)position.XPos+Controller.getDirectionValues(rotation).XPos] == -1){
                        Map.getUnitMap()[(int)position.YPos, (int)position.XPos] = 0;
                        position.YPos += Controller.getDirectionValues(rotation).YPos;
                        position.XPos += Controller.getDirectionValues(rotation).XPos;
                        Map.getUnitMap()[(int)position.YPos, (int)position.XPos] = id;
                        Map.updateUnitFog(this);
                    }
                    moving = false;
                }
            } else if (path != null && path.Count != 1){
                if (!(position.XPos == path.First().XPos && position.YPos == path.First().YPos)){
                    path.RemoveAt(path.Count-1);
                    Move(Controller.getDirection(position, path.Last()));
                    
                }
            }
        }

        public void Move(RotationAngle direction){
            if (!moving){
                rotation = direction;
                if (Map.getUnitMap()[position.YPos + Controller.getDirectionValues(rotation).YPos, position.XPos + Controller.getDirectionValues(rotation).XPos] == 0){
                    Map.getUnitMap()[position.YPos + Controller.getDirectionValues(rotation).YPos, position.XPos + Controller.getDirectionValues(rotation).XPos] = -1;
                    moving = true;
                    movingProcess = 0;
                }
            }
        }

        List<IntPosition> path;
        internal void moveTo(IntPosition tile) {
            path = Pathfinder.getShortestPath(position, tile);
        }

        internal int getWalkingPhase() {
            float factor = 1.0f;
            if (Controller.diagonal(rotation)){
                factor = 1.414f;
            }
            return (int)((MovingProcess/MovementPoints*factor) * 4);
        }

        public static int MovementPoints {
            get { return Unit.movementPoints; }
            set { Unit.movementPoints = value; }
        }

        public static int LastID {
            get { return lastID; }
        }

        public bool Moving {
            get { return moving; }
            set { moving = value; }
        }

        public double MovingProcess {
            get { return movingProcess; }
            set { movingProcess = value; }
        }

        internal Player Owner {
            get { return owner; }
            set { owner = value; }
        }

        internal UnitType UnitType {
            get { return unitType; }
            set { unitType = value; }
        }

        public RotationAngle Rotation {
            get { return rotation; }
            set { rotation = value; }
        }

        public bool Idle {
            get { return idle; }
            set { idle = value; }
        }

        public IntPosition Position {
            get { return position; }
            set { position = value; }
        }

        public int Id {
            get { return id; }
        }
    }
}
