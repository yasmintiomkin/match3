using System;
using UnityEngine;
using ExtensionMethods;

public class GameLogicSelect
{
    public BoardViewManager boardViewManager;
    public CellData[,] dataGrid;
    public int width, height;

    public enum Status { noneSelected, oneSelected, sameSelected, twoAdjacent, twoNotAdjacent }

    public Vector2Int selectedPosition1, selectedPosition2;


    public void PrepareForReuse(CellData[,] dataGrid, BoardViewManager boardViewManager)
    {
        this.dataGrid = dataGrid;
        width = dataGrid.GetLength(0);
        height = dataGrid.GetLength(1);

        this.boardViewManager = boardViewManager;

        selectedPosition1 = new Vector2Int(-1, -1);
        selectedPosition2 = new Vector2Int(-1, -1);

        Reset();
    }

    public void Reset()
    {
        if (selectedPosition1.IsSet())
        {
            dataGrid[selectedPosition1.x, selectedPosition1.y].displayAction = CellData.DisplayAction.none;
        }

        if (selectedPosition2.IsSet())
        {
            dataGrid[selectedPosition2.x, selectedPosition2.y].displayAction = CellData.DisplayAction.none;
        }

        selectedPosition1.UnSet();
        selectedPosition2.UnSet();
    }

   public Status UpdateSelectedStatusOnMouseButtonDown()
   {
        bool isSelected = false;
        Vector2Int selectedPosition = boardViewManager.GetSelectedPosition(ref isSelected);
        if (!isSelected) return Status.noneSelected;

        if (!selectedPosition1.IsSet())
        {
            selectedPosition1 = selectedPosition;
            dataGrid[selectedPosition1.x, selectedPosition1.y].displayAction = CellData.DisplayAction.animateSelect;
            Debug.Log(string.Format("SELECT one"));
            return Status.oneSelected;
        }
        else
        {
            if (selectedPosition.Equals(selectedPosition1))
            {
                // if same cell was selected, unselect it
                dataGrid[selectedPosition1.x, selectedPosition1.y].displayAction = CellData.DisplayAction.animateUnselect;
                Debug.Log(string.Format("SELECT same"));
                return Status.sameSelected;
            }

            // a new cell was selected
            selectedPosition2 = selectedPosition;

            // check if the positions are adjacent
            if (selectedPosition1.IsAdjacent(selectedPosition2))
            {
                dataGrid[selectedPosition1.x, selectedPosition1.y].displayAction = CellData.DisplayAction.none;
                //dataGrid[selectedPosition2.x, selectedPosition2.y].displayAction = CellData.DisplayAction.animateSelect;
                Debug.Log(string.Format("SELECT two IS adjacent"));
                return Status.twoAdjacent;
            }
            else
            {
                dataGrid[selectedPosition1.x, selectedPosition1.y].displayAction = CellData.DisplayAction.animateUnselect;
                dataGrid[selectedPosition2.x, selectedPosition2.y].displayAction = CellData.DisplayAction.none;
                Debug.Log(string.Format("SELECT two NOT adjacent"));
                return Status.twoNotAdjacent;
            }
        }
    }
}

namespace ExtensionMethods
{
    public static class Vector2IntExtensions
    {
        public static void UnSet(this ref Vector2Int vector)
        {
            vector.x = -1;
            vector.y = -1;
        }

        public static bool IsSet(this Vector2Int vector)
        {
            return vector.x >= 0;
        }

        public static bool IsAdjacent(this Vector2Int position1, Vector2Int position2)
        {
            // diagonal is OK
            int diffX = Math.Abs(position1.x - position2.x);
            int diffY = Math.Abs(position1.y - position2.y);
            return diffX <= 1 && diffY <= 1;
        }
    }
}