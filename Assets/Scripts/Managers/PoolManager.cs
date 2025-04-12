using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;

public enum EPoolType
{
    Block,
    BlockTile,
}


public class PoolManager : ManagerBase<PoolManager>
{
    public readonly int POOL_COUNT = 81;

    private const string PREFAB_ROOT_PATH = "Prefabs/";

    private Dictionary<EPoolType, Queue<IGObject>> _poolList = new Dictionary<EPoolType, Queue<IGObject>>();

    private Dictionary<EPoolType, string> _objectPath = new Dictionary<EPoolType, string>() 
    {
        { EPoolType.Block, "IGBlock" },
        { EPoolType.BlockTile, "IGBlockTile" },
    };

    public void Push<T>(EPoolType type, T obj) where T : IGObject
    {
        obj.transform.parent = this.transform;
        obj.gameObject.SetActive(false);
        _poolList[type].Enqueue(obj);
    }

    public T Pop<T>(EPoolType type) where T : IGObject
    {
        if(_poolList.ContainsKey(type) == false)
        {
            Create(type);
            Debug.Log($"PoolManager Create {type} Total Pooling Count : {_poolList[type].Count}");
        }
        else if (_poolList[type].Count <= 0)
        {
            Create(type);
            Debug.Log($"PoolManager Create {type} Total Pooling Count : {_poolList[type].Count}");
        }

        var obj = _poolList[type].Dequeue();
        obj.gameObject.SetActive(true);

        T component = obj.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError($"Object in pool doesn't have component of type {typeof(T)}");
        }

        return component;
    }

    private void Create(EPoolType type)
    {
        var resource = Resources.Load<GameObject>(PREFAB_ROOT_PATH + $"{_objectPath[type]}");

        if (resource == null)
            return;


        if (_poolList.ContainsKey(type) == false)
        {
            _poolList.Add(type, new Queue<IGObject>());
        }


        for (int count = 0; count < POOL_COUNT; ++count)
        {
            var prefab = Instantiate(resource,this.transform);
            prefab.SetActive(false);

            IGObject igObject = prefab.GetComponent<IGObject>();

            igObject?.Initialize();

            _poolList[type].Enqueue(igObject);
        }

    }

    private void OnDestroy()
    {
        foreach (var poolItem in _poolList)
        {
            foreach(var item in poolItem.Value)
            {
            }
        }
    }
  

    public override void InitializeManager()
    {
    }

    public override void ClearManager()
    {
    }

    public override void FinalizeManager()
    {
    }
}

