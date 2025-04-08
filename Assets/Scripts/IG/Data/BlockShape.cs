using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class BlockShape
{
    public int[,] Shape { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Complexity { get; private set; }
    public BlockShape(int[,] shape, int complexity)
    {
        Shape = shape;
        Height = shape.GetLength(0);
        Width = shape.GetLength(1);
        Complexity = complexity;
    }

    public BlockShape Rotate()
    {
        int[,] rotated = new int[Width, Height];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                rotated[j, Height - 1 - i] = Shape[i, j];
            }
        }
        return new BlockShape(rotated, Complexity);
    }

    private void OnDestroy(){
        
    }
}