using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    public List<Sprite> spriteList = new List<Sprite>();

    private CellData cellData;
    private SpriteRenderer spriteRenderer;

    public bool isAnimating;
    public float scale = 1f;

    public void Init(CellData cellData)
    {
        this.cellData = cellData;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite()
    {
        int spriteIndex = cellData.spriteId - 1;
        if (spriteIndex < 0 || spriteIndex > spriteList.Count - 1)
        {
            AnimateDestroy();
        }
        else
        {
            if (scale < 1.0f)
            {
                gameObject.transform.localScale = Vector3.one;
            }
           
            Sprite newSprite = spriteList[spriteIndex];
            spriteRenderer.sprite = newSprite;

            if (cellData.fallHeight > 0)
            {
                AnimateFall();
            }
        }
    }

    private void AnimateDestroy()
    {
        isAnimating = true;

        Hashtable hash = new Hashtable();

        scale = 0f;
        hash.Add("x", scale);
        hash.Add("y", scale);
        hash.Add("easeType", "easeInOutExpo");
        hash.Add("time", 1);
        hash.Add("oncomplete", "OnCompleteAnimation");
        iTween.ScaleBy(gameObject, hash);
    }

    private void AnimateFall()
    {
        isAnimating = true;

        float origY = transform.position.y;

        // move to drop source without animation
        transform.position = new Vector3(transform.position.x, transform.position.y + cellData.fallHeight, transform.position.z);

        // animate drop to destination
        float fallDuration = 1;// cellData.fallHeight * 0.5f; // proportional to drop height
        iTween.MoveTo(gameObject, new Vector3(transform.position.x, origY, transform.position.z), fallDuration);
    }

    private void OnCompleteAnimation()
    {
        isAnimating = false;
    }
}
