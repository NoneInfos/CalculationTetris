using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using IGMain;

public class IGBoardController : IGController
{
    private Dictionary<Vector2Int, IGTile> _board;

    public override void ClearController()
    {
    }

    public override void FinalizeController()
    {
    }

    public override void InitializeController()
    {
        for (int y = 0; y < IGConfig.BOARD_ROW; ++y)
        {
            for (int x = 0; x < IGConfig.BOARD_COL; ++x)
            {
                var _tile_BG = PoolManager.Instacne.Pop(ETileType.BG);
                _tile_BG.transform.parent = this.gameObject.transform;
                _tile_BG.name = $"BG {x} {y}";
                IGTile tile = _tile_BG.GetComponent<IGTile>();

                tile.SetIndex(int.Parse($"{x}{y}"));
                tile.SetPos(new Vector2(x * IGConfig.TILE_WIDTH, y * IGConfig.TILE_HEIGHT));
                tile.SetUI();

                if (_board == null)
                    _board = new Dictionary<Vector2Int, IGTile>();

                _board[new Vector2Int(x, y)] = tile;
            }
        }
    }

    public override void UpdateController()
    {
        if (_engine._blockController.IsBlockMoving)
        {
            if(_engine._blockController.SelectedBlock != null)
            {

            }
        }
    }

    public void PlaceBlockOnBoard(IGBlock block)
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

        }
    }

    public bool CheckPlaceBlockOnBoard(IGBlock block)
    {

        return false;
    }
}
