using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using IGMain;
using DG.Tweening;

public class IGBoardController : ControllerBase
{

    [SerializeField] private SpriteRenderer gridLineRenderer;

    private IGBoard _board;


    private int totalClearedLines = 0;
    private int totalClearedSquares = 0;
    public override void InitializeController()
    {

        //ApplyTheme(ThemeManager.Instance.CurrentTheme);
        ThemeManager.Instance.OnThemeChanged += ApplyTheme;
    }

    private void OnDestroy()
    {
        ThemeManager.Instance.OnThemeChanged -= ApplyTheme;
    }

    private void ApplyTheme(Theme theme)
    {
        //gridLineRenderer.color = theme.gridLineColor;
        //foreach (var tile in _IGBoard)
        //{
        //    tile.GetComponent<SpriteRenderer>().color = theme.backgroundColor;
        //}
    }


    public void SetBoard(IGBoard board)
    {
        _board = board;
    }


    public int GetTotalClearedLines()
    {
        return totalClearedLines;
    }

    public int GetTotalClearedSquares()
    {
        return totalClearedSquares;
    }



    //public bool IsGameOver()
    //{
    //    foreach (var block in _engine._blockController.AvailableBlocks)
    //    {
    //        for (int y = 0; y < IGConfig.BOARD_ROW; y++)
    //        {
    //            for (int x = 0; x < IGConfig.BOARD_COL; x++)
    //            {
    //                if (_IGBoard.CanPlaceBlock(block, x, y))
    //                    return false;
    //            }
    //        }
    //    }
    //    return true;
    //}

    public override void ClearController()
    {
    }

    public override void FinalizeController()
    {
    }

    public override void UpdateController()
    {
    }

    public bool CanPlaceBlockAtPosition(IGBlock block, Vector2Int boardPosition)
    {
        // 여기서 보드 모델에 접근하여 충돌 체크
        return _board.CanPlaceBlock(block, boardPosition);
    }

}
