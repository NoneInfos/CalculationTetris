using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
public class IGEngine : MonoBehaviour
{

    private List<IGController> _controllers = new List<IGController>();


    private void Awake()
    {
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
