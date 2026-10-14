using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class Monster : Person
    {
        public int damage { get; set; }
        public int armor { get; set; }
        public int experienceReward { get; set; }
        public int remuneration { get; set; }
        public Monster(string name, int level) : base(name, level)
        {
            this.hp = this.hpMax = level * 500;
            setEnergy(level * 100, level * 100);
            this.damage = level * 50;
            this.armor = level * 10;
            this.experienceReward = level * 100;
            this.remuneration = level * 5;
        }
        public int getExpReward() => this.experienceReward;

        public int getDamage() { return damage; }

        public int getArmor() { return armor; }


    }
}
