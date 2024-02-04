using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using UnityEngine.EventSystems;

public class IGTile_Block : IGObject, IPointerDownHandler, IPointerUpHandler, IDragHandler
{    
    [SerializeField] List<IGTile> _blockTiles;

    public IGBlcokController BlockController { get; set; } = null;

    public bool IsNodeAllColide
    { get
        {
            if(_blockTiles == null || _blockTiles.Count < 1)
            {
                return false;
            }

            foreach(var node in _blockTiles)
                if(!node.IsColide)
                    return false;

            return true;
        }
    }

    private int[,] blockType = IGConfig.IBlock;

    private Vector2 initialPosition = Vector2.zero;

    
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
        BlockController.IsBlockMoving = true;
        BlockController.SelectedBlock = this;
        this.transform.localScale = new Vector3(1f,1f,1f);
        foreach (var node in _blockTiles)
        {
            ((IGTile_BlockNode)node).transform.localScale = new Vector3(.8f, .8f, .8f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        BlockController.IsBlockMoving = false;
        BlockController.SelectedBlock = null;
        this.transform.localScale = new Vector3(.7f, .7f, .7f);
        this.transform.position = initialPosition;
        foreach (var node in _blockTiles)
        {
            ((IGTile_BlockNode)node).transform.localScale = new Vector3(1f,1f,1f);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);

        //Debug.DrawLine(Camera.main.ScreenToWorldPoint(eventData.position) - new Vector3(44f, 0f),
        //  Camera.main.ScreenToWorldPoint(eventData.position) + new Vector3(44f, 0f),
        //  Color.blue, 1f);

    }

    public void PlaceOnBoard()
    {

    }

    private void OnBoardAnimation()
    {
        
    }
}
