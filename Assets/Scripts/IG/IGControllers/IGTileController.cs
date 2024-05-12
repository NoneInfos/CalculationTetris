using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
public class IGTileController : IGController
{
    private List<IGTile> _tileList = new List<IGTile>();
    public List<IGTile> TileList { get { return _tileList; }  }

    public override void ClearController()
    {
    }

    public override void FinalizeController()
    {
    }

    public override void InitializeController()
    {
        for(int y = 0; y < 9; ++y)
        {
            for(int x = 0; x < 9; ++x)
            {
                var _tile_BG = PoolManager.Instacne.Pop(ETileType.BG);
                _tile_BG.transform.parent = this.gameObject.transform;
                IGTile tile = _tile_BG.GetComponent<IGTile>();

                _tileList.Add(tile);
                tile.SetIndex(int.Parse($"{x}{y}"));
                tile.SetPos(new Vector2(x * IGConfig.TILE_WIDTH, y * IGConfig.TILE_HEIGHT));
                tile.SetUI();
            }
        }
    }

    public override void UpdateController()
    {
        
    }
}