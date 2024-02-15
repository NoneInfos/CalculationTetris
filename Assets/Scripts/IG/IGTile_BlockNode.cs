using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
using TMPro;
using UnityEngine.EventSystems;
public class IGTile_BlockNode : IGTile
{

    [SerializeField] private LayerMask layerMask;
    private float radius = 36f;

    private Collider2D[] colliderList;

    private Collider2D nearestCollider;

    public GameObject NearestObject { get { return nearestCollider?.gameObject; } }

    public IGTile NearestTile { get { return nearestCollider?.gameObject.GetComponent<IGTile>();  } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(IGConfig.TILE_WIDTH_HALF, IGConfig.TILE_HEIGHT_HALF, 0));

        Gizmos.color = Color.red;
        if(nearestCollider != null)
        {
            Gizmos.DrawWireSphere(nearestCollider.transform.position, 20f);

        }

    }

    private void Update()
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        //nearestCollider = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        Vector2 left = (Vector2)transform.position - new Vector2(IGConfig.TILE_WIDTH_HALF, IGConfig.TILE_HEIGHT_HALF);
        Vector2 right = (Vector2)transform.position + new Vector2(IGConfig.TILE_WIDTH_HALF, IGConfig.TILE_HEIGHT_HALF);
        var nearestColliders = Physics2D.OverlapAreaAll(left, right, layerMask);

        if (nearestColliders.Length <= 0)
        {
            //collider.GetComponent<IGTile_BG>()?.SetCollide(false);
        }
        else
        {

            float closestDistance = 0f;

            foreach (Collider2D collider in nearestColliders)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                if(closestDistance == 0f)
                {
                    nearestCollider = collider;
                    closestDistance = distance;
                }
                else if (distance < closestDistance)
                {
                    nearestCollider = collider;
                    closestDistance = distance;
                }
            }

            if(NearestTile != null) NearestTile.IsColide = true;
        }
    }

    
}
