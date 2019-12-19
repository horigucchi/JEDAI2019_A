using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ポーズ画面のクラス
public class Pause : MonoBehaviour
{
    [SerializeField]
    private Slider slider = null;
    // 必要なオブジェクト
    private GameObject panel = null;
    // ポーズボタン
    private GameObject button = null;
    // ゲームに戻るボタン
    private GameObject returnButton = null;
    // 最初からボタン
    private GameObject restartButton = null;
    // スライダーの値変更用
    private float val = 0;

    private void Start()
    {
        // オブジェクトの取得
        panel = gameObject.transform.GetChild(0).gameObject;
        button = gameObject.transform.GetChild(1).gameObject;
        returnButton = panel.transform.GetChild(0).gameObject;
        restartButton = panel.transform.GetChild(1).gameObject;
        val = 0;
    }

    private void Update()
    {
        // ポーズボタンが押されたとき
        if (button.GetComponent<PauseButton>().GetFlag())
        {
            // ポーズの処理に入る
            panel.SetActive(true);
            returnButton.SetActive(true);
            button.GetComponent<PauseButton>().ReverseFlag();
            Time.timeScale = 0.0f;
        }
        else
        {
            // ポーズ中にゲームに戻るボタンが押されたとき
            if (returnButton.GetComponent<PauseButton>().GetFlag())
            {
                // ポーズを解除する
                PauseEnd();
            }
            else if (restartButton.GetComponent<PauseButton>().GetFlag())
            {
                // ポーズを解除してシーンを再読み込みする
                PauseEnd();
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            }
        }

        val += Time.deltaTime;
        slider.value = val;
    }

    // ポーズを解除する関数
    private void PauseEnd()
    {
        // ポーズ中かどうかのチェック
        if (!button.activeSelf)
        {
            panel.SetActive(false);
            button.SetActive(true);
            returnButton.GetComponent<PauseButton>().ReverseFlag();
            Time.timeScale = 1.0f;
        }
    }
}
