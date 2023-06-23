using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGConfig
{
    /// <summary>
    /// 5x5 (중점 계산이 편해서?)크기의 블록으로 구성
    /// </summary>
    public static readonly int[,] IBlock =
    {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,1,1,1,0},
        {0,0,0,1,0},
        {0,0,0,0,0},
    };
    public static readonly int[,] OBlock =
    {
        {1,1,1,1,1},
        {1,1,1,1,1},
        {1,0,0,0,1},
        {1,1,1,0,1},
        {1,1,1,1,1}
    };
    public static readonly int[,] ZBlock =
    {
        {1,1,1,1,1},
        {1,1,1,0,1},
        {1,1,1,0,1},
        {1,0,0,0,1},
        {1,1,1,1,1}
    };
    public static readonly int[,] SBlock =
    {
        {1,1,1,1,1},
        {1,1,1,0,1},
        {1,1,1,0,1},
        {1,0,0,0,1},
        {1,1,1,1,1}
    };
    public static readonly int[,] JBlock =
    {
        {1,1,1,1,1},
        {1,1,1,0,1},
        {1,1,1,0,1},
        {1,0,0,0,1},
        {1,1,1,1,1}
    };
    public static readonly int[,] LBlock =
    {
        {1,1,1,1,1},
        {1,1,1,0,1},
        {1,1,1,0,1},
        {1,0,0,0,1},
        {1,1,1,1,1}
    };
    public static readonly int[,] TBlock =
    {
        {1,1,1,1,1},
        {1,1,1,0,1},
        {1,1,1,0,1},
        {1,0,0,0,1},
        {1,1,1,1,1}
    };

}
