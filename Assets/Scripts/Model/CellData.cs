using UnityEngine;

public class CellData
{
    public bool animateAction;

    public int spriteId;
    public bool isMatchAdjacent;
    public int fallHeight;

    public  enum DisplayAction { generateSprite, animateDestroy, animateFall, none }

    public DisplayAction displayAction = DisplayAction.generateSprite;

    public void PrepareForReuse()
    {
        animateAction = true;

        spriteId = 0;
        isMatchAdjacent = false;
        fallHeight = 0;
        displayAction = DisplayAction.generateSprite;
    }

    public void PrepareForAction()
    {
        isMatchAdjacent = false;
        fallHeight = 0;
        displayAction = DisplayAction.none;
    }

    public bool IsEmpty()
    {
        return spriteId <= 0;
    }

    public void SetEmpty()
    {
        spriteId = 0;
        displayAction = DisplayAction.generateSprite;
    }
}