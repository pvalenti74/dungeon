﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    //Sealed denotes the end of an inheritance chain. Player cannot be inherited.
    public sealed class Player : Character
    {

        //FIELDS - Funny
        //no fields, no business rules

        //PROPS - People
        public Race PlayerRace { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public int Score { get; set; }

        //CTORs - Collect
        public Player(string name, /*int hitChance, int block, int maxLife,*/ Race playerRace, Weapon equippedWeapon) 
            : base(name, 70, 5, 40)//hitchance, block, maxlife/life
        {            
            PlayerRace = playerRace;
            EquippedWeapon = equippedWeapon;

            #region Potential Expansion: Racial Bonuses
            //build a switch based on the PlayerRace. Apply buffs/debuffs depending on which race they picked.
            switch (PlayerRace)
            {
                case Race.Human:
                    HitChance += 5;
                    Life -= 3;
                    MaxLife -= 3;
                    break;
                
                case Race.Dwarf:
                    HitChance += 5;
                    Life -= 3;
                    MaxLife -= 3;
                    break;
                case Race.Giant:
                    HitChance += 10;
                    Life -= 2;
                    MaxLife -= 20;
                    break;
                case Race.Orc:
                    HitChance += 5;
                    Life -= 3;
                    MaxLife -= 3;
                    break;
              
                default:
                    break;
            }

            #endregion
        }//End CTOR

        //METHODS - Monkeys
        private static string GetRaceDesc(Race race)
        {
            switch (race)
            {
                case Race.Human:
                    return "Human";
                case Race.Tiefling:
                    return "Tiefling";
                case Race.Dwarf:
                    return "Dwarf";
                case Race.Giant:
                    return "Giant";
                case Race.Orc:
                    return "Orc";
                case Race.Cyborg:
                    return "Cyborg";
                default:
                    return "";
            }
        }//end getRaceDesc()
        public override string ToString()
        {
            return base.ToString() + $"\nWeapon: \n{EquippedWeapon}\n" +
                $"Description: \n{GetRaceDesc(PlayerRace)}";
        }

        //CalcDamage() override -> return a random number between the EquippedWeapon's MinDamage and MaxDamage properties.
        public override int CalcDamage()
        {
            //throw new NotImplementedException();
            Random rand = new Random();
            //.Next() 0 to int.MaxValue
            //.Next(int value) 0 to value - 1
            //.Next(int value1, int value2) value1 to value2 - 1
            int damage = rand.Next(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage + 1);
            //Upper bound is exclusive, so we add 1 to the max damage.
            return damage;
        }
        //CalcHitChance() override -> return the HitChance + EquippedWeapon's BonusHitChance property.
        public override int CalcHitChance()
        {
            int chance = HitChance + EquippedWeapon.BonusHitChance;
            return chance;
        
        }
        
    
    }

}
