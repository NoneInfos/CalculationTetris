using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using IGMain;
using DG.Tweening;

public class IGBoardController : IGController
{

    [SerializeField] private SpriteRenderer gridLineRenderer;


    private IGBoard _IGBoard;
    private int totalClearedLines = 0;
    private int totalClearedSquares = 0;
    public override void InitializeController()
    {
        _IGBoard = new IGBoard();

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
    

   

    public int GetTotalClearedLines()
    {
        return totalClearedLines;
    }

    public int GetTotalClearedSquares()
    {
        return totalClearedSquares;
    }



    public bool IsGameOver()
    {
        foreach (var block in _engine._blockController.AvailableBlocks)
        {
            for (int y = 0; y < IGConfig.BOARD_ROW; y++)
            {
                for (int x = 0; x < IGConfig.BOARD_COL; x++)
                {
                    if (_IGBoard.CanPlaceBlock(block, x, y))
                        return false;
                }
            }
        }
        return true;
    }

    public override void ClearController()
    {
    }

    public override void FinalizeController()
    {
    }

    public override void UpdateController()
    {
    }

    
}
