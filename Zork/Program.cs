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
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.LOOK:
                        outputString = "A rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                        outputString = "You moved NORTH.";
                        break;

                    case Commands.SOUTH:
                        outputString = "You moved SOUTH.";
                        break;

                    case Commands.EAST:
                        outputString = "You moved EAST.";
                        break;

                    case Commands.WEST:
                        outputString = "You moved WEST.";
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

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}
