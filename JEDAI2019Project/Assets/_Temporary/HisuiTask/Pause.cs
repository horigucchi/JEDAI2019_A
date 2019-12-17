using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ポーズ画面のクラス
public class Pause : MonoBehaviour
{
    // 必要なオブジェクト
    public GameObject panel = null;
    // ポーズボタン
    public GameObject button = null;
    // ゲームに戻るボタン
    public GameObject returnButton = null;


    private void Start()
    {
        // オブジェクトの取得
        panel = gameObject.transform.GetChild(0).gameObject;

        button = gameObject.transform.GetChild(1).gameObject;
        returnButton = panel.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (button.GetComponent<Button>().GetFlag())
        {
            panel.SetActive(true);
            returnButton.SetActive(true);
            button.GetComponent<Button>().SetFlag();
            Time.timeScale = 0.0f;
        }
        else
        {
            if (returnButton.GetComponent<Button>().GetFlag())
            {
                panel.SetActive(false);
                button.SetActive(true);
                returnButton.GetComponent<Button>().SetFlag();
                Time.timeScale = 1.0f;
            }
        }
    }
}
