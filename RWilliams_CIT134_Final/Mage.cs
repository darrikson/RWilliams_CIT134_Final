using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWilliams_CIT134_Final
{
    class Mage
    {
        public static string Info => "\nMage:\n" +
                "Lowest attack and defence, and middling HP\n" +
                "but has access to abilities that inflict extra damage and statuses\n";
        public static int HP = 4000;
        public decimal Atk = (decimal) 1.0; 
        public int Def = 50;

        public Mage()
        {

        }
        public static int SubMenu()
        //needs to return either a 0 (runs previous menu), a 1 (choice made) or a 2 (recurse)
        {
            Console.WriteLine("Mage Ability Menu:");
            Console.WriteLine("1.) Fire");
            Console.WriteLine("2.) Ice");
            Console.WriteLine("3.) Bolt");
            Console.WriteLine("4.) Poison");
            Console.WriteLine("5.) Frighten");
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
                Console.WriteLine("INFO Mage Ability Menu INFO:");
                Console.WriteLine("Please enter the number of the ability you wish to learn about");
                Console.WriteLine("1.) Fire");
                Console.WriteLine("2.) Ice");
                Console.WriteLine("3.) Bolt");
                Console.WriteLine("4.) Poison");
                Console.WriteLine("5.) Frighten");
                Console.WriteLine("0.) Return to Previous Menu");
                try
                {
                    input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 0:
                            input = 0;
                            break;
                        case 1:
                            Console.WriteLine("\nShoots a fireball at the opponent" +
                                "\nThe fireball does damage and has a 30% chance of inflicting a burn"+
                                "\nBurn will do an addition 5% damage per turn for 3 turns\n");
                            break;
                        case 2:
                            Console.WriteLine("\nSummons ice around the target, doing large damage"+
                                "\nHas a 50% chance to freeze the target" +
                                "\nFrozen targets will not be able to take their turn, but will immediatly thaw next round\n");
                            break;
                        case 3:
                            Console.WriteLine("\nSummons lightning down on the target, damaging them"+
                                "\nThe lightning has a 30% chance of dazing the target"+
                                "\nDazed targets have a 50% chance to stumble and lose their next action\n");
                            break;
                        case 4:
                            Console.WriteLine("\nSours the target's blood for a turn"+
                                "\nHas a 25% chance of a poison taking hold"+
                                "\nPoisoned targets will lose an additional 8% HP per turn for 3 turns\n");
                            break;
                        case 5:
                            Console.WriteLine("\nSummons an illusion to frighten the target"+
                                "\nThe illusion does no damage, but frightened targets take additional damage"+
                                "\nLasts for 2 turns\n");
                            break;
                        default:
                            Console.WriteLine("\nInput not recognized, please try again\n");
                            input = 6;
                            break;
                    }
                }catch (Exception ex)
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
                case 1: // Burn is first implemented and tested Ability and Status
                    PL.AtkStsMod = (decimal) 1.7;
                    Console.WriteLine($"{PL.Name} raised their staff and shot a briliant fireball at {OP.Name}!");
                    var Dmg =Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    PL.AtkStsMod = 1;
                    Random randB = new Random();
                    if (randB.Next(1,4) == 1) //seed 1 to test true
                    {
                        OP.StsFlag = true;
                        OP.Statuses[5].Flag = true;
                        OP.HPStsDmgMod = (decimal) .07;
                        OP.Statuses[5].Counter = Program.RoundCounter;
                        OP.Statuses[5].Message = $"{OP.Name} is suffering from a burn!";
                        Console.WriteLine($"{OP.Name} was burned!");
                    }
                    break;
                case 2: //Freeze
                    Console.WriteLine($"{PL.Name} taps the butt of their staff to the ground, and {OP.Name} is surrounded by ice!");
                    PL.AtkStsMod = (decimal) 2.5;
                    Dmg = Program.DmgCalc(PL, OP);
                    PL.AtkStsMod = 1;
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    Random randF = new Random(); 
                    if (randF.Next(1,3) == 1) //seed 1 to test true
                    {
                        OP.StsFlag = true;
                        OP.ActiveFlag = false;
                        OP.ActiveMessage = $"{OP.Name} is encased in ice!";
                        OP.Statuses[12].Flag = true;
                        OP.Statuses[12].Counter = Program.RoundCounter;
                        Console.WriteLine($"{OP.Name} was flash frozen!");

                    }
                    break;
                case 3: //Bolt
                    PL.AtkStsMod = 2;
                    Console.WriteLine($"{PL.Name} raises their staff high above their head, lightning crashes down on {OP.Name}!");
                    Dmg = Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    PL.AtkStsMod = 1;
                    Random randBo = new Random(); //seed 1 to test true
                    if (randBo.Next(1,4)==1)
                    {
                        OP.StsFlag = true;
                        OP.Statuses[10].Flag = true;
                        OP.Statuses[10].Counter = Program.RoundCounter;
                        OP.Statuses[10].Message = $"{OP.Name} looks unsteady...";
                        Console.WriteLine($"{OP.Name} was struck senseless!");
                    }
                    break;
                case 4: //Poison
                    PL.AtkStsMod = (decimal) 1.7;
                    Console.WriteLine($"{PL.Name} points their staff at {OP.Name}, {OP.Name}'s blood turns sour");
                    Dmg = Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    PL.AtkStsMod = 1;
                    Random randP = new Random(); //seed 1 to test true
                    if(randP.Next(1,5) == 1)
                    {
                        OP.StsFlag = true;
                        OP.HPStsDmgMod = (decimal).085;
                        OP.Statuses[6].Flag = true;
                        OP.Statuses[6].Counter = Program.RoundCounter;
                        OP.Statuses[6].Message = $"{OP.Name} is suffering from poisoned blood";
                        Console.WriteLine($"A strong poison took hold in {OP.Name}'s blood");
                    }
                    break;
                case 5: //Frighten
                    //100% activation rate, no random
                    Console.WriteLine($"{PL.Name} waves their staff around in the air, and a horrible specter appears before {OP.Name}'s eyes!");
                    OP.StsFlag = true;
                    OP.DefStsMod = (decimal).5;
                    OP.Statuses[1].Flag = true;
                    OP.Statuses[1].Counter = Program.RoundCounter;
                    OP.Statuses[1].Message = $"{OP.Name} is shaking with fear!";
                    break;
            }
        }
    }
}
