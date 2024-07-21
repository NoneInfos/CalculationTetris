using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using UnityEngine.EventSystems;
using DG.Tweening;

public class IGBlock : IGObject, IPointerDownHandler, IPointerUpHandler, IDragHandler
{    

    public List<IGBlockTile> BlockNodes { get; private set; }
    public IGBlockController BlockController { get; set; }

    private BlockShape currentShape;


    private int[,] blockType = IGConfig.IBlock;

    private Vector2 initialPosition = Vector2.zero;

    public override void Initialize()
    {
        base.Initialize();
        this.transform.position = initialPosition;


        if (BlockNodes == null)
            BlockNodes = new List<IGBlockTile>();

        CreateBlock(IGConfig.OBlock);
    }



    private void Start()
    {
        //for(int x = 0; x < 3; ++x)
        //{
        //    for(int y = 0; y < 3; ++y)
        //    {
        //        var index = x * 3 + y;
        //        if (blockType[x,y] == 1)
        //            BlockNodes[index].gameObject.SetActive(true);
        //        else
        //        {
        //            BlockNodes[index].gameObject.SetActive(false);
        //        }
        //    }
        //}
        this.transform.localScale = new Vector3(.7f, .7f, .7f);

        initialPosition = this.transform.position;

        //ApplyTheme(ThemeManager.Instance.CurrentTheme);
        ThemeManager.Instance.OnThemeChanged += ApplyTheme;
    }
    private void OnDestroy()
    {
        ThemeManager.Instance.OnThemeChanged -= ApplyTheme;
    }

    public void CreateBlock(int[,] shape)
    {
        for (int y = -1; y < shape.GetLength(0) - 1; ++y)
        {
            for (int x = -1; x < shape.GetLength(1) - 1; ++x)
            {
                if (shape[y + 1, x + 1] == 1)
                {
                    GameObject tile = PoolManager.Instance.Pop(ETileType.BlockNode);
                    tile.transform.SetParent(transform);
                    tile.transform.localPosition = new Vector3(x, -y, 0) * IGConfig.TILE_WIDTH;
                    BlockNodes.Add(tile.transform.GetComponent<IGBlockTile>());
                }
            }
        }
    }

    public void ClearBlock()
    {
        foreach (var tile in BlockNodes)
        {
            PoolManager.Instance.Push(ETileType.BlockNode, tile.gameObject);
        }
        BlockNodes.Clear();
    }

    private void ApplyTheme(Theme theme)
    {
        foreach (var node in BlockNodes)
        {
            node.GetComponent<SpriteRenderer>().color = theme.blockColor;
        }
    }

    public void SetBlockShape(BlockShape shape)
    {
        currentShape = shape;
        UpdateBlockVisual();
    }

    private void UpdateBlockVisual()
    {
        //foreach (var node in BlockNodes)
        //{
        //    Destroy(node.gameObject);
        //}
        //BlockNodes.Clear();

        //for (int y = 0; y < currentShape.Height; y++)
        //{
        //    for (int x = 0; x < currentShape.Width; x++)
        //    {
        //        if (currentShape.Shape[y, x] == 1)
        //        {
        //            var nodePrefab = Resources.Load<IGBlockTile>("Prefabs/IGBlockTile");
        //            var node = Instantiate(nodePrefab, transform);
        //            node.transform.localPosition = new Vector3(x * IGConfig.TILE_WIDTH, -y * IGConfig.TILE_HEIGHT, 0);
        //            BlockNodes.Add(node);
        //        }
        //    }
        //}
    }

    public void Rotate()
    {
        currentShape = currentShape.Rotate();
        UpdateBlockVisual();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (State == EState.UnStable)
            return;

        HandleOnPointerDown();

        BlockController.HandleBlockOnPointerDown(this);
        AudioManager.Instance.Play("BlockPickup");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (State == EState.UnStable)
            return;

        HandleOnPointerUp();

        BlockController.HandleBlockOnPointerUp();
        AudioManager.Instance.Play("BlockPlace");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (State == EState.UnStable)
            return;

        this.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);

        BlockController.HandleBlockeOnDrag();
    }

    private void HandleOnPointerDown()
    {
        this.transform.localScale = Vector3.one;

        foreach (var node in BlockNodes)
        {
            node.transform.localScale = new Vector3(.8f, .8f, .8f);
        }
    }

    private void HandleOnPointerUp()
    {

    }

    public void AnimatePlaceBlockOnBoard()
    {
        foreach(var tile in BlockNodes)
        {
            if(tile is IGBlockTile node)
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

    public void AnimatePlacement(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, 0.3f).SetEase(Ease.OutBack);
    }

    public void AnimateFade()
    {
        foreach (var node in BlockNodes)
        {
            node.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() => {
                node.gameObject.SetActive(false);
            });
        }
    }
}
