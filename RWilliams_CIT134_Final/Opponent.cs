using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWilliams_CIT134_Final
{
    class Fiend
    {
        public string Name { get; set; }
        private int _HP;
        public int HP
        {
            get
            {
                if (_HP < 0)
                {
                    return 0;
                }
                else
                {
                    return _HP;
                }
            }
            set { _HP = value; }
        }
        public decimal AtkVal { get; set; }
        public decimal DefVal { get; set; }
        public bool StsFlag { get; set; }
        public decimal AtkStsMod { get; set; }
        public decimal DefStsMod { get; set; }
        public decimal HPStsDmgMod { get; set; }
        public bool ActiveFlag { get; set; }
        public int ClassDesignation { get; set; }
        public bool Undead { get; set; }
        public string ActiveMessage { get; set; }
        public int DefCounter;
        public bool DefendFlag;
        public OPStatus[] Statuses = new OPStatus[15];

        public Fiend () //basic constructor
        {
            StsFlag = false;
            AtkStsMod =  1;
            DefStsMod =  1;
            HPStsDmgMod = 0;
            ActiveFlag = true;
            var might = "Might";
            var weak = "Weak";
            var pen = "Penance";
            var bar = "Barrier";
            var drn = "Drain";
            var brn = "Burn";
            var pos = "Poison";
            var sb = "Soul Bind";
            var stn = "Stun";
            var dzef = "Dizzy Effect";
            var diz = "Dizzy";            
            var mb = "Mighty Blow";
            var frz = "Freeze";
            var pet = "Petrify";
            var vnq = "Vanquish";
            OPStatus Might = new OPStatus(might);
            OPStatus Weak = new OPStatus(weak);
            OPStatus Penance = new OPStatus(pen);
            OPStatus Barrier = new OPStatus(bar);
            OPStatus Drain = new OPStatus(drn);
            OPStatus Burn = new OPStatus(brn);
            OPStatus Poison = new OPStatus(pos);
            OPStatus SoulBind = new OPStatus(sb);
            OPStatus Stun = new OPStatus(stn);
            OPStatus DizzyEF = new OPStatus(dzef);
            OPStatus Dizzy = new OPStatus(diz);
            OPStatus MightyBlow = new OPStatus(mb);
            OPStatus Freeze = new OPStatus(frz);
            OPStatus Petrify = new OPStatus(pet);
            OPStatus Vanquish = new OPStatus(vnq);
            Statuses[0] = Might; Statuses[1] = Weak; Statuses[2] = Penance; Statuses[3] = Barrier; Statuses[4] = Drain;
            Statuses[5] = Burn; Statuses[6] = Poison; Statuses[7] = SoulBind; Statuses[8] = Stun; Statuses[9] = DizzyEF;
            Statuses[10] = Dizzy; Statuses[11] = MightyBlow; Statuses[12] = Freeze; Statuses[13] = Petrify;
            Statuses[14] = Vanquish;
        }
        public void Defend()
        {
            this.DefendFlag = true;
            DefCounter = Program.RoundCounter;
            this.DefVal *= (decimal)1.5;
        }
        public void StsCheck(Opponent OP)
        {
            if (this.DefendFlag)
            {
                this.DefendExhaust(this.ClassDesignation);
            }
            if (this.StsFlag)
            {
                this.StatusExhaust(OP);
            }
        }

        public void StatusExhaust(Opponent OP)
        {
            for (int x = 0; x < OP.Statuses.Length; x++)
            {
                if (OP.Statuses[x].Flag == true)
                {
                    if (!string.IsNullOrEmpty(OP.Statuses[x].Message))
                    {
                        Console.WriteLine(OP.Statuses[x].Message);
                    }
                        OP.Statuses[x].Exhaust(OP, x);                   
                }
            }
        }

        public void DefendExhaust(int classD)
        {
            if (DefCounter != Program.RoundCounter)
            {
                this.DefendFlag = false;
                switch (classD)
                {
                    case 1:
                        this.DefVal = 70;
                        break;
                    case 2:
                        this.DefVal = 50;
                        break;
                    case 3:
                        this.DefVal =80;
                        break;
                }
            }
        }

        public static int DetOp(Opponent OP)
        {
            switch(OP.ClassDesignation)
            {
                case 1:
                    return Opponent.VampDec();
                case 2:
                    return Opponent.GorgDec();
                case 3:
                    return Opponent.WispDec();
                default:
                    return 1;
            }
        }
    }

    class Opponent : Fiend  //Opponent inherits from fiend)
    {
        public Opponent(int classD)
        {
            switch (classD)
            { 
                case 1:
                    Name = "Vamp";
                    HP = 18000;
                    AtkVal = (decimal).6;
                    DefVal = 63;
                    Undead = true;
                    ClassDesignation = 1;
                    break;
                case 2:
                    Name = "Gorgon";
                    HP = 17500;
                    AtkVal = (decimal).8;
                    DefVal = 57;
                    Undead = false;
                    ClassDesignation = 2;
                    break;
                case 3:
                    Name = "Wisp";
                    HP = 15000;
                    AtkVal = (decimal).4;
                    DefVal = 70;
                    Undead = true;
                    ClassDesignation = 3;
                    break;
            }
        
        }

        public static int VampDec()
        {
            var Dec = Program.OutsideRand.Next(1, 6);
            switch (Dec)
            {
                case 1:
                    return 1;
                case 2:
                    Program.OPSub = 1;
                    return 2;
                case 3:
                    Program.OPSub = 2;
                    return 2;
                case 4:
                    Program.OPSub = 3;
                    return 2;
                case 5:
                    return 3;
                default:
                    return 1;
            }
        }

        public static int GorgDec()
        {
            var Dec = Program.OutsideRand.Next(1, 6);
            switch (Dec)
            {
                case 1:
                    return 1;
                case 2:
                    Program.OPSub = 1;
                    return 2;
                case 3:
                    Program.OPSub = 2;
                    return 2;
                case 4:
                    Program.OPSub = 3;
                    return 2;
                case 5:
                    return 3;
                default:
                    return 1;
            }
        }

        public static int WispDec()
        {
            var Dec = Program.OutsideRand.Next(1, 7);
            switch (Dec)
            {
                case 1:
                    return 1;
                case 2:
                    Program.OPSub = 1;
                    return 2;
                case 3:
                    Program.OPSub = 2;
                    return 2;
                case 4:
                    Program.OPSub = 3;
                    return 2;
                case 5:
                    Program.OPSub = 4;
                    return 2;
                case 6:
                    return 3;
                default:
                    return 1;
            }
        }

        public static void VampAct(int OPDec, int OPSub, Opponent OP, Player PL)
        {
            switch (OPDec)
            {
                case 1:
                    Console.WriteLine($"{OP.Name} swiped at {PL.Name} with their claws!");
                    var Dmg = Program.DmgCalcOP(OP, PL);
                    Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                    PL.HP -= Dmg;
                    Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                    break;
                case 2:
                    switch (OPSub)
                    {
                        case 1: //Withering Gaze
                            Console.WriteLine($"{OP.Name} hisses and bares their fangs at {PL.Name}");
                            Console.WriteLine($"{PL.Name} is frightened!");
                            PL.StsFlag = true;
                            PL.DefStsMod = (decimal).5;
                            PL.Statuses[1].Flag = true;
                            PL.Statuses[1].Counter = Program.RoundCounter;
                            PL.Statuses[1].Message = $"{PL.Name} is shaking with fear!";
                            break;
                        case 2: //Drain
                            OP.AtkStsMod = (decimal)1.5;
                            Console.WriteLine($"{OP.Name} swiped at {PL.Name} with their claws!");
                            Dmg = Program.DmgCalcOP(OP, PL);
                            Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                            PL.HP -= Dmg;
                            Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                            OP.AtkStsMod = 1;
                            PL.StsFlag = true;
                            PL.Statuses[4].Flag = true;
                            PL.Statuses[4].Counter = Program.RoundCounter;
                            PL.Statuses[4].Message = $"{PL.Name} is bleeding from a wound!";
                            PL.HPStsDmgMod = (decimal).1;
                            Console.WriteLine($"{OP.Name} inflicted a bleeding wound on {PL.Name}!");
                            Console.WriteLine($"{OP.Name} latched onto the wound and took a deep drink!");
                            var healing = PL.HP * PL.HPStsDmgMod;
                            OP.HP += (int)healing;
                            Console.WriteLine($"{OP.Name} regained {(int)healing} HP!");
                            Console.WriteLine($"{OP.Name} HP: {OP.HP}");
                            break;
                        case 3: //Stun
                            OP.AtkStsMod = (decimal)2;
                            Console.WriteLine($"{OP.Name} swiped at {PL.Name} with their claws!");
                            Dmg = Program.DmgCalcOP(OP, PL);
                            Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                            PL.HP -= Dmg;
                            Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                            OP.AtkStsMod = 1;
                            Random randSt = new Random(); //seed 1 to test true
                            if (randSt.Next(1, 3) == 1)
                            {
                                PL.StsFlag = true;
                                PL.Statuses[8].Flag = true;
                                PL.Statuses[8].Counter = Program.RoundCounter;
                                Console.WriteLine($"{PL.Name} is reeling from that blow!");
                            }
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine($"{OP.Name} raised their cape in a defensive posture!");
                    OP.Defend();
                    break;
            }
        }
        public static void GorgAct(int OPDec, int OPSub, Opponent OP, Player PL)
        {
            switch (OPDec)
            {
                case 1:
                    Console.WriteLine($"{OP.Name} swiped at {PL.Name} with their claws!");
                    var Dmg = Program.DmgCalcOP(OP, PL);
                    Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                    PL.HP -= Dmg;
                    Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                    break;
                case 2:
                    switch (OPSub)
                    {
                        case 1: //Petrify
                            Console.WriteLine($"{OP.Name} tries to lock eyes with {PL.Name}!");
                            Random randPet = new Random();
                            if (randPet.Next(1,9) == 1)
                            {
                                Console.WriteLine($"{PL.Name} was caught by {OP.Name}'s gaze!");
                                PL.StsFlag = true;
                                PL.Statuses[13].Flag = true;
                                PL.Statuses[13].Counter = Program.RoundCounter;
                                PL.ActiveFlag = false;
                                PL.ActiveMessage = $"{PL.Name} is encased in stone!";
                                Console.WriteLine($"{PL.Name} was turned to stone!");
                            }
                            else
                            {
                                Console.WriteLine($"{PL.Name} managed to look away!");
                            }
                            break;
                        case 2: //Dizzy
                            Console.WriteLine($"{OP.Name} whips at {PL.Name} with its tail!");
                            Dmg = Program.DmgCalcOP(OP, PL);
                            Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                            PL.HP -= Dmg;
                            Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                            Random randDz = new Random(); //seed 1 to test true
                            if (randDz.Next(1, 4) == 1)
                            {
                                PL.StsFlag = true;
                                PL.Statuses[10].Flag = true;
                                PL.Statuses[10].Counter = Program.RoundCounter;
                                PL.Statuses[10].Message = $"{PL.Name} looks unsteady...";
                                Console.WriteLine($"{PL.Name} was struck senseless!");
                            }
                            break;
                        case 3: //Rage
                            OP.AtkStsMod = (decimal).8;
                            Console.WriteLine($"{OP.Name} lets out a furious roar, and begins to randomly lash out at {PL.Name}!");
                            Console.WriteLine($"{OP.Name} swiped at {PL.Name} with their claws!");
                            Dmg = Program.DmgCalcOP(OP, PL);
                            Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                            PL.HP -= Dmg;
                            Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                            Console.WriteLine($"{OP.Name} swiped at {PL.Name} with their claws!");
                            Dmg = Program.DmgCalcOP(OP, PL);
                            Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                            PL.HP -= Dmg;
                            Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                            Console.WriteLine($"{OP.Name} swiped at {PL.Name} with their claws!");
                            Dmg = Program.DmgCalcOP(OP, PL);
                            Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                            PL.HP -= Dmg;
                            Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                            OP.AtkStsMod = 1;
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine($"{OP.Name} raised their arms in a defensive posture!");
                    OP.Defend();
                    break;
            }
        }
        public static void WispAct(int OPDec, int OPSub, Opponent OP, Player PL)
        {
            switch (OPDec)
            {
                case 1:
                    Console.WriteLine($"{OP.Name} lashed out at {PL.Name}!");
                    var Dmg = Program.DmgCalcOP(OP, PL);
                    Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                    PL.HP -= Dmg;
                    Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                    break;
                case 2: //Shriek
                    switch (OPSub)
                    {
                        case 1:
                            Console.WriteLine($"{OP.Name} lets out a terrifying shriek!");
                            Random randDiz = new Random();
                            if (randDiz.Next(1, 4) == 1)
                            {
                                PL.StsFlag = true;
                                PL.Statuses[9].Flag = true;
                                PL.Statuses[9].Counter = Program.RoundCounter;
                                PL.Statuses[9].Message = $"{PL.Name} looks unsteady...";
                                Console.WriteLine($"{PL.Name} was scared senseless!");
                            }
                            Random randW = new Random();
                            if (randW.Next(1,3) == 1)
                            {
                                PL.StsFlag = true;
                                PL.DefStsMod = (decimal).5;
                                PL.Statuses[1].Flag = true;
                                PL.Statuses[1].Counter = Program.RoundCounter;
                                PL.Statuses[1].Message = $"{PL.Name} is shaking with fear!";
                                Console.WriteLine($"{PL.Name} was scared so bad they lost confidence!");
                            }
                            break;
                        case 2: //Burn
                            Console.WriteLine($"{OP.Name}'s flames turn a bright white as it surges at {PL.Name}!");
                            Dmg = Program.DmgCalcOP(OP, PL);
                            Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                            PL.HP -= Dmg;
                            Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                            Random randB = new Random();
                            if (randB.Next(1, 4) == 1) //seed 1 to test true
                            {
                                PL.StsFlag = true;
                                PL.Statuses[5].Flag = true;
                                PL.HPStsDmgMod = (decimal).07;
                                PL.Statuses[5].Counter = Program.RoundCounter;
                                PL.Statuses[5].Message = $"{PL.Name} is suffering from a burn!";
                                Console.WriteLine($"{PL.Name} was burned!");
                            }
                            break;
                        case 3: //Freeze
                            Console.WriteLine($"{OP.Name}'s flames turn a pale blue and {OP.Name} surrounds {PL.Name}!");
                            Dmg = Program.DmgCalcOP(OP, PL);
                            Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                            PL.HP -= Dmg;
                            Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                            Random randF = new Random();
                            if (randF.Next(1, 3) == 1) //seed 1 to test true
                            {
                                PL.StsFlag = true;
                                PL.ActiveFlag = false;
                                PL.ActiveMessage = $"{PL.Name} is encased in ice!";
                                PL.Statuses[12].Flag = true;
                                PL.Statuses[12].Counter = Program.RoundCounter;
                                PL.Statuses[12].Message = $"{PL.Name} is frozen solid!";
                                Console.WriteLine($"{PL.Name} was flash frozen!");

                            }
                            break;
                        case 4: //Stone
                            Console.WriteLine($"{OP.Name}'s flames turn dark grey and {OP.Name} lunges at {PL.Name}!");
                            Dmg = Program.DmgCalcOP(OP, PL);
                            Console.WriteLine($"{OP.Name} did {Dmg} damage to {PL.Name}!");
                            PL.HP -= Dmg;
                            Console.WriteLine($"{PL.Name}'s remaining HP: {PL.HP}");
                            Random randPet = new Random();
                            if (randPet.Next(1, 9) == 1)
                            {
                                Console.WriteLine($"{PL.Name} was caught by {OP.Name}!");
                                Console.WriteLine($"{PL.Name} feels their skin hardening up!");
                                PL.StsFlag = true;
                                PL.Statuses[13].Flag = true;
                                PL.Statuses[13].Counter = Program.RoundCounter;
                                PL.ActiveFlag = false;
                                PL.ActiveMessage = $"{PL.Name} is encased in stone!";
                                Console.WriteLine($"{PL.Name} was turned to stone!");
                            }
                            else
                            {
                                Console.WriteLine($"{PL.Name} coughs up some dust, but is otherwise fine");
                            }
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine($"{OP.Name} gathers itself into a defensive ball!");
                    OP.Defend();
                    break;
            }
        }
    }
}
