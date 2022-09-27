using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWilliams_CIT134_Final
{
    class PLStatus
    {
        public bool Flag;
        public int Counter;
        public string Message;
        public string Name;

        public PLStatus(string name)
        {
            Name = name;
        }

        public void Exhaust(Player PL, Opponent OP, int statNum)
        {
            switch (statNum)
            {
                case 0:  //Might
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 3)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.AtkStsMod = 1;
                        Console.WriteLine($"{PL.Name}'s strength faded");
                    }
                    break;
                case 1: //Weak
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 3)
                    {
                        PL.DefStsMod = 1;
                        PL.Statuses[statNum].Flag = false;
                        Console.WriteLine($"{PL.Name} has regained their confidence");
                    }
                    break;
                case 2: //Penance
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 4)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.DefStsMod = 1;
                        Console.WriteLine($"The aura surrounding {PL.Name} faded away");
                    }
                    break;
                case 3: //Barrier
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 4)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.DefStsMod = 1;
                        Console.WriteLine($"The barrier surrounding {PL.Name} faded away");
                    }
                    break;
                case 4: //Drain
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 1)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.HPStsDmgMod = 0;
                        Console.WriteLine($"{PL.Name} staunched the bleeding");
                    }
                    break;
                case 5: //Burn
                    if (Program.RoundCounter < PL.Statuses[statNum].Counter +4)
                    {
                        PL.StsFlag = true;
                        PL.HPStsDmgMod = (decimal).05;
                        PL.Statuses[statNum].Flag = true;
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 4)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.HPStsDmgMod = 0;
                        Console.WriteLine("The burning has faded");
                    }
                    break;
                case 6: //Poison
                    if (Program.RoundCounter < PL.Statuses[statNum].Counter + 4)
                    {
                        PL.StsFlag = true;
                        PL.HPStsDmgMod = (decimal).08;
                        PL.Statuses[statNum].Flag = true;
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 4)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.HPStsDmgMod = 0;
                        Console.WriteLine("The poison faded away");
                    }
                    break;
                case 7: //SoulBind
                    if (Program.RoundCounter < PL.Statuses[statNum].Counter + 4)
                    {
                        OP.StsFlag = true;
                        PL.StsFlag = true;
                        OP.HPStsDmgMod = (decimal).15;
                        PL.HPStsDmgMod = (decimal).05;
                        OP.Statuses[statNum].Flag = true;
                        PL.Statuses[statNum].Flag = true;
                        OP.Statuses[statNum].Message = $"{OP.Name} is suffering under the soul binding";
                        PL.Statuses[statNum].Message = $"{PL.Name} is tightening the soul binding";
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 4)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.HPStsDmgMod = 0;
                        Console.WriteLine($"{PL.Name} released the soul binding!");
                    }
                    break;
                case 8: //Stun
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 1)
                    {
                        PL.ActiveFlag = false;
                        PL.ActiveMessage = $"{PL.Name} hesitated to act";
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 2)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.ActiveFlag = true;
                        Console.WriteLine($"{PL.Name} came back to their senses");
                    }
                    break;
                case 9: //DizzyEffect
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 1)
                    {
                        PL.ActiveFlag = true;
                        PL.Statuses[statNum].Flag = false;
                        Console.WriteLine($"{PL.Name} recovered from stumbling");
                    }
                        break;
                case 10: //Dizzy
                    if (Program.RoundCounter < PL.Statuses[statNum].Counter + 4)
                    {
                        Random dizRand = new Random(); //seed to 1 to test true
                        if (dizRand.Next(1, 3) == 1)
                        {
                            PL.ActiveFlag = false;
                            PL.Statuses[statNum - 1].Flag = true;
                            PL.Statuses[statNum - 1].Counter = Program.RoundCounter;
                            PL.ActiveMessage = $"{PL.Name} stumbled from dizziness!";
                        }
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 4)
                    {
                        PL.Statuses[statNum].Flag = false;
                        Console.WriteLine($"{PL.Name} has regained their senses");
                    }
                    break;
                case 11: //MightyBlow
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 1)
                    {
                        Console.WriteLine($"{PL.Name} notices {OP.Name} dropped their guard!");
                        Console.WriteLine($"{PL.Name} lets loose a mighty swing with their sword!");
                        var Dmg = Program.DmgCalc(PL, OP);
                        Console.WriteLine($"{PL.Name} did {Dmg} damage to {OP.Name}!");
                        OP.HP -= Dmg;
                        Console.WriteLine($"{OP.Name}'s remaining HP: {OP.HP}");
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 2)
                    {
                        PL.SetAtk(2);
                        PL.Statuses[statNum].Flag = false;
                        PL.ActiveFlag = true;
                    }
                    break;
                case 12: //Freeze
                    if (Program.RoundCounter < PL.Statuses[statNum].Counter +2)
                    {
                        PL.StsFlag = true;
                        PL.ActiveFlag = false;
                        PL.ActiveMessage = $"{PL.Name} is encased in ice!";
                        PL.Statuses[statNum].Flag = true;
                        PL.Statuses[statNum].Message = $"{PL.Name} is frozen solid!";
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 2)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.ActiveFlag = true;
                        Console.WriteLine($"{PL.Name} thawed out");
                    }
                    break;
                case 13: //Petrify
                    if (Program.RoundCounter < PL.Statuses[statNum].Counter +3)
                    {
                        PL.StsFlag = true;
                        PL.Statuses[statNum].Flag = true;
                        PL.ActiveFlag = false;
                        PL.ActiveMessage = $"{PL.Name} is encased in stone!";
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 3)
                    {
                        PL.Statuses[statNum].Flag = false;
                        PL.ActiveFlag = true;
                        Console.WriteLine($"{PL.Name} broke free of the stone!");
                    }
                    break;
                case 14: //Vanquish
                    if (Program.RoundCounter < PL.Statuses[statNum].Counter +4)
                    {
                        PL.ActiveFlag = false;
                        PL.ActiveMessage = $"{PL.Name} is concentrating intently whilst praying";
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter +4 && OP.Undead)
                    {
                        Console.WriteLine($"A light appears above {OP.Name}, it quickly intensifies");
                        Console.WriteLine($"The light coalesces into a single point above {OP.Name}");
                        Console.WriteLine($"It suddenly stabs downward, piercing {OP.Name}!");
                        var Dmg = OP.HP;
                        OP.HP = 0;
                        Console.WriteLine($"{OP.Name} took {Dmg} damage! HP Remaining: {OP.HP}");
                        Console.WriteLine($"{OP.Name} was turned to dust!");
                        PL.ActiveFlag = false;
                        PL.ActiveMessage = $"{PL.Name} sighs in relief and relaxes";
                    }
                    if (Program.RoundCounter == PL.Statuses[statNum].Counter + 4 && OP.Undead == false)
                    {
                        Console.WriteLine($"A light appears above {OP.Name}, it quickly intensifies");
                        Console.WriteLine($"The light coalesces into a single point above {OP.Name}");
                        Console.WriteLine("The light softly disperses into the air without taking effect");
                        Console.WriteLine($"{PL.Name} opens their eyes to see {OP.Name} still standing before them");
                        PL.Statuses[statNum].Flag = false;
                        PL.ActiveFlag = true;
                    }
                    break;
            }

        }
    }
}
