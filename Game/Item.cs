using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Item : NameTrait   
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }

        public Item(int id, string name, int price)
        {
            Id = id;
            this.name = name;   
            Price = price;
        }

        public virtual string Use(Player player)
        {
            return $"Предмет '{name}' використано.";
        }

        public override string ToString()
        {
            return $"#{Id} {name} ({Price} монет)";
        }
    }
}
