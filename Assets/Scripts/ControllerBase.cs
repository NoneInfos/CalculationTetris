using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    //bool IsActivated { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inParentController"></param>
    void InitializeController();

    /// <summary>
    /// �ʱⰪ���� ����
    /// </summary>
    void ClearController();

    /// <summary>
    /// ����(�޸� ��ȯ ó��)
    /// </summary>
    void FinalizeController();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inDeltaTime"></param>
    //void AdvanceTime(float inDeltaTime);
}
