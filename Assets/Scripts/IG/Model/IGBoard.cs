using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

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
            Debug.Log("IGBoard Initialize");
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
                    tile.transform.SetParent(IGBoardManager.Instance.gameObject.transform);
                    board[y, x] = tile;
                }
            }
        }

        public bool IsTileOccupied(int x, int y)
        {
            // 범위 체크
            if (x < 0 || x >= IGConfig.BOARD_COL || y < 0 || y >= IGConfig.BOARD_ROW)
                return true; // 범위 밖은 점유된 것으로 간주

            return board[y, x].IsPlaceBlock;
        }

        public bool CanPlaceBlock(IGBlock block, Vector2Int boardPosition)
        {
            // 블록의 모든 타일에 대해 충돌 검사
            foreach (var tilePosition in block.GetRelativeTilePositions())
            {
                int boardX = boardPosition.x + tilePosition.x;
                int boardY = boardPosition.y + tilePosition.y;

                // 보드 밖이거나 이미 점유된 타일이면 배치 불가능
                if (IsTileOccupied(boardX, boardY))
                    return false;
            }

            return true;
        }

        // public bool IsFilledRow(int row)
        // {
        //     return Rows[row].All(tile => tile.IsFilled);
        // }

        // public bool IsFilledColumn(int col)
        // {
        //     return Cols[col].All(tile => tile.IsFilled);
        // }


        //private void AnimateClearLine(int index, bool isRow)
        //{
        //    for (int i = 0; i < (isRow ? IGConfig.BOARD_COL : IGConfig.BOARD_ROW); i++)
        //    {
        //        var tile = board[isRow ? index : i, isRow ? i : index];
        //        tile.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).OnComplete(() => {
        //            tile.IsPlaceBlock = false;
        //            tile.ResetTile();
        //            tile.GetComponent<SpriteRenderer>().DOFade(1, 0.1f);
        //        });
        //    }
        //}

        //private void AnimateClearSquare(int startX, int startY)
        //{
        //    for (int y = startY; y < startY + 3; y++)
        //    {
        //        for (int x = startX; x < startX + 3; x++)
        //        {
        //            var tile = board[y, x];
        //            tile.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).OnComplete(() => {
        //                tile.IsPlaceBlock = false;
        //                tile.ResetTile();
        //                tile.GetComponent<SpriteRenderer>().DOFade(1, 0.1f);
        //            });
        //        }
        //    }
        //}

        private void ClearSquare(int startX, int startY)
        {
            for (int y = startY; y < startY + 3; y++)
            {
                for (int x = startX; x < startX + 3; x++)
                {
                    board[y, x].IsPlaceBlock = false;
                    board[y, x].ResetTile();
                }
            }
            AudioManager.Instance.Play("SquareClear");
            IncrementClearedSquares();
        }

        public bool CanPlaceBlock(IGBlock block, int boardX, int boardY)
        {
            //foreach (var node in block.BlockNodes)
            //{
            //    int x = boardX + Mathf.RoundToInt(node.transform.localPosition.x / IGConfig.TILE_WIDTH);
            //    int y = boardY + Mathf.RoundToInt(node.transform.localPosition.y / IGConfig.TILE_HEIGHT);

            //    if (x < 0 || x >= IGConfig.BOARD_COL || y < 0 || y >= IGConfig.BOARD_ROW)
            //        return false;

            //    if (board[y, x].IsPlaceBlock)
            //        return false;
            //}
            return true;
        }


        private Vector2Int WorldToBoardPosition(Vector3 worldPosition)
        {
            Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
            return new Vector2Int(
                Mathf.RoundToInt(localPosition.x / IGConfig.TILE_WIDTH),
                Mathf.RoundToInt(localPosition.y / IGConfig.TILE_HEIGHT)
            );
        }

        public void PlaceBlockOnBoard(IGBlock block)
        {
            var blockPosToBoardPos = WorldToBoardPosition(block.transform.position);

            if (!CanPlaceBlock(block, blockPosToBoardPos.x, blockPosToBoardPos.y))
                return;

            //foreach (var node in block.BlockNodes)
            //{
            //    int x = blockPosToBoardPos.x + Mathf.RoundToInt(node.transform.localPosition.x / IGConfig.TILE_WIDTH);
            //    int y = blockPosToBoardPos.y + Mathf.RoundToInt(node.transform.localPosition.y / IGConfig.TILE_HEIGHT);

            //    board[x, y].IsPlaceBlock = true;

            //    node.transform.parent = board[x, y].transform;
            //    node.transform.localPosition = Vector3.zero;
            //}

            CheckAndClearLines();
        }


        private void CheckAndClearLines()
        {
            int clearedLines = 0;

            // Check rows
            for (int y = 0; y < IGConfig.BOARD_ROW; y++)
            {
                if (IsLineFull(y, true))
                {
                    ClearLine(y, true);
                    clearedLines++;
                }
            }

            for (int x = 0; x < IGConfig.BOARD_COL; x++)
            {
                if (IsLineFull(x, false))
                {
                    ClearLine(x, false);
                    clearedLines++;
                }
            }

            for (int y = 0; y < IGConfig.BOARD_ROW; y += 3)
            {
                for (int x = 0; x < IGConfig.BOARD_COL; x += 3)
                {
                    if (IsSquareFull(x, y))
                    {
                        ClearSquare(x, y);
                        clearedLines++;
                    }
                }
            }

            ScoreManager.Instance.AddScore(clearedLines);

            SaveManager.Instance.AddLinesCleared(clearedLines);
        }


        private bool IsLineFull(int index, bool isRow)
        {
            for (int i = 0; i < (isRow ? IGConfig.BOARD_COL : IGConfig.BOARD_ROW); i++)
            {
                if (!board[isRow ? index : i, isRow ? i : index].IsPlaceBlock)
                    return false;
            }
            return true;
        }

        private void ClearLine(int index, bool isRow)
        {
            for (int i = 0; i < (isRow ? IGConfig.BOARD_COL : IGConfig.BOARD_ROW); i++)
            {
                board[isRow ? index : i, isRow ? i : index].IsPlaceBlock = false;
                board[isRow ? index : i, isRow ? i : index].ResetTile();
            }
            AudioManager.Instance.Play("LineClear");
            IncrementClearedLines(1);
        }

        private bool IsSquareFull(int startX, int startY)
        {
            for (int y = startY; y < startY + 3; y++)
            {
                for (int x = startX; x < startX + 3; x++)
                {
                    if (!board[y, x].IsPlaceBlock)
                        return false;
                }
            }
            return true;
        }

        private void IncrementClearedLines(int linesCleared)
        {
            //totalClearedLines += linesCleared;
        }

        private void IncrementClearedSquares()
        {
            //totalClearedSquares++;
        }


    }
}
