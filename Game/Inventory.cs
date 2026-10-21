using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Inventory
    {
        private const int MAX_SLOTS = 10;
        private List<Item> items;

        public Inventory()
        {
            items = new List<Item>();
        }

        public int Count => items.Count;
        public int MaxSlots => MAX_SLOTS;
        private int nextId = 1;

        // Додати предмет
        public bool Add(Item item)
        {
            if (items.Count >= MAX_SLOTS)
            {
                Console.WriteLine("Інвентар заповнено!");
                return false;
            }

            item.Id = nextId;
            nextId++;

            items.Add(item);
            Console.WriteLine($"Додано: {item.Name}");
            return true;
        }

        // Видалити по id
        public bool Remove(int id)
        {
            Item item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                Console.WriteLine($"Предмет з id={id} не знайдено.");
                return false;
            }

            items.Remove(item);
            Console.WriteLine($"Видалено: {item.Name}");
            return true;
        }
        public void ShowAll(Player player)
        {
            // 1. Звиччайні предмети
            Console.WriteLine($"=== Інвентар ({items.Count}/{MAX_SLOTS}) ===");

            if (items.Count == 0)
            {
                Console.WriteLine("  (порожньо)");
            }
            else
            {
                foreach (Item item in items)
                    Console.WriteLine("  " + item);
            }

            Console.WriteLine();

            // 2. Зброя (Gun)
            player.ShowGuns();
            Console.WriteLine();

            // 3. Магазини
            player.ShowGunMagazines();
        }


        // Показати усі предмети
        public void ShowAll()
        {
            Console.WriteLine($"=== Інвентар ({items.Count}/{MAX_SLOTS}) ===");

            if (items.Count == 0)
            {
                Console.WriteLine("  (порожньо)");
                return;
            }

            foreach (Item item in items)
                Console.WriteLine("  " + item);
        }

        // Використати предмет по id
        public bool Use(int id, Player player)
        {
            Item item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                Console.WriteLine($"Предмет з id={id} не знайдено.");
                return false;
            }

            Console.WriteLine(item.Use(player));
            return true;
        }

        // Отримати предмет по id
        public Item GetById(int id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }

        // Перевірка: чи є предмет
        public bool Contains(int id)
        {
            return items.Any(i => i.Id == id);
        }
    }
}
