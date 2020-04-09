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

    public void PerformAction()
    {
        int spriteIndex = cellData.spriteId - 1;

        switch (cellData.displayAction)
        {
            case CellData.DisplayAction.none:
                break;

            case CellData.DisplayAction.generateSprite:
                GenerateSprite();
                break;

            case CellData.DisplayAction.animateDestroy:
                AnimateDestroy();
                break;

            case CellData.DisplayAction.animateFall:
                GenerateSprite();
                AnimateFall();
                break;

        }
    }

    private void GenerateSprite()
    {
        int spriteIndex = cellData.spriteId - 1;

        if (scale < 1.0f)
        {
            gameObject.transform.localScale = Vector3.one;
        }

        Sprite newSprite = spriteList[spriteIndex];
        spriteRenderer.sprite = newSprite;
    }

    private void AnimateDestroy()
    {
        if (!cellData.animateAction)
        {
            return;
        }

        isAnimating = true;

        scale = 0f;

        Hashtable hash = new Hashtable();

        hash.Add("x", scale);
        hash.Add("y", scale);
        hash.Add("easeType", "easeInOutExpo");
        hash.Add("time", 1);
        hash.Add("oncomplete", "OnCompleteAnimation");
        iTween.ScaleBy(gameObject, hash);
    }

    private void AnimateFall()
    {
        if (!cellData.animateAction)
        {
            return;
        }

        isAnimating = true;

        float origY = transform.position.y;

        // move to drop source without animation
        transform.position = new Vector3(transform.position.x, transform.position.y + cellData.fallHeight, transform.position.z);

        // animate drop to destination
        float fallDuration = 1;// cellData.fallHeight * 0.5f; // proportional to drop height
       
        Hashtable hash = new Hashtable();

        hash.Add("x", transform.position.x);
        hash.Add("y", origY);
        // transform.position.z
        //hash.Add("easeType", "easeInOutExpo");
        hash.Add("time", fallDuration);
        hash.Add("oncomplete", "OnCompleteAnimation");
        iTween.MoveTo(gameObject, hash);
    }

    private void OnCompleteAnimation()
    {
        isAnimating = false;
    }
}
