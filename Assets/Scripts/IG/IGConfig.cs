using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGConfig
{

    public static readonly int BOARD_COL = 9;
    public static readonly int BOARD_ROW = 9;

    public static readonly int SCREEN_WIDTH = 720;
    public static readonly int SCREEN_HEIGHT = 1280;
    public static readonly int SCREEN_WIDTH_HALF = SCREEN_WIDTH / 2;        //360
    public static readonly int SCREEN_HEIGHT_HALF = SCREEN_HEIGHT / 2;      //640

    public static readonly int TILE_WIDTH = 76;
    public static readonly int TILE_HEIGHT = 76;

    public static readonly int TILE_WIDTH_HALF = TILE_WIDTH / 2;
    public static readonly int TILE_HEIGHT_HALF = TILE_HEIGHT / 2;

    /// <summary>
    /// 3x3 ũ���� �������� ����
    /// </summary>
    public static readonly int[,] IBlock =
    {
        {1,1,1},
        {1,0,1},
        {1,1,1},
    };
    public static readonly int[,] OBlock =
    {
        {1,1,1},
        {1,1,1},
        {1,0,0},
    };
    public static readonly int[,] ZBlock =
    {
        {1,1,1},
        {1,1,1},
        {1,1,1},
    };
    public static readonly int[,] SBlock =
    {
        {1,1,1},
        {1,1,1},
        {1,1,1},
    };
    public static readonly int[,] JBlock =
    {
        {1,1,1},
        {1,1,1},
        {1,1,1},
    };
    public static readonly int[,] LBlock =
    {
        {1,1,1},
        {1,1,1},
        {1,1,1},
    };
    public static readonly int[,] TBlock =
    {
        {1,1,1},
        {1,1,1},
        {1,1,1},
    };


    //public static readonly int[,] IBlock =
    //{
    //    {0,0,0,0,0},
    //    {0,0,0,0,0},
    //    {0,1,1,1,0},
    //    {0,0,0,1,0},
    //    {0,0,0,0,0},
    //};
    //public static readonly int[,] OBlock =
    //{
    //    {1,1,1,1,1},
    //    {1,1,1,1,1},
    //    {1,0,0,0,1},
    //    {1,1,1,0,1},
    //    {1,1,1,1,1}
    //};
    //public static readonly int[,] ZBlock =
    //{
    //    {1,1,1,1,1},
    //    {1,1,1,0,1},
    //    {1,1,1,0,1},
    //    {1,0,0,0,1},
    //    {1,1,1,1,1}
    //};
    //public static readonly int[,] SBlock =
    //{
    //    {1,1,1,1,1},
    //    {1,1,1,0,1},
    //    {1,1,1,0,1},
    //    {1,0,0,0,1},
    //    {1,1,1,1,1}
    //};
    //public static readonly int[,] JBlock =
    //{
    //    {1,1,1,1,1},
    //    {1,1,1,0,1},
    //    {1,1,1,0,1},
    //    {1,0,0,0,1},
    //    {1,1,1,1,1}
    //};
    //public static readonly int[,] LBlock =
    //{
    //    {1,1,1,1,1},
    //    {1,1,1,0,1},
    //    {1,1,1,0,1},
    //    {1,0,0,0,1},
    //    {1,1,1,1,1}
    //};
    //public static readonly int[,] TBlock =
    //{
    //    {1,1,1,1,1},
    //    {1,1,1,0,1},
    //    {1,1,1,0,1},
    //    {1,0,0,0,1},
    //    {1,1,1,1,1}
    //};

}
