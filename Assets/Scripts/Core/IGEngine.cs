using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IGMain;
public class IGEngine : MonoBehaviour
{

    private float gameTime = 0f;

    [SerializeField] private GameObject settingsMenuPanel;

    [SerializeField] Transform TF_Cameras;

    private bool isChallengeModeActive = false;


    private float gameStartTime;

    private void Awake()
    {
        PoolManager.Instance.InitializeManager();
        IGGameManager.Instance.InitializeManager();
    }


    void SetupCamera()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);

        float verticalSize = (IGConfig.BOARD_ROW * IGConfig.TILE_HEIGHT / 2f);
        float horizontalSize = (IGConfig.BOARD_COL * IGConfig.TILE_WIDTH / 2f);

        // 화면 비율에 따라 적절한 size 선택
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = horizontalSize / verticalSize;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = verticalSize;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = verticalSize * differenceInSize;
        }
    }



    private void Start()
    {

    }
   
    private void OnDestroy()
    {
    }

    private void Update()
    {

    }

    public void StartGame()
    {
        SetupCamera();
        GameStateManager.Instance.SetGameState(GameState.Playing);

        DifficultyManager.Instance.ResetDifficulty();
        SaveManager.Instance.IncrementGamesPlayed();
        AudioManager.Instance.Play("GameStart");
    }

   

    
}
