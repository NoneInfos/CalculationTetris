using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGBlockManager : MonoBehaviour
{


    public void CreateBlock(int[,] shape)
    {
        //for (int y = -1; y < shape.GetLength(0) - 1; ++y)
        //{
        //    for (int x = -1; x < shape.GetLength(1) - 1; ++x)
        //    {
        //        if (shape[y + 1, x + 1] == 1)
        //        {
        //            GameObject tile = PoolManager.Instance.Pop(ETileType.BlockNode);
        //            tile.transform.SetParent(transform);
        //            tile.transform.localPosition = new Vector3(x, -y, 0) * IGConfig.TILE_WIDTH;
        //            BlockNodes.Add(tile.transform.GetComponent<IGBlockTile>());
        //        }
        //    }
        //}
    }
}
