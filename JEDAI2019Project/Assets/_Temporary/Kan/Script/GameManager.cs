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

    public KiteController player;
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
    // Start is called before the first frame update
    void Start()
    {
        levelTime = 0;
        clear.enabled = false;
        RetryButton.SetActive(false);
    }

    // Update is called once per frame
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


    void GameClear()
    {
        
        stage.ScrollSpeed = 0f;
        clear.enabled = true;


        player.CanMove = false;
        player.AddForce(new Vector2(1, 0));
    }

    public void GameOver()
    {
        RetryButton.SetActive(true);
        PauseButton.SetActive(false);
        Horiguchi.YarnController.Instance.gameObject.SetActive(false);
        stage.StageClear = true;
        stage.ScrollSpeed = 0f;
    } 

    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
