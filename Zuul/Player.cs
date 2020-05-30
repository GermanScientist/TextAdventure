using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuul
{
    class Player
    {
        public Room currentRoom;
        private int health;

        public Player()
        {
            health = 5;
        }

        public void HealPlayer(int amount = 1)
        {
            health += amount;

            Console.WriteLine("You have gained " + amount + " healthpoint");
        }

        public void DamagePlayer(int amount = 1)
        {
            health -= amount;

            Console.WriteLine("You have lost " + amount + " healthpoint");
        }

        public int GetPlayerHealth()
        {
            return health;
        }

        public bool IsAlive()
        {
            if(health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
