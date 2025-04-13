using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IGMain
{
    public class IGGameController : ControllerBase
    {

        public IGBlockController _blockController { private set; get; }

        public IGBoardController _boardController { private set; get; }

        public IGInputController _inputController {private set; get;}

        public override void ClearController()
        {
        }

        public override void FinalizeController()
        {
        }

        public override void InitializeController()
        {
            _boardController = CreateObj<IGBoardController>(this.transform, true, "InGame");
            _boardController.InitializeController();

            _inputController = CreateObj<IGInputController>(this.transform, true, "InGame");
            _inputController.InitializeController();
        }

        public override void UpdateController()
        {
        }


      



        public void RestartGame()
        {
            _blockController.ClearController();
            _boardController.ClearController();
            _inputController.ClearController();
            //StartGame();
        }

    }

}
