using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace IGMain
{
    public class IGBlock : IGObject
    {    

        public List<IGBlockTile> BlockNodes { get; private set; }
        public IGBlockController BlockController { get; set; }

        private BlockShape currentShape;


        private int[,] blockType = IGConfig.IBlock;

        private Vector2 initialPosition = Vector2.zero;

        public override void Initialize()
        {
            base.Initialize();
            this.transform.position = initialPosition;


            if (BlockNodes == null)
                BlockNodes = new List<IGBlockTile>();
        }



        private void Start()
        {
            //for(int x = 0; x < 3; ++x)
            //{
            //    for(int y = 0; y < 3; ++y)
            //    {
            //        var index = x * 3 + y;
            //        if (blockType[x,y] == 1)
            //            BlockNodes[index].gameObject.SetActive(true);
            //        else
            //        {
            //            BlockNodes[index].gameObject.SetActive(false);
            //        }
            //    }
            //}
            this.transform.localScale = new Vector3(.7f, .7f, .7f);

            initialPosition = this.transform.position;

            //ApplyTheme(ThemeManager.Instance.CurrentTheme);
            //ThemeManager.Instance.OnThemeChanged += ApplyTheme;
        }
        private void OnDestroy()
        {
            //ThemeManager.Instance.OnThemeChanged -= ApplyTheme;
        }

       

        public void ClearBlock()
        {
            foreach (var tile in BlockNodes)
            {
                PoolManager.Instance.Push(ETileType.BlockNode, tile.gameObject);
            }
            BlockNodes.Clear();
        }

        public void SetBlockShape(BlockShape shape)
        {
            currentShape = shape;
        }


        public bool IsAllBlockNodeColideWithBoardNode()
        {   
            if(BlockNodes == null || BlockNodes.Count < 1)
                {
                    return false;
                }

                foreach(var node in BlockNodes)
                    if(!node.IsColide)
                        return false;

                return true;
        }
    }
}