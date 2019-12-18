using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    startMenu,StageClear,GameOver,Pause
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //凧Controller
    public KiteController player;
    //
    public StageController stage;


    float levelTime;


    GameState gameState;


    public Text clear;
    public GameObject RetryButton;
    public GameObject PauseButton;

    private void Awake()
    {
        Instance = this;
    }
   

    void Start()
    {
        levelTime = 0;
        clear.enabled = false;
        RetryButton.SetActive(false);
    }

    
    void Update()
    {


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

        //クリア文字を表示させる
        clear.enabled = true;

        //プレイヤーの移動操作をやめさせて
        player.CanMove = false;

        //凧を画面の右に移動させます
        player.AddForce(new Vector2(1, 0));
    }

    /// <summary>
    /// プレイヤーが死ぬ時の処理
    /// </summary>
    public void GameOver()
    {

        //リトライボタンを表示させる
        RetryButton.SetActive(true);
        //一時停止ボタンを隠させる
        PauseButton.SetActive(false);
        //Rollerを隠させる
        Horiguchi.YarnController.Instance.gameObject.SetActive(false);
        //ステージクリアFlagをTrueにする
        stage.StageClear = true;

        player.GameOver();

    } 

    /// <summary>
    /// 今のSceneを再読み込み
    /// </summary>
    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
