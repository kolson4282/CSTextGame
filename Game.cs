class Game
{

    private GameState gameState;
    private bool running;
    private Player player;
    private Enemy enemy;
    private int enemyCount;

    public Game()
    {
        gameState = GameState.MainMenu;
        player = new Player("Player 1");
        enemy = new Enemy("", 0, 0);
        running = false;
    }

    public void Run()
    {
        running = true;
        player.Inventory.Add(new HealthPotion("Minor Health", 5));
        player.Inventory.Add(new Sword("Long Sword", 15));
        player.Inventory.Add(new Sword("Short Sword", 10));
        player.Inventory.Add(new PlateArmor("Starter Armor", 10));

        while (running)
        {
            if (player.HP <= 0)
            {
                gameState = GameState.GameOver;
            }
            switch (gameState)
            {
                case GameState.MainMenu:
                    Menu();
                    break;
                case GameState.Playing:
                    Play();
                    break;
                case GameState.Inventory:
                    Inventory();
                    break;
                case GameState.Battle:
                    Battle();
                    break;
                case GameState.GameOver:
                    running = false;
                    Console.WriteLine("Game Over");
                    Console.WriteLine($"You beat {enemyCount} enemies");
                    break;
                default:
                    Console.WriteLine("State not implemented");
                    gameState = GameState.Playing;
                    break;
            }
        }
    }

    private void Menu()
    {
        Console.WriteLine("Welcome to The Arena");
        Console.WriteLine("Be sure to equip your equipment before going into the first battle.");
        gameState = GameState.Playing;

    }

    private void Play()
    {
        Console.WriteLine(player.Stats());
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("I: Open Inventory");
        Console.WriteLine("B: Go to next battle");
        Console.WriteLine("Q: Quit");
        string input = Console.ReadLine()!.ToUpper();
        switch (input)
        {
            case "I":
                gameState = GameState.Inventory;
                break;
            case "B":
                enemy = new Enemy($"Enemy {enemyCount + 1}", 60, 20); // create a new enemy. Maybe eventually use same enemy if you haven't won yet. 
                gameState = GameState.Battle;
                break;
            case "DIE":
                gameState = GameState.GameOver;
                break;
            case "Q":
                running = false;
                break;
            default:
                Console.WriteLine("Invalid Option. Please Try again.");
                break;
        }
    }

    private void Inventory()
    {
        Console.WriteLine(player.Stats());
        //Only items currently implemented are a health potion and an armor buff item. Need to add more later. 
        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("There is nothing in your inventory");
        }
        else
        {
            Console.WriteLine("This is what you have in your inventory.");
            foreach (Item item in player.Inventory)
            {
                Console.WriteLine(player.Inventory.IndexOf(item) + ". " + item);
            }
            Console.WriteLine($"Which item would you like to choose? (-1 to exit)");
            try
            {
                int input = Convert.ToInt32(Console.ReadLine());
                Item item = player.Inventory[input];
                Console.WriteLine("Would you like to Use this item or Drop this item? (U/D)");
                string answer = Console.ReadLine()!.ToUpper();
                if (answer == "D")
                {
                    Console.WriteLine($"Dropped {item}");
                    player.DropItem(item);
                }
                else if (answer == "U")
                {
                    player.UseItem(player.Inventory[input]);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("That is an invalid answer.");
                Console.WriteLine(e.Message);
            }
        }
        gameState = GameState.Playing;
    }

    private void Battle()
    {   //Get into a battle and fight an enemy as defined in the Play() method
        Console.WriteLine(player.Stats());
        // Only options are attack and run. Assuming run always gets away. Will probably change this eventually.
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("A: Attack");
        Console.WriteLine("R: Run");
        string input = Console.ReadLine()!.ToUpper();
        switch (input)
        {
            case "A":
                Console.WriteLine($"You attacked {enemy.Name} for {player.ATT} damage.");
                enemy.TakeDamage(player.ATT);
                if (enemy.HP <= 0)
                {
                    Console.WriteLine("You Won!");
                    enemyCount++;
                    Console.WriteLine("You got a health potion");
                    player.Inventory.Add(new HealthPotion("Minor Health", 5));
                    gameState = GameState.Playing;
                    break;
                }
                Console.WriteLine($"{enemy.Name} Attacked back for {enemy.ATT} damage. Your armor blocked {player.BaseAC} of it");
                player.TakeDamage(enemy.ATT);
                break;
            case "R":
                Console.WriteLine("Got away safely");
                gameState = GameState.Playing;
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }
    static void Main()
    {
        Game g = new Game();
        g.Run();
    }

    enum GameState
    {
        MainMenu,
        Playing,
        Inventory,
        Battle,
        GameOver,
    }
}

