using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuul
{
    public class Potion : Item
    {
        // Constructor of base class Item is called with arguments
        public Potion(string _description, int _weight) : base(_description, _weight)
        {
        }

        // this method 'overrides' the 'virtual' method in base class Item.
        public override void Use()
        {
            Console.WriteLine("Gluck, gluck, gluck. Health restored!");
        }
    }
}
