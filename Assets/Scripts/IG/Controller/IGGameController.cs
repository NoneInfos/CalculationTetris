using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IGMain
{
    public class IGGameController : ControllerBase
    {
        public override void ClearController()
        {
        }

        public override void FinalizeController()
        {
        }

        public override void InitializeController()
        {
            _boardController = CreateObj<IGBoardController>(this.transform, true, "InGame");
            //_boardController.SetEngine(this);
            _boardController.InitializeController();
        }

        public override void UpdateController()
        {
        }


        public IGBlockController _blockController { private set; get; }

        public IGBoardController _boardController { private set; get; }



        public void RestartGame()
        {
            _blockController.ClearController();
            _boardController.ClearController();
            //StartGame();
        }

    }

}
