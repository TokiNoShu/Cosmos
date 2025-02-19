using System;
using System.Collections.Generic;

public class Player
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int Fuel { get; set; }
    public int Health { get; set; }
    public int Cosmocoins { get; set; }
    public Dictionary<string, string> Inventory { get; set; }
    public List<string> ActiveQuests { get; set; }
    public GalaxyMap GalaxyMap { get; set; }

    public Player(GalaxyMap galaxyMap)
    {
        PositionX = 0;
        PositionY = 0;
        Fuel = 100;
        Health = 100;
        Cosmocoins = 0;
        Inventory = new Dictionary<string, string>();
        ActiveQuests = new List<string>();
        GalaxyMap = galaxyMap;
    }

    public void Move(int dx, int dy)
    {
        int newX = PositionX + dx;
        int newY = PositionY + dy;

        if (newX >= 0 && newX < GalaxyMap.Width && newY >= 0 && newY < GalaxyMap.Height)
        {
            PositionX = newX;
            PositionY = newY;
            Fuel -= 1;
        }
        else
        {
            Console.WriteLine("Вы достигли границы галактики!");
        }
    }
}