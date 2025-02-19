using System;
using System.Linq;
using System.Threading;

public class Game
{
    private Player Player { get; set; }
    private GalaxyMap GalaxyMap { get; set; }
    private QuestSystem QuestSystem { get; set; }
    private BattleSystem BattleSystem { get; set; }
    private Shop Shop { get; set; }
    private Random Random { get; set; }

    public Game()
    {
        GalaxyMap = new GalaxyMap(20, 20);
        Player = new Player(GalaxyMap);
        QuestSystem = new QuestSystem();
        BattleSystem = new BattleSystem();
        Shop = new Shop();
        Random = new Random();
        InitializeGame();
    }

    private void InitializeGame()
    {
        int centerX = GalaxyMap.Width / 2;
        int centerY = GalaxyMap.Height / 2;

        for (int x = centerX - 1; x <= centerX; x++)
        {
            for (int y = centerY - 1; y <= centerY; y++)
            {
                GalaxyMap.AddPlanet(new Planet("Солнце", "Центр Солнечной системы.", null, new List<string>(), x, y, 'S', 0));
            }
        }

        GalaxyMap.AddPlanet(new Planet("Меркурий", "Ближайшая к Солнцу планета.", null, new List<string> { "Кислотный кристалл" }, Random.Next(0, GalaxyMap.Width), Random.Next(0, GalaxyMap.Height), 'M', 100));
        GalaxyMap.AddPlanet(new Planet("Венера", "Вторая планета от Солнца.", null, new List<string> { "Вулканический газ" }, Random.Next(0, GalaxyMap.Width), Random.Next(0, GalaxyMap.Height), 'V', 120));
        GalaxyMap.AddPlanet(new Planet("Земля", "Третья планета от Солнца.", "Галактическая Империя", new List<string> { "Древесина", "Металл" }, Random.Next(0, GalaxyMap.Width), Random.Next(0, GalaxyMap.Height), 'E', 150));
        GalaxyMap.AddPlanet(new Planet("Марс", "Четвертая планета от Солнца.", null, new List<string> { "Марсианский камень" }, Random.Next(0, GalaxyMap.Width), Random.Next(0, GalaxyMap.Height), 'R', 200));
        GalaxyMap.AddPlanet(new Planet("Юпитер", "Пятая планета от Солнца.", null, new List<string> { "Газовый конденсат" }, Random.Next(0, GalaxyMap.Width), Random.Next(0, GalaxyMap.Height), 'J', 250));
        GalaxyMap.AddPlanet(new Planet("Сатурн", "Шестая планета от Солнца.", null, new List<string> { "Кольцевая пыль" }, Random.Next(0, GalaxyMap.Width), Random.Next(0, GalaxyMap.Height), 'S', 300));
        GalaxyMap.AddPlanet(new Planet("Уран", "Седьмая планета от Солнца.", null, new List<string> { "Ледяной кристалл" }, Random.Next(0, GalaxyMap.Width), Random.Next(0, GalaxyMap.Height), 'U', 350));
        GalaxyMap.AddPlanet(new Planet("Нептун", "Восьмая планета от Солнца.", null, new List<string> { "Глубинный газ" }, Random.Next(0, GalaxyMap.Width), Random.Next(0, GalaxyMap.Height), 'N', 400));
        GalaxyMap.AddPlanet(new Planet("База повстанцев", "Не особо-то и секретная база повстанцев.", "Повстанцы", new List<string> { "Оружие", "Боеприпасы" }, Random.Next(0, GalaxyMap.Width), Random.Next(0, GalaxyMap.Height), 'P', 500));
    }

    public void Start()
    {
        Console.WriteLine("           == Ближайшее будущее ==\n");
        Console.WriteLine("На заре постиндустриальной эпохи богатейшие меценаты");
        Console.WriteLine("решили, что пора валить с 'матушки Земли' в космос -");
        Console.WriteLine("искать новые способы набивания карманов, чем дали");
        Console.WriteLine("толчок развития технологий мешпланетных перелетов\n\n");
        Console.WriteLine("                == 3025 год ==\n");
        Console.WriteLine("Вы капитан космического экспресса 'Зимагер', чья");
        Console.WriteLine("задача - катать ящики и бочки из одной части солнечной");
        Console.WriteLine("системы в другую. Недавно весь ваш трюм обчистили пираты.");
        Console.WriteLine("Коррумпированная верхушка власти закрывает глаза на");
        Console.WriteLine("проделки разбойников. По другую сторону баррикады -");
        Console.WriteLine("повстанцы, измеряющие силу в размере дыры, которую");
        Console.WriteLine("способен оставить твой лазер. Придётся выбирать, к кому");
        Console.WriteLine("примкнуть. Надевай фуражку, капитан!");
        Console.WriteLine("Добро пожаловать в 'Космическое приключение'!\n");
        WaitForInput();

        while (true)
        {
            GalaxyMap.Draw(Player);
            HandleInput();
            UpdateGame();
            Console.CursorVisible = false;
            Thread.Sleep(100);
        }
    }

    private void WaitForInput()
    {
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey(true);
    }

