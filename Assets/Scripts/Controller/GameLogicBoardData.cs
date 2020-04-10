using UnityEngine;

public class GameLogicBoardData
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

        PrepareBoardCells();
    }

    public void PrepareBoardCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (dataGrid[x, y] == null)
                {
                    dataGrid[x, y] = new CellData();
                }

                dataGrid[x, y].PrepareForReuse();
                dataGrid[x, y].spriteId = SpriteGenerator();
            }
        }
    }

    public void SetAnimateDisplayAction(bool isActive)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                dataGrid[x, y].animateDisplayAction = isActive;
            }
        }
    }

    private int SpriteGenerator()
    {
        return UnityEngine.Random.Range(2, 6);
    }

    private void PrepareForAction()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                dataGrid[x, y].PrepareForAction();
            }
        }
    }

    public bool SwitchCells(Vector2Int position1, Vector2Int position2)
    {
        PrepareForAction();

        int val1 = dataGrid[position1.x, position1.y].spriteId;
        int val2 = dataGrid[position2.x, position2.y].spriteId;

        // switch values without switching display
        dataGrid[position1.x, position1.y].spriteId = val2;
        dataGrid[position2.x, position2.y].spriteId = val1;

        bool isAdjacentExist = MarkEqualAdjacent(true);

        if (isAdjacentExist)
        {
            dataGrid[position1.x, position1.y].moveDistance = position2 - position1;
            dataGrid[position1.x, position1.y].displayAction = CellData.DisplayAction.animateMove;
            dataGrid[position2.x, position2.y].moveDistance = position1 - position2;
            dataGrid[position2.x, position2.y].displayAction = CellData.DisplayAction.animateMove;

            return true;
        }
        else
        {
            // switch back
            dataGrid[position1.x, position1.y].spriteId = val1;
            dataGrid[position2.x, position2.y].spriteId = val2;
            return false;
        }
    }
    public bool MarkEqualAdjacent(bool isSimulation)
    {
        PrepareForAction();

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
                        if (isSimulation) return true;

                        isDetected = true;
                        MarkAdjacentRowCells(x - 1, y, numEqual);
                    }
                    // reset the counter
                    numEqual = 1;
                }

                // if last x cell, check again
                if (x == width - 1 && numEqual >= minAdjacent2Win)
                {
                    if (isSimulation) return true;

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
                        if (isSimulation) return true;

                        isDetected = true;
                        MarkAdjacentColumnCells(x, y - 1, numEqual);
                    }
                    // reset the counter
                    numEqual = 1;
                }

                // if last y cell, check again
                if (y == height - 1 && numEqual >= minAdjacent2Win)
                {
                    if (isSimulation) return true;

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
                    dataGrid[x, y].displayAction = CellData.DisplayAction.animateDestroy;
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

    public void FillEmpty()
    {
        PrepareForAction();

        for (int x = 0; x < width; x++)
        {
            FillEmptyInColumn(x);
        }
    }


    private void FillEmptyInColumn(int x)
    {
        // go over column bottom up and calculate the number of cells each cell needs to drop
        
        int firstEmptyY = -1;
        for (int y = height - 1; y >= 0; y--)
        {
            if (dataGrid[x, y].IsEmpty())
            {
                if (firstEmptyY < 0)
                {
                    firstEmptyY = y;
                }
             }
            else
            {
                if (firstEmptyY >= 0)
                {
                    // we're at a non-empty cell with previous empty cells
                    // drop it down to the first empty cell
                    dataGrid[x, firstEmptyY].spriteId = dataGrid[x, y].spriteId;
                    dataGrid[x, firstEmptyY].fallDistance = firstEmptyY - y;
                    dataGrid[x, firstEmptyY].displayAction = CellData.DisplayAction.animateFall;
                    dataGrid[x, y].SetEmpty();

                    // now firstEmptyY is one above 
                    firstEmptyY--;
                }
            }
        }

        // fill top empty cells
        if (dataGrid[x, 0].IsEmpty())
        {
            if (firstEmptyY < 0)
            {
                // if the top cell is the only empty cell
                // then firstEmptyY is -1 so correct it
                firstEmptyY = 0;
            }

            // all top cells drop the same height since they 
            // fall from above
            int topCellsDrop = firstEmptyY + 1;
            for (int yTop = firstEmptyY; yTop >= 0; yTop--)
            {
                dataGrid[x, yTop].fallDistance = topCellsDrop;
                dataGrid[x, yTop].spriteId = SpriteGenerator();
                dataGrid[x, yTop].displayAction = CellData.DisplayAction.animateFall;
            }
        }
    }
}