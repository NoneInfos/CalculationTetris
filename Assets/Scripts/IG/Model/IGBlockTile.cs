using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGMain
{
    public class IGBlockTile : IGTile
    {
        [SerializeField] Sprite SPR_BG;


        
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_nearestColider == null)
                _nearestColider = collision;
            else
            {
                var dist = Vector3.Distance(this.gameObject.transform.position, collision.gameObject.transform.position);

                if (dist < _nearestObjectDist)
                    _nearestObjectDist = dist;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision == _nearestColider)
            {
                _nearestColider = null;
            }
        }
    }


}
