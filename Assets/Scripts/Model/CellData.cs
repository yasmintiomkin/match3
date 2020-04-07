using UnityEngine;

public class CellData
{
    public int spriteId;
    public bool isMatchAdjacent;
    public bool isAnimating;
    public int dropHeight;

    public void PrepareForReuse()
    {
        spriteId = 0;
        isMatchAdjacent = false;
        isAnimating = false;
        dropHeight = 0;
    }

    public bool IsEmpty()
    {
        return spriteId <= 0;
    }

    public void SetEmpty()
    {
        spriteId = 0;
        isMatchAdjacent = false;
    }
}