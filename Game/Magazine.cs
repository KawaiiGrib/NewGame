using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Magazine
    {
        public int Id { get; private set; }
        public Caliber Caliber { get; private set; }
        public int AmmoCount { get; private set; }   // скільки патрон
        public int MaxAmmo { get; private set; }

        public Magazine(int id, Caliber caliber, int maxAmmo)
        {
            Id = id;
            Caliber = caliber;
            MaxAmmo = maxAmmo;
            AmmoCount = maxAmmo;
        }

        public bool IsEmpty => AmmoCount <= 0;

        // Вистріл — зменшує кількість патрон
        public bool Shoot()
        {
            if (IsEmpty) return false;
            AmmoCount--;
            return true;
        }

        // Перезарядка магазина (поповнення)
        public void Refill()
        {
            AmmoCount = MaxAmmo;
        }

        public override string ToString()
        {
            return $"[Магазин #{Id}] Калібр: {Caliber}, Патрони: {AmmoCount}/{MaxAmmo}";
        }
    }
}
