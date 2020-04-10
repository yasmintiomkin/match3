using UnityEngine;
using ExtensionMethods;

public class CellData
{
    public int spriteId;
    public bool isMatchAdjacent;
    public int fallDistance;
    public Vector2Int moveDistance;

    public enum DisplayAction { generateSprite, animateDestroy, animateFall, animateMove, animateSelect, animateUnselect, none }

    public bool animateDisplayAction;
    public DisplayAction displayAction = DisplayAction.generateSprite;

    public void PrepareForReuse()
    {
        animateDisplayAction = false;

        spriteId = 0;
        isMatchAdjacent = false;
        fallDistance = 0;
        moveDistance = new Vector2Int(0, 0);
        displayAction = DisplayAction.generateSprite;
    }

    public void PrepareForAction()
    {
        isMatchAdjacent = false;
        fallDistance = 0;
        moveDistance = new Vector2Int(0, 0);
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