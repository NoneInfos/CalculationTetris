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
        private IGBlockTile[,] _blockTiles;

        private BlockShape _blockShape;

        private Vector2 initialPosition = Vector2.zero;

        public override void Initialize()
        {
            base.Initialize();

            this.transform.position = initialPosition;

            if (_blockTiles != null){
                if(_blockTiles.Length > 0){
                    Clear();
                }
            }

            CreateBlock();

            this.transform.localScale = new Vector3(.7f, .7f, .7f);
            initialPosition = this.transform.position;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Clear();
        }

        public override void Clear()
        {
            base.Clear();

            _blockShape = null;

            for (int x = 0; x < _blockTiles.GetLength(1); ++x)
            {
                for (int y = 0; y < _blockTiles.GetLength(0); ++y)
                {
                    if(_blockTiles[x,y] != null)
                        PoolManager.Instance.Push<IGBlockTile>(EPoolType.BlockTile, _blockTiles[x, y]);
                }
            }
        }

      

        public void CreateBlock()
        {
            _blockTiles = new IGBlockTile[IGConfig.BLOCK_WIDTH, IGConfig.BLOCK_HEIGHT];
            _blockShape = new BlockShape();

            for (int y = -1; y < _blockShape.Height - 1; ++y)
            {
                for (int x = -1; x < _blockShape.Width - 1; ++x)
                {
                    if (_blockShape.Shape[y + 1, x + 1] == 1)
                    {
                        IGBlockTile tile = PoolManager.Instance.Pop<IGBlockTile>(EPoolType.BlockTile);
                        tile.transform.SetParent(transform);
                        tile.transform.localPosition = new Vector3(x, -y, 0) * IGConfig.TILE_WIDTH;
                        _blockTiles[x + 1,y + 1] = tile;
                    }
                }
            }
        }

       


        //public bool IsAllBlockNodeColideWithBoardNode()
        //{   
        //    if(_blockTileList == null || _blockTileList.Count < 1)
        //        {
        //            return false;
        //        }

        //        foreach(var node in _blockTileList)
        //            if(!node.IsColide)
        //                return false;

        //        return true;
        //}
    }
}