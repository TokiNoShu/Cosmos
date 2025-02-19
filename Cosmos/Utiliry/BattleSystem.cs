using System;
using System.Collections.Generic;

public class BattleSystem
{
    private Random Random { get; set; }

    public BattleSystem()
    {
        Random = new Random();
    }

    private void WaitForInput()
    {
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey(true);
    }

    public void StartBattle(Player player)
    {
        var enemies = new List<Enemy>
        {
            new Enemy("Пираты", Random.Next(30, 50), Random.Next(10, 20))
        };

        while (enemies.Any(e => e.Health > 0) && player.Health > 0)
        {
            Console.WriteLine($"Ваше здоровье: {player.Health}");
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Health > 0)
                {
                    Console.WriteLine($"{i + 1} - {enemies[i].Type} (Здоровье: {enemies[i].Health})");
                }
            }

            string action = "";
            while (action != "1" && action != "2" && action != "3")
            {
                Console.WriteLine("Выберите действие: 1 - Стрелять, 2 - Использовать бомбу, 3 - Щит");
                action = Console.ReadLine();

                if (action != "1" && action != "2" && action != "3")
                {
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите 1, 2 или 3.");
                }
            }

            if (action == "1")
            {
                int target = -1;
                while (target < 1 || target > enemies.Count || enemies[target - 1].Health <= 0)
                {
                    Console.WriteLine("Выберите цель:");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out target))
                    {
                        if (target < 1 || target > enemies.Count)
                        {
                            Console.WriteLine("Неверный номер цели. Пожалуйста, выберите существующую цель.");
                        }
                        else if (enemies[target - 1].Health <= 0)
                        {
                            Console.WriteLine("Эта цель уже уничтожена. Выберите другую.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод. Пожалуйста, введите номер цели.");
                    }
                }

                int damage = Random.Next(10, 20);
                enemies[target - 1].Health -= damage;
                Console.WriteLine($"Вы нанесли {damage} урона {enemies[target - 1].Type}!");
                WaitForInput();
            }
            else if (action == "2")
            {
                if (player.Inventory.ContainsKey("Бомба"))
                {
                    foreach (var enemy in enemies)
                    {
                        enemy.Health -= 50;
                    }
                    player.Inventory.Remove("Бомба");
                    Console.WriteLine("Вы использовали бомбу и нанесли 50 урона всем врагам!");
                    WaitForInput();
                }
                else
                {
                    Console.WriteLine("У вас нет бомб.");
                    WaitForInput();
                }
            }
            else if (action == "3")
            {
                player.Health += 20;
                Console.WriteLine("Вы использовали щит и восстановили 20 здоровья!");
                WaitForInput();
            }

            foreach (var enemy in enemies)
            {
                if (enemy.Health > 0)
                {
                    player.Health -= enemy.Damage;
                    Console.WriteLine($"{enemy.Type} нанес вам {enemy.Damage} урона!");
                    WaitForInput();
                }
            }
        }

        if (player.Health > 0)
        {
            Console.WriteLine("Вы победили врагов!");
            WaitForInput();
        }
        else
        {
            Console.WriteLine("Вы погибли в бою. Игра окончена.");
            WaitForInput();
            Environment.Exit(0);
        }
    }
}