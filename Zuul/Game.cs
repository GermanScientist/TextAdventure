using System;

namespace Zuul
{
	public class Game
	{
		private Parser parser;
        private Player player;

        public Game ()
		{
            player = new Player();
            parser = new Parser();

            createRooms();
        }

		private void createRooms()
		{
			Room outside, theatre, pub, lab, office, attic, basement;
            Hammer hammer;
            Potion potion;
            Poison poison;
            Key key;

            //Create the items
            hammer = new Hammer("hammer", 3, "normal");
            potion = new Potion("potion", 1, "good");
            poison = new Poison("poison", 1, "bad");
            key = new Key("key", 2, "normal");

            // create the rooms
            outside = new Room("outside the main entrance of the university");
			theatre = new Room("in a lecture theatre");
			pub = new Room("in the campus pub");
			lab = new Room("in a computing lab");
			office = new Room("in the computing admin office");
            attic = new Room("in the attic of the university");
            basement = new Room("in the basement of the university");

            //Put items in rooms
            theatre.inventory.Put(hammer);
            lab.inventory.Put(potion);
            lab.inventory.Put(poison);
            pub.inventory.Put(key);

			// initialise room exits
			outside.setExit("east", theatre);
			outside.setExit("south", lab);
			outside.setExit("west", pub);

			theatre.setExit("west", outside);
            theatre.setExit("up", attic);

			pub.setExit("east", outside);

			lab.setExit("north", outside);
			lab.setExit("east", office);
            lab.setExit("down", basement);

			office.setExit("west", lab);
            office.LockRoom(true);

            attic.setExit("down", theatre);

            basement.setExit("up", lab);

			player.currentRoom = outside;  // start game outside
		}


		/**
	     *  Main play routine.  Loops until end of play.
	     */
		public void play()
		{
			printWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.
			bool finished = false;
			while (! finished && player.IsAlive()) {
				Command command = parser.getCommand();
				finished = processCommand(command);
			}
            if(! player.IsAlive())
            {
                Console.WriteLine("You have died, better luck next time.");
            } 
            else
            {
                Console.WriteLine("Thank you for playing.");
            }
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.currentRoom.getLongDescription());
		}

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown()) {
				Console.WriteLine("I don't know what you mean...");
				return false;
			}

			string commandWord = command.getCommandWord();
			switch (commandWord) {
				case "help":
					printHelp();
					break;
				case "go":
					goRoom(command);
					break;
				case "quit":
					wantToQuit = true;
					break;
                case "look":
                    Console.WriteLine(player.currentRoom.getLongDescription());
                    break;
                case "pickup":
                    Pickup(command);
                    break;
                case "drop":
                    Drop(command);
                    break;
                case "inventory":
                    Console.WriteLine("Your inventory consists of: \n");
                    Console.WriteLine(player.inventory.Show());
                    break;
                case "use":
                    Use(command);
                    break;
            }

			return wantToQuit;
		}

		// implementations of user commands:
        private void Pickup(Command command)
        {
            if(!command.hasSecondWord())
            {
                Console.WriteLine("Pickup what?");
                return;
            }

            string itemToPickup = command.getSecondWord();
            player.currentRoom.inventory.Swap(player.inventory, itemToPickup);

            player.inventory.Show();
        }

        private void Drop(Command command)
        {
            if (!command.hasSecondWord())
            {
                Console.WriteLine("Pickup what?");
                return;
            }

            string itemToDrop = command.getSecondWord();

            player.inventory.Swap(player.currentRoom.inventory, itemToDrop);

            player.inventory.Show();
        }

        private void Use(Command command)
        {
            if (!command.hasSecondWord())
            {
                Console.WriteLine("Use what?");
                return;
            }

            string itemToUse = command.getSecondWord();
            
            if(player.inventory.GetItem(itemToUse) != null)
            {
                player.inventory.GetItem(itemToUse).Use();

                if (player.inventory.GetItem(itemToUse).type == "bad")
                {
                    player.InjurePlayer();
                } else if(player.inventory.GetItem(itemToUse).type == "good")
                {
                    player.HealPlayer(5);
                }

                player.inventory.Take(itemToUse);
            } else
            {
                Console.WriteLine(itemToUse + " does not exist in inventory");
            }
        }

        /**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
        private void printHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
		}

		/**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
		private void goRoom(Command command)
		{
			if(!command.hasSecondWord()) {
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.getSecondWord();

			// Try to leave current room.
			Room nextRoom = player.currentRoom.getExit(direction);

			if (nextRoom == null) {
				Console.WriteLine("There is no door to "+direction+"!");
			} 
            else 
            {
                if(nextRoom.IsLocked())
                {
                    Console.WriteLine("You need a key to use this exit");

                    if (player.inventory.GetItem("key") != null)
                    {
                        GoToNextRoom(nextRoom);
                        Console.WriteLine("You used a key to use this exit");
                    } else
                    {
                        Console.WriteLine("You dont have a key for this exit");
                    }
                } 
                else
                {
                    GoToNextRoom(nextRoom);
                }
			}
		}

        private void GoToNextRoom(Room nextRoom)
        {
            Console.Clear();

            player.currentRoom = nextRoom;

            Console.WriteLine("Amount of bad items = " + player.inventory.CheckForBadItems());

            //Only damages player is injured
            if (player.IsInjured())
            {
                //Damages player based on how many bad items he has
                player.DamagePlayer();
                Console.WriteLine("You are injured, moving around will cost HP");
            }

            Console.WriteLine("health = " + player.GetPlayerHealth());
            Console.WriteLine(player.currentRoom.getLongDescription());
        }
	}
}
