using DungeonLibrary;
using System.ComponentModel.Design;
using System.Net.Security;
using System.Numerics;

namespace DungeonApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Introduction
            //TODO Intro
            Console.WriteLine("Oh good, you're stil alive, I hope that bonk on the head I gave you didnt rattle you too hard..." +
                "The sign said stay out,\nbut yet, here you are...Anyway, welcome to your last few moments. " +
                "Inside this dungeon you will be met by as many monsters as you can stay alive to see.\nBig ones, scary ones, even some not of this earth." +
                "I guess all thats left is to pick your weapon now, but it wont matter, you wont be leaving here alive. Good luck partner.");
            #endregion

            #region Player Creation
            //Player Creation, after we've learned how to create custom Datatypes.
            //Reference the notes in the TestHarness for some ideas of how to expand player creation.

            List<Weapon> weaponOptions = new List<Weapon>();

            //Potential expansion - Let the user choose from a list of pre-made weapons.
            Weapon sword = new("LightSaber", 1, 8, 10, true, WeaponType.Sword);
            weaponOptions.Add(sword);
            Weapon glock = new("Glock 19", 2, 7, 9, true, WeaponType.Gun);
            weaponOptions.Add(glock);
            Weapon slingshot = new("slingshot", 1, 5, 5, true, WeaponType.Slingshot);
            weaponOptions.Add(slingshot);
            Weapon boxingGlove = new("Boxing Glove", 4, 5, 7, true, WeaponType.boxingGlove);
            weaponOptions.Add(boxingGlove);
            Weapon metalPipe = new("Metal Pipe", 3, 8, 15, false, WeaponType.metalPipe);
            weaponOptions.Add(metalPipe);
            //Potential Expansion - Let the user choose their name and Race
            Player player = new("Dungeon Fighter", Race.Human, null);
            
            


            #endregion
            
            //Outer Loop
            bool quit = false;
            do
            { 
                #region Monster and room generation
                //We need to generate a new monster and a new room for each encounter.                
                //TODONE Generate a room - random string description
                Console.WriteLine(GetRoom());
                //Generate a Monster (custom datatype) 
                Monster monster = Monster.GetMonster();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nOh no it's a " + monster.Name + "!!!");
                Console.ResetColor();
                #endregion

                #region Encounter Loop                
                //This menu repeats until either the monster dies or the player quits, dies, or runs away.
                bool reload = false;//set to true to "reload" the monster & the room

                chooseWeapon(weaponOptions, player);
        

                do
                {
                    #region Menu
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("\nPlease choose an action:\n" +
                        "1) pick weapon\n"+
                        "2) Attack\n" +
                        "3) Run away\n" +
                        "4) Weapon info\n"+
                        "5) Player Info\n" +
                        "6) Monster Info\n" +
                        "7) Exit\n");

                    char action = Console.ReadKey(true).KeyChar;
                    Console.Clear();
                    switch (action)
                    {

                        case '1':
                            chooseWeapon(weaponOptions, player);
                            break;
                        
                        case '2':
                            Console.WriteLine("Attack!");
                            reload = Combat.DoBattle(player, monster);
                            break;
                        
                        case '3':
                            Console.WriteLine("Run Away!!");
                            Combat.DoAttack(monster, player);
                            //Leave the inner loop (reload the room) and get a new room & monster.
                            reload = true;
                            break;
                        
                        case '4':
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Weapon info: ");
                            //print weapon details to the screen                          
                            Console.WriteLine(player.EquippedWeapon);
                            break;

                        case '5':
                            Console.WriteLine("player info: ");
                            //print player details to the screen
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(player);
                            break;

                        case '6':
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Monster info:");
                            Console.WriteLine(monster);
                            break;


                        case '7':
                            //quit the whole game. "reload = true;" gives us a new room and monster, "quit = true" quits the game, leaving both the inner AND outer loops.
                            Console.WriteLine("No one likes a quitter!");
                            quit = true;
                            break;

                        default:
                            Console.WriteLine("What are you? Scared to fight?");
                            break;
                    }//end switch
                    #endregion
                    //Check Player Life. If they are dead, quit the game and show them their score.
                    if (player.Life <= 0 )
                    {
                        Console.WriteLine("Dude... You died! That'll teach you!\a");
                        quit = true;//leave both loops.
                    }
                    else if  (player.Life <=1)
                    {
                        Console.WriteLine("Last life point, hope you learned a lesson sneaking into my cave Hahaha!");
                    }
                    else if (player.Life <= 5)
                    {
                        Console.WriteLine("I told you to be more careful! You're bad at this.");
                        
                    }
                    else if(player.Life <=10)
                    {
                        Console.WriteLine("You've taken some damage there partner, better be careful.");
                       
                    }

                    } while (!reload && !quit); //While reload and quit are both FALSE (!true), keep looping. If either becomes true, leave the inner loop.
                #endregion

            } while (!quit);//While quit is NOT true, keep looping.

            #region Exit
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("You defeated " + player.Score + 
                " monster" + (player.Score == 1 ? "." : "s."));
            #endregion

        }//End Main()

        private static void chooseWeapon(List<Weapon> weaponOptions, Player player)
        {
            bool weaponNotChosen = true;
            string invalidChoice = "";

            while (weaponNotChosen)
            {
                
                Console.WriteLine($"{invalidChoice}\nselect your weapon");
                foreach (Weapon weapon in weaponOptions)
                {
                    Console.WriteLine($"{weapon.Name[0].ToString().ToUpper()}.){weapon.Name}");
                }

                string choice = Console.ReadLine().ToLower();

                foreach (Weapon weapon in weaponOptions)
                {
                    if (choice == weapon.Name[0].ToString().ToLower() || choice == weapon.Name.ToLower())
                    {
                        player.EquippedWeapon = weapon;
                        weaponNotChosen = false;
                    }
                }

                if (weaponNotChosen && string.IsNullOrEmpty(invalidChoice))
                    invalidChoice = "Invalid choice, try again.";
            }

            if (player.EquippedWeapon is not null)
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Selected weapon:  " + player.EquippedWeapon.Name);
        }

        private static string GetRoom()
        {
            string[] rooms =
            {
                "\nSuddenly a large metal door creeks open, Looking around you see all the skulls on the ground from past victims.....wait what is that?!",
                "\nAll of a sudden a trap door opens. Dazed and confused you notice all The scratches on the walls, kinda makes you wish you had listened to the sign and turned around.....wait what is that?!",
                "\nA metal fence comes up from the floor trapping you inside...Are those skeletons on the chandelier?.... thats strange...wait what is that?!",
                "\nA steel cage drops from the ceiling, there is no escape....wait what is that?!",
                "\nSuddenly the lights come on and theres a creepy noise getting louder and louder. That jewelry box chime is a little unsettling isnt it? wait what is that?!",
                "\nLooking around you see a fountain full of red water, surely that cant be blood....right? Wait what is that?!",
                "\nWhy didnt i listen to the sign...wait what is that?!",
                "\nWhy are the lights flickering? Did someone forget to pay the Ameren bill? wait what is that?!",
                "\nTheres an awful lot of bones laying around on the floor. Does this place not have a janitor? wait what is that?!",
                "\nLooking around you see A gargoyle perched on the windowsill keeps watch over the room, occasionally shooting a jet of water out of its mouth. wait what is that?!"
            };

            Random rand = new Random();
            //rooms.Length
            int index = rand.Next(rooms.Length);
            string room = rooms[index];
            return room;



            

            //Refactor:
            //return rooms[new Random().Next(rooms.Length)];
        }//End GetRoom()

    }//End class
}//end namespace