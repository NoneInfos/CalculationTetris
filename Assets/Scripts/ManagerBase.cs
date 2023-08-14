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
    /// 초기값으로 설정
    /// </summary>
    void ClearManager();

    /// <summary>
    /// 제거(메모리 반환 처리)
    /// </summary>
    void FinalizeManager();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inDeltaTime"></param>
    //void AdvanceTime(float inDeltaTime);
}
