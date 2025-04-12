using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGController : MonoBehaviour
{
    public abstract void ClearController();

    public abstract void FinalizeController();

    public abstract void InitializeController();

    public abstract void UpdateController();
   
}
