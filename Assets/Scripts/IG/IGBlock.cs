using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using UnityEngine.EventSystems;
using DG.Tweening;

public class IGBlock : IGObject, IPointerDownHandler, IPointerUpHandler, IDragHandler
{    
    public List<IGBlockTile> BlockNodes { get; private set; }

    public IGBlcokController BlockController { get; set; } = null;
  

    private int[,] blockType = IGConfig.IBlock;

    private Vector2 initialPosition = Vector2.zero;

    public override void Initialize()
    {
        base.Initialize();
        this.transform.position = initialPosition;

        var prefab = Resource.Load<IGBlockTile>();
        for(int i = 0; i< 9; ++i)
        {
            var obj = Instanciate(prefab,this.transform);
            obj.SetActive(false);
            BlockNodes.Add(obj);
        }
    }

    private void Start()
    {
        for(int x = 0; x < 3; ++x)
        {
            for(int y = 0; y < 3; ++y)
            {
                var index = x * 3 + y;
                if (blockType[x,y] == 1)
                    BlockNodes[index].gameObject.SetActive(true);
                else
                {
                    BlockNodes[index].gameObject.SetActive(false);
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

        foreach (var node in BlockNodes)
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
        foreach(var tile in BlockNodes)
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

    public bool IsAllBlockNodeColideWithBoardNode()
    {   
        if(BlockNodes == null || BlockNodes.Count < 1)
            {
                return false;
            }

            foreach(var node in BlockNodes)
                if(!node.IsColide)
                    return false;

            return true;
    }
}
