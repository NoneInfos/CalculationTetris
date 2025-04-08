using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;

namespace IGMain
{
    public class IGBlock : IGObject
    {    
        private List<IGBlockTile> _blockTileList;

        private BlockShape _currentShape;

        private int[,] blockType = IGConfig.IBlock;

        private Vector2 initialPosition = Vector2.zero;

        public override void Initialize()
        {
            base.Initialize();

            this.transform.position = initialPosition;

            if (_blockTileList != null){
                if(_blockTileList.Count > 0){
                    Clear();
                }
            }
            else
                _blockTileList = new List<IGBlockTile>();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Clear();
        }

        public override void Clear()
        {
            base.Clear();
            foreach (var tile in _blockTileList)
            {
                PoolManager.Instance.Push(ETileType.BlockNode, tile.gameObject);
            }
            _blockTileList.Clear();
        }



        private void Create()
        {

            
            for(int x = 0; x < 3; ++x)
            {
               for(int y = 0; y < 3; ++y)
               {
                   var index = x * 3 + y;
                   if (blockType[x,y] == 1)
                       _blockTileList[index].gameObject.SetActive(true);
                   else
                   {
                       _blockTileList[index].gameObject.SetActive(false);
                   }
               }
            }
            this.transform.localScale = new Vector3(.7f, .7f, .7f);

            initialPosition = this.transform.position;

            //ApplyTheme(ThemeManager.Instance.CurrentTheme);
            //ThemeManager.Instance.OnThemeChanged += ApplyTheme;
        }
        


        public void SetBlockShape(BlockShape shape)
        {
            _currentShape = shape;
        }


        public bool IsAllBlockNodeColideWithBoardNode()
        {   
            if(_blockTileList == null || _blockTileList.Count < 1)
                {
                    return false;
                }

                foreach(var node in _blockTileList)
                    if(!node.IsColide)
                        return false;

                return true;
        }
    }
}