using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Person : NameTrait
    {
        static private int autoInc = 1;
        protected int id;

        private void generateId()
        {
            this.id = autoInc++;
        }

        protected int level = 0;
        protected int hp = 0;
        protected int ep = 0;
        protected int hpMax = 0;
        public int energy = 500;
        public int enMax = 500;
        public int getEp() => this.ep;
        public void increaseEp(int ep)
        {
            if (this.ep + ep > this.enMax)
            {
                this.ep = this.enMax;
            }
            else
            {
                this.ep += ep;
            }
        }

        public bool decreaseEp(int ep)
        {
            if (this.ep - ep >= 0)
            {
                this.ep -= ep;
                return true;
            }

            return false;
        }



        public int getHp()
        {
            return hp;
        }

        public int getLevel()
        {
            return level;
        }

        public int damageHp(int damage)
        {
            if (hp - damage < 0)
                hp = 0;
            else
                hp -= damage;

            return hp;
        }

        public int rescueHp(int value)
        {
            if (hp + value > hpMax)
                hp = hpMax;
            else
                hp += value;

            return hp;
        }
        public void setEnergy(int ep, int enMax)
        {
            this.ep = ep;
            this.enMax = enMax;
        }
        protected void showHealthBar()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("-= ========== =-");
            Console.WriteLine($"-= {this.name} lvl.{this.level} =-");
            Console.WriteLine("-= ========== =-");
            Console.Write("-=[");

            int count = 0;

            if (hp != 0)
                count = hp / (hpMax / 10);

            if (hp > 0 && count == 0)
                count = 1;

            if (count <= 3)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (count > 3 && count <= 6)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 1; i <= 10; i++)
            {
                if (i <= count)
                    Console.Write("#");
                else
                    Console.Write(" ");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("]=-\n");

            Console.WriteLine($"-= [{this.hp}/{this.hpMax}] =-");
            Console.Write("-=[");

            int count1 = 0;

            if (ep != 0)
                count1 = ep / (enMax / 10);

            if (ep > 0 && count1 == 0)
                count1 = 1;

            if (count1 <= 3)
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            else if (count1 > 3 && count1 <= 6)
                Console.ForegroundColor = ConsoleColor.Blue;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;

            for (int i = 1; i <= 10; i++)
            {
                if (i <= count1)
                    Console.Write("#");
                else
                    Console.Write(" ");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("]=-\n");

            Console.WriteLine($"-= [{this.ep}/{this.enMax}] =-");
            Console.WriteLine("-= ========== =-");

            Console.ResetColor();
        }

        public string name { get; set; }

        public Person(string name, int level)
        {
            generateId();
            this.name = name;
            this.level = level;
        }
        public void showInfo()
        {
            this.showHealthBar();
        }
    }
    public class NameTrait
    {
        protected string name;

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }
    }
}
