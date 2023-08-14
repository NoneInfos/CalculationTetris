using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
public class IGTileController : IGController
{
    //private const string T_INNER_ODD = "t_inner_odd";
    //private const string T_INNER_EVEN = "t_inner_even";
    public override void ClearController()
    {
        throw new System.NotImplementedException();
    }

    public override void FinalizeController()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeController()
    {
        IGTile tile = PoolManager.Instacne.Pop(ETileType.BG);
    }
}