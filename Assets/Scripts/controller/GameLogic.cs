using System;
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

    private GameLogicBoardData boardData;
    private GameLogicSelect boardSelect;

    private bool isSelectActive;


    public void Start()
    {
        boardData = new GameLogicBoardData();
        boardData.PrepareForReuse(gridWidth, gridHeight, minAdjacent2Win);

        boardViewManager.PrepareForReuse(boardData.dataGrid);

        boardSelect = new GameLogicSelect();
        boardSelect.PrepareForReuse(boardData.dataGrid, boardViewManager);

        StartCoroutine(PrepareForReuse());
    }

    public IEnumerator PrepareForReuse()
    {
        yield return StartCoroutine(EvaluateBoard());

        boardData.SetAnimateDisplayAction(true);

        isSelectActive = true;
    }

    public void Update()
    {
        if (!isSelectActive) return;

        isSelectActive = false;
        if (Input.GetMouseButtonDown(0))
        {
            GameLogicSelect.Status selectionStatus = boardSelect.UpdateSelectedStatusOnMouseButtonDown();
            switch (selectionStatus)
            {
                case GameLogicSelect.Status.noneSelected:
                    break;

                case GameLogicSelect.Status.oneSelected:
                    StartCoroutine(RedrawBoard());
                    break;

                case GameLogicSelect.Status.twoNotAdjacent:
                case GameLogicSelect.Status.sameSelected:
                    StartCoroutine(RedrawBoard());
                    boardSelect.Reset(); 
                    break;

                case GameLogicSelect.Status.twoAdjacent:
                    StartCoroutine(RedrawBoard());

                    bool isAdjacentExist = boardData.SwitchCells(boardSelect.selectedPosition1, boardSelect.selectedPosition2);
                    if (isAdjacentExist)
                    {
                        StartCoroutine(RedrawBoard());
                        StartCoroutine(EvaluateBoard());
                        boardSelect.Reset();
                    }
                    else
                    {
                        boardSelect.Reset();
                        StartCoroutine(RedrawBoard());
                    }

                    break;
            }
        }
        isSelectActive = true;
    }

    public IEnumerator RedrawBoard()
    {
        yield return StartCoroutine(boardViewManager.PerformAction());
    }

    public IEnumerator EvaluateBoard()
    {
        isSelectActive = false;
        bool isAdjacentExist = boardData.MarkEqualAdjacent(false);
        if (isAdjacentExist)
        {
            while (isAdjacentExist)
            {
                yield return StartCoroutine(boardViewManager.PerformAction());
                boardData.FillEmpty();
                yield return StartCoroutine(boardViewManager.PerformAction());
                isAdjacentExist = boardData.MarkEqualAdjacent(false);
            }
        }

        isSelectActive = true;
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