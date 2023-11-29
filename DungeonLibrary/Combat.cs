using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Combat
    {
        // lets create a method for a one sided attack
        public static void DoAttack(Character attacker, Character defender)
        {
            // find the chance the attacker lands a hit 
            int chance = attacker.CalcHitChance() - defender.CalcBlock();
            int roll = new Random().Next(1, 101);
            // if the roll is less than or equal to the adjsuted hit chance, we hit
            bool hit = roll <= chance;

            // Thread.Sleep() will temporarily suspend the program giving the illustration that something is happening like a dice roll for instance
            Thread.Sleep(300);

            if (hit) // if (roll <= chance)
            {
                // calculate the damage
                int damage = attacker.CalcDamage();


                // subtract and assign it to the defenders life
                defender.Life -= damage;


                // output the result 
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{attacker.Name} hit {defender.Name} for {damage} damage!");
                Console.ResetColor();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{attacker.Name} missed!");
                Console.ResetColor();
              
            }
        }
        public static bool DoBattle(Player player, Monster monster)
        {
            DoAttack(player, monster);
            if(monster.Life > 0)
            {
                DoAttack(monster, player);
                return false;
            }

            else
            {
                player.Score++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You killed the {monster.Name}!");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("You werent supposed to do that! Enjoy it now because it wont happen again, SEND HIM TO THE NEXT ROOM!");
                Console.ResetColor();
                return true;

            }





        }


    }
}
