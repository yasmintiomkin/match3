using System;
using UnityEngine;

public class GameLogicSelect
{
    public int[,] dataGrid;
    public int width, height;
    public BoardViewManager boardViewManager;

    private bool ignoreOnUpdate;
    public Vector2Int selectedPosition1, selectedPosition2;

    /*
    public GameLogicSelect(int width, int height, int[,] dataGrid, BoardViewManager boardViewManager)
    {
        this.width = width;
        this.height = height;
        this.dataGrid = dataGrid;
        this.boardViewManager = boardViewManager;

        Reset();
    }

    public void Reset()
    {
        selectedPosition1 = new Vector2Int(-1, -1);
        selectedPosition2 = new Vector2Int(-1, -1);
        ignoreOnUpdate = false;
    }

    public bool IsAdjacentSelected(ref Vector2Int position1, ref Vector2Int position2)
    {
        //if (ignoreOnUpdate) return false;

        Vector2Int selectedPosition = boardViewManager.GetSelectedPosition();
        if (selectedPosition.x < 0) return false; // no selection identified

        if (selectedPosition1.x < 0) // not set
        {
            selectedPosition1 = selectedPosition;
        }
        else
        {
            if (IsEqual(selectedPosition, selectedPosition1)) return false;
            // TODO: same point selection. maybe unselect?
            
            ignoreOnUpdate = true;
            selectedPosition2 = selectedPosition;

            // check if the positions are adjacent
            if (IsAdjacent(selectedPosition1, selectedPosition2))
            {
                Debug.Log(string.Format("IS adjacent"));
                // TODO: animate switch and switch back
                position1 = selectedPosition1;
                position2 = selectedPosition2;
                Reset();
                return true;
            }
            else
            {
                // TODO: switch and recalculate
                // if no results then switch back
                Debug.Log(string.Format("NOT adjacent"));
                Reset();
                return false;
            }
        }

        return false;
    }

    private bool IsEqual(Vector2Int position1, Vector2Int position2)
    {
        return position1.x == position2.x && position1.y == position2.y;
    }

    private bool IsAdjacent(Vector2Int position1, Vector2Int position2)
    {
        // diagonal is OK
        int diffX = Math.Abs(position1.x - position2.x);
        int diffY = Math.Abs(position1.y - position2.y);
        return diffX <= 1 && diffY <= 1;
        //return (position1 - position2).magnitude > 1.0; // we don't mess with float. also the user can select the same point
    }
    */
}