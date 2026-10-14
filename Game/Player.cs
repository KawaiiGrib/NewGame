using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public abstract class Player : Person
    {
        public int str = 0;
        protected int dex = 0;
        protected int end = 0;
        protected int exp = 0;
        protected int attack = 0;
        protected int protection = 0;
        protected Random random = null;
        protected List<IUseSkill> skills = null;

        Weapon weapon = null;
        Armor armor = null;
        public Inventory Inventory { get; private set; }
        public Armor EquippedArmor { get; private set; }

        public Player(Random random, string name) : base(name, 1)
        {
            this.random = random;
            this.skills = new List<IUseSkill>();
            this.ep = this.enMax = 100;

            this.Inventory = new Inventory();
        }
        public int GetEn()
        {
            return energy;
        }
        public void changeEn(int value)
        {
            if (value + energy > this.enMax)
            {
                energy = this.enMax;
            }
            else if (value + energy < 0)
            {
                energy = 0;
            }
            else
            {
                energy = value + energy;
            }
        }
        public void checkLevelUp()
        {
            int expForNextLevel = this.level * 100;

            while (this.exp >= expForNextLevel)
            {
                this.exp -= expForNextLevel;
                this.level++;

                // Увеличивает характеристики при повышении уровня
                this.str += 3;
                this.dex += 2;
                this.end += 2;

                // Восстанавливает HP и EP
                setBaseHp();
                this.ep = this.enMax;

                Console.WriteLine($"🎉 Вітаємо! Ви досягли {this.level} рівня!");
                Console.WriteLine($"Сила +3, Спритність +2, Витривалість +2");
                Console.WriteLine($"HP и EP повністю відновлені!");
                Console.ReadLine();
            }
        }
        public int GetenMax()
        {
            return enMax;
        }

        public void setWeapon(Weapon weapon) => this.weapon = weapon;
        public Weapon getWeapon() => this.weapon;

        public void setArmor(Armor armor) => this.armor = armor;
        public Armor getArmor() => this.armor;

        public int Damage()
        {
            if (this.weapon == null)
                this.attack = this.str;
            else
                this.attack = this.weapon.getharm() + (this.str * 3);

            return this.attack;
        }

        public int Defense()
        {
            if (this.armor == null)
                this.protection = this.end;
            else
                this.protection = this.armor.getprotection() + this.end;

            return protection;
        }

        public int CalculateDamage()
        {
            attack = this.Damage();

            if (dex > 10 && this.random.Next(1, 101) <= dex)
            {
                Console.WriteLine("Критичний удар!");
                attack *= 2;
            }

            return attack;
        }

        protected void setBaseHp()
        {
            this.hp = this.hpMax = 50 * this.end;
        }


        public bool addedSkill(IUseSkill skill)
        {
            foreach (var tmpSkill in this.skills)
            {
                if (tmpSkill.GetType().Name == skill.GetType().Name)
                {
                    throw new Exception("Дублікат вміння!");
                }
            }

            this.skills.Add(skill);
            return true;
        }

        public void showSkills()
        {
            for (int i = 0; i < this.skills.Count; i++)
            {
                int cost = 0;

                // Определяем стоимость скилла
                if (this.skills[i] is Skill skill)
                {
                    cost = skill.getEpCost();
                }

                Console.WriteLine($"{i + 1} - {this.skills[i].getName()} (EP: {cost})");
            }

        }

        public UseSkillResultData useSkill(int index)
        {
            index--;

            if (index < 0 || index >= this.skills.Count)
                throw new Exception("Такого індекса не існує!");


            UseSkillResultData data = this.skills[index].UseSkill(this);

            if (!data.IsSuccses())
                Console.WriteLine("Нема єнергії!");

            return data;
        }

        public void addExp(int exp) => this.exp += exp;

        public void ChangeMaxStats(int bonusHp, int bonusEnergy)
        {
            if (bonusHp != 0)
            {
                this.hpMax += bonusHp;
                this.hp += bonusHp;
            }

            if (bonusEnergy != 0)
            {
                this.enMax += bonusEnergy;
                this.ep += bonusEnergy;
            }
        }
    }
    class Barbarian : Player
    {
        public Barbarian(Random random, string name) : base(random, name)
        {
            this.str = 30;
            this.dex = 15;
            this.end = 15;
            this.setWeapon(new Weapon(4, "Сокира"));

            setBaseHp();
            Inventory.Add(new Weapon(0, "Бойова сокира", 100, damage: 15, bonusHp: 10));
            Inventory.Add(new Item(0, "Зілля здоров'я", 50));
        }
    }
    class Tank : Player
    {
        public Tank(Random random, string name) : base(random, name)
        {
            this.str = 15;
            this.dex = 15;
            this.end = 30;
            this.setWeapon(new Weapon(3, "Меч"));

            setBaseHp();
            Inventory.Add(new Weapon(0, "Танковий меч", 100, damage: 10, bonusEnergy: 20));
            Inventory.Add(new Armor(0, "Шкіряна броня", 80, protection: 5));
            Inventory.Add(new Item(0, "Зілля здоров'я", 50));
        }
    }
    class Robber : Player
    {
        public Robber(Random random, string name) : base(random, name)
        {
            this.str = 15;
            this.dex = 30;
            this.end = 15;
            this.setWeapon(new Weapon(5, "Кінжал"));

            setBaseHp();
            Inventory.Add(new Weapon(0, "Гострий кинджал", 90, damage: 12, bonusEnergy: 15));
            Inventory.Add(new Item(0, "Зілля здоров'я", 50));
        }
    }
    enum PlayerType // набір іменованних констант
    {
        Barbarian = 1,
        Tank,
        Robber
    }
}
