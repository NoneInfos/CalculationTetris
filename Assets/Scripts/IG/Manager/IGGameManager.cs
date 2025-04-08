using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGMain
{
    public class IGGameManager : ManagerBase<IGGameManager>
    {
        private IGBlockManager _blockManager;

        private IGBoardManager _boardManager;


        public override void InitializeManager()
        {
            _blockManager = IGBlockManager.Instance;
            _boardManager = IGBoardManager.Instance;

           _blockManager.InitializeManager();
           _boardManager.InitializeManager();
        }
        public override void ClearManager()
        {
            _blockManager.ClearManager();
            _boardManager.ClearManager();
        }

        public override void FinalizeManager()
        {
            _blockManager.FinalizeManager();
            _boardManager.FinalizeManager();
        }

        

      
    }

}
