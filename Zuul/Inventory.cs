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

        public string Show()
        {
            string returnstring = "";
            for (int i = 0; i < items.Count; i++)
            {
                returnstring += items[i].Show();
            }

            return returnstring;
        }

        public void Swap(Inventory other, string item)
        {
            Item pickup = this.Take(item);

            if (pickup != null)
            {
                other.Put(pickup);
            }
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

        public int CheckForBadItems()
        {
            int amount = 0;
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].type == "bad")
                {
                    amount++;
                }
            }

            return amount;
        }
    }
}
