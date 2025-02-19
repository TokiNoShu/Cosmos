public class Nebula
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public bool IsExplored { get; set; }

    public Nebula(int posX, int posY)
    {
        PositionX = posX;
        PositionY = posY;
        IsExplored = false;
    }
}