using System;
using System.Collections.Generic;
using System.Linq;

public class GalaxyMap
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<Planet> Planets { get; set; }
    public List<Asteroid> Asteroids { get; set; }
    public List<Nebula> Nebulas { get; set; }

    public GalaxyMap(int width, int height)
    {
        Width = width;
        Height = height;
        Planets = new List<Planet>();
        Asteroids = new List<Asteroid>();
        Nebulas = new List<Nebula>();
    }

    public void AddPlanet(Planet planet)
    {
        Planets.Add(planet);
    }

    public void AddAsteroid(Asteroid asteroid)
    {
        Asteroids.Add(asteroid);
    }

    public void AddNebula(Nebula nebula)
    {
        Nebulas.Add(nebula);
    }

    public Planet GetPlanet(string planetName)
    {
        return Planets.FirstOrDefault(p => p.Name == planetName);
    }

    public void ExploreNebula(Player player)
    {
        var nebula = Nebulas.FirstOrDefault(n => n.PositionX == player.PositionX && n.PositionY == player.PositionY);
        if (nebula != null && !nebula.IsExplored)
        {
            nebula.IsExplored = true;
            Console.WriteLine("Вы исследовали туманность и нашли редкие ресурсы!");
            player.Inventory["Карта туманности"] = "1";
        }
        else
        {
            Console.WriteLine("Здесь нет туманности или она уже исследована.");
        }
    }

    public void Draw(Player player)
    {
        Console.Clear();

        Console.Write("┌");
        Console.Write(new string('─', Width));
        Console.WriteLine("┐");

        for (int y = 0; y < Height; y++)
        {
            Console.Write("│");

            for (int x = 0; x < Width; x++)
            {
                if (x == player.PositionX && y == player.PositionY)
                {
                    Console.Write("■");
                }
                else if (Planets.Any(p => p.PositionX == x && p.PositionY == y))
                {
                    var planet = Planets.First(p => p.PositionX == x && p.PositionY == y);
                    Console.Write(planet.Symbol);
                }
                else if (Asteroids.Any(a => a.PositionX == x && a.PositionY == y))
                {
                    Console.Write("O");
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.Write("│");

            if (y == 0)
            {
                Console.Write("    Легенда:           Управление:");
            }
            else if (y == 1)
            {
                Console.Write("    ■ - Игрок         ← → ↑ ↓ - Движение");
            }
            else if (y == 2)
            {
                Console.Write("    M - Меркурий      E - Взаимодействие");
            }
            else if (y == 3)
            {
                Console.Write("    V - Венера        S - Магазин");
            }
            else if (y == 4)
            {
                Console.Write("    E - Земля         N - Исследовать туманность");
            }
            else if (y == 5)
            {
                Console.Write("    R - Марс          T - Продать ресурсы");
            }
            else if (y == 6)
            {
                Console.Write("    J - Юпитер        U - Улучшить корабль");
            }
            else if (y == 7)
            {
                Console.Write("    S - Сатурн        Q - Выход");
            }
            else if (y == 8)
            {
                Console.Write("    U - Уран");
            }
            else if (y == 9)
            {
                Console.Write("    N - Нептун");
            }
            else if (y == 10)
            {
                Console.Write("    P - База повстанцев");
            }
            else if (y == 11)
            {
                Console.Write("    O - Астероид");
            }

            Console.WriteLine();
        }

        Console.Write("└");
        Console.Write(new string('─', Width));
        Console.WriteLine("┘");

        Console.WriteLine($"Топливо: {player.Fuel}, Здоровье: {player.Health}, Космотугрики: {player.Cosmocoins}");
    }
}