using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.PopulationGenerators
{
    public class RandomWithControlPopulationGenerator : IPopulationGenerator
    {
        string[] masks;

        int populationSize;
        int backpackCapacity;
        List<Item> items;
        Random rnd;

        public RandomWithControlPopulationGenerator()
        {
        }


        public IList<Encoding> Generate(int populationSize, IList<Item> items, int backpackCapacity)
        {
            GenerateMasks(items.Count);
            var result = new List<Encoding>();
            for(int i = 0; i < populationSize; i++)
            {
                int[] newBeing = new int[items.Count];
                int index = Helpers.Rnd.Next(0, masks.Length);
                int sumWeight = 0;
                for(int j = 0; j < masks[index].Length; j++)
                {
                    var item = items[j];
                    var c = masks[index][j];
                    if (c == '1')
                    {
                        if (sumWeight + item.Weight <= backpackCapacity)
                        {
                            //newBeing.Add(item);
                            newBeing[j] = 1;
                            sumWeight += item.Weight;
                        }
                        else
                            break;
                    }
                }
                result.Add(new Encoding() { Combination = newBeing, 
                    Suitability = Helpers.GetSuitability(newBeing)});
            }

            return result;
        }

        void GenerateMasks(int L)
        {
            int maxNumber = (int)Math.Pow(2, L);
            masks = Enumerable.Range(0, maxNumber).Select(i => Convert.ToString(i, 2).PadLeft(L, '0')).ToArray();
        }
    }
}
