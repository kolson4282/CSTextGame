class Game
{

    private GameState gameState;
    private bool running;
    private Player player;
    private Queue<Level> levels;
    private Level currentLevel;
    private int enemyCount;

    public Game()
    {
        gameState = GameState.MainMenu;
        player = new Player("Player 1");
        levels = SetupLevels();
        currentLevel = new Level();
        running = false;
    }

    public static Queue<Level> SetupLevels()
    {
        Queue<Level> levels = new Queue<Level>();
        levels.Enqueue(new Level(new Enemy("Enemy 1", 20, 5), new HealthPotion("Minor Health", 5)));
        levels.Enqueue(new Level(new Enemy("Enemy 2", 25, 5), new Sword("Short Sword", 20)));
        levels.Enqueue(new Level(new Enemy("Enemy 3", 30, 10), new PlateArmor("Starter Armor", 20)));
        levels.Enqueue(new Level(new Enemy("Enemy 4", 40, 20), new Sword("Long Sword", 30)));
        levels.Enqueue(new Level(new Enemy("Enemy 5", 50, 20), new HealthPotion("Major Health", 50)));
        levels.Enqueue(new Level(new Enemy("Boss", 100, 30), new Trophy("You Won!")));
        return levels;
    }
    public void Run()
    {
        running = true;

        while (running)
        {
            if (player.HP <= 0)
            {
                gameState = GameState.GameOver;
            }
            else if (levels.Count() <= 0 && currentLevel.Enemy.HP <= 0)
            {
                gameState = GameState.Won;
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
                    Console.WriteLine("Game Over");
                    Console.WriteLine($"You beat {enemyCount} enemies");
                    running = false;
                    break;
                case GameState.Won:
                    Console.WriteLine("You defeated all the enemies");
                    Console.WriteLine("Ending Stats:");
                    Console.WriteLine($"Defeated {enemyCount} enemies");
                    Console.WriteLine(player.Stats());
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Console.WriteLine();
                    running = false;
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
                if (currentLevel.Enemy.HP <= 0)
                {
                    currentLevel = levels.Dequeue(); // move to the next level if this one is done
                }
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
                Console.WriteLine($"You attacked {currentLevel.Enemy.Name} for {player.ATT} damage.");
                currentLevel.Enemy.TakeDamage(player.ATT);
                if (currentLevel.Enemy.HP <= 0)
                {
                    Console.WriteLine("You Won!");
                    enemyCount++;
                    if (currentLevel.Item != null)
                    {
                        Console.WriteLine($"You got a {currentLevel.Item}");
                        player.Inventory.Add(currentLevel.Item);
                    }
                    gameState = GameState.Playing;
                    break;
                }
                Console.WriteLine($"{currentLevel.Enemy.Name} Attacked back for {currentLevel.Enemy.ATT} damage. Your armor blocked {player.BaseAC} of it");
                player.TakeDamage(currentLevel.Enemy.ATT);
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
        Won,
    }
}

