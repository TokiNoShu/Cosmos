using System;

public class Shop
{
    private void WaitForInput()
    {
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey(true);
    }

    public void ShowItems()
    {
        Console.WriteLine("Добро пожаловать в магазин!");
        Console.WriteLine("1 - Турбоускоритель (50 космотугриков)");
        Console.WriteLine("2 - Бомба (30 космотугриков)");
        Console.WriteLine("3 - Ремкомплект (20 космотугриков)");
        Console.WriteLine("4 - Улучшение брони (100 космотугриков)");
        Console.WriteLine("5 - Карта туманности (40 космотугриков)");
    }

    public void BuyItem(Player player, string item)
    {
        switch (item)
        {
            case "1":
                if (player.Cosmocoins >= 50)
                {
                    player.Cosmocoins -= 50;
                    player.Fuel += 50;
                    Console.WriteLine("Вы купили турбоускоритель!");
                }
                else
                {
                    Console.WriteLine("Недостаточно космотугриков.");
                }
                break;
            case "2":
                if (player.Cosmocoins >= 30)
                {
                    player.Cosmocoins -= 30;
                    player.Inventory["Бомба"] = "1";
                    Console.WriteLine("Вы купили бомбу!");
                }
                else
                {
                    Console.WriteLine("Недостаточно космотугриков.");
                }
                break;
            case "3":
                if (player.Cosmocoins >= 20)
                {
                    player.Cosmocoins -= 20;
                    player.Health += 50;
                    Console.WriteLine("Вы купили ремкомплект и восстановили 50 здоровья!");
                }
                else
                {
                    Console.WriteLine("Недостаточно космотугриков.");
                }
                break;
            case "4":
                if (player.Cosmocoins >= 100)
                {
                    player.Cosmocoins -= 100;
                    player.Health += 100;
                    Console.WriteLine("Вы улучшили броню и увеличили здоровье на 100!");
                }
                else
                {
                    Console.WriteLine("Недостаточно космотугриков.");
                }
                break;
            case "5":
                if (player.Cosmocoins >= 40)
                {
                    player.Cosmocoins -= 40;
                    player.Inventory["Карта туманности"] = "1";
                    Console.WriteLine("Вы купили карту туманности!");
                }
                else
                {
                    Console.WriteLine("Недостаточно космотугриков.");
                }
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }
        WaitForInput();
    }

    public void SellResources(Player player)
    {
        Console.WriteLine("Ваши ресурсы:");
        foreach (var resource in player.Inventory)
        {
            Console.WriteLine($"{resource.Key} - {resource.Value}");
        }
        Console.WriteLine("Выберите ресурс для продажи (или введите '0' для выхода):");
        string choice = Console.ReadLine();
        if (player.Inventory.ContainsKey(choice))
        {
            player.Cosmocoins += 20;
            player.Inventory.Remove(choice);
            Console.WriteLine($"Вы продали {choice} за 20 космотугриков.");
        }
        else
        {
            Console.WriteLine("Неверный выбор.");
        }
        WaitForInput();
    }

    public void UpgradeShip(Player player)
    {
        Console.WriteLine("1 - Турбоускоритель (50 космотугриков)");
        Console.WriteLine("2 - Улучшение брони (100 космотугриков)");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                if (player.Cosmocoins >= 50)
                {
                    player.Cosmocoins -= 50;
                    player.Fuel += 50;
                    Console.WriteLine("Вы купили турбоускоритель!");
                }
                else
                {
                    Console.WriteLine("Недостаточно космотугриков.");
                }
                break;
            case "2":
                if (player.Cosmocoins >= 100)
                {
                    player.Cosmocoins -= 100;
                    player.Health += 100;
                    Console.WriteLine("Вы улучшили броню!");
                }
                else
                {
                    Console.WriteLine("Недостаточно космотугриков.");
                }
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }
        WaitForInput();
    }
}