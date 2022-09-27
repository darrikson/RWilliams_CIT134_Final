using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWilliams_CIT134_Final
{
    class Cleric
    {
        public static string Info => "\nCleric: \n"
                + "Boasts the highest defence and HP but standard attack stat. \n" +
                "Has access to healing, buff and debuff, and can instantly kill the undead\n";
        public static int HP = 5000;
        public decimal Atk = (decimal) 1.4;
        public int Def = 75;

        public Cleric ()
        {

        }

        public static int SubMenu() 
            //needs to return either a 0 (runs previous menu), a 1 (choice made) or a 2 (recurse)
        {
            Console.WriteLine("Cleric Ability Menu:");
            Console.WriteLine("1.) Barrier");
            Console.WriteLine("2.) Penance");
            Console.WriteLine("3.) Bash");
            Console.WriteLine("4.) Heal");
            Console.WriteLine("5.) Vanquish");
            Console.WriteLine("6.) View Ability Info");
            Console.WriteLine("0.) Return to Previous Menu");
            var userInput = int.Parse(Console.ReadLine());
            switch (userInput)
            {
                case 0:
                    return 0;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    Program.SubMenu = userInput;
                    return 1;
                case 6:
                    AbilityInfo();
                    userInput = 2;
                    return userInput;
                default:
                    Console.WriteLine("Input not recognized, please try again");
                    userInput = 2;
                    return userInput;
            }
        }

        private static void AbilityInfo()
        {
            var input = 1;
            while (input != 0)
            {
                Console.WriteLine("INFO Cleric Ability Menu INFO:");
                Console.WriteLine("Please enter the number of the ability you wish to learn about");
                Console.WriteLine("1.) Barrier");
                Console.WriteLine("2.) Penance");
                Console.WriteLine("3.) Bash");
                Console.WriteLine("4.) Heal");
                Console.WriteLine("5.) Vanquish");
                Console.WriteLine("0.) Return to Previous Menu");
                try
                {
                    input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 0:
                            input = 0;
                            break;
                        case 1: //Barrier
                            Console.WriteLine("\nRaises a barrier around the Cleric" +
                                "\nWhile the barrier is active, the Cleric's defense is doubled" +
                                "\nThe barrier lasts 3 turns\n");
                            break;
                        case 2: //Penance
                            Console.WriteLine("\nPrays to weaken the enemy" +
                                "\nAn aura appears around the target, weakening them for 3 turns" +
                                "\nWhile the aura is active, the target's abiliy to defend will be halved\n");
                            break;
                        case 3: //Bash
                            Console.WriteLine("\nSmites the enemy with a mace" +
                                "\nThe blow has a 50% chance of concussing the target" +
                                "\nA concussed target will lose their ability to act on the next round\n");
                            break;
                        case 4: //Heal
                            Console.WriteLine("\nSays a small prayer to recover HP" +
                                "\nThe cleric will recover a small amount of HP" +
                                "\nThe HP recovered is a random amount between 220 and 500\n");
                            break;
                        case 5: //Vanquish
                            Console.WriteLine("\nCalls on the divine to banish the undead from the earth" +
                                "\nThe Cleric prays intently for 3 rounds, during this period the Cleric is open to attack" +
                                "\nIf the Cleric survives, then the undead are instantly vaporized\n");
                            break;
                        default:
                            Console.WriteLine("\nInput not recognized, please try again\n");
                            input = 6;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again");
                    Console.WriteLine("");
                    input = 6;
                }
            }
        }

        public static void RunSubMenu(Player PL, Opponent OP)
        {
            switch (Program.SubMenu)
            {
                case 1: //Barrier
                    Console.WriteLine($"{PL.Name} raises their mace above their head and says a small prayer");
                    Console.WriteLine($"{PL.Name} is surrounded by a divine light!");
                    PL.StsFlag = true;
                    PL.Statuses[3].Flag = true;
                    PL.Statuses[3].Counter = Program.RoundCounter;
                    PL.Statuses[3].Message = $"The barrier shimmers around {PL.Name}";
                    PL.DefStsMod = (decimal)2;
                    break;
                case 2: //Penance
                    Console.WriteLine($"{PL.Name} bows their head and solemnly prays");
                    Console.WriteLine($"{OP.Name} is surrounded by a shining aura");
                    OP.StsFlag = true;
                    OP.Statuses[2].Flag = true;
                    OP.Statuses[2].Counter = Program.RoundCounter;
                    OP.Statuses[2].Message = $"The aura shines around {OP.Name}";
                    OP.DefStsMod = (decimal).4;
                    break;
                case 3: //Bash
                    Console.WriteLine($"{PL.Name} smacks their mace against {OP.Name} with tremendous force!");
                    var Dmg = Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    Random randS = new Random(); //seed 1 to test true
                    if (randS.Next(1, 3) == 1)
                    {
                        OP.StsFlag = true;
                        OP.Statuses[8].Flag = true;
                        OP.Statuses[8].Counter = Program.RoundCounter;
                        Console.WriteLine($"{OP.Name} flinched!");
                    }
                    break;
                case 4: //Heal
                    Console.WriteLine($"{PL.Name} bows their head and prays to the divine for healing");
                    Console.WriteLine($"{PL.Name} was surrounded by a warm light!");
                    Program.PlayerHeal(PL);
                    Console.WriteLine($"The light around {PL.Name} softly faded away");
                    break;
                case 5: //Vanquish
                    PL.StsFlag = true;
                    PL.Statuses[14].Flag = true;
                    PL.Statuses[14].Counter = Program.RoundCounter;
                    PL.ActiveFlag = false;
                    PL.ActiveMessage = $"{PL.Name} is concentrating intently whilst praying";
                    Console.WriteLine($"{PL.Name} brings their mace to their chest and bows their head, praying intently");
                    break;
            }
        }
    }
}
