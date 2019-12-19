using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Horiguchi.Engine;


public enum GameState
{
    StartMenu, InStage, StageClear, GameOver, Pause
}


public class GameManager : Singleton<GameManager>
{
    //public static GameManager Instance;

    //凧Controller
    public KiteController player;
    //
    public StageController stage;

    public BGMController BGM;

    [SerializeField]
    float BGMDelay;

    [SerializeField]
    float ResultDelay;

    float levelTime;

    GameState gameState;


    public Text clear;
    public GameObject RetryButton;
    public GameObject PauseButton;

    //private void Awake()
    //{
    //    Instance = this;
    //}
   

    void Start()
    {
        levelTime = 0;
        clear.enabled = false;
        RetryButton.SetActive(false);
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseStage();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartStage();
        }


        levelTime = Time.timeSinceLevelLoad;

        if (levelTime >= 14.0f)
        {
            //stage.StageClear = true;
        }

        if (levelTime>= 20.0f)
        {
            //GameClear();
        }

        
    }


    /// <summary>
    /// ステージクリア時の処理
    /// </summary>
    public void GameClear()
    {
        gameState = GameState.StageClear;

        //クリア文字を表示させる
        clear.enabled = true;

        //プレイヤーの移動操作をやめさせて
        player.CanMove = false;

        //凧を画面の右に移動させます
        player.AddVelocity(new Vector2(1, 0));

        
    }

    /// <summary>
    /// プレイヤーが死ぬ時の処理
    /// </summary>
    public void GameOver()
    {
        gameState = GameState.GameOver;

       
        //
        stage.SpawnFlag = false;

        player.GameOver();

        StartCoroutine(ShowResultDelay(ResultDelay, gameState));

    } 


    /// <summary>
    /// ステージをスタートさせる
    /// </summary>
    public void StartStage()
    {
        gameState = GameState.InStage;
        SetSpawnState(true);
        StartCoroutine(PlayFrontBGM(BGMDelay));
    }
    /// <summary>
    /// ステージを一時停止させる
    /// </summary>
    public void PauseStage()
    {
        gameState = GameState.Pause;
        SetSpawnState(false);
    }

    /// <summary>
    /// 一時停止を解除させる
    /// </summary>
    public void ResumeStage()
    {
        gameState = GameState.InStage;
        SetSpawnState(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RestartStage()
    {
        gameState = GameState.InStage;
        stage.ResetStage();
    }

    /// <summary>
    /// ステージ生成状態を設定する
    /// </summary>
    /// <param name="ifSpawn">true:障害物を生成させる　false:生成を停止させる</param>
    void SetSpawnState(bool ifSpawn)
    {
        stage.SpawnFlag = ifSpawn;
    }

    /// <summary>
    /// 今のSceneを再読み込み
    /// </summary>
    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }


    IEnumerator PlayFrontBGM(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        BGM.Play("Front");
    }

    IEnumerator ShowResultDelay(float delaySec,GameState state)
    {
        yield return new WaitForSeconds(delaySec);
        switch (state)
        {
           
            case GameState.StageClear:
                //TODO:
                //Show Result

                break;
            case GameState.GameOver:
                //リトライボタンを表示させる
                RetryButton.SetActive(true);
                //一時停止ボタンを隠させる
                PauseButton.SetActive(false);
                //Rollerを隠させる
                Horiguchi.YarnController.Instance.gameObject.SetActive(false);

                AudioController.PlaySnd("crow1", Camera.main.transform.position, 0.5f);
                break;
            default:
                break;
        }

        
    }

}
