using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;

public class IGInputController : MonoBehaviour
{
   

    public void InitializeController(IGController inParentController)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        ///////////////////////////////////////////////////////////////////////////////////
        //#if UNITY_EDITOR
        //        if (Input.GetMouseButton(0))
        //        {
        //            var currentTouchPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //            Vector3 LB = new Vector3(currentTouchPostion.x - ((76 * 3) / 2), currentTouchPostion.y, currentTouchPostion.z);
        //            Vector3 LT = new Vector3(currentTouchPostion.x - ((76 * 3) / 2), currentTouchPostion.y + ((76 * 3)), currentTouchPostion.z);
        //            Vector3 RT = new Vector3(currentTouchPostion.x + ((76 * 3) / 2), currentTouchPostion.y + ((76 * 3)), currentTouchPostion.z);
        //            Vector3 RB = new Vector3(currentTouchPostion.x + ((76 * 3) / 2), currentTouchPostion.y, currentTouchPostion.z);
        //            _lineRenderer.positionCount = 5;
        //            _lineRenderer.SetPositions(new Vector3[5] { LB, LT, RT, RB, LB });
        //        }
        //#endif

        ///////////////////////////////////////////////////////////////////////////////////

        //#if UNITY_EDITOR
        //        if (Input.GetMouseButton(0))
        //        {
        //            if (_isSampleObejct)
        //            {
        //                Debug.LogError(_ghostObjectParent.transform.position);
        //                _ghostObjectParent.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        //                return;
        //            }
        //            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        //            RaycastHit hitInfo;
        //            if (Physics.Raycast(rayOrigin, out hitInfo))
        //            {
        //                //if (hitInfo.transform.gameObject.GetComponent<Block>() != null)
        //                //{
        //                //    GameObject _target = hitInfo.transform.gameObject;
        //                //    Color randomColor = new Color(Random.value, Random.value, Random.value);
        //                //    Color randomUnityColor = UnityEngine.Random.ColorHSV();
        //                //    if (hitInfo.transform.GetComponent<Renderer>() != null)
        //                //    {
        //                //        Renderer renderer = hitInfo.transform.GetComponent<Renderer>();
        //                //        renderer.material.color = randomUnityColor;
        //                //    }
        //                //    if (_isSampleObejct)
        //                //        return;
        //                //    _isSampleObejct = true;
        //                //    for (int i = (COL / 2) * -1; i < (COL / 2) + 1; ++i)
        //                //    {
        //                //        for (int j = (ROW / 2) * -1; j < (ROW / 2) + 1; ++j)
        //                //        {
        //                //            float x = _target.transform.position.x + (i * 0.5f);
        //                //            float y = _target.transform.position.y + (j * 0.5f);
        //                //            GameObject obj = Instantiate(_object, new Vector2(x, y), Quaternion.identity, _ghostObjectParent);
        //                //            obj.SetActive(true);
        //                //        }
        //                //    }
        //                //}
        //            }
        //        }
        //        else
        //        {
        //            _isSampleObejct = false;
        //            _ghostObjectParent.transform.position = Vector3.zero;
        //        }
        //#endif
        //        foreach (Touch touch in Input.touches)
        //        {
        //        }
    }
}
