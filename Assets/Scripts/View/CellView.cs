using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    public List<Sprite> spriteList = new List<Sprite>();

    private CellData cellData;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;

    public bool isAnimating;
    public float scale = 1f;



    public void Init(CellData cellData)
    {
        this.cellData = cellData;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = gameObject.transform.localScale;
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

            case CellData.DisplayAction.animateMove:
                GenerateSprite();
                AnimateMove();
                break;

            case CellData.DisplayAction.animateSelect:
                AnimateSelect();
                break;

            case CellData.DisplayAction.animateUnselect:
                AnimateUnselect();
                break;
        }
    }

    private void GenerateSprite()
    {
        int spriteIndex = cellData.spriteId - 1;

       if (scale != 1.0f)
        {
            gameObject.transform.localScale = originalScale;
            scale = 1.0f;
        }

        Sprite newSprite = spriteList[spriteIndex];
        spriteRenderer.sprite = newSprite;
    }

    private void AnimateSelect()
    {
        scale = 1.2f;
        AnimateResize(true);
    }

    private void AnimateUnselect()
    {
         scale = 1 / scale;
         AnimateResize(true);
    }

    private void AnimateDestroy()
    {
        scale = 0f;
        AnimateResize(true);
    }

    private void AnimateResize(bool notifiyAnimating)
    {
        if (!cellData.animateDisplayAction)
        {
            return;
        }

        if (notifiyAnimating) isAnimating = true;

        float time = 0.4f;

        Hashtable hash = new Hashtable();

        hash.Add("x", scale);
        hash.Add("y", scale);
        hash.Add("easeType", "easeOutSine");
        hash.Add("time", time);

        if (notifiyAnimating) hash.Add("oncomplete", "OnCompleteAnimation");
        
        iTween.ScaleBy(gameObject, hash);
    }

    private void AnimateMove()
    {
        if (!cellData.animateDisplayAction)
        {
            return;
        }

        isAnimating = true;

        float origY = transform.position.y;
        float origX = transform.position.x;

        // move to animation start without animation
        float moveFromX = origX + cellData.moveDistance.x;
        float moveFromY = origY + cellData.moveDistance.y;
        transform.position = new Vector3(moveFromX, moveFromY, transform.position.z);

        // animate to destination
        float time = 0.6f;// cellData.fallHeight * 0.4f; // proportional to drop height

        Hashtable hash = new Hashtable();

        hash.Add("x", origX);
        hash.Add("y", origY);
        // transform.position.z
        hash.Add("easeType", "easeOutElastic");
        hash.Add("time", time);
        hash.Add("oncomplete", "OnCompleteAnimation");
        iTween.MoveTo(gameObject, hash);
    }

    private void AnimateFall()
    {
        if (!cellData.animateDisplayAction)
        {
            return;
        }

        isAnimating = true;

        float origY = transform.position.y;
        float origX = transform.position.x;

        // move to animation start without animation
        float moveFromX = origX;
        float moveFromY = origY + cellData.fallDistance;
        transform.position = new Vector3(moveFromX, moveFromY, transform.position.z);

        // animate to destination
        float time = 0.6f;// cellData.fallHeight * 0.4f; // proportional to drop height
       
        Hashtable hash = new Hashtable();

        hash.Add("x", origX);
        hash.Add("y", origY);
        // transform.position.z
        hash.Add("easeType", "easeOutElastic");
        hash.Add("time", time);
        hash.Add("oncomplete", "OnCompleteAnimation");
        iTween.MoveTo(gameObject, hash);
    }

    private void OnCompleteAnimation()
    {
        isAnimating = false;
    }
}
