using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGController : MonoBehaviour, ControllerBase
{
    public abstract void ClearController();

    public abstract void FinalizeController();

    public abstract void InitializeController();
   
}
