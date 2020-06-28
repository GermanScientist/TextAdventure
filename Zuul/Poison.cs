using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuul
{
    public class Poison : Item
    {
        // Constructor of base class Item is called with arguments
        public Poison(string _description, int _weight, string _type) : base(_description, _weight, _type)
        {
        }

        // this method 'overrides' the 'virtual' method in base class Item.
        public override void Use()
        {
            Console.WriteLine("Did-- ..Did you just ..drink poison? Even though you knew it was poison? It LITERALLY had a sign above it with 'WATCH OUT THIS IS POISON', yet you still drank it?");
        }
    }
}
