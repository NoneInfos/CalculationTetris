using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ManagerBase<T> : SingletonClass<T> where T : ManagerBase<T>
{
    public abstract void InitializeManager();

    public abstract void ClearManager();

    public abstract void FinalizeManager();

    public T CreateObj<T>(Transform inParent, bool isActive = false, string inLayerName = "Default") where T : Component
    {
        GameObject go = new GameObject(typeof(T).Name);
        go.layer = LayerMask.NameToLayer(inLayerName);
        go.transform.SetParent(inParent);
        return go.AddComponent<T>();
    }
}
