using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGBoardController : IGController
{
    public override void ClearController()
    {
    }

    public override void FinalizeController()
    {
    }

    public override void InitializeController()
    {
        var tileList = _engine._tileController.TileList;
    }

    public override void UpdateController()
    {
    }
}
