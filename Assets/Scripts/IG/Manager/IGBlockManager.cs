using System.Collections;
using System.Collections.Generic;
using IGMain;
using UnityEngine;

public class IGBlockManager : ManagerBase<IGBlockManager>
{
    private IGBlockController _blockController;
    private List<IGBlock> _blockList;
    private Vector2[] _spawnPositions;

    public override void InitializeManager()
    {
        if(_blockList != null){
            if(_blockList.Count > 0)
                ClearManager();
        }
        else
            _blockList = new List<IGBlock>();


        _spawnPositions = new Vector2[]
        {
            new Vector2(-240f, -470f),
            new Vector2(0f, -470f),
            new Vector2(240f, -470f),
        };

        SpawnBlocks();

    }
    public override void ClearManager()
    {
        foreach(var block in _blockList){
            if(block != null)
                Destroy(block);
        }

        _blockList.Clear();
    }

    

    public override void FinalizeManager()
    {

    }

    public void SpawnBlocks()
    {
        for(int i =0 ; i < 3; ++i)
        {
            var block = PoolManager.Instance.Pop<IGBlock>(EPoolType.Block);
            _blockList.Add(block);
            block.transform.localPosition = _spawnPositions[i];
        }
    }


    //private void SpawnBlock(int index)
    //{
    //    //BlockShape randomShape = blockShapes[Random.Range(0, blockShapes.Count)];
    //    //var block = PoolManager.Instance.Pop(EPoolType.Block);
    //    //block.gameObject.transform.localPosition = spawnPositions[index];
    //    //block.gameObject.transform.SetParent(this.transform);

    //    //IGBlock igBlock = block.GetComponent<IGBlock>();
    //    ////igBlock.BlockController = this;
    //    //igBlock.SetBlockShape(randomShape);
    //}

}
