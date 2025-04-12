using System.Collections;
using System.Collections.Generic;
using IGMain;
using UnityEngine;

public class IGBlockManager : ManagerBase<IGBlockManager>
{
    private IGBlockController _blockController;
    private List<IGBlock> _blockList;

    public override void InitializeManager()
    {
        if(_blockList != null){
            if(_blockList.Count > 0)
                ClearManager();
        }
        else
            _blockList = new List<IGBlock>();


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
        for(int i =0; i < 3; ++i)
        {
            _blockList.Add(PoolManager.Instance.Pop<IGBlock>(EPoolType.Block));
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
