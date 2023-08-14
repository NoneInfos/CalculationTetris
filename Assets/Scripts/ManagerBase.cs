using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ManagerBase 
{
    /// <summary>
    /// 
    /// </summary>
    //bool IsActivated { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inParentController"></param>
    void InitializeManager();

    /// <summary>
    /// �ʱⰪ���� ����
    /// </summary>
    void ClearManager();

    /// <summary>
    /// ����(�޸� ��ȯ ó��)
    /// </summary>
    void FinalizeManager();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inDeltaTime"></param>
    //void AdvanceTime(float inDeltaTime);
}
