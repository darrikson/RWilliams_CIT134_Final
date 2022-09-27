//Name: Rick Williams
//Section: CIT 134
//Date: 27th November 2019


//QuickPlay Method:
//Seed the random on line 30 to 25
//Select Cleric (1) at Class Selection
//Wisp will be generated as your opponent
//Select Ability(2) then Vanquish(5)
//Wait three turns, at the start of the 4th turn, Wisp will die
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWilliams_CIT134_Final
{
    class Program
    {
        public static int RoundCounter;
        public static int UserInput; //manipulated by SelectOption() line 507
        public static int SubMenu; //manipulated in individual Class menus (Ninja,Cleric,Fighter,Mage)
        public static Random OutsideRand = new Random();
        public static int OPSub; //manipulated by SelectOptionOP() line 569, and then in sub methods in Opponent class
        public static int[,] RoundData = new int[5,7];
        public static void Main()
        {
            Random rand = new Random(); //25 used as random testing seed
            Console.ForegroundColor = ConsoleColor.Green;
            //Opening Scroll
            Console.WriteLine("Greetings! Welcome to the Final Fight for Your Life!");
            Console.ReadKey();
            Console.WriteLine("The game will go as follows: You will set your Name and then select your character class,");
            Console.ReadKey();
            Console.WriteLine("You will then be thrust into battle against a random fiend to fight for your life!");
            Console.ReadKey();
            Console.WriteLine("The battle will continue until either you or the fiend falls!");
            Console.ReadKey();
            Console.Clear();
            
            //Instantiate Player Object with Name and selected Class
            Console.WriteLine("Please give your character a name:");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Fine, then your Name is now Nancy\n");
                name = "Nancy";
            }
            Player POne = new Player(name);

            //Give the player info on classes
            Cleric GCleric = new Cleric();
            Fighter GFighter = new Fighter();
            Mage GMage = new Mage();
            Ninja GNinja = new Ninja();
            int userInput;
            do
            {
                Console.WriteLine("Select a class to learn more about it.");
                Console.WriteLine("After reviewing the choices select 5 to continue" +
                " on to choose your class.");
                Console.WriteLine("1.) Cleric");
                Console.WriteLine("2.) Fighter");
                Console.WriteLine("3.) Mage");
                Console.WriteLine("4.) Ninja");

                try
                {
                    userInput = int.Parse(Console.ReadLine());
                    switch (userInput)
                    {
                        case 1:
                            DisplayInfo(Cleric.Info);
                            userInput = 0;
                            break;
                        case 2:
                            DisplayInfo(Fighter.Info);
                            userInput = 0;
                            break;
                        case 3:
                            DisplayInfo(Mage.Info);
                            userInput = 0;
                            break;
                        case 4:
                            DisplayInfo(Ninja.Info);
                            userInput = 0;
                            break;
                        case 5:
                            break;
                        default:
                            Console.WriteLine("\nInput not recognized, please try again.\n");
                            userInput = 0;
                            break;
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    userInput = 0;
                }
            } while (userInput == 0);
            Console.Clear();
            do
            {
                Console.WriteLine("CLASS SELECTION");
                Console.WriteLine("Please select your class:");
                Console.WriteLine("1.) Cleric");
                Console.WriteLine("2.) Fighter");
                Console.WriteLine("3.) Mage");
                Console.WriteLine("4.) Ninja");
                try
                {
                    userInput = int.Parse(Console.ReadLine());
                    switch (userInput)
                    {
                        case 1:
                            POne.SetPlayer(GCleric);
                            break;
                        case 2:
                            POne.SetPlayer(GFighter);
                            break;
                        case 3:
                            POne.SetPlayer(GMage);
                            break;
                        case 4:
                            POne.SetPlayer(GNinja);
                            break;
                        default:
                            Console.WriteLine("\nInput not recognized, please try again\n");
                            userInput = 0;
                            break;

                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    userInput = 0;
                }
            } while (userInput == 0);

            //Player instantiation test:
            //Console.WriteLine($"name: {POne.Name}\n" +
            //    $"hp: {POne.HP}\n" +
            //    $"attack: {POne.GetAtk()}\n" +
            //    $"defense: {POne.GetDef()}\n" +
            //    $"class: {POne.GetClassD()}");
            //Console.ReadKey();

            //instantiate opponent
            Opponent opponent = new Opponent(rand.Next(1,4));

            //Fiend instantiation test: Should return Wisp with proper seed
            //Console.WriteLine($"Name: {opponent.Name}");
            //Console.WriteLine($"HP: {opponent.HP}");
            //Console.WriteLine($"AtkVal: {opponent.AtkVal}");
            //Console.WriteLine($"DefVal: {opponent.DefVal}" );
            //Console.WriteLine($"Status Flag: {opponent.StsFlag}");
            //Console.WriteLine($"AtkStsMod: {opponent.AtkStsMod}");
            //Console.WriteLine($"DefStsMod: {opponent.DefStsMod}");
            //Console.WriteLine($"HPStsDmgMod: {opponent.HPStsDmgMod}");
            //Console.WriteLine($"Active Flag: {opponent.ActiveFlag}");
            //Console.WriteLine($"Class Designation: {opponent.ClassDesignation}");
            //Console.WriteLine($"Undead: {opponent.Undead}");

            Console.WriteLine($"A {opponent.Name} appeared! Get ready to fight!");
            Console.ReadKey();
            Console.Clear();

            //Begin Battle Logic Flow
            Battle(POne, opponent);  
        }

        private static void Battle(Player PL, Opponent OP)
        {
            do
            {
                UserInput = 0;
                RoundCounter += 1;
                PopRoundDataStart(PL, OP);
                Console.WriteLine($"RoundCounter = {RoundCounter}");
                PL.StsCheck(PL, OP);
                if (PL.ActiveFlag)
                {
                    while (UserInput == 0) //recurses until UserInput is manipulated to make a choice
                    {
                        SelectOption(PL); 
                    }
                //goes into do/while. SubMenu will use the ClassDesignation to call the correct
                //sub menu display method for the class, defined in each individual class. The choice selected
                //in the sub menu sets an outside variable used in the RunAction
                    
                }
                else
                {
                    Console.WriteLine(PL.ActiveMessage);
                }
                RunAction(UserInput, PL, OP);
                if (OP.HP == 0)
                {
                    PopRoundDataEnd(PL, OP);
                    break;
                }
                OP.StsCheck(OP);
                var OPDecision = SelectOptionOP(OP);
                if (OP.ActiveFlag)
                {
                    RunActionOP(OPDecision, OPSub, OP, PL);
                }
                else
                {
                    Console.WriteLine(OP.ActiveMessage);
                }
                if (PL.HP > 0 && PL.HPStsDmgMod > 0)
                {
                    decimal dmg = PL.HP*PL.HPStsDmgMod;
                    PL.HP -= (int)dmg;
                    Console.WriteLine($"{PL.Name} took {(int)dmg} additional damage");
                    Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                }
                if (OP.HP > 0 && OP.HPStsDmgMod > 0)
                {
                    decimal dmg = OP.HP * OP.HPStsDmgMod;
                    OP.HP -= (int)dmg;
                    Console.WriteLine($"{OP.Name} took {(int)dmg} additional damage");
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                }
                PopRoundDataEnd(PL, OP);
                Console.ReadKey();
                Console.Clear();
            } while (PL.HP > 0 && OP.HP > 0);

            if (PL.HP > 0)
            {
                Console.WriteLine($"{PL.Name} defeated the {OP.Name}!");
                Console.WriteLine("C  O  N  G  R  A  T  U  L  A  T  I  I  O  N  S  !");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("WINNER DATA");
                DisplayPLMem(PL);
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("LOSER DATA");
                DisplayOPMem(OP);
                Console.ReadKey();
                Console.Clear();
                DisplayStatusesPL(PL);
                Console.ReadKey();
                Console.Clear();
                DisplayStatusesOP(OP);
                Console.ReadKey();
                Console.Clear();
                DisplayRoundData(RoundData);
            }
            if (PL.HP == 0)
            {
                Console.WriteLine($"{OP.Name} has slain {PL.Name}");
                Console.WriteLine($"It was a valiant effort {PL.Name}...");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("WINNER DATA");
                DisplayOPMem(OP);
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("LOSER DATA");
                DisplayPLMem(PL);
                DisplayStatusesPL(PL);
                Console.ReadKey();
                Console.Clear();
                DisplayStatusesOP(OP);
                Console.ReadKey();
                Console.Clear();
                DisplayRoundData(RoundData);
            }
        }

        private static void DisplayOPMem(Opponent OP)
        {
            //I tried to find an easier way to do this, but I either got too much data,
            //or it uses System.Reflection, which I couldn't figure out
            Console.WriteLine($"Name: {OP.Name}");
            Console.WriteLine($"HP: {OP.HP}");
            Console.WriteLine($"Attack Stat: {OP.AtkVal}");
            Console.WriteLine($"Defense Stat: {OP.DefVal}");
            Console.WriteLine($"Status Flag: {OP.StsFlag}");
            Console.WriteLine($"Attack Modifier from Status: {OP.AtkStsMod}");
            Console.WriteLine($"Defense Modifier from Status: {OP.DefStsMod}");
            Console.WriteLine($"HP Damage modifier from Status: {OP.HPStsDmgMod}");
            Console.WriteLine($"Able to take action Flag: {OP.ActiveFlag}");
            Console.WriteLine($"SubClass Number: {OP.ClassDesignation}");
            Console.WriteLine($"Is Undead?: {OP.Undead}");
            Console.WriteLine($"Message when unable to take action: {OP.ActiveMessage}");
            Console.WriteLine($"Round when Defend was used: {OP.DefCounter}");
            Console.WriteLine($"Active Defense: {OP.DefendFlag}");
    }

        private static void DisplayPLMem(Player PL)
        {
            Console.WriteLine($"Name: {PL.Name}");
            Console.WriteLine($"HP: {PL.HP}");
            Console.WriteLine($"Attack Stat: {PL.AtkVal}");
            Console.WriteLine($"Defense Stat: {PL.DefVal}");
            Console.WriteLine($"Status Flag: {PL.StsFlag}");
            Console.WriteLine($"Attack Modifier from Status: {PL.AtkStsMod}");
            Console.WriteLine($"Defense Modifier from Status: {PL.DefStsMod}");
            Console.WriteLine($"HP Damage modifier from Status: {PL.HPStsDmgMod}");
            Console.WriteLine($"Able to take action Flag: {PL.ActiveFlag}");
            Console.WriteLine($"SubClass Number: {PL.ClassDesignation}");
            Console.WriteLine($"Message when unable to take action: {PL.ActiveMessage}");
            Console.WriteLine($"Round when Defend was used: {PL.DefCounter}");
            Console.WriteLine($"Active Defense: {PL.DefendFlag}");
        }

        private static void DisplayRoundData(int[,] RoundD)
        {
            Console.WriteLine("ROUND DATA");
            Console.WriteLine(string.Format("{0,6} {1,18} {2,20} {3,16} {4,18} {5,13} {6,16}","Round", "PlayerHP at Start", "Opponent HP at Start", "PlayerHP at End", "Opponent HP at End", "Damage Given", "Damage Received"));
            for (int row = 0; row < RoundD.GetLength(0); row++)
            {
                for (int col = 0; col < RoundD.GetLength(1); col++)
                {
                    if (RoundCounter == RoundD[row, col])
                    {
                        Console.Write(">");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    switch (col)
                    {
                        case 0:
                            Console.Write($"{RoundD[row, col],5}");
                            break;
                        case 1:
                            Console.Write($"{RoundD[row, col],18}");
                            break;
                        case 2:
                            Console.Write($"{RoundD[row, col],20}");
                            break;
                        case 3:
                            Console.Write($"{RoundD[row, col],16}");
                            break;
                        case 4:
                            Console.Write($"{RoundD[row, col],18}");
                            break;
                        case 5:
                            Console.Write($"{RoundD[row, col],13}");
                            break;
                        case 6:
                            Console.Write($"{RoundD[row, col],16}");
                            break;
                    }
                        
                    
                }
                Console.WriteLine();
            }
        }

        private static void DisplayStatusesOP(Opponent OP)
        {
            Console.WriteLine("OPPONENT STATUS ARRAY");
            Console.WriteLine(string.Format("{0,-12} {1,-5} {2,-7} {3,-7}", "Name", "Flag", "Counter", "Message"));
            foreach (OPStatus status in OP.Statuses)
            {
                Console.WriteLine(string.Format($"{status.Name,-12} {status.Flag,-5} {status.Counter,-7} {status.Message,-7}"));
            }
            Console.WriteLine();
        }

        private static void DisplayStatusesPL(Player PL)
        {
            Console.WriteLine("PLAYER STATUS ARRAY");
            Console.WriteLine(string.Format("{0,-12} {1,-5} {2,-7} {3,-7}", "Name", "Flag", "Counter", "Message" ));
            foreach(PLStatus status in PL.Statuses)
            {
                Console.WriteLine(string.Format($"{status.Name,-12} {status.Flag,-5} {status.Counter,-7} {status.Message,-7}"));
            }
            Console.WriteLine();
        }

        private static void PopRoundDataEnd(Player PL, Opponent OP)
        {
            var row = 0;
            switch (RoundCounter%5)
            {
                case 1:
                    row = 0;
                    break;
                case 2:
                    row = 1;
                    break;
                case 3:
                    row = 2;
                    break;
                case 4:
                    row = 3;
                    break;
                case 0:
                    row = 4;
                    break;
            }
            var DmgRec = RoundData[row, 1] - PL.HP;
            var DmgGiv = RoundData[row, 2] - OP.HP;
            RoundData[row, 3] = PL.HP;
            RoundData[row, 4] = OP.HP;
            RoundData[row, 5] = DmgGiv;
            RoundData[row, 6] = DmgRec;

        }

        private static void PopRoundDataStart(Player PL, Opponent OP)
        {
            var row = 0;
            switch (RoundCounter%5)
            {
                case 1:
                    row = 0;
                    break;
                case 2:
                    row = 1;
                    break;
                case 3:
                    row = 2;
                    break;
                case 4:
                    row = 3;
                    break;
                case 0:
                    row = 4;
                    break;
            }
            RoundData[row, 0] = RoundCounter;
            RoundData[row, 1] = PL.HP;
            RoundData[row, 2] = OP.HP;
        }

        private static void RunActionOP(int OPDecision, int OPSub, Opponent OP, Player PL)
        {
            switch (OP.ClassDesignation)
            {
                case 1:
                    Opponent.VampAct(OPDecision, OPSub, OP, PL);
                    break;
                case 2:
                    Opponent.GorgAct(OPDecision, OPSub, OP, PL);
                    break;
                case 3:
                    Opponent.WispAct(OPDecision, OPSub, OP, PL);
                    break;
            }
        }

        public static void RunAction(int action, Player PL, Opponent OP)
        {
            switch (action)
            {
                case 1:
                    Console.WriteLine($"{PL.Name} attacked {OP.Name} with their weapon!");
                    var Dmg = DmgCalc(PL, OP);
                    Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                    OP.HP -= Dmg;
                    Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    break;
                case 2:
                    RunSubAction(PL.GetClassD(), PL, OP);
                    break;
                case 3:
                    Console.WriteLine($"{PL.Name} took a defensive stance!");
                    PL.Defend();
                    break;
            }
        }

        private static void RunSubAction(int classD, Player PL, Opponent OP)
        {
            switch (classD)
            {
                case 1:
                    Cleric.RunSubMenu(PL, OP);
                    break;
                case 2:
                    Fighter.RunSubMenu(PL, OP);
                    break;
                case 3:
                    Mage.RunSubMenu(PL, OP);
                    break;
                case 4:
                    Ninja.RunSubMenu(PL, OP);
                    break;
            }
        }

        public static int DmgCalc(Player PL, Opponent OP)
        {
            var dmg = ((((OutsideRand.Next(150, 250) * PL.GetAtk()) * PL.AtkStsMod) * 100) / (OP.DefVal * OP.DefStsMod));
            return (int ) dmg;
        }

        public static int DmgCalcOP(Opponent OP, Player PL)
        {
            var dmg = ((((OutsideRand.Next(100, 200) * OP.AtkVal) * OP.AtkStsMod) * 100) / (PL.GetDef() * PL.DefStsMod));
            return (int)dmg;
        }

        public static int SelectOption(Player PL)
            //return one of 4 outcomes: 0 (Recursion) 1 (Fight) 2 (Display SubMenu) 3 (Defend)
        {
            Console.WriteLine("Select an action:");
            Console.WriteLine("1.) Fight");
            Console.WriteLine("2.) Ability");
            Console.WriteLine("3.) Defend");
            try
            {
                var choice = int.Parse(Console.ReadLine());
                if (choice == 2)
                {
                    var control = choice;
                    while (control == 2)
                    {
                        control = DisplaySubMenu(PL.GetClassD());
                        switch (control)
                        {
                            case 0:
                                control = 1;
                                UserInput = 0;
                                break;
                            case 1:
                                control = 1;
                                UserInput = 2;
                                break;
                            case 2:
                                control = 2;
                                break;
                        }
                    }
                }
                else
                {
                    UserInput = choice;
                }
                return UserInput;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UserInput = 0;
            }
            return UserInput;
        }

        private static int DisplaySubMenu(int classD)
        {
            switch (classD)
            {
                case 1:
                    return Cleric.SubMenu();
                case 2:
                    return Fighter.SubMenu();
                case 3:
                    return Mage.SubMenu();
                case 4:
                    return Ninja.SubMenu();
                default:
                    return 0;
            }
        }

        private static int SelectOptionOP(Opponent OP)
        {
            return Fiend.DetOp(OP);
        }

        public static void PlayerHeal(Player PL)
        {
            var healing = OutsideRand.Next(200, 501);
            PL.HP += healing;
            Console.WriteLine($"{PL.Name} regained {healing} HP!");
            Console.WriteLine($"{PL.Name} HP: {PL.HP}");
        }
        private static void DisplayInfo(string classInfo)
        {
            Console.WriteLine(classInfo);
        }
    }
}
