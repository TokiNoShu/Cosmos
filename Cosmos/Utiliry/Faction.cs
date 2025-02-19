using System;
using System.Collections.Generic;

public class Faction
{
    public string Name { get; set; }
    public int LoyaltyLevel { get; set; }
    public List<string> Quests { get; set; }
    public string CurrentQuest { get; set; }

    public Faction(string name)
    {
        Name = name;
        LoyaltyLevel = 0;
        Quests = new List<string>();
        CurrentQuest = null;
    }

    public void AddQuest(string quest)
    {
        Quests.Add(quest);
    }

    public string GetLoyaltyBar()
    {
        int barLength = 10;
        int filled = (LoyaltyLevel * barLength) / 100;
        int empty = barLength - filled;

        return new string('█', filled) + new string('░', empty);
    }
}