using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using UnityEngine.EventSystems;

public class IGTile_Block : IGObject, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    [SerializeField] List<IGTile> _blockTiles;


    int[,] blockType = IGConfig.IBlock;

    Vector2 initialPosition = Vector2.zero;

    private void Start()
    {
        for(int x = 0; x < 3; ++x)
        {
            for(int y = 0; y < 3; ++y)
            {
                var index = x * 3 + y;
                if (blockType[x,y] == 1)
                    _blockTiles[index].gameObject.SetActive(true);
                else
                {
                    _blockTiles[index].gameObject.SetActive(false);
                }
            }
        }
        this.transform.localScale = new Vector3(.7f, .7f, .7f);

        initialPosition = this.transform.position;
    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1f,1f,1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(.7f, .7f, .7f);
        this.transform.position = initialPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);

        //Debug.DrawLine(Camera.main.ScreenToWorldPoint(eventData.position) - new Vector3(44f, 0f),
        //  Camera.main.ScreenToWorldPoint(eventData.position) + new Vector3(44f, 0f),
        //  Color.blue, 1f);

    }
}
