using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IGMain
{
    public class IGBlockView : MonoBehaviour
    {
        
        private void SetScale(Vector2 scale){
            this.transform.localScale = scale;
        }

        public void Rotate()
        {
            //currentShape = currentShape.Rotate();
        }



        //private void ApplyTheme(Theme theme)
        //{
        //    foreach (var node in BlockNodes)
        //    {
        //        node.GetComponent<SpriteRenderer>().color = theme.blockColor;
        //    }
        //}

        //public void AnimatePlaceBlockOnBoard()
        //{
        //    foreach (var tile in BlockNodes)
        //    {
        //        if (tile is IGBlockTile node)
        //        {
        //            if (node.NearestTile == null)
        //                continue;

        //            DOTween.To(() => node.transform.position, pos => node.transform.position = pos, node.NearestObject.transform.position, 0.1f);
        //            this.transform.localScale = Vector3.one;
        //            node.transform.localScale = Vector3.one;
        //            if (node.NearestTile != null)
        //            {
        //                node.NearestTile.IsPlaceBlock = true;
        //                node.State = EState.UnStable;
        //            }
        //        }
        //    }
        //    this.State = EState.UnStable;
        //}

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

        //public void AnimatePlacement(Vector3 targetPosition)
        //{
        //    transform.DOMove(targetPosition, 0.3f).SetEase(Ease.OutBack);
        //}

        //public void AnimateFade()
        //{
        //    foreach (var node in BlockNodes)
        //    {
        //        node.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() => {
        //            node.gameObject.SetActive(false);
        //        });
        //    }
        //}
    }
}