    private void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.LeftArrow: Player.Move(-1, 0); break;
                case ConsoleKey.RightArrow: Player.Move(1, 0); break;
                case ConsoleKey.UpArrow: Player.Move(0, -1); break;
                case ConsoleKey.DownArrow: Player.Move(0, 1); break;
                case ConsoleKey.E: InteractWithPlanet(); break;
                case ConsoleKey.S:
                    var planet = GalaxyMap.Planets.FirstOrDefault(p => p.PositionX == Player.PositionX && p.PositionY == Player.PositionY);
                    if (planet != null && planet.Faction != null)
                    {
                        Shop.ShowItems();
                        string choice = Console.ReadLine();
                        Shop.BuyItem(Player, choice);
                    }
                    else
                    {
                        Console.WriteLine("Магазин доступен только на планетах с фракциями.");
                        WaitForInput();
                    }
                    break;
                case ConsoleKey.N:
                    GalaxyMap.ExploreNebula(Player);
                    break;
                case ConsoleKey.T:
                    Shop.SellResources(Player);
                    break;
                case ConsoleKey.U:
                    Shop.UpgradeShip(Player);
                    break;
            }
        }
    }

    private void InteractWithPlanet()
    {
        var planet = GalaxyMap.Planets.FirstOrDefault(p => p.PositionX == Player.PositionX && p.PositionY == Player.PositionY);
        if (planet != null)
        {
            Console.WriteLine($"Вы находитесь на планете {planet.Name}.");
            Console.WriteLine(planet.Description);
            Console.WriteLine("Выберите действие: 1 - Собрать ресурсы, 2 - Взаимодействовать с фракцией, 3 - Пополнить топливо");
            string action = Console.ReadLine();
            if (action == "1")
            {
                CollectResources(planet);
            }
            else if (action == "2")
            {
                InteractWithFaction(planet);
            }
            else if (action == "3" && (planet.Name == "Земля" || planet.Name == "База повстанцев"))
            {
                Refuel(planet);
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
                WaitForInput();
            }
        }
    }

    private void CollectResources(Planet planet)
    {
        if (planet.Resources.Count > 0)
        {
            string resource = planet.Resources[0];
            if (!Player.Inventory.ContainsKey(planet.Name))
            {
                Player.Inventory[planet.Name] = resource;
                planet.Resources.RemoveAt(0);
                Console.WriteLine($"Вы собрали {resource} с планеты {planet.Name}.");
            }
            else
            {
                Console.WriteLine($"Вы уже собрали ресурс с планеты {planet.Name}.");
            }
        }
        else
        {
            Console.WriteLine("На этой планете пока нет ресурсов. Подождите, пока они восстановятся.");
        }
        WaitForInput();
    }

    private void InteractWithFaction(Planet planet)
    {
        if (planet.Faction != null)
        {
            var faction = QuestSystem.Factions.FirstOrDefault(f => f.Name == planet.Faction);
            if (faction != null)
            {
                Console.WriteLine($"Вы взаимодействуете с фракцией {faction.Name}.");
                Console.WriteLine($"Уровень лояльности: {faction.GetLoyaltyBar()} ({faction.LoyaltyLevel}%)");

                if (faction.CurrentQuest == null)
                {
                    Console.WriteLine("Доступные задания:");
                    for (int i = 0; i < faction.Quests.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {faction.Quests[i]}");
                    }
                    Console.WriteLine("Выберите задание (или введите '0' для выхода):");
                    string choice = Console.ReadLine();
                    if (int.TryParse(choice, out int questIndex) && questIndex > 0 && questIndex <= faction.Quests.Count)
                    {
                        QuestSystem.TakeQuest(Player, faction, questIndex - 1);
                    }
                }
                else
                {
                    Console.WriteLine($"Текущее задание: {faction.CurrentQuest}");
                    Console.WriteLine("Вы хотите завершить задание? (1 - Да, 2 - Нет)");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        QuestSystem.CompleteQuest(Player, faction);
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("На этой планете нет фракции.");
        }
        WaitForInput();
    }

    private void Refuel(Planet planet)
    {
        if (planet.Name == "Земля" || planet.Name == "База повстанцев")
        {
            int fuelCost = 10;
            int fuelAmount = 50;

            Console.WriteLine($"Пополнение топлива стоит {fuelCost} космотугриков. Вы получите {fuelAmount} единиц топлива.");
            Console.WriteLine("Вы хотите пополнить топливо? (1 - Да, 2 - Нет)");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                if (Player.Cosmocoins >= fuelCost)
                {
                    Player.Cosmocoins -= fuelCost;
                    Player.Fuel += fuelAmount;
                    Console.WriteLine($"Топливо пополнено! Теперь у вас {Player.Fuel} единиц топлива.");
                }
                else
                {
                    Console.WriteLine("У вас недостаточно космотугриков для пополнения топлива.");
                }
            }
            else
            {
                Console.WriteLine("Пополнение топлива отменено.");
            }
        }
        else
        {
            Console.WriteLine("Здесь нельзя пополнить топливо.");
        }
        WaitForInput();
    }

    private void UpdateGame()
    {
        if (Player.Fuel <= 0)
        {
            Console.WriteLine("У вас закончилось топливо. Игра окончена.");
            WaitForInput();
            Environment.Exit(0);
        }

        foreach (var planet in GalaxyMap.Planets)
        {
            planet.Update();
        }

        if (Random.Next(0, 1000) < 10)
        {
            BattleSystem.StartBattle(Player);
        }
    }
}