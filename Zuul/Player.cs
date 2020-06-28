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
        private int maxHealth;
        private bool isInjured;

        public Inventory inventory = new Inventory(10);

        public Player()
        {
            maxHealth = 5;
            health = maxHealth;
            isInjured = false;
        }

        public void HealPlayer(int amount = 1)
        {
            health += amount;
            if(health > maxHealth)
            {
                health = maxHealth;
            }

            Console.WriteLine("You have gained " + amount + " healthpoint");

            isInjured = false;
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

        public bool IsInjured()
        {
            return isInjured;
        }

        public void InjurePlayer()
        {
            isInjured = true;
        }
    }
}
