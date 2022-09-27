using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWilliams_CIT134_Final
{
    class Player
    {
        public string Name { get; }
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
            set //sets hard ceiling on HP dependant on class. Looks messy, but it works.
            { 
                switch (ClassDesignation)
                {
                    case 1:
                        if (value > Cleric.HP)
                        {
                            _HP = Cleric.HP;
                        }
                        else
                        {
                            _HP = value;
                        }
                        break;
                    case 2:
                        if (value > Fighter.HP)
                        {
                            _HP = Fighter.HP;
                        }
                        else
                        {
                            _HP = value;
                        }
                        break;
                    case 3:
                        if (value > Mage.HP)
                        {
                            _HP = Mage.HP;
                        }
                        else
                        {
                            _HP = value;
                        }
                        break;
                    case 4:
                        if (value > Ninja.HP)
                        {
                            _HP = Ninja.HP;
                        }
                        else
                        {
                            _HP = value;
                        }
                        break;
                }               
            }
        }
        private decimal _AtkVal;
        private decimal _DefVal;
        public decimal AtkVal { get => _AtkVal; set { _AtkVal = value; } }
        public decimal DefVal { get => _DefVal; set { _DefVal = value; } }
        public bool StsFlag { get; set; }
        public decimal AtkStsMod { get; set; }
        public decimal DefStsMod { get; set; }
        public decimal HPStsDmgMod { get; set; }
        public bool ActiveFlag { get; set; }
        private int _ClassDesignation;
        public int ClassDesignation { get => _ClassDesignation; set { _ClassDesignation = value; } }
        public string ActiveMessage { get; set; }
        public int DefCounter;
        public bool DefendFlag;
        public PLStatus[] Statuses = new PLStatus[15];

        public Player()
        {

        }

        public Player (string pname)
        {
            Name = pname;
            StsFlag = false;
            AtkStsMod = 1;
            DefStsMod = 1;
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
            PLStatus Might = new PLStatus(might);
            PLStatus Weak = new PLStatus(weak);
            PLStatus Penance = new PLStatus(pen);
            PLStatus Barrier = new PLStatus(bar);
            PLStatus Drain = new PLStatus(drn);
            PLStatus Burn = new PLStatus(brn);
            PLStatus Poison = new PLStatus(pos);
            PLStatus SoulBind = new PLStatus(sb);
            PLStatus Stun = new PLStatus(stn);
            PLStatus DizzyEF = new PLStatus(dzef);
            PLStatus Dizzy = new PLStatus(diz);
            PLStatus MightyBlow = new PLStatus(mb);
            PLStatus Freeze = new PLStatus(frz);
            PLStatus Petrify = new PLStatus(pet);
            PLStatus Vanquish = new PLStatus(vnq);
            Statuses[0] = Might; Statuses[1] = Weak; Statuses[2] = Penance; Statuses[3] = Barrier; Statuses[4] = Drain;
            Statuses[5] = Burn; Statuses[6] = Poison; Statuses[7] = SoulBind; Statuses[8] = Stun; Statuses[9] = DizzyEF;
            Statuses[10] = Dizzy; Statuses[11] = MightyBlow; Statuses[12] = Freeze; Statuses[13] = Petrify;
            Statuses[14] = Vanquish;
        }

        public void SetPlayer (Cleric cleric)
        {
            SetClass(1);
            HP = Cleric.HP;
            SetAtk(cleric.Atk);
            SetDef(cleric.Def);
        }

        public void SetPlayer (Fighter fighter)
        {
            SetClass(2);
            HP = Fighter.HP;
            SetAtk(fighter.Atk);
            SetDef(fighter.Def);
        }

        public void SetPlayer(Mage mage)
        {
            SetClass(3);
            HP = Mage.HP;
            SetAtk(mage.Atk);
            SetDef(mage.Def);
        }

        public void SetPlayer (Ninja ninja)
        {
            SetClass(4);
            HP = Ninja.HP;
            SetAtk(ninja.Atk);
            SetDef(ninja.Def);
        }

        public decimal GetAtk()
        {
            return AtkVal;
        }
        public void SetAtk(decimal atk)
        {
            AtkVal = atk;
        }
        public decimal GetDef()
        {
            return DefVal;
        }
        public void SetDef(decimal def)
        {
            DefVal = def;
        }
        public int GetClassD()
        {
            return ClassDesignation;
        }
        public void SetClass(int desig)
        {
            ClassDesignation = desig;
        }
        public void Defend()
        {
            this.DefendFlag = true;
            DefCounter = Program.RoundCounter;
            this.SetDef(GetDef()*(decimal)1.5);
        }
        public void StsCheck(Player PL, Opponent OP)
        {
            if (this.DefendFlag)
            {
                this.DefendExhaust(this.ClassDesignation);
            }
            if (this.StsFlag)
            {
                this.StatusExhaust(PL, OP);
            }
        }

        public void StatusExhaust(Player PL, Opponent OP)
        {
            for (int x = 0; x < PL.Statuses.Length; x++)
            {
                if (PL.Statuses[x].Flag == true)
                {
                    if (!string.IsNullOrEmpty(PL.Statuses[x].Message))
                    {
                        Console.WriteLine(PL.Statuses[x].Message);
                    }
                    PL.Statuses[x].Exhaust(PL, OP, x);
                }
            }
        }

        public void DefendExhaust(int classD)
        {
            if ( DefCounter != Program.RoundCounter)
            {
                this.DefendFlag = false;
                switch (classD)
                {
                    case 1:
                        this.SetDef(80);
                        break;
                    case 2:
                        this.SetDef(70);
                        break;
                    case 3:
                        this.SetDef(50);
                        break;
                    case 4:
                        this.SetDef(60);
                        break;
                }
            }
        }
    }
}
