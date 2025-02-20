using System;
using System.Collections.Generic;

public class Planet
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Faction { get; set; }
    public List<string> Resources { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public char Symbol { get; set; }

    public int ResourceRegenerationTime { get; set; }
    private int _currentRegenerationTime;

    public Planet(string name, string description, string faction, List<string> resources, int posX, int posY, char symbol, int regenerationTime = 5)
    {
        Name = name;
        Description = description;
        Faction = faction;
        Resources = resources;
        PositionX = posX;
        PositionY = posY;
        Symbol = symbol;
        ResourceRegenerationTime = regenerationTime;
        _currentRegenerationTime = 0;
    }

    public void Update()
    {
        if (Resources.Count == 0 && _currentRegenerationTime < ResourceRegenerationTime)
        {
            _currentRegenerationTime++;
        }
        else if (Resources.Count == 0 && _currentRegenerationTime >= ResourceRegenerationTime)
        {
            Resources.Add("Ресурс восстановлен");
            _currentRegenerationTime = 0;
            Console.WriteLine($"Ресурсы на планете {Name} восстановлены!");
        }
    }
}