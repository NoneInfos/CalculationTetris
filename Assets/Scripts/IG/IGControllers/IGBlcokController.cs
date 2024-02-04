using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
