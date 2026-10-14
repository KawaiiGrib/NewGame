using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Armor : Item
    {
        string armorName = "";
        public int Protection { get; set; }
        public int BonusHp { get; set; }
        public Armor(int id, string name, int price, int protection, int bonusHp = 0)
            : base(id, name, price)
        {
            this.armorName = name;
            this.Protection = protection;
            this.BonusHp = bonusHp;
        }

        public Armor(int protection, string armorName)
            : base(0, armorName, 0)
        {
            this.Protection = protection;
            this.armorName = armorName;
        }

        public void setprotection(int protection) => this.Protection = protection;
        public int getprotection() => this.Protection;

        public override string Use(Player player)
        {
            player.setArmor(this);
            player.ChangeMaxStats(BonusHp, 0);
            return $"{player.getName()} одягнув броню: {armorName}";
        }

        public override string ToString()
        {
            return $"[Броня] #{Id} {armorName} | захист {Protection} | {Price} монет";
        }
    }
}
