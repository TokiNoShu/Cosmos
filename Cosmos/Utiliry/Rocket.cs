namespace Cosmos.Utility
{
    class Rocket
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Fuel { get; set; } = 100;
        private int fuelCounter = 0;
        private const int WORLD_SIZE = 100;

        public Rocket()
        {
            PositionX = 5;
            PositionY = 5;
        }

        public void MoveLeft(List<Asteroides> asteroides)
        {
            PositionX = (PositionX - 1 + WORLD_SIZE) % WORLD_SIZE;
            if (IsCollision(PositionX, PositionY, asteroides))
            {
                PositionX = (PositionX + 1) % WORLD_SIZE;
            }
        }

        public void MoveRight(List<Asteroides> asteroides)
        {
            PositionX = (PositionX + 1) % WORLD_SIZE;
            if (IsCollision(PositionX, PositionY, asteroides))
            {
                PositionX = (PositionX - 1 + WORLD_SIZE) % WORLD_SIZE;
            }
        }

        public void MoveUp(List<Asteroides> asteroides)
        {
            PositionY = (PositionY - 1 + WORLD_SIZE) % WORLD_SIZE;
            if (IsCollision(PositionX, PositionY, asteroides))
            {
                PositionY = (PositionY + 1) % WORLD_SIZE;
            }
        }

        public void MoveDown(List<Asteroides> asteroides)
        {
            PositionY = (PositionY + 1) % WORLD_SIZE;
            if (IsCollision(PositionX, PositionY, asteroides))
            {
                PositionY = (PositionY - 1 + WORLD_SIZE) % WORLD_SIZE;
            }
        }

        public bool IsCollision(int x, int y, List<Asteroides> asteroides)
        {
            return asteroides.Any(asteroid => asteroid.PositionX == x && asteroid.PositionY == y);
        }

        public void UpdateFuel()
        {
            fuelCounter++;
            if (fuelCounter >= 20)
            {
                Fuel -= 1;
                fuelCounter = 0;
            }

            if (Fuel <= 0)
            {
                Console.WriteLine("У вас закончилось топливо, вы теперь будете болтаться в космосе до самой смерти. Игра окончена");
                Environment.Exit(0);
            }
        }
        public void CollectResources(string location, Inventory inventory)
        {
            if (location == "пещера")
            {
                inventory.AddItem("камень", 1);
                Console.WriteLine("Вы собрали камень!");
            }
            else if (location == "лес")
            {
                inventory.AddItem("палка", 1);
                Console.WriteLine("Вы собрали палку!");
            }
            else if (location == "пляж")
            {
                inventory.AddItem("кокос", 1);
                Console.WriteLine("Вы собрали кокос!");
            }
            else
            {
                Console.WriteLine("Здесь нечего собирать.");
            }
        }
    }
}