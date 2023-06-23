using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
public class IGTileController : IIGEngineController
{
    public Sprite[] Sprites;
    private static IGTileController _instance;
    private const string T_INNER_ODD = "t_inner_odd";
    private const string T_INNER_EVEN = "t_inner_even";
    //public static IGBGTileController Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<IGBGTileController>();
    //            if (_instance == null)
    //            {
    //                GameObject obj = new GameObject("IGBGTileController");
    //                _instance = obj.AddComponent<IGBGTileController>();
    //            }
    //        }
    //        return _instance;
    //    }
    //}
    
  

    public void InitController()
    {
        //if (_instance != null && _instance != this)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}
        //_instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
}