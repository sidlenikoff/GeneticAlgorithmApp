using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app
{
    public struct Item
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }

        public Item(int id, int weight, int price)
        {
            Id = id;
            Weight = weight;
            Price = price;
        }

        public override string ToString()
        {
            return $"[{Id}]\nВес: {Weight}\nЦена: {Price}";
        }
    }
}
