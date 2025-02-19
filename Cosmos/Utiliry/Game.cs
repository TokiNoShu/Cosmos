namespace Cosmos.Utility
{
    class Game
    {
        private Dictionary<string, string> planets;
        private string currentPlanet;
        private int fuel;
        private int food;
        private int health;
        private List<string> inventory;
        private List<string> quests;

        public Game()
        {
            planets = new Dictionary<string, string>
            {
                {"Меркурий", "Вы на Меркурие. Здесь очень жарко и может пойти кислотный дождь."},
                {"Венера", "Вы на Венере. Тут жарче чем на любой другой планете." },
                {"Земля", "Вы на Земле. Здесь много ресурсов." },
                {"Марс", "Вы на марсе. Здесь холодно и пустынно." },
                {"Юпитер", "Вы на Юпитере. Здесь сильные бури." },
                {"Сатурн", "Вы на Сатурне. Тут красивые кольца:>"},
                {"Уран", "Вы на Уране. " },
                {"Нептун", "Вы на Нептуне. " }
            };

            currentPlanet = "Земля";
            fuel = 100;
            food = 50;
            health = 100;
            inventory = new List<string>();
            quests = new List<string>();
        }
        public void Start()
        {
            Console.WriteLine("Добро пожаловать в 'Космическе приключение'!");
            Console.WriteLine("Вы можете исследовать планеты: Земля, Марс, Юпитер");
            Console.WriteLine("Введите 'выход' для завершения игры.");

            while (true)
            {
                Console.WriteLine(planets[currentPlanet]);
                Console.WriteLine($"Топливо: {fuel}, Еда: {food}, Здоровье {health}");
                Console.WriteLine("Квесты: " + (quests.Count > 0 ? string.Join(", ", quests) : "Нет акивных квестов"));
                Console.Write("Что вы хотите сделать? ");
                string command = Console.ReadLine().ToLower();

                if (command == "выход")
                {
                    Console.WriteLine("Спасибо за игру");
                    break;
                }
                else if (command == "идти на Марс" && fuel >= 20)
                {
                    currentPlanet = "Марс";
                    fuel -= 20;
                    RandomEvent();
                }
                else if (command == "идти на Юпитер" && fuel >= 30)
                {
                    currentPlanet = "Юпитер";
                    fuel -= 30;
                    RandomEvent();
                }
                else if (command == "вернуться на Землю" && fuel >= 20)
                {
                    currentPlanet = "Земля";
                    fuel -= 20;
                    RandomEvent();
                }
                else if (command == "собрать ресурсы" && currentPlanet == "Земля")
                {
                    inventory.Add("ресурсы");
                    Console.WriteLine("Вы собрали ресурсы!");
                }
                else if (command == "поесть")
                {
                    if (food > 0)
                    {
                        int eatedFood = 100 - health;
                        if (food > eatedFood)
                            food -= eatedFood;
                        else food = 0; eatedFood = food;
                        Console.WriteLine($"Вы поели. Еда уменьшилась на {eatedFood}");
                    }
                    else Console.WriteLine("У вас нет еды");
                }
                else if (command == "сразиться с пиратами" && currentPlanet == "Марс")
                {
                    Battle();
                }
                else if (command == "взять квест" && currentPlanet == "Земля")
                {
                    quests.Add("Собрать 5 ресурсов");
                    Console.WriteLine("Вы получили квест: Собрать 5 ресурсв.");
                }
                else if (command == "проверить квесты")
                    CheckQuests();
                else Console.WriteLine("Неизвестная команда. Попробуйте снова.");
                if (fuel <= 0)
                {
                    Console.WriteLine("У вас закончилось топливо, вы теперь будете болтаться в космосе до самой смерти. Игра окончена");
                    break;
                }
                else if (health <= 0)
                {
                    Console.WriteLine("Вы умерли. Игра окончена");
                    break;

                }
            }
        }

        private void RandomEvent()
        {
            Random rand = new();
            int eventChanse = rand.Next(1, 101);
            if (eventChanse <= 20)
            {
                Console.WriteLine("Случайное событие! Вы нашли дополнительное топливо!");
                fuel += 20;
            }
            else if (eventChanse <= 40)
            {
                Console.WriteLine("Случайное событие! Вы столкнулись с космическими пиратами!");
                Battle();
            }
            else if (eventChanse <= 60)
            {
                Console.WriteLine("Случайное событие! Вы нашли еду!");
                food += 10;
            }
        }

        private void Battle()
        {
            Console.WriteLine("Вы вступили в бой с космическими пиратами!");
            Random rand = new();
            int pirateHealth = rand.Next(30, 100);

            while (pirateHealth > 0 && health > 0)
            {
                Console.WriteLine($"Ваше здоровье: {health}, Здоровье пиратов: {pirateHealth}");
                Console.WriteLine("Вы хотите атаковаь (введите 'атаковать') или сбежать (введите 'сбежать')? ");
                string action = Console.ReadLine().ToLower();

                if (action == "атаковать")
                {
                    int dmg = rand.Next(10, 21);
                    pirateHealth -= dmg;
                    Console.WriteLine($"Вы нанесли {dmg} урона пиратам!");
                }
                else if (action == "сбежать")
                {
                    int CTE = rand.Next(0, 3);
                    if (CTE == 0)
                        Console.WriteLine("Вы сбежали от пиратов!");
                    else Console.WriteLine("Вы не смогли сбежать от пиратов, придётся сражаться, или пробовать ещё раз");
                }
                else Console.WriteLine("Неизвестная команда. Попробуйте снова.");

                int piratedmg = rand.Next(10, 21);
                health -= piratedmg;
                Console.WriteLine($"Пираты нанесли вам {piratedmg} урона!");
            }
            if (health <= 0)
            {
                Console.WriteLine("Вы погибли в бою!");
            }
            else
            {
                Console.WriteLine("Вы победили пиратов и забрали их добычу");
                inventory.Add("добыча");
            }
        }

        private void CheckQuests()
        {
            for (int i = quests.Count - 1; i >= 0; i--)
            {
                if (quests[i] == "Собрать 5 ресурсов" && inventory.Count >= 5)
                {
                    Console.WriteLine("Вы завершили квест: Собрать 5 ресурсов!");
                    quests.RemoveAt(i);
                    inventory.Remove("ресурсы");
                    inventory.Remove("ресурсы");
                    inventory.Remove("ресурсы");
                    inventory.Remove("ресурсы");
                    inventory.Remove("ресурсы");
                }
            }
        }
    }
}
