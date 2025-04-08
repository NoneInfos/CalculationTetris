using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGMain
{
    public class IGGameManager : ManagerBase<IGGameManager>
    {
        public IGGameController _gameController { private set; get; }

        public IGInputController _inputController { private set; get; }

        private bool _isGameOver = false;


        public override void ClearManager()
        {
        }

        public override void FinalizeManager()
        {
        }

        public override void InitializeManager()
        {
            _gameController = CreateObj<IGGameController>(this.transform, true, "InGame");
            //_gameController.SetEngine(this);
            _gameController.InitializeController();

           
        }

      
    }

}
