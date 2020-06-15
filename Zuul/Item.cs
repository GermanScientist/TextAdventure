using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuul
{
    public abstract class Item
    {
        public string description { get; set; }
        public int weight { get; set; }

        public Item(string _description, int _weight)
        {
            this.description = _description;
            this.weight = _weight;
        }

        // this method is executed when called on a subclass.
        public string Show()
        {
            return " - Item '" + this.description + "' weighs " + this.weight;
        }

        // this method is 'virtual', and should be 'override' in subclasses.
        public virtual void Use()
        {
            Console.WriteLine("Generic 'Use' method called");
        }
    }
}
