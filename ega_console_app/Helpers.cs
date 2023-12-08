using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app
{
    public static class Helpers
    {
        public static Random Rnd = new Random();
        public static IList<Item> Items { get; set; }



        public static int GetSuitability(int[] combination)
        {
            int suitability = 0;
            for (int i = 0; i < combination.Length; i++)
                if (combination[i] == 1)
                    suitability += Items[i].Price;
            return suitability;
        }

        public static int GetWeight(int[] combination)
        {
            int weight = 0;
            for(int i = 0; i < combination.Length; i++)
                if (combination[i] == 1)
                    weight += Items[i].Weight;
            return weight;
        }

        public static IList<Item> GetItemsInBackpack(int[] combination)
        {
            IList<Item> items = new List<Item>();
            for(int i = 0; i < combination.Length; i++)
                if (combination[i] == 1)
                    items.Add(Items[i]);
            return items;
        }



        public static int SelectRandomSection(List<float> values)
        {
            float randomNumber = (float)(Rnd.NextDouble() * values.Sum());

            float[] leftBorders = new float[values.Count];
            leftBorders[0] = values[0];
            for (int i = 1; i < values.Count; i++)
                leftBorders[i] = values[i] + leftBorders[i - 1];

            int section = -1;
            for (int i = 0; i < values.Count; i++)
                if (randomNumber < leftBorders[i])
                { section = i; break; }

            return section;
        }
    }
}
