using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using UnityEngine.EventSystems;
using DG.Tweening;

public class IGTile_Block : IGObject, IPointerDownHandler, IPointerUpHandler, IDragHandler
{    
    [SerializeField] private List<IGTile_BlockNode> _blockNodes;

    public List<IGTile_BlockNode> BlockNodes { get { return _blockNodes; } }

    public IGBlcokController BlockController { get; set; } = null;

    public bool IsNodeAllColide
    { get
        {
            if(_blockNodes == null || _blockNodes.Count < 1)
            {
                return false;
            }

            foreach(var node in _blockNodes)
                if(!node.IsColide)
                    return false;

            return true;
        }
    }

    private int[,] blockType = IGConfig.IBlock;

    private Vector2 initialPosition = Vector2.zero;

    public override void Initialize()
    {
        base.Initialize();
        this.transform.position = initialPosition;
    }

    private void Start()
    {
        for(int x = 0; x < 3; ++x)
        {
            for(int y = 0; y < 3; ++y)
            {
                var index = x * 3 + y;
                if (blockType[x,y] == 1)
                    _blockNodes[index].gameObject.SetActive(true);
                else
                {
                    _blockNodes[index].gameObject.SetActive(false);
                }
            }
        }
        this.transform.localScale = new Vector3(.7f, .7f, .7f);

        initialPosition = this.transform.position;
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        if (State == EState.UnStable)
            return;

        this.transform.localScale = Vector3.one;

        foreach (var node in _blockNodes)
        {
            node.transform.localScale = new Vector3(.8f, .8f, .8f);
        }

        BlockController.HandleBlockOnPointerDown(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (State == EState.UnStable)
            return;

        BlockController.HandleBlockOnPointerUp();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (State == EState.UnStable)
            return;

        this.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);

        BlockController.HandleBlockeOnDrag();
    }

    public void AnimatePlaceBlockOnBoard()
    {
        foreach(var tile in _blockNodes)
        {
            if(tile is IGTile_BlockNode node)
            {
                if (node.NearestTile == null)
                    continue;

                DOTween.To(() => node.transform.position, pos => node.transform.position = pos, node.NearestObject.transform.position, 0.1f);
                this.transform.localScale = Vector3.one;
                node.transform.localScale = Vector3.one;
                if (node.NearestTile != null)
                {
                    node.NearestTile.IsPlaceBlock = true;
                    node.State = EState.UnStable;
                }
            }
        }
        this.State = EState.UnStable;
    }
}
