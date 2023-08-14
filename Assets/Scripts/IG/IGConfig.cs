using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGConfig
{

    private static readonly int MAP_COL = 9;
    private static readonly int MAP_ROW = 9;

    /// <summary>
    /// 5x5 (���� ����� ���ؼ�?)ũ���� ������� ����
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
