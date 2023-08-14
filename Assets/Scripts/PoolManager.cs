using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;

public class PoolManager : SingletonClass<PoolManager>
{
    public readonly int POOL_COUNT = 81;

    private const string PREFAB_ROOT_PATH = "Prefabs/";


    private Dictionary<ETileType, Queue<IGTile>> _poolList = new Dictionary<ETileType, Queue<IGTile>>();

    private Dictionary<ETileType, string> _objectPath = new Dictionary<ETileType, string>() 
    {
        { ETileType.BG, "IGTile" },
        {ETileType.Block, "IGBlock" }
    };

    public bool IsActivated => throw new System.NotImplementedException();

    public void Push(ETileType type, IGTile obj)
    {
        _poolList[type].Enqueue(obj);
    }

    public IGTile Pop(ETileType type)
    {
        if(_poolList[type].Count <= 0)
        {
            Create(type, _objectPath[type]);
            Debug.Log($"PoolManager Create {type} Total Pooling Count : {_poolList[type].Count}");
        }
        return _poolList[type].Dequeue();
    }

    private void Create(ETileType type, string objectPath)
    {
        var prefab = Resources.Load(PREFAB_ROOT_PATH + $"{objectPath}");

        if(_poolList.ContainsKey(type) == false)
        {
            _poolList.Add(type, new Queue<IGTile>());
        }

        for (int count = 0; count < POOL_COUNT; ++count)
        {
            _poolList[type].Enqueue(Instantiate(prefab) as IGTile);
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

    public void InitController()
    {
        foreach (var poolObject in _objectPath)
        {
            Create(poolObject.Key, poolObject.Value);
        }

    }

    public void InitializeController(IGController inParentController)
    {
        throw new System.NotImplementedException();
    }

    public void ClearController()
    {
        throw new System.NotImplementedException();
    }

    public void FinalizeController()
    {
        throw new System.NotImplementedException();
    }

    public void AdvanceTime(float inDeltaTime)
    {
        throw new System.NotImplementedException();
    }
}

public enum ETileType
{
    BG,
    Block,
}
