using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            roomIndex = 1;
            while (command != Commands.QUIT)
            {
                Console.Write(Rooms[roomIndex]+ "\n> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.LOOK:
                        outputString = "A rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:                        
                    case Commands.WEST:
                        if (Move(command))
                        {
                            outputString = $"You moved {command}.";
                        }

                        else
                        {
                            outputString = "This way is shut!";
                        }
                        break;

                    case Commands.QUIT:
                        outputString = "Thanks you for playing!";
                        break;

                    case Commands.UNKNOWN:
                        outputString = "Unknown command.";
                        break;
                    default:
                        outputString = "";
                        break;
                };

                Console.WriteLine(outputString);
            }
        }

        private static int roomIndex;

        private static bool Move(Commands command)
        {
            switch (command)
            {
                case Commands.EAST:
                    if (roomIndex < Rooms.Length - 1)
                    {
                        roomIndex += 1;
                        return true;
                    }
                    break;

                case Commands.WEST:
                    if (roomIndex > 0)
                    {
                        roomIndex -= 1;
                        return true;
                    }
                    break;

                default:
                    break;
            }
            return false;
        }

        private static string[] Rooms = { "Forest", "West of House", "Behind House", "Clearing", "Canyon View" };

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}
