using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS {
    class UnitClass {
        int hp;
        public int HP {
            get { return hp; }
            set { hp = value; }
        }

        int speed;
        public int Speed {
            get { return speed; }
            set { speed = value; }
        }

        int baseDamage;

        public int BaseDamage {
            get { return baseDamage; }
            set { baseDamage = value; }
        }
        DamageType damageType;

        public DamageType DamageType {
            get { return damageType; }
            set { damageType = value; }
        }
        int range;

        public int Range {
            get { return range; }
            set { range = value; }
        }
        int sight;

        public int Sight {
            get { return sight; }
            set { sight = value; }
        }
        int goldCost;

        public int GoldCost {
            get { return goldCost; }
            set { goldCost = value; }
        }
        int lumberCost;

        public int LumberCost {
            get { return lumberCost; }
            set { lumberCost = value; }
        }
        int foodCost;

        public int FoodCost {
            get { return foodCost; }
            set { foodCost = value; }
        }
        List<DamageType> weakTo;

        public List<DamageType> WeakTo {
            get { return weakTo; }
            set { weakTo = value; }
        }
        List<DamageType> strongTo;

        public List<DamageType> StrongTo {
            get { return strongTo; }
            set { strongTo = value; }
        }
        List<Ability> abilities;

        public List<Ability> Abilities {
            get { return abilities; }
            set { abilities = value; }
        }
        List<Spell> spells;

        public List<Spell> Spells {
            get { return spells; }
            set { spells = value; }
        }

        UnitTextures unitTextures;
        public UnitTextures UnitTextures {
            get { return unitTextures; }
            set { unitTextures = value; }
        }

        public UnitClass(   int iHP, 
                            int iSpeed, 
                            int iBaseDamage, 
                            DamageType iDamageType, 
                            int iRange, 
                            int iSight, 
                            int iGoldCost, 
                            int iLumberCost, 
                            int iFoodCost, 
                            List<DamageType> iWeakTo,
                            List<DamageType> iStrongTo,
                            List<Ability> iAbilities,
                            List<Spell> iSpells,
                            UnitTextures ut){

            hp = iHP;
            speed = iSpeed;
            baseDamage = iBaseDamage;
            damageType = iDamageType;
            range = iRange;
            sight = iSight;
            goldCost = iGoldCost;
            lumberCost = iLumberCost;
            foodCost = iFoodCost;
            weakTo = iWeakTo;
            strongTo = iStrongTo;
            abilities = iAbilities;
            spells = iSpells;
            unitTextures = ut;
        }


    }
}
