using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;


public class BlockShape
{
    public int[,] Shape { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Complexity { get; private set; }

    

    public BlockShape()
    {
        int random = Random.Range(0, IGConfig.BlockTypes.Count);
        Shape = IGConfig.BlockTypes.ElementAt(random).Value;
        Height = Shape.GetLength(0);
        Width = Shape.GetLength(1);
        Complexity = 0;
    }


    private void OnDestroy(){
        
    }
}