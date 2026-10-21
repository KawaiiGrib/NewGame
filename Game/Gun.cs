using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Gun
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Caliber Caliber { get; private set; }
        public Magazine CurrentMagazine { get; private set; }
        public FireMode Mode { get; set; } = FireMode.Single;

        public Gun(int id, string name, Caliber caliber, Magazine startMagazine)
        {
            Id = id;
            Name = name;
            Caliber = caliber;
            CurrentMagazine = startMagazine;
        }

        public bool ChangeMagazine(Magazine newMagazine)
        {
            if (newMagazine == null) return false;
            if (newMagazine.Caliber != Caliber)
            {
                Console.WriteLine($"Невірний калібр! Потрібен {Caliber}");
                return false;
            }

            CurrentMagazine = newMagazine;
            Console.WriteLine($"{Name}: магазин замінено на #{newMagazine.Id}");
            return true;
        }

        public bool Shoot()
        {
            if (CurrentMagazine == null || CurrentMagazine.IsEmpty)
            {
                Console.WriteLine($"{Name}: магазин порожній!");
                return false;
            }

            CurrentMagazine.Shoot();
            Console.WriteLine($"{Name}: постріл! Залишилось: {CurrentMagazine.AmmoCount}");
            return true;
        }

        public override string ToString()
        {
            string magInfo = CurrentMagazine != null
                ? $"#{CurrentMagazine.Id} ({CurrentMagazine.AmmoCount}/{CurrentMagazine.MaxAmmo})"
                : "без магазину";

            return $"[Зброя #{Id}] {Name} | {Caliber} | {magInfo}";
        }
    }
    public enum FireMode
    {
        Single,
        Burst,
        Auto
    }
}
