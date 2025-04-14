using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using System.Reflection;

public class IGBoardManager : ManagerBase<IGBoardManager>
{
    private IGBoard _igBoard;


    public override void ClearManager()
    {
        Debug.Log($"{this.name} {MethodBase.GetCurrentMethod().Name}");
    }

    public override void FinalizeManager()
    {
        Debug.Log($"{this.name} {MethodBase.GetCurrentMethod().Name}");
    }

    public override void InitializeManager()
    {
        Debug.Log($"{this.name} {MethodBase.GetCurrentMethod().Name}");

        _igBoard = new IGBoard();
        _igBoard.Initialize();
    }

    public void RegisterBoardController(IGBoardController boardController)
    {
        boardController.SetBoard(_igBoard);
    }
}
