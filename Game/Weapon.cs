using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Weapon : Item
    {
        public string weaponName = "";
        public int harm { get; set; }
        public int BonusHp { get; set; }
        public int BonusEnergy { get; set; }
        public Weapon(int id, string name, int price, int damage,
                      int bonusHp = 0, int bonusEnergy = 0)
            : base(id, name, price)
        {
            this.weaponName = name;
            this.harm = damage;
            this.BonusHp = bonusHp;
            this.BonusEnergy = bonusEnergy;
        }

        public Weapon(int harm, string weaponName)
            : base(0, weaponName, 0)
        {
            this.harm = harm;
            this.weaponName = weaponName;
        }

        public void setharm(int harm) => this.harm = harm;
        public int getharm() => this.harm;

        public override string Use(Player player)
        {
            player.setWeapon(this);
            player.ChangeMaxStats(BonusHp, BonusEnergy);
            return $"{player.getName()} озброївся: {weaponName}";
        }

        public override string ToString()
        {
            return $"[Зброя] #{Id} {weaponName} | урон {harm} | {Price} монет";
        }
    }
}
