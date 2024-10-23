using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using IGMain;
using DG.Tweening;

public class IGBoardController : IGController
{

    [SerializeField] private SpriteRenderer gridLineRenderer;

    private IGTile[,] _board;

    private IGBoard _IGBoard;
    private int totalClearedLines = 0;
    private int totalClearedSquares = 0;
    public override void InitializeController()
    {
        _board = new IGTile[IGConfig.BOARD_ROW, IGConfig.BOARD_COL];
        CreateBoard();

        //ApplyTheme(ThemeManager.Instance.CurrentTheme);
        ThemeManager.Instance.OnThemeChanged += ApplyTheme;
    }

    private void OnDestroy()
    {
        ThemeManager.Instance.OnThemeChanged -= ApplyTheme;
    }

    private void ApplyTheme(Theme theme)
    {
        gridLineRenderer.color = theme.gridLineColor;
        foreach (var tile in _board)
        {
            tile.GetComponent<SpriteRenderer>().color = theme.backgroundColor;
        }
    }

    private void CreateBoard()
    {
        this.transform.position = Vector3.zero;
        for (int y = 0; y < IGConfig.BOARD_ROW; ++y)
        {
            for (int x = 0; x < IGConfig.BOARD_COL; ++x)
            {
                var tile = PoolManager.Instance.Pop(ETileType.BG);
                tile.transform.parent = this.transform;
                tile.transform.localPosition = new Vector3((x * IGConfig.TILE_WIDTH), (y * IGConfig.TILE_HEIGHT), 0);

                IGTile igTile = tile.GetComponent<IGTile>();
                igTile.SetIndex(y * IGConfig.BOARD_COL + x);
                igTile.SetUI();

                _board[x, y] = igTile;
                _board[x, y].name = $"[{x}, {y}]";
            }
        }

        //  board = new Tile[boardHeight, boardWidth];
        // Vector2 boardCenter = new Vector2((boardWidth - 1) * tileSize * 0.5f, (boardHeight - 1) * tileSize * 0.5f);

        // for (int y = 0; y < boardHeight; y++)
        // {
        //     for (int x = 0; x < boardWidth; x++)
        //     {
        //         Vector2 tilePosition = new Vector2(x * tileSize, -y * tileSize) - boardCenter;
        //         GameObject tileObject = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                
        //         Tile tile = tileObject.GetComponent<Tile>();
        //         if (tile == null)
        //         {
        //             tile = tileObject.AddComponent<Tile>();
        //         }
                
        //         tile.SetIndex(x, y);
        //         tile.name = $"Tile_{x}_{y}";
                
        //         board[y, x] = tile;
        //     }
        // }
    }

    private Vector2Int WorldToBoardPosition(Vector3 worldPosition)
    {
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
        return new Vector2Int(
            Mathf.RoundToInt(localPosition.x / IGConfig.TILE_WIDTH),
            Mathf.RoundToInt(localPosition.y / IGConfig.TILE_HEIGHT)
        );
    }

    public bool CanPlaceBlock(IGBlock block, int boardX, int boardY)
    {
        foreach (var node in block.BlockNodes)
        {
            int x = boardX + Mathf.RoundToInt(node.transform.localPosition.x / IGConfig.TILE_WIDTH);
            int y = boardY + Mathf.RoundToInt(node.transform.localPosition.y / IGConfig.TILE_HEIGHT);

            if (x < 0 || x >= IGConfig.BOARD_COL || y < 0 || y >= IGConfig.BOARD_ROW)
                return false;

            if (_board[y, x].IsPlaceBlock)
                return false;
        }
        return true;
    }

    public void PlaceBlockOnBoard(IGBlock block)
    {
        var blockPosToBoardPos = WorldToBoardPosition(block.transform.position);

        if (!CanPlaceBlock(block, blockPosToBoardPos.x, blockPosToBoardPos.y))
            return;

        foreach (var node in block.BlockNodes)
        {
            int x = blockPosToBoardPos.x + Mathf.RoundToInt(node.transform.localPosition.x / IGConfig.TILE_WIDTH);
            int y = blockPosToBoardPos.y + Mathf.RoundToInt(node.transform.localPosition.y / IGConfig.TILE_HEIGHT);

            _board[x, y].IsPlaceBlock = true;

            node.transform.parent = _board[x, y].transform;
            node.transform.localPosition = Vector3.zero;
        }

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
            if (!_board[isRow ? index : i, isRow ? i : index].IsPlaceBlock)
                return false;
        }
        return true;
    }

    private void ClearLine(int index, bool isRow)
    {
        for (int i = 0; i < (isRow ? IGConfig.BOARD_COL : IGConfig.BOARD_ROW); i++)
        {
            _board[isRow ? index : i, isRow ? i : index].IsPlaceBlock = false;
            _board[isRow ? index : i, isRow ? i : index].ResetTile();
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
                if (!_board[y, x].IsPlaceBlock)
                    return false;
            }
        }
        return true;
    }

    public int GetTotalClearedLines()
    {
        return totalClearedLines;
    }

    public int GetTotalClearedSquares()
    {
        return totalClearedSquares;
    }

    // �� �޼��带 ������ ���ŵ� ������ ȣ��
    private void IncrementClearedLines(int linesCleared)
    {
        totalClearedLines += linesCleared;
    }

    // �� �޼��带 3x3 �簢���� ���ŵ� ������ ȣ��
    private void IncrementClearedSquares()
    {
        totalClearedSquares++;
    }


    private void ClearSquare(int startX, int startY)
    {
        for (int y = startY; y < startY + 3; y++)
        {
            for (int x = startX; x < startX + 3; x++)
            {
                _board[y, x].IsPlaceBlock = false;
                _board[y, x].ResetTile();
            }
        }
        AudioManager.Instance.Play("SquareClear");
        IncrementClearedSquares();
    }

    public bool IsGameOver()
    {
        foreach (var block in _engine._blockController.AvailableBlocks)
        {
            for (int y = 0; y < IGConfig.BOARD_ROW; y++)
            {
                for (int x = 0; x < IGConfig.BOARD_COL; x++)
                {
                    if (CanPlaceBlock(block, x, y))
                        return false;
                }
            }
        }
        return true;
    }

    public override void ClearController()
    {
    }

    public override void FinalizeController()
    {
    }

    public override void UpdateController()
    {
    }

    private void AnimateClearLine(int index, bool isRow)
    {
        for (int i = 0; i < (isRow ? IGConfig.BOARD_COL : IGConfig.BOARD_ROW); i++)
        {
            var tile = _board[isRow ? index : i, isRow ? i : index];
            tile.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).OnComplete(() => {
                tile.IsPlaceBlock = false;
                tile.ResetTile();
                tile.GetComponent<SpriteRenderer>().DOFade(1, 0.1f);
            });
        }
    }

    private void AnimateClearSquare(int startX, int startY)
    {
        for (int y = startY; y < startY + 3; y++)
        {
            for (int x = startX; x < startX + 3; x++)
            {
                var tile = _board[y, x];
                tile.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).OnComplete(() => {
                    tile.IsPlaceBlock = false;
                    tile.ResetTile();
                    tile.GetComponent<SpriteRenderer>().DOFade(1, 0.1f);
                });
            }
        }
    }
}
