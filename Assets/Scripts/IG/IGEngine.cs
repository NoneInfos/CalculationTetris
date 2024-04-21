using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
public class IGEngine : MonoBehaviour
{
    public IGBlcokController _blockController { private set; get; }

    public IGInputController _inputController { private set; get; }

    public IGBoardController _boardController { private set; get; }

    [SerializeField] Transform TF_Cameras;

    private void Awake()
    {
        PoolManager.Instacne.InitializeManager();

        TF_Cameras.transform.position = new Vector2(IGConfig.SCREEN_WIDTH_HALF - ((IGConfig.TILE_WIDTH_HALF / 2) * 3), 200f);
    }

    private void Start()
    {
        _blockController = CreateObj<IGBlcokController>(this.transform, true, "InGame");
        _blockController.SetEngine(this);
        _blockController.InitializeController();

        _boardController = CreateObj<IGBoardController>(this.transform, true, "InGame");
        _boardController.SetEngine(this);
        _boardController.InitializeController();
    }


    private void Update()
    {
        _blockController.UpdateController();
        _boardController.UpdateController();
    }


    public T CreateObj<T>(Transform inParent, bool isActive = false, string inLayerName = "Default") where T : Component
    {
        GameObject go = new GameObject(typeof(T).Name);
        go.layer = LayerMask.NameToLayer(inLayerName);
        go.transform.SetParent(inParent);
        return go.AddComponent<T>();
    }

}
