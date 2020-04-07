using System;
using UnityEngine;

public class GameLogicConfigure
{
    public int[,] dataGrid;
    public int width, height;
    public int minAdjacent2Win = 3;
    public BoardViewManager boardViewManager;
    public int numConfigurations;

    /*
public GameLogicConfigure(int width, int height, int minAdjacent2Win, BoardViewManager boardViewManager)
{
    this.width = width;
    this.height = height;
    this.minAdjacent2Win = minAdjacent2Win;
    this.boardViewManager = boardViewManager;

    dataGrid = new int[width, height]; // initial value is 0

    Configure(false);
}


public void SwitchCells(Vector2Int position1, Vector2Int position2)
{
    int val1 = dataGrid[position1.x, position1.y];
    int val2 = dataGrid[position2.x, position2.y];

    // switch values without switching display
    dataGrid[position1.x, position1.y] = val2;
    dataGrid[position2.x, position2.y] = val1;

    if (DetectedEqualAdjacent(true))
    {
        boardViewManager.Destroy(position1.x, position1.y, true);
        boardViewManager.Destroy(position2.x, position2.y, true);

        Configure(true);
    }
    else
    {
        // switch back
        dataGrid[position1.x, position1.y] = val1;
        dataGrid[position2.x, position2.y] = val2;
    }
}

void Configure(bool animated)
{
    numConfigurations++;
    if (numConfigurations > 100)
    {
        numConfigurations = 0;
        // TODO: handle since something went totally wrong. maybe restart the game
        return;
    }

    Debug.Log(string.Format("=========> numConfigurations {0}", numConfigurations));


    for (int x = 0; x < width; x++)
    {
        for (int y = height - 1; y >= 0; y--)
        {
            if (y > 0 && dataGrid[x, y] < 0)
            {
                for (int y1 = y - 1; y1 >= 0; y1--)
                {
                    if (dataGrid[x, y1] > 0)
                    {
                        dataGrid[x, y] = dataGrid[x, y1];
                        dataGrid[x, y1] = 0;
                        boardViewManager.Destroy(x, y1, true);
                        y = y1;
                    }
                }
                break;
            }
        }
    }
    PrintGrid();

    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            if (dataGrid[x, y] <= 0)
            {
                dataGrid[x, y] = UnityEngine.Random.Range(1, 5);
            }
            // else cell is configured
        }
    }
    PrintGrid();

    boardViewManager.Configure(dataGrid, animated);

    // clean board from adjacent cells
    if (DetectedEqualAdjacent(false))
    {
        DestroyEqualAdjacent(animated);
    }
}

private void PrintGrid()
{
    //string row;

    //for (int y = 0; y < height; y++)
    //{
    //    row = "";
    //    for (int x = 0; x < width; x++)
    //    {
    //        row = string.Format("{0},{1}", row, dataGrid[x, y]);
    //    }
    //    Debug.Log(string.Format("{0}", row));
    //}

    //Debug.Log(string.Format("\n"));
}

private bool DetectedEqualAdjacent(bool isSimulation)
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
                    MarkAdjacentRowCells(x, y, numEqual, isSimulation);
                }
                // reset the counter
                numEqual = 1;
            }

            // if last x cell, check again
            if (x == width - 1 && numEqual >= minAdjacent2Win)
            {
                isDetected = true;
                MarkAdjacentRowCells(x, y, numEqual, isSimulation);
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
                    MarkAdjacentColumnCells(x, y, numEqual, isSimulation);
                }
                // reset the counter
                numEqual = 1;
            }

            // if last y cell, check again
            if (y == height - 1 && numEqual >= minAdjacent2Win)
            {
                isDetected = true;
                MarkAdjacentColumnCells(x, y, numEqual, isSimulation);
            }
        }
    }

    return isDetected;
}

private bool IsEqualAdjacentRowCells(int x, int y)
{
    // we might have marked it when marking columns so ABS first
    return (Math.Abs(dataGrid[x, y]) == Math.Abs(dataGrid[x - 1, y]));
}

private bool IsEqualAdjacentColumnCells(int x, int y)
{
    // we might have marked it when marking columns so ABS first
    return (Math.Abs(dataGrid[x, y]) == Math.Abs(dataGrid[x, y - 1]));
}

private void MarkAdjacentRowCells(int endIndex, int y, int numAdjacent, bool isSimulation)
{
    if (isSimulation) return;

    for (int x = endIndex - numAdjacent + 1; x < numAdjacent; x++)
    {
        // mark adjacent x cells with minus and erase later
        // we might have marked it when marking columns so ABS first
        dataGrid[x, y] = -(Math.Abs(dataGrid[x, y]));
    }
}

private void MarkAdjacentColumnCells(int x, int endIndex, int numAdjacent, bool isSimulation)
{
    if (isSimulation) return;

    for (int y = endIndex - numAdjacent + 1; y < numAdjacent; y++)
    {
        // mark adjacent column cells with minus and erase later
        // we might have marked it when marking rows so ABS first
        dataGrid[x, y] = -(Math.Abs(dataGrid[x, y]));
    }
}

private void DestroyEqualAdjacent(bool animated)
{

    boardViewManager.DestroyEqualAdjacent(dataGrid, animated);

    // configure data for destroyed cells
    Configure(animated);
}

private void DropOverEmpty()
{

}

private void FillUpperxs()
{

}
*/
}
