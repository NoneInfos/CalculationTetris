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

            IGBoardManager.Instance.RegisterBoardController(_boardController);

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

        public bool CheckBlockPlacement(IGBlock block, Vector2Int gridPosition)
        {
            // 게임 컨트롤러가 보드 컨트롤러에 요청을 전달
            return _boardController.CanPlaceBlockAtPosition(block, gridPosition);
        }

    }

}
