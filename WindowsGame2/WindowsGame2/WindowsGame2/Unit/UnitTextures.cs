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
    public class UnitTextures {
        Texture2D icon;
        Dictionary<RotationAngle, Texture2D> idleTextures;
        Dictionary<RotationAngle, List<Texture2D>> walkingTextures;
        Dictionary<RotationAngle, List<Texture2D>> attackingTextures;
        Dictionary<RotationAngle, List<Texture2D>> dyingTextures;

        public UnitTextures(Texture2D i,
                            Dictionary<RotationAngle, Texture2D> idle,
                            Dictionary<RotationAngle, List<Texture2D>> walking,
                            Dictionary<RotationAngle, List<Texture2D>> attacking,
                            Dictionary<RotationAngle, List<Texture2D>> dying){
            icon = i;
            idleTextures = idle;
            walkingTextures = walking;
            attackingTextures = attacking;
            dyingTextures = dying;
        }
        public Texture2D getIcon(){
            return icon;
        }
        public Dictionary<RotationAngle, Texture2D> getIdleTextures(){
            return idleTextures;
        }
        public Dictionary<RotationAngle, List<Texture2D>> getWalkingTextures(){
            return walkingTextures;
        }
        public Dictionary<RotationAngle, List<Texture2D>> getAttackingTextures(){
            return attackingTextures;
        }
        public Dictionary<RotationAngle, List<Texture2D>> getDyingTextures(){
            return dyingTextures;
        }
    }
}
