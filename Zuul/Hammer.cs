using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuul
{
    public class Hammer : Item
    {
        // Constructor of base class Item is called with arguments
        public Hammer(string _description, int _weight, string _type) : base(_description, _weight, _type)
        {
        }

        // this method 'overrides' the 'virtual' method in base class Item.
        public override void Use()
        {
            Console.WriteLine("You used the hammer.");
        }
    }
}
