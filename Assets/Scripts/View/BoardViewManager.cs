using UnityEngine;
using System.Collections;

public class BoardViewManager : MonoBehaviour
{
    public CellView cellPF;

    private int width, height;
    private CellData[,] dataGrid;
    private CellView[,] viewGrid;

    public void PrepareForReuse(CellData[,] dataGrid)
    {
        this.dataGrid = dataGrid;
        width = dataGrid.GetLength(0);
        height = dataGrid.GetLength(1);

        // TODO: if previously created, delete cells?? 

        viewGrid = new CellView[width, height];

        PrepareBoardCells();
    }

    private void PrepareBoardCells()
    {
        int offsetX = width / 2;
        int offsetY = height / 2;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (viewGrid[x, y] == null)
                {
                    // cellView was never created 
                    Vector3 position = new Vector3(x - offsetX, -y + offsetY, 0);
                    CellView view = Instantiate<CellView>(cellPF, position, Quaternion.identity);
                    viewGrid[x, y] = view;
                    view.Init(dataGrid[x, y]);
                }

                viewGrid[x, y].PerformAction();
            }
        }
    }

    public IEnumerator PerformAction()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                    viewGrid[x, y].PerformAction();
            }
        }

        // return control only when all animations have completed
        bool continueLooping = true;
        int count = 0;
        while (continueLooping) 
        {
            // wait one frame
            yield return null;

            continueLooping = false;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (viewGrid[x, y].isAnimating)
                    {
                        continueLooping = true;
                        break;
                    }
                }

                if (continueLooping)
                {
                    break; // no need to go over the rest of the x values. 
                }
            }

            count++;
            Debug.Log("bvm MarkEqualAdjacent continuelooping - " + count);
        } 

        Debug.Log("bvm MarkEqualAdjacent 3");

    }

    public void FillEmpty()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //if (dataGrid[x, y].fallHeight > 0)
                //{
                    viewGrid[x, y].PerformAction();
                //}
            }
        }
    }

        /*
        public void Configure(int[,] dataGrid, bool animated)
        {
            width = dataGrid.GetLength(0);
            height = dataGrid.GetLength(1);

            if (viewGrid == null)
            {
                // first time creation
                viewGrid = new CellView[width, height];
            }

            int offsetX = width / 2;
            int offsetY = height / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (viewGrid[x, y] == null) // initial value is null
                    {
                        Vector3 position = new Vector3(x - offsetX, -y + offsetY, 0);
                        CellView view = Instantiate<CellView>(cellPF, position, Quaternion.identity);
                        view.Init();
                        view.SetType(dataGrid[x, y]);
                        viewGrid[x, y] = view;
                    }
                    // else cell is configured
                    //Debug.Log(string.Format("[{0},{1}] = {2}, {3}", x, y, dataGrid[x, y], viewGrid[x, y].transform.position));
                }
            }
        }

        public void DestroyEqualAdjacent(int[,] dataGrid, bool animated)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (dataGrid[x, y] < 0)
                    {
                        Destroy(viewGrid[x, y]);
                        viewGrid[x, y] = null;
                    }
                }
            }
        }

        public void Destroy(int x, int y, bool animated)
        {
            Destroy(viewGrid[x, y]);
            viewGrid[x, y] = null;
        }

        public Vector2Int GetSelectedPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (viewGrid[x, y].GetComponent<Collider2D>().OverlapPoint(wp))
                        {
                            //Debug.Log(string.Format("wp {2},{3} collide cell {0},{1}", x, y, wp.x, wp.y));
                            return new Vector2Int(x, y);
                        }
                    }

                }
            }

            return new Vector2Int(-1, -1);
        }
        */
    }
