using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWilliams_CIT134_Final
{
    class Ninja
    {
        public static string Info => "\nNinja:\n" +
                "Lowest HP and middling defence, but a higher than average attack\n" +
                "Abilities include healing and status infliction\n";
        public static int HP = 3500;
        public decimal Atk = (decimal) 1.7;
        public int Def = 60;

        public Ninja()
        {

        }

        public static int SubMenu()
        //needs to return either a 0 (runs previous menu), a 1 (choice made) or a 2 (recurse)
        {
            Console.WriteLine("Ninja Ability Menu:");
            Console.WriteLine("1.) Poison Blade");
            Console.WriteLine("2.) Suiton");
            Console.WriteLine("3.) Izunami");
            Console.WriteLine("4.) Pill");
            Console.WriteLine("5.) Soul Bind");
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
                Console.WriteLine("INFO Ninja Ability Menu INFO:");
                Console.WriteLine("Please enter the number of the ability you wish to learn about");
                Console.WriteLine("1.) Poison Blade");
                Console.WriteLine("2.) Suiton");
                Console.WriteLine("3.) Izunami");
                Console.WriteLine("4.) Pill");
                Console.WriteLine("5.) Soul Bind");
                Console.WriteLine("0.) Return to Previous Menu");
                try
                {
                    input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 0:
                            input = 0;
                            break;
                        case 1: // Poison Blade
                            Console.WriteLine("\nAttacks with an envenomed blade" +
                                "\nHas a 25% chance of a poison taking hold" +
                                "\nPoisoned targets will lose an additional 8% HP per turn for 3 turns\n");
                            break;
                        case 2: //Suiton
                            Console.WriteLine("\nDrenches the target with freezing sheets of water" +
                                "\nHas a 50% chance to freeze the target" +
                                "\nFrozen targets will not be able to take their turn, but will immediatly thaw next round\n");
                            break;
                        case 3: //Izunami
                            Console.WriteLine("\nDrops a bolt of lightning down on the target, damaging them" +
                                "\nThe bolt has a 50% chance of stunning the target" +
                                "\nStunned targets will lose their focus on the next round\n");
                            break;
                        case 4: //Pill
                            Console.WriteLine("\nSwallows a bitter pill to recover HP" +
                                "\nThe ninja will recover a small amount of HP and gain a surge of strength" +
                                "\nThe HP recovered is a random amount between 220 and 500, strength last 2 rounds\n");
                            break;
                        case 5: //SoulBind
                            Console.WriteLine("\nBinds the fates of the Ninja and the target, making them share in suffering" +
                                "\nThe Ninja and target will each take additional HP damage per turn while Soul Bind is active" +
                                "\nThe Ninja takes additional 5% and the target takes additional 15%, lasts 3 turns\n");
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
                case 1: //Poison Blade
                    Console.WriteLine($"{PL.Name} dashes in and slashes {OP.Name} with a envenomed blade!");
                    var Dmg = Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    Random randP = new Random(); //seed 1 to test true
                    if (randP.Next(1, 5) == 1)
                    {
                        OP.StsFlag = true;
                        OP.HPStsDmgMod = (decimal).085;
                        OP.Statuses[6].Flag = true;
                        OP.Statuses[6].Counter = Program.RoundCounter;
                        OP.Statuses[6].Message = $"{OP.Name} is suffering from poison";
                        Console.WriteLine($"A strong poison took hold in {OP.Name}'s veins");
                    }
                    break;
                case 2: //Suiton
                    Console.WriteLine($"{PL.Name} makes several hand signs, finishing with a large flailing arm flourish");
                    Console.WriteLine($"{OP.Name} is deluged by sheets of freezing cold water!");
                    Dmg = Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    Random randF = new Random();
                    if (randF.Next(1, 3) == 1) //seed 1 to test true
                    {
                        OP.StsFlag = true;
                        OP.ActiveFlag = false;
                        OP.ActiveMessage = $"{OP.Name} is frozen stiff!";
                        OP.Statuses[12].Flag = true;
                        OP.Statuses[12].Counter = Program.RoundCounter;
                        Console.WriteLine($"{OP.Name} was flash frozen!");

                    }
                    break;
                case 3: //Izunami
                    Console.WriteLine($"{PL.Name} makes several hand signs and ends with a fist dropping into an open palm");
                    Console.WriteLine($"A bolt of lightning crashes down on {OP.Name}!");
                    Dmg = Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    Random randBo = new Random(); //seed 1 to test true
                    if (randBo.Next(1, 3) == 1)
                    {
                        OP.StsFlag = true;
                        OP.Statuses[8].Flag = true;
                        OP.Statuses[8].Counter = Program.RoundCounter;
                        Console.WriteLine($"{OP.Name} flinched!");
                    }
                    break;
                case 4: //Pill
                    Console.WriteLine($"{PL.Name} takes a small pill from a pouch at their waist and pops it into their mouth");
                    Console.WriteLine($"{PL.Name} swallows, grimacing at the bitter taste, but immediatly begins to feel better");
                    Program.PlayerHeal(PL);
                    PL.StsFlag = true;
                    PL.Statuses[0].Flag = true;
                    PL.Statuses[0].Counter = Program.RoundCounter;
                    PL.Statuses[0].Message = $"{PL.Name} has a surge of strength!";
                    PL.AtkStsMod = 2;
                    Console.WriteLine($"{PL.Name} feels a power coming up from within");
                    break;
                case 5: //SoulBind
                    Console.WriteLine($"{PL.Name} makes several hand signs, ending with a slight bow, holding forward a fist packed into an open hand");
                    Console.WriteLine($"Small motes of dust circle around {OP.Name} and {PL.Name}");
                    OP.StsFlag = true;
                    PL.StsFlag = true;
                    OP.HPStsDmgMod = (decimal).15;
                    PL.HPStsDmgMod = (decimal).05;
                    OP.Statuses[7].Flag = true;
                    PL.Statuses[7].Flag = true;
                    OP.Statuses[7].Counter = Program.RoundCounter;
                    PL.Statuses[7].Counter = Program.RoundCounter;
                    OP.Statuses[7].Message = $"{OP.Name} is suffering under the soul binding";
                    PL.Statuses[7].Message = $"{PL.Name} is tightening the soul binding";
                    break;
            }
        }
    }
}
