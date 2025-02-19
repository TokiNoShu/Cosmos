using System;
using System.Collections.Generic;

public class QuestSystem
{
    public List<Faction> Factions { get; set; }

    public QuestSystem()
    {
        Factions = new List<Faction>();
        InitializeFactions();
    }

    private void InitializeFactions()
    {
        var empire = new Faction("Галактическая Империя");
        empire.AddQuest("Собрать 3 кислотных кристалла с Меркурия");
        empire.AddQuest("Уничтожить пиратскую базу в туманности");
        empire.AddQuest("Доставить послание на Уран");
        empire.AddQuest("Привезти 2 марсианских камня с Марса");
        empire.AddQuest("Подавить мятеж на планете");

        var rebels = new Faction("Повстанцы");
        rebels.AddQuest("Уничтожить склад на Земле");
        rebels.AddQuest("Исследовать туманность рядом с Юпитером");
        rebels.AddQuest("Освободить пленных на Сатурне");
        rebels.AddQuest("Собрать 3 газовых конденсата с Юпитера");
        rebels.AddQuest("Уничтожить генератор щита на Нептуне");

        Factions.Add(empire);
        Factions.Add(rebels);
    }

    public void TakeQuest(Player player, Faction faction, int questIndex)
    {
        if (faction.CurrentQuest == null)
        {
            faction.CurrentQuest = faction.Quests[questIndex];
            player.ActiveQuests.Add(faction.CurrentQuest);
            Console.WriteLine($"Вы взяли задание: {faction.CurrentQuest}");
        }
        else
        {
            Console.WriteLine("У вас уже есть активное задание. Завершите его перед тем, как брать новое.");
        }
    }

    public void CompleteQuest(Player player, Faction faction)
    {
        if (faction.CurrentQuest != null)
        {
            Console.WriteLine($"Вы выполнили задание: {faction.CurrentQuest}");
            faction.LoyaltyLevel += 1;
            player.ActiveQuests.Remove(faction.CurrentQuest);
            faction.CurrentQuest = null;
        }
        else
        {
            Console.WriteLine("У вас нет активных заданий.");
        }
    }
}