using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ポーズ画面のクラス
public class Pause : MonoBehaviour
{
    // 必要なオブジェクト
    public GameObject panel = null;
    // ポーズボタン
    public GameObject button = null;
    // ゲームに戻るボタン
    public GameObject returnButton = null;
    // 最初からボタン
    public GameObject restartButton = null;


    private void Start()
    {
        // オブジェクトの取得
        panel = gameObject.transform.GetChild(0).gameObject;

        button = gameObject.transform.GetChild(1).gameObject;
        returnButton = panel.transform.GetChild(0).gameObject;
        restartButton = panel.transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if (button.GetComponent<PauseButton>().GetFlag())
        {
            panel.SetActive(true);
            returnButton.SetActive(true);
            button.GetComponent<PauseButton>().SetFlag();
            Time.timeScale = 0.0f;
        }
        else
        {
            if (returnButton.GetComponent<PauseButton>().GetFlag())
            {
                panel.SetActive(false);
                button.SetActive(true);
                returnButton.GetComponent<PauseButton>().SetFlag();
                Time.timeScale = 1.0f;
            }
            else if (restartButton.GetComponent<PauseButton>().GetFlag())
            {
                panel.SetActive(false);
                button.SetActive(true);
                returnButton.GetComponent<PauseButton>().SetFlag();
                Time.timeScale = 1.0f;
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            }
        }
    }
}
