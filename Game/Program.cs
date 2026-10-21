using System.Text;
using System;
using System.Collections.Generic;
using System.Collections;
using Game;

namespace Game
{
    class Engine
    {

        Random random = null;
        string[] names = { "Zombie", "Canopee", "Vino", "Lavalee", "Sakura", "Spike", "Tanos", "Krab", "Gloomen", "Nimph" };
        string[] armorNames = { "leather armor", "mail armor", "iron armor", "diamond armor", "netherite armor" };
        string[] weaponNames = { "katana", "rapier", "shotgun", "flamethrower", "rifle", "rocket launcher", "sword" };
        public Engine(Random rand)
        {
            this.random = rand;
        }
        protected void showOnePanchInfo(string name, int damage)
        {
            Console.WriteLine($" -= {name} наніс удар, кількість очок урона {damage} =- ");
        }
        private int prepareStrike(int damage, int defence)
        {
            damage = damage - defence;

            if (damage < 0)
                return 1;

            return damage;
        }


        public void battle(Player player)
        {
            Monster monster = this.generateMonster(player.getLevel());
            while (player.getHp() > 0 && monster.getHp() > 0)
            {
                Console.Clear();
                player.showInfo();
                monster.showInfo();

                Console.WriteLine("Оберіть дію: ");
                player.showSkills();
                int choice = Convert.ToInt32(Console.ReadLine());
                UseSkillResultData data = player.useSkill(choice);

                int damage;
                if (data.GetSkillType() == SkillType.BattleSkill)
                {
                    damage = prepareStrike(data.getValue(), monster.getArmor());
                    monster.damageHp(damage);
                    showOnePanchInfo(player.getName(), damage);
                }

                if (monster.getEp() >= 10)
                {
                    monster.decreaseEp(10);
                    damage = prepareStrike(monster.getDamage(), player.Defense());
                    player.damageHp(damage);
                    showOnePanchInfo(monster.getName(), damage);
                }
                else
                {
                    Console.WriteLine($"{monster.getName()} не має сил атакувати!");
                }

                Console.ReadLine();
            }

            if (player.getHp() > 0)
            {
                Console.WriteLine($"Ви перемогли {monster.getName()} і отримали {monster.getExpReward()} очок досвід!");

                player.addExp(monster.getExpReward());
                player.checkLevelUp();
            }
            else
            {
                Console.WriteLine("YOU DIED");
            }

        }


        public Monster generateMonster(int level)
        {
            int minLevel = Math.Max(1, level - 1);
            int maxLevel = level + 1;
            int monsterLevel = random.Next(minLevel, maxLevel + 1);


            return new Monster(names[random.Next(0, names.Length)], monsterLevel);
        }

        public Player createPlayer(string name, PlayerType playerType)
        {
            Player player = null;

            if (playerType == PlayerType.Barbarian)
            {

                player = new Barbarian(this.random, name);
                player.addedSkill(new DoubleWeapon());
            }
            else if (playerType == PlayerType.Tank)
            {
                player = new Tank(this.random, name);
                player.addedSkill(new Explosion());
            }
            else if (playerType == PlayerType.Robber)
            {
                player = new Robber(this.random, name);
                player.addedSkill(new Iaido());
            }

            player.addedSkill(new Strike());
            player.addedSkill(new MightyStrike());
            player.addedSkill(new Regenration());
            player.addedSkill(new Rest());

            return player;
        }
        public Armor createArmor(int protection = 0, string armorName = "")
        {
            if (protection == 0)
            {
                protection = random.Next(3, 20);
            }

            if (armorName.Length == 0)
            {
                armorName = armorNames[random.Next(0, armorNames.Length)];
            }

            return new Armor(protection, armorName);
        }
        public Weapon createWeapon(int harm, string weaponName)
        {
            weaponName = weaponNames[random.Next(0, weaponNames.Length)];
            harm = random.Next(3, 20);
            return new Weapon(harm, weaponName);
        }
    }
    class Events
    {
        public void randomEvent(Random random, int ranEv, int characteristics, Player player)
        {
            characteristics = random.Next(1, 3);
            ranEv = random.Next(1, 3);
            if (ranEv == 1)
            {
                if (characteristics == 1)
                {
                    if (player.GetEn() != player.GetenMax())
                    {
                        player.energy += 10;
                    }
                }
                else
                {
                    player.energy -= 10;
                }
            }
            else
            {
                Console.WriteLine("Нічого не відбулося");
            }
        }
    }


    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = Encoding.Unicode;
            Random random = new Random();
            Engine engine = new Engine(random);
            string playerName = MessageHelper.getString("Введіть ім'я вашого персонажа: ");
            PlayerType playerType = (PlayerType)MessageHelper.getInt("Введіть тип вашого персонажа (Варвар: 1, Танк: 2, Разбійник: 3): ");
            Player player = engine.createPlayer(playerName, playerType);

