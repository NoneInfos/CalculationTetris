using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGController : MonoBehaviour
{
    protected IGEngine _engine;

    public abstract void ClearController();

    public abstract void FinalizeController();

    public abstract void InitializeController();

    public abstract void UpdateController();

    public virtual void SetEngine(IGEngine engine)
    {
        _engine = engine;
    }
}
