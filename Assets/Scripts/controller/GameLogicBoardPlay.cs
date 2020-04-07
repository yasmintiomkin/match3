using UnityEngine;
using UnityEditor;

public class GameLogicBoardPlay
{
    public CellData[,] dataGrid;
    public int width, height;
    public int minAdjacent2Win = 3;


    public void PrepareForReuse(int width, int height, int minAdjacent2Win)
    {
        this.width = width;
        this.height = height;
        this.minAdjacent2Win = minAdjacent2Win;

        dataGrid = new CellData[width, height]; 

        ConfigureEmptyBoardCells();
    }

    public void ConfigureEmptyBoardCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (dataGrid[x, y] == null)
                {
                    dataGrid[x, y] = new CellData();
                }

                if (dataGrid[x, y].IsEmpty())
                {
                    dataGrid[x, y].PrepareForReuse();
                    dataGrid[x, y].spriteId = UnityEngine.Random.Range(1, 4);
                }
                // else configured
            }
        }
    }

    public bool MarkEqualAdjacent()
    {
        bool isDetected = false;
        int numEqual;

        // detect per row
        for (int y = 0; y < height; y++)
        {
            numEqual = 1;

            for (int x = 1; x < width; x++)
            {

                if (IsEqualAdjacentRowCells(x, y))
                {
                    numEqual++;
                }
                else
                {
                    if (numEqual >= minAdjacent2Win)
                    {
                        isDetected = true;
                        MarkAdjacentRowCells(x - 1, y, numEqual);
                    }
                    // reset the counter
                    numEqual = 1;
                }

                // if last x cell, check again
                if (x == width - 1 && numEqual >= minAdjacent2Win)
                {
                    isDetected = true;
                    MarkAdjacentRowCells(x, y, numEqual);
                }
            }
        }

        // detect per column
        for (int x = 0; x < width; x++)
        {
            numEqual = 1;

            for (int y = 1; y < height; y++)
            {

                if (IsEqualAdjacentColumnCells(x, y))
                {
                    numEqual++;
                }
                else
                {
                    if (numEqual >= minAdjacent2Win)
                    {
                        isDetected = true;
                        MarkAdjacentColumnCells(x, y - 1, numEqual);
                    }
                    // reset the counter
                    numEqual = 1;
                }

                // if last y cell, check again
                if (y == height - 1 && numEqual >= minAdjacent2Win)
                {
                    isDetected = true;
                    MarkAdjacentColumnCells(x, y, numEqual);
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (dataGrid[x, y].isMatchAdjacent)
                {
                    dataGrid[x, y].SetEmpty();
                }
            }
        }

         return isDetected;
    }

    private bool IsEqualAdjacentRowCells(int x, int y)
    {
        return dataGrid[x, y].spriteId == dataGrid[x - 1, y].spriteId;
    }

    private bool IsEqualAdjacentColumnCells(int x, int y)
    {
        return dataGrid[x, y].spriteId == dataGrid[x, y - 1].spriteId;
    }

    private void MarkAdjacentRowCells(int endIndex, int y, int numAdjacent)
    {
        for (int x = endIndex; x > endIndex - numAdjacent; x--)
        {
             dataGrid[x, y].isMatchAdjacent = true;
        }
    }

    private void MarkAdjacentColumnCells(int x, int endIndex, int numAdjacent)
    {
        for (int y = endIndex; y > endIndex - numAdjacent; y--)
        {
            dataGrid[x, y].isMatchAdjacent = true;
        }
    }
}