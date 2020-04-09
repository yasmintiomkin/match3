﻿using System;
using UnityEngine;
using System.Collections;

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

    private GameLogicBoardData boardData;

    private Vector2Int selectedPosition1, selectedPosition2;


    public void Start()
    {
        selectedPosition1 = new Vector2Int(-1, -1);
        selectedPosition2 = new Vector2Int(-1, -1);

        boardData = new GameLogicBoardData();
        boardData.PrepareForReuse(gridWidth, gridHeight, minAdjacent2Win);

        boardViewManager.PrepareForReuse(boardData.dataGrid);

        // gameLogicConfigure = new GameLogicConfigure(gridWidth, gridHeight, minAdjacent2Win, boardViewManager);
        // gameLogicSelect = new GameLogicSelect(gridWidth, gridHeight, gameLogicConfigure.dataGrid, boardViewManager);
    }

    public void Update()
    {
        if (boardData == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(EvaluateBoard());
        }
    }

    public IEnumerator EvaluateBoard()
    {
        bool isAdjacentExist = boardData.MarkEqualAdjacent();
        if (isAdjacentExist)
        {
            yield return StartCoroutine(boardViewManager.MarkEqualAdjacent());
            yield return new WaitForSeconds(1);

            Debug.Log("after coroutine");
            boardData.FillEmpty();
            boardViewManager.FillEmpty();
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