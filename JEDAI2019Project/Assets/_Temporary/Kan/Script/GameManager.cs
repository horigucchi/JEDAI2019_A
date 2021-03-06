﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Horiguchi.Engine;


public enum GameState
{
    StartMenu, InStage, StageClear, GameOver, Pause
}

/// <summary>
/// ゲーム遷移クラス
/// </summary>
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

    [SerializeField]
    private Slider slider = null;

    GameState gameState;

    public GameObject ClearUI;
    public GameObject StageClearUI;
    public GameObject RetryUI;
    public GameObject PauseUI;
    public GameObject PointUI;

    //private void Awake()
    //{
    //    Instance = this;
    //}
   

    void Start()
    {
        StageClearUI.SetActive(false);
        RetryUI.SetActive(false);
        //StartStage();
    }

    
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    PauseStage();
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    RestartStage();
        //}

    }


    /// <summary>
    /// ステージクリア時の処理
    /// </summary>
    public void GameClear()
    {
        gameState = GameState.StageClear;

        if(stage.CurrentStage == 1)
        {
            //クリア文字を表示させる
            StageClearUI.SetActive(true);
        }
        if(stage.CurrentStage == 2)
        {
            ClearUI.SetActive(true);
        }

        //プレイヤーの移動操作をやめさせて
        player.CanMove = false;

        //凧を画面の右に移動させます
        player.AddVelocity(new Vector2(1, 0));

        //PointUI.SetActive(false);

    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    public void GameOver()
    {
        gameState = GameState.GameOver;

        stage.SpawnFlag = false;

        player.GameOver();

        StartCoroutine(ShowResultDelay(ResultDelay));

        PointUI.SetActive(false);
    } 


    /// <summary>
    /// ステージをスタートさせる
    /// </summary>
    public void StartStage(int stageNumber)
    {
        gameState = GameState.InStage;
        StartCoroutine(PlayFrontBGM(BGMDelay));
        slider.maxValue = GetStageLeftTime();
        slider.value = 0;
        stage.CurrentStage = stageNumber;
        Horiguchi.YarnController.Instance.gameObject.SetActive(true);
        slider.maxValue = GetStageLeftTime();
        slider.value = 0;
        SetSpawnState(true);
        PointUI.SetActive(true);
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
        //
        gameState = GameState.InStage;
        //
        PauseStage();
        stage.ResetStage();

        Pause.Instance.Reset();
        player.Reset();

        Horiguchi.YarnController.Instance.gameObject.SetActive(true);
        slider.maxValue = GetStageLeftTime();
        slider.value = 0;
        RetryUI.SetActive(false);
        PauseUI.SetActive(true);

        ResumeStage();

        PointUI.SetActive(true);
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


    /// <summary>
    /// リザルト画面を遅延表示させる
    /// </summary>
    /// <param name="delaySec">遅延秒数</param>
    IEnumerator ShowResultDelay(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        switch (gameState)
        {
           
            case GameState.StageClear:
                //TODO:
                //Show Result
                GameClear();
                break;
            case GameState.GameOver:
                //リトライボタンを表示させる
                RetryUI.SetActive(true);
                //一時停止ボタンを隠させる
                PauseUI.SetActive(false);
                //Rollerを隠させる
                Horiguchi.YarnController.Instance.gameObject.SetActive(false);

                //AudioController.PlaySnd("crow1", Camera.main.transform.position, 0.5f);
                AudioController.PlaySnd("gameover2", Camera.main.transform.position, 0.5f);
                break;
            default:
                break;
        }

        
    }


    public float GetStageLeftTime()
    {
        return stage.GetStageLeftTime();
    }


    public void StartStage2()
    {
        Debug.Log("stage2");
        //stage
        PauseStage();
        stage.CurrentStage = 2;
        stage.ResetStage();
        ResumeStage();

        Debug.Log(GetStageLeftTime());
        //ui
        slider.maxValue = GetStageLeftTime();
        Pause.Instance.Reset();
        slider.value = 0f;

        StageClearUI.SetActive(false);
        PointUI.SetActive(true);



        //player
        player.Reset();

    }
}
