using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWilliams_CIT134_Final
{
    class OPStatus
    {
        public bool Flag;
        public int Counter;
        public string Message;
        public string Name;

        public OPStatus(string name)
        {
            Name = name;
        }

        public void Exhaust(Opponent OP, int statNum)
        {
            switch (statNum)
            {
                case 0:  //Might
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 3)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.AtkStsMod = 1;
                        Console.WriteLine($"{OP.Name}'s strength faded");
                    }
                    break;
                case 1: //Weak
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 3)
                    {
                        OP.DefStsMod = 1;
                        OP.Statuses[statNum].Flag = false;
                        Console.WriteLine($"{OP.Name} has regained their confidence");
                    }
                    break;
                case 2: //Penance
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 4)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.DefStsMod = 1;
                        Console.WriteLine($"The aura surrounding {OP.Name} faded away");
                    }
                    break;
                case 3: //Barrier
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 4)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.DefStsMod = 1;
                        Console.WriteLine($"The barrier surrounding {OP.Name} faded away");
                    }
                    break;
                case 4: //Drain
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 1)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.HPStsDmgMod = 0;
                        Console.WriteLine($"{OP.Name} staunched the bleeding");
                    }
                    break;
                case 5: //Burn
                    if (Program.RoundCounter < OP.Statuses[statNum].Counter + 4)
                    {
                        OP.StsFlag = true;
                        OP.HPStsDmgMod = (decimal).07;
                        OP.Statuses[statNum].Flag = true;
                    }
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 4)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.HPStsDmgMod = 0;
                        Console.WriteLine("The burning has faded");
                    }
                    break;
                case 6: //Poison
                    if (Program.RoundCounter < OP.Statuses[statNum].Counter + 4)
                    {
                        OP.StsFlag = true;
                        OP.HPStsDmgMod = (decimal).085;
                        OP.Statuses[statNum].Flag = true;
                    }
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 4)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.HPStsDmgMod = 0;
                        Console.WriteLine("The poison faded away");
                    }
                    break;
                case 7: //SoulBind
                    if (Program.RoundCounter < OP.Statuses[statNum].Counter + 4)
                    {
                        OP.StsFlag = true;
                        OP.HPStsDmgMod = (decimal).15;
                        OP.Statuses[statNum].Flag = true;
                        OP.Statuses[statNum].Message = $"{OP.Name} is suffering under the soul binding";
                    }
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 4)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.HPStsDmgMod = 0;
                        Console.WriteLine($"{OP.Name} was released from the soul binding!");
                    }
                    break;
                case 8: //Stun
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 1)
                    {
                        OP.ActiveFlag = false;
                        OP.ActiveMessage = $"{OP.Name} hesitated to act";
                    }
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 2)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.ActiveFlag = true;
                        Console.WriteLine($"{OP.Name} came back to their senses");
                    }
                    break;
                case 9: //DizzyEffect
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 1)
                    {
                        OP.ActiveFlag = true;
                        OP.Statuses[statNum].Flag = false;
                        Console.WriteLine($"{OP.Name} recovered from stumbling");
                    }
                    break;
                case 10: //Dizzy
                    if (Program.RoundCounter < OP.Statuses[statNum].Counter + 4)
                    {
                        Random dizRand = new Random(); //seed to 1 to test true
                        if (dizRand.Next(1, 3) == 1)
                        {
                            OP.ActiveFlag = false;
                            OP.Statuses[statNum - 1].Flag = true;
                            OP.Statuses[statNum - 1].Counter = Program.RoundCounter;
                            OP.ActiveMessage = $"{OP.Name} stumbled from dizziness!";
                        }
                    }
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 4)
                    {
                        OP.Statuses[statNum].Flag = false;
                        Console.WriteLine($"{OP.Name} has regained their senses");
                    }
                    break;
                case 11: //MightyBlow
                    //if (Program.RoundCounter == OP.Statuses[statNum].Counter + 1)
                    //{
                    //    Console.WriteLine($"{OP.Name} notices {PL.Name} dropped their guard!");
                    //    Console.WriteLine($"{OP.Name} lets loose a mighty swing with their sword!");
                    //    var Dmg = Program.DmgCalcOP(OP, PL);
                    //    Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                    //    OP.HP -= Dmg;
                    //    Console.WriteLine($"{PL.Name}'s remaining HP: {OP.HP}");
                    //}
                    //if (Program.RoundCounter == OP.Statuses[statNum].Counter + 2)
                    //{
                    //    OP.AtkVal = 2;
                    //    OP.Statuses[statNum].Flag = false;
                    //    OP.ActiveFlag = true;
                    //}
                    break;
                case 12: //Freeze
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 1)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.ActiveFlag = true;
                        Console.WriteLine($"{OP.Name} thawed out");
                    }
                    break;
                case 13: //Petrify
                    if (Program.RoundCounter == OP.Statuses[statNum].Counter + 3)
                    {
                        OP.Statuses[statNum].Flag = false;
                        OP.ActiveFlag = true;
                        Console.WriteLine($"{OP.Name} broke free of the stone!");
                    }
                    break;
                case 14: //Vanquish
                    //if (Program.RoundCounter < OP.Statuses[statNum].Counter + 4)
                    //{
                    //    OP.ActiveFlag = false;
                    //    OP.ActiveMessage = $"{OP.Name} is concentrating intently whilst praying";
                    //}
                    //if (Program.RoundCounter == OP.Statuses[statNum].Counter + 4 && OP.Undead)
                    //{
                    //    Console.WriteLine($"A light appears above {OP.Name}, it quickly intensifies");
                    //    Console.WriteLine($"The light coalesces into a single point above {OP.Name}");
                    //    Console.WriteLine($"It suddenly stabs downward, piercing {OP.Name}!");
                    //    var Dmg = OP.HP;
                    //    OP.HP = 0;
                    //    Console.WriteLine($"{OP.Name} took {Dmg} damage! HP Remaining: {OP.HP}");
                    //    Console.WriteLine($"{OP.Name} was turned to dust!");
                    //    OP.ActiveFlag = false;
                    //    OP.ActiveMessage = $"{OP.Name} sighs in relief and relaxes";
                    //}
                    //if (Program.RoundCounter == OP.Statuses[statNum].Counter + 4 && OP.Undead == false)
                    //{
                    //    Console.WriteLine($"A light appears above {OP.Name}, it quickly intensifies");
                    //    Console.WriteLine($"The light coalesces into a single point above {OP.Name}");
                    //    Console.WriteLine("The light softly disperses into the air without taking effect");
                    //    Console.WriteLine($"{OP.Name} opens their eyes to see {OP.Name} still standing before them");
                    //    OP.Statuses[statNum].Flag = false;
                    //    OP.ActiveFlag = true;
                    //}
                    break;
            }

        }

    }
}
    


