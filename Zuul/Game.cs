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

            //Create the items
            hammer = new Hammer("hammer", 3);
            potion = new Potion("potion", 1);

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
            theatre.inventory.Put(potion);

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
				player.currentRoom = nextRoom;
                player.DamagePlayer();
                Console.WriteLine("health = " + player.GetPlayerHealth());
                Console.WriteLine(player.currentRoom.getLongDescription());
			}
		}
	}
}
