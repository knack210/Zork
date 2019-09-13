using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;        
            while (command != Commands.QUIT)
            {
                Console.Write(Rooms[LocationRow,LocationColumn] + "\n> ");
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

        private static int LocationColumn = 1;
        private static int LocationRow = 1;

        private static bool Move(Commands command)
        {
            switch (command)
            {
                case Commands.EAST:
                    if (LocationColumn < (Rooms.GetLength(1) - 1))

                    {
                        LocationColumn += 1;
                        return true;
                    }
                    break;

                case Commands.WEST:
                    if (LocationColumn > 0)
                    {
                        LocationColumn -= 1;
                        return true;
                    }
                    break;

                case Commands.NORTH:
                    if (LocationRow < (Rooms.GetLength(0) - 1))
                    {
                        LocationRow += 1;
                        return true;
                    }
                    break;

                case Commands.SOUTH:
                    if (LocationRow > 0)
                    {
                        LocationRow -= 1;
                        return true;
                    }
                    break;

                default:
                    break;
            }
            return false;
        }

        private static string[,] Rooms = {   {"Rocky Trail", "South of House", "Canyon View" },
                                            { "Forest", "West of House", "Behind House" },
                                            {"Dense Woods", "North of House", "Clearing"}
        };

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}
