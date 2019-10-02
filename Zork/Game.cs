using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Zork
{
	public class Game
	{
		public World World { get; set; }

		[JsonIgnore]
		public Player Player { get; private set; }

		[JsonIgnore]
		public bool IsRunning { get; set; }

		[JsonIgnore]
		public CommandManager CommandManager { get; }

		public Game(World world, Player player)
		{
			World = world;
			Player = player;
		}

		public Game()
		{
			Command[] commands =
			{
				new Command("LOOK", new string[] { "LOOK", "L" },
					(game, commandContext) => Console.WriteLine(game.Player.Location.Description)),

				new Command("QUIT", new string[] { "QUIT", "Q" },
					(game, commandContext) => game.IsRunning = false),

				new Command("NORTH", new string[] { "NORTH", "N" },MovementCommands.North),

				new Command("SOUTH", new string[] { "SOUTH", "S" }, MovementCommands.South),

				new Command("EAST", new string[] { "EAST", "E" }, MovementCommands.East),

				new Command("WEST", new string[] { "WEST", "W" }, MovementCommands.West)
			};
			CommandManager = new CommandManager(commands);
		}

		public void Run()
		{
			IsRunning = true;
			Room previousRoom = null;
			while (IsRunning)
			{
				Console.WriteLine(Player.Location);
				if (previousRoom != Player.Location)
				{
					Console.WriteLine(Player.Location.Description);
					previousRoom = Player.Location;
				}

				Console.Write("\n> ");
				if (CommandManager.PerformCommand(this, Console.ReadLine().Trim()))
				{
					Player.Moves++;
				}
				else
				{
					Console.WriteLine("That's not a verb I recognize.");
				}
			}
		}

		public static Game Load(string filename)
		{
			Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));
			game.Player = game.World.SpawnPlayer();

			return game;
		}

		private void LoadCommands()
		{
			var commandMethods = (from type in Assembly.GetExecutingAssembly().GetTypes()
								  from method in type.GetMethods()
								  let attribute = method.GetCustomAttribute<CommandAttribute>()
								  where type.IsClass && type.GetCustomAttribute<CommandClassAttribute>() != null
								  where attribute != null
								  select new Command(attribute.CommandName, attribute.Verbs,
								  (Action<Game, CommandContext>)Delegate.CreateDelegate(typeof(Action<Game, CommandContext>), method)));

			CommandManager.AddCommands(commandMethods);
		}
	}       
}
