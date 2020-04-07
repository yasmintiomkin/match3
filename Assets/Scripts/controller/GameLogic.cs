using System;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public BoardViewManager boardViewManager;

    [SerializeField]
    private int gridWidth = 4;

    [SerializeField]
    private int gridHeight = 4;

    [SerializeField]
    private int minAdjacent2Win = 3;

    //private GameLogicConfigure gameLogicConfigure;
    //private GameLogicSelect gameLogicSelect;

    private GameLogicBoardPlay boardPlay;

    private Vector2Int selectedPosition1, selectedPosition2;


    public void Start()
    {
        selectedPosition1 = new Vector2Int(-1, -1);
        selectedPosition2 = new Vector2Int(-1, -1);

        boardPlay = new GameLogicBoardPlay();
        boardPlay.PrepareForReuse(gridWidth, gridHeight, minAdjacent2Win);

        boardViewManager.PrepareForReuse(boardPlay.dataGrid);

       // gameLogicConfigure = new GameLogicConfigure(gridWidth, gridHeight, minAdjacent2Win, boardViewManager);
       // gameLogicSelect = new GameLogicSelect(gridWidth, gridHeight, gameLogicConfigure.dataGrid, boardViewManager);
    }

    public void Update()
    {
        if (boardPlay == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            boardPlay.MarkEqualAdjacent();
            boardViewManager.MarkEqualAdjacent();
        }
    }

    //public void Update()
    //{
    //    //if (gameLogicSelect == null || gameLogicConfigure == null) return;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        /*
    //        if (gameLogicSelect.IsAdjacentSelected(ref selectedPosition1, ref selectedPosition2))
    //        {
    //            gameLogicConfigure.SwitchCells(selectedPosition1, selectedPosition2);
    //        }
    //        */
    //    }
    //}
}