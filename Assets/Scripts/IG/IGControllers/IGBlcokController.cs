using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IGBlcokController : IGController
{
    public IGTile_Block SelectedBlock = null;

    public bool IsBlockMoving = false;

    //private  List<IGTile_Block> _blockList = new List<IGTile_Block>();

    private List<Vector2> spawnPostion = new List<Vector2>() 
    {
        new Vector2( 58f,-176f ),
        new Vector2( 298f,-176f ),
        new Vector2( 538f,-176f )
    };
    
    public override void ClearController()
    {
    }

    public override void FinalizeController()
    {
    }

    public override void InitializeController()
    {
        SpawnBlock();
    }

    public override void UpdateController()
    {

    }

    private void SpawnBlock()
    {
        for(int i =0; i<3; ++i)
        {
            var block = PoolManager.Instacne.Pop(ETileType.Block);
            block.gameObject.transform.localPosition = spawnPostion[i];
            block.gameObject.transform.parent = this.transform;

            block.GetComponent<IGTile_Block>().BlockController = this;
        }
    }

    public void HandleBlockOnPointerDown(IGTile_Block block)
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
        //_engine._boardController
    }


    public void PlaceBlockOnBoard(IGTile_Block block)
    {
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
        //Debug.LogError(SelectedBlock != null);
        //Debug.LogError(SelectedBlock.BlockNodes != null);
        //Debug.LogError(SelectedBlock.BlockNodes.Any(node => node.NearestTile != null &&
        //    node.NearestTile.State == IGMain.EState.UnStable));

        //블록의 인접타일을 체크하는거보다는 보드의 정보로 판단해야할거같은데

        return SelectedBlock != null &&
            SelectedBlock.BlockNodes != null &&
            SelectedBlock.BlockNodes.Any(node => node.NearestTile != null &&
            node.NearestTile.State == IGMain.EState.UnStable) == false;
    }
}
