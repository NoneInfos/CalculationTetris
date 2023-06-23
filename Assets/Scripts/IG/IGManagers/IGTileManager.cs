using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGTileManager : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _ghostObjectParent;
    private static readonly int COL = 9;
    private static readonly int ROW = 9;
    private bool _isSampleObejct = false;
    
    void Init()
    {
        _object.SetActive(false);
        float x = -304f;
        float y = 304f;
        for (int i = 0; i < 9; ++i)
        {
            for (int j = 0; j < 9; ++j)
            {
                GameObject obj = Instantiate(_object, new Vector2(x + (j * 76), y - (i * 76)), Quaternion.identity, this.transform);
                obj.SetActive(true);
                obj.GetComponent<SpriteRenderer>().sprite = GetSprite(i + j);
            }
        }
    }
    // Update is called once per frame
   

    public Sprite GetSprite(int index)
    {
        int spriteIndex = index % 2 == 0 ? 5 : 6;
        return Sprites[spriteIndex];
    }
}
