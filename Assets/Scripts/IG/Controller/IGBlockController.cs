using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using IGMain;

public class IGBlockController : IGController
{

    private List<Vector2> spawnPositions = new List<Vector2>() 
    {
        new Vector2( Screen.width * 0.33f * -1f, Screen.height * 0.33f * -1f ),
        new Vector2( Screen.width * 0f, Screen.height * 0.33f * -1f ),
        new Vector2( Screen.width * 0.33f, Screen.height * 0.33f * -1f )
    };

    private List<BlockShape> blockShapes;
    public IGBlock SelectedBlock { get; private set; }
    public bool IsBlockMoving { get; private set; }

    private IGBlock _nextBlock;
    public List<IGBlock> AvailableBlocks { get; private set; }

    private int totalPlacedBlocks = 0;
    private void PrepareNextBlock()
    {
        _nextBlock = CreateNewBlock();
        UIManager.Instance.UpdateNextBlockPreview(_nextBlock);
    }


    public int GetTotalPlacedBlocks()
    {
        return totalPlacedBlocks;
    }

    // �� �޼��带 ������ ���������� ��ġ�� ������ ȣ��
    private void IncrementPlacedBlocks()
    {
        totalPlacedBlocks++;
    }
    public void SpawnNextBlock()
    {
        _nextBlock.gameObject.SetActive(true);
        _nextBlock.transform.position = spawnPositions[0];
        SelectedBlock = _nextBlock;

        PrepareNextBlock();
    }

    public override void ClearController()
    {
    }

    public override void FinalizeController()
    {
    }

    public override void InitializeController()
    {
        InitializeBlockShapes();
        SpawnBlocks();
        PrepareNextBlock();

    }

    private void InitializeBlockShapes()
    {
        blockShapes = new List<BlockShape>
        {
            new BlockShape(new int[,] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } }, 0), // O shape
            new BlockShape(new int[,] { { 1, 1, 1, 1 } }, 0), // I shape
            new BlockShape(new int[,] { { 1, 1, 1 }, { 0, 1, 0 } }, 0), // T shape
            new BlockShape(new int[,] { { 1, 1 }, { 1, 1 } }, 1), // Square shape
            new BlockShape(new int[,] { { 1, 1, 0 }, { 0, 1, 1 } }, 1), // Z shape
            new BlockShape(new int[,] { { 0, 1, 1 }, { 1, 1, 0 } }, 2), // S shape
            new BlockShape(new int[,] { { 1, 0, 0 }, { 1, 1, 1 } }, 2), // L shape
            new BlockShape(new int[,] { { 0, 0, 1 }, { 1, 1, 1 } }, 2)  // J shape
        };
    }

    private void SpawnBlocks()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnBlock(i);
        }
    }

    private void SpawnBlock(int index)
    {
        BlockShape randomShape = blockShapes[Random.Range(0, blockShapes.Count)];
        var block = PoolManager.Instance.Pop(ETileType.Block);
        block.gameObject.transform.localPosition = spawnPositions[index];
        block.gameObject.transform.SetParent(this.transform);

        IGBlock igBlock = block.GetComponent<IGBlock>();
        igBlock.BlockController = this;
        igBlock.SetBlockShape(randomShape);
    }
    public void RotateSelectedBlock()
    {
        if (SelectedBlock != null)
        {
            SelectedBlock.Rotate();
        }
    }

    public override void UpdateController()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateSelectedBlock();
        }
    }

    private void SpawnBlock()
    {
        for(int i =0; i<3; ++i)
        {
            var block = PoolManager.Instance.Pop(ETileType.Block);
            block.gameObject.transform.localPosition = spawnPositions[i];
            block.gameObject.transform.parent = this.transform;

            block.GetComponent<IGBlock>().BlockController = this;
        }
    }

    public void HandleBlockOnPointerDown(IGBlock block)
    {
        SelectedBlock = block;
        IsBlockMoving = true;
    }

    public void HandleBlockOnPointerUp()
    {
        PlaceBlockOnBoard(SelectedBlock);

        SelectedBlock = null;
        IsBlockMoving = false;
    }

    public void HandleBlockeOnDrag()
    {
    }


    public void PlaceBlockOnBoard(IGBlock block)
    {
        IncrementPlacedBlocks();
        _engine._boardController.PlaceBlockOnBoard(block);
    }

    public void AnimatePlaceBlockOnBoard()
    {
        if (SelectedBlock == null)
            return;

        SelectedBlock.AnimatePlaceBlockOnBoard();
    }

    public bool CheckNearestTiles()
    {
        return SelectedBlock != null &&
            SelectedBlock.BlockNodes != null &&
            SelectedBlock.BlockNodes.Any(node => node.NearestTile != null &&
            node.NearestTile.State == IGMain.EState.UnStable) == false;
    }

    private IGBlock CreateNewBlock()
    {
        BlockShape randomShape = GetRandomBlockShape();
        var block = PoolManager.Instance.Pop(ETileType.Block);
        block.gameObject.SetActive(false);

        IGBlock igBlock = block.GetComponent<IGBlock>();
        igBlock.BlockController = this;
        igBlock.SetBlockShape(randomShape);

        if (AvailableBlocks == null)
            AvailableBlocks = new List<IGBlock>();

        AvailableBlocks.Add(igBlock);

        return igBlock;
    }

    private BlockShape GetRandomBlockShape()
    {
        int complexity = Random.Range(0, DifficultyManager.Instance.CurrentExtraShapeComplexity + 1);
        List<BlockShape> availableShapes = blockShapes.Where(s => s.Complexity <= complexity).ToList();
        return availableShapes[Random.Range(0, availableShapes.Count)];
    }

}
