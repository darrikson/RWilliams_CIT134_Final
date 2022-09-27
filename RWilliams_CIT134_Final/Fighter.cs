using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWilliams_CIT134_Final
{
    class Fighter
    {
        public static string Info => "\nFighter:\n" +
                "Has the highest Attack, middling HP and high Def\n" +
                "Abilities include buffs, status attacks, and a boosted attack\n";
        public static int HP = 4300;
        public decimal Atk = (decimal) 2.0;
        public int Def = 70;

        public Fighter()
        {

        }

        public static int SubMenu()
        //needs to return either a 0 (runs previous menu), a 1 (choice made) or a 2 (recurse)
        {
            Console.WriteLine("Fighter Ability Menu:");
            Console.WriteLine("1.) Boast");
            Console.WriteLine("2.) Mighty Blow");
            Console.WriteLine("3.) Disorient");
            Console.WriteLine("4.) Crippling Blow");
            Console.WriteLine("5.) Quick Strike");
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
                Console.WriteLine("INFO Fighter Ability Menu INFO:");
                Console.WriteLine("Please enter the number of the ability you wish to learn about");
                Console.WriteLine("1.) Boast");
                Console.WriteLine("2.) Mighty Blow");
                Console.WriteLine("3.) Disorient");
                Console.WriteLine("4.) Crippling Blow");
                Console.WriteLine("5.) Quick Strike");
                Console.WriteLine("0.) Return to Previous Menu");
                try
                {
                    input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 0:
                            input = 0;
                            break;
                        case 1: //Boast
                            Console.WriteLine("\nThe fighter lets loose a brag or insult, giving a boost to their confidence" +
                                "\nThe confidence boost lets them hit even harder" +
                                "\nLasts for 2 turns\n");
                            break;
                        case 2: //Mighty Blow
                            Console.WriteLine("\nThe fighter sacrifices their action to concentrate on finding an opening" +
                                "\nThis effort doesn't allow them to defend while focusing" +
                                "\nBut the fighter will score a massive hit next round\n");
                            break;
                        case 3: //Disorient
                            Console.WriteLine("\nThe fighter will viciously beat the target" +
                                "\nThe blows have a 30% chance of dazing the target" +
                                "\nDazed targets have a 50% chance to stumble and lose their next action\n");
                            break;
                        case 4: //Crippling Blow
                            Console.WriteLine("\nStrikes the target in a vital area, shaking their confidence" +
                                "\nThe target's defense will be less effective while their confidence is shaken" +
                                "\nLasts for 2 turns\n");
                            break;
                        case 5: //Quick Strike
                            Console.WriteLine("\nAttacks the target with a brutal hit" +
                                "\nThe blow has a 50% chance of concussing the opponent" +
                                "\nA concussed opponent will lose their focus on the next round\n");
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
                case 1: //Boast
                    Random randB = new Random();
                    switch (randB.Next(1, 5)) 
                    {
                        case 1:
                            Console.WriteLine($"{PL.Name} says: 'Might as well stop breathing, you're not going to be doing that much longer'");
                            Console.WriteLine($"to {OP.Name} while looking down at their sword.");
                            break;
                        case 2:
                            Console.WriteLine($"{PL.Name} stares at {OP.Name} for several seconds before uttering: '...Die'");
                            break;
                        case 3:
                            Console.WriteLine($"{PL.Name} says: 'I like your fire! Too bad I'm going to have to put it out.'");
                            break;
                        case 4:
                            Console.WriteLine($"{PL.Name} raises their sword and stares at {OP.Name} down its length");
                            Console.WriteLine($"{PL.Name}: 'If you move, that's my signal to kill you'");
                            break;
                    }
                    PL.StsFlag = true;
                    PL.Statuses[0].Flag = true;
                    PL.Statuses[0].Counter = Program.RoundCounter;
                    PL.Statuses[0].Message = $"{PL.Name} has a surge of strength!";
                    PL.AtkStsMod = 2;
                    Console.WriteLine($"{PL.Name} feels a power coming up from within");
                    break;
                case 2: //Mighty Blow
                    PL.StsFlag = true; 
                    PL.SetAtk(4);
                    PL.Statuses[11].Flag = true;
                    PL.Statuses[11].Counter = Program.RoundCounter;
                    PL.ActiveFlag = false;
                    PL.ActiveMessage = $"{PL.Name} is recovering from the exertion";
                    Console.WriteLine($"{PL.Name} is looking for an opening!");
                    break;
                case 3: //Disorient
                    Console.WriteLine($"{PL.Name} rushes in and lands a vicious flurry of blows on {OP.Name}!");
                    var Dmg = Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    Random randBo = new Random(); //seed 1 to test true
                    if (randBo.Next(1, 4) == 1)
                    {
                        OP.StsFlag = true;
                        OP.Statuses[10].Flag = true;
                        OP.Statuses[10].Counter = Program.RoundCounter;
                        OP.Statuses[10].Message = $"{OP.Name} looks unsteady...";
                        Console.WriteLine($"{OP.Name} was struck senseless!");
                    }
                    break;
                case 4: //Crippling Blow
                    Console.WriteLine($"{PL.Name} stabs {OP.Name}, burying the blade deep before ripping it free");
                    Console.WriteLine($"{OP.Name} moves to close the wound, {PL.Name} can tell {OP.Name} is badly shaken");
                    Dmg = Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    OP.StsFlag = true;
                    OP.DefStsMod = (decimal).5;
                    OP.Statuses[1].Flag = true;
                    OP.Statuses[1].Counter = Program.RoundCounter;
                    OP.Statuses[1].Message = $"{OP.Name} is quaking with doubt";
                    break;
                case 5: //Quick Strike
                    Console.WriteLine($"{PL.Name} lands a brtual hit against {OP.Name} with the hilt of their blade!");
                    Dmg = Program.DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    Random randQS = new Random(); //seed 1 to test true
                    if (randQS.Next(1, 3) == 1)
                    {
                        OP.StsFlag = true;
                        OP.Statuses[8].Flag = true;
                        OP.Statuses[8].Counter = Program.RoundCounter;
                        Console.WriteLine($"{OP.Name} flinched!");
                    }
                    break;
            }
        }
    }
}
