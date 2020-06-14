using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuul
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();
        private int max_weight = 0;

        public Inventory(int mw)
        {
            this.max_weight = mw;
        }

        //Add with instance
        public int Put(Item item)
        {
            if (this.TotalWeight() + item.weight < this.max_weight)
            {
                items.Add(item);
                Console.WriteLine(item.description + " added to Inventory");
                return 1;
            }
            Console.WriteLine(item.description + " is too heavy!");
            return 0;
        }

        // Remove by instance
        public Item Take(Item item)
        {
            if (items.Remove(item))
            {
                Console.WriteLine("Removed " + item.description + " from Inventory");
                return item;
            }
            Console.WriteLine("Could not find " + item.description + " in Inventory");
            return null;
        }

        // Remove by description
        public Item Take(string desc)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].description == desc)
                {
                    Item item = items[i];
                    this.Take(item);
                    return item;
                }
            }
            Console.WriteLine("Could not find '" + desc + "' in Inventory");
            return null;
        }

        public void Show()
        {
            Console.WriteLine("Inventory contains:");
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Show();
            }
            Console.WriteLine("Total weight: " + this.TotalWeight() + " out of " + this.max_weight);
        }

        private int TotalWeight()
        {
            int totalWeight = 0;
            for (int i = 0; i < items.Count; i++)
            {
                totalWeight += items[i].weight;
            }
            return totalWeight;
        }
    }
}
