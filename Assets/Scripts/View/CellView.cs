using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    public List<Sprite> spriteList = new List<Sprite>();

    private CellData cellData;
    private SpriteRenderer spriteRenderer;


    public void Init(CellData cellData)
    {
        this.cellData = cellData;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite()
    {
        int spriteIndex = cellData.spriteId - 1;
        if (spriteIndex < 0 || spriteIndex > spriteList.Count)
        {
            // TODO - delete animation
            gameObject.SetActive(false);
        }
        else
        {
            Sprite newSprite = spriteList[spriteIndex];
            spriteRenderer.sprite = newSprite;

            gameObject.SetActive(true);
        }
    }
}
