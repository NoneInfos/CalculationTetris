using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGMain
{
    public class IGBlockTile : IGTile
    {

        private Collider2D _nearestColider;

        private float _nearestObjectDist;



        public GameObject NearestObject { get { return _nearestColider != null ? _nearestColider.gameObject : null; } }

        public IGTile NearestTile { get { return _nearestColider != null ? _nearestColider.gameObject.GetComponent<IGTile>() : null; } }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(IGConfig.TILE_WIDTH_HALF, IGConfig.TILE_HEIGHT_HALF, 0));

            Gizmos.color = Color.red;
            if (_nearestColider != null)
            {
                Gizmos.DrawWireSphere(_nearestColider.transform.position, 20f);
            }
        }


        public override void SetCollide(bool isCollide)
        {
            IsColide = isCollide;
            UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            // 충돌 상태에 따른 시각적 피드백
            _spriteRenderer.color = IsColide ?
                new Color(1f, 0.3f, 0.3f, 0.8f) : // 붉은색 (충돌)
                Color.white; // 기본색
        }

    }


}