            // Створюємо магазини
            Magazine mag1 = new Magazine(1, Caliber.Caliber556, 30);
            Magazine mag2 = new Magazine(2, Caliber.Caliber556, 30);
            Magazine mag3 = new Magazine(3, Caliber.Caliber762, 25);
            Magazine mag4 = new Magazine(4, Caliber.Caliber9mm, 17);

            // Створюємо зброю
            Gun ak = new Gun(1, "AK-47", Caliber.Caliber762, mag3);
            Gun m4 = new Gun(2, "M4A1", Caliber.Caliber556, mag1);
            Gun glock = new Gun(3, "Glock 17", Caliber.Caliber9mm, mag4);

            // Видаємо гравцю
            player.AddGun(ak);
            player.AddGun(m4);
            player.AddGun(glock);
            player.AddGunMagazine(mag2); // запасний магазин

            bool isPlaying = true;
            while (isPlaying)
            {
                Console.Clear();
                Console.WriteLine($"=== {player.getName()} ===");
                Console.WriteLine("1 - Боротися з монстром");
                Console.WriteLine("2 - Інвентар");
                Console.WriteLine("0 - Вийти з гри");
                string choice = Console.ReadLine();

                if(choice == "1") engine.battle(player);
                else if (choice == "2") ShowInventory(player);
                else if (choice == "3") { Console.WriteLine("До побачення!"); isPlaying = false; }
                else { Console.WriteLine("Некоректний вибір."); Console.ReadLine(); }
            }
        }
        static void ShowInventory(Player player)
        {
            bool inMenu = true;
            while (inMenu)
            {
                Console.Clear();

                // Передаем player, чтобы инвентарь показал предметы + оружие + магазины
                player.Inventory.ShowAll(player);

                Console.WriteLine();
                Console.WriteLine("--- Дії з предметами ---");
                Console.WriteLine("1 - Використати предмет (за id)");
                Console.WriteLine("2 - Видалити предмет (за id)");
                Console.WriteLine("--- Дії зі зброєю ---");
                Console.WriteLine("3 - Обрати зброю (за id)");
                Console.WriteLine("4 - Перезарядити активну зброю");
                Console.WriteLine("5 - Перемкнути на наступну зброю");
                Console.WriteLine("6 - Зробити постріл (тест)");
                Console.WriteLine("0 - Назад");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть id предмета: ");
                        if (int.TryParse(Console.ReadLine(), out int useId))
                            player.Inventory.Use(useId, player);
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Write("Введіть id предмета: ");
                        if (int.TryParse(Console.ReadLine(), out int remId))
                            player.Inventory.Remove(remId);
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Write("Введіть id зброї: ");
                        if (int.TryParse(Console.ReadLine(), out int gunId))
                            player.SelectGun(gunId);
                        Console.ReadLine();
                        break;

                    case "4":
                        player.ReloadGun();
                        Console.ReadLine();
                        break;

                    case "5":
                        player.SwitchToNextGun();
                        Console.ReadLine();
                        break;

                    case "6":
                        player.FireGun();
                        Console.ReadLine();
                        break;

                    case "0":
                        inMenu = false;
                        break;
                }
            }
        }
    }
}