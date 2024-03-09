using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using IGMain;

public class IGBoardController : IGController
{

    private List<IGTile> _tileList;

    public override void ClearController()
    {
    }

    public override void FinalizeController()
    {
    }

    public override void InitializeController()
    {
        _tileList = _engine._tileController.TileList;
    }

    public override void UpdateController()
    {
        if (_engine._blockController.IsBlockMoving)
        {
            if(_engine._blockController.SelectedBlock != null)
            {

            }
        }

        var colideStr = "";
        for(int i =0; i < _tileList.Count; ++i)
        {
            if (_tileList[i].IsColide)
            {
                colideStr += $"{_tileList[i].Index.ToString()} , ";
            }
        }

        //Debug.LogError(colideStr);
    }

    public void PlaceBlockOnBoard(IGTile_Block block)
    {
        if (_engine._blockController.SelectedBlock != null)
        {
            if (_engine._blockController.SelectedBlock != block)
            {
                _engine._blockController.SelectedBlock.Initialize();
                block.Initialize();
                return;
            }

            if (_engine._blockController.CheckNearestTiles() == false)
            {
                block.Initialize();
                return;
            }

            block.PlaceBlockOnBoard();
        }
    }

    public bool CheckPlaceBlockOnBoard(IGTile_Block block)
    {

        return false;
    }
}
