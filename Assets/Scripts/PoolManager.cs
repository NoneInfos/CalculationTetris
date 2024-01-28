using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;

public class PoolManager : SingletonClass<PoolManager>, ManagerBase
{
    public readonly int POOL_COUNT = 81;

    private const string PREFAB_ROOT_PATH = "Prefabs/";

    private Dictionary<ETileType, Queue<GameObject>> _poolList = new Dictionary<ETileType, Queue<GameObject>>();

    private Dictionary<ETileType, string> _objectPath = new Dictionary<ETileType, string>() 
    {
        { ETileType.BG, "IGTile_BG" },
        { ETileType.Block, "IGTile_Block" },
        { ETileType.BlockNode, "IGTile_BlockNode" }
    };

    public void Push(ETileType type, GameObject obj)
    {
        obj.gameObject.SetActive(false);
        _poolList[type].Enqueue(obj);
    }

    public GameObject Pop(ETileType type)
    {
        if(_poolList[type].Count <= 0)
        {
            Create(type);
            Debug.Log($"PoolManager Create {type} Total Pooling Count : {_poolList[type].Count}");
        }
        var obj =  _poolList[type].Dequeue();
        obj.SetActive(true);
        return obj;
    }

    private void Create(ETileType type)
    {
        var resource = Resources.Load<GameObject>(PREFAB_ROOT_PATH + $"{_objectPath[type]}");

        if (resource == null)
            return;


        if (_poolList.ContainsKey(type) == false)
        {
            _poolList.Add(type, new Queue<GameObject>());
        }

        for (int count = 0; count < POOL_COUNT; ++count)
        {
            var prefab = Instantiate(resource,this.transform);
            prefab.SetActive(false);
            _poolList[type].Enqueue(prefab);
        }

    }

    private void OnDestroy()
    {
        foreach (var poolItem in _poolList)
        {
            foreach(var item in poolItem.Value)
            {
                //Destroy(item);
            }
        }
    }

    public void InitializeManager()
    {
        Create(ETileType.BG);

        Create(ETileType.Block);
    }

    public void ClearManager()
    {
    }

    public void FinalizeManager()
    {
    }
}

public enum ETileType
{
    BG,
    Block,
    BlockNode,
}
