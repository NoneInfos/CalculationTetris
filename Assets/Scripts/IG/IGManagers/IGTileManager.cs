using IGMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGTileManager : IGManager<IGTileManager>
{
  
    public override void ClearManager()
    {
        throw new System.NotImplementedException();
    }

    public override void FinalizeManager()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeManager()
    {
        float x = -304f;
        float y = 304f;
        for (int i = 0; i < 9; ++i)
        {
            for (int j = 0; j < 9; ++j)
            {
                //GameObject obj = Instantiate(OBJ_Tile);
                //obj.SetActive(true);
                //obj.GetComponent<SpriteRenderer>().sprite = GetSprite(i + j);
            }
        }
    }
}
