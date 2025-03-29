using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IGMain
{
    public class IGBoard : IGObject
    {    

        public IGBoardTile[,] board;

        public Dictionary<int, List<IGBoardTile>> Rows;

        public Dictionary<int, List<IGBoardTile>> Cols;

        public override void Initialize()
        {
            base.Initialize();

            GenerateBoard();
            
            //var path ="Prefabs/IGBlockTile";
            //var prefab = Resources.Load<IGBoardTile>(path);

            // for(int col = 0; col < IGConfig.BOARD_COL; ++col)
            // {
            //     for(int row = 0; row < IGConfig.BOARD_ROW; ++row)
            //     {
            //         var obj = Instantiate(prefab,this.transform);
            //         obj.gameObject.SetActive(false);

            //         BlockNodes.Add(obj);    
            //         Rows[row].Add(obj);
            //         col == row ? Cols[col].Add(obj);
            //     }
            // }

            float aspectRatio = (float)Screen.width / Screen.height;
            float verticalSize = IGConfig.BOARD_COL * IGConfig.TILE_WIDTH * 0.5f;
            float horizontalSize = IGConfig.BOARD_ROW * IGConfig.TILE_HEIGHT * 0.5f / aspectRatio;
            Camera.main.orthographicSize = Mathf.Max(verticalSize, horizontalSize);
        }

        void GenerateBoard()
        {
            var path ="Prefabs/IGBoardTile";
            var prefab = Resources.Load<GameObject>(path);

            float startX = -(IGConfig.BOARD_COL * IGConfig.TILE_WIDTH) / 2f;  // 음수로 시작
            float startY = (IGConfig.BOARD_ROW * IGConfig.TILE_HEIGHT) / 2f;  // 양수로 시작


            board = new IGBoardTile[IGConfig.BOARD_COL, IGConfig.BOARD_ROW];
            Vector2 boardCenter = new Vector2((IGConfig.BOARD_COL - 1) * IGConfig.TILE_WIDTH * 0.5f, (IGConfig.BOARD_ROW - 1) * IGConfig.TILE_HEIGHT * 0.5f);

            

            for (int y = 0; y < IGConfig.BOARD_ROW; y++)
            {
                for (int x = 0; x < IGConfig.BOARD_COL; x++)
                {
                    float posX = startX + (x * IGConfig.TILE_WIDTH) + (IGConfig.TILE_WIDTH / 2f); // 타일 중심을 위해 tileSize/2 더함
                    float posY = startY - (y * IGConfig.TILE_HEIGHT) - (IGConfig.TILE_HEIGHT / 2f); // 아래로 내려가므로 빼기

                    Vector2 tilePosition = new Vector2(posX,posY);
                    GameObject tileObject = Instantiate(prefab, tilePosition, Quaternion.identity);
                    
                    IGBoardTile tile = tileObject.GetComponent<IGBoardTile>();
                    if (tile == null)
                    {
                        tile = tileObject.AddComponent<IGBoardTile>();
                    }
                    
                    //tile.SetIndex(x, y);
                    tile.name = $"Tile_{x}_{y}";
                    
                    board[y, x] = tile;
                }
            }
        }

    
    
        // public bool IsFilledRow(int row)
        // {
        //     return Rows[row].All(tile => tile.IsFilled);
        // }

        // public bool IsFilledColumn(int col)
        // {
        //     return Cols[col].All(tile => tile.IsFilled);
        // }
    }
}
