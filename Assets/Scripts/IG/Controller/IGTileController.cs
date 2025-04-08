using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
public class IGTileController : ControllerBase
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
    }

    public override void UpdateController()
    {
        
    }
}