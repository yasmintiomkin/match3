using UnityEngine;

public class CellData
{
    public int spriteId;
    public bool isMatchAdjacent;
    public int fallHeight;

    public void PrepareForReuse()
    {
        spriteId = 0;
        isMatchAdjacent = false;
        fallHeight = 0;
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

    public bool ShouldDrop()
    {
        return fallHeight > 0;
    }
}