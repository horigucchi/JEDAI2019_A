using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// スコアの操作を行うクラス
public class ScoreController : Horiguchi.Engine.Singleton<ScoreController>
{

    // スコア総計
    static int SCORE_NUM = 0;
    // 描画用スコア格納変数
    private int DrawScore = 0;
    // 見た目のスコア加算スピード
    private const int addSpeed = 3;
    // 加算ポイントのフェードスピード
    private const int fadeSpeed = 3;
    // Textオブジェクト
    private GameObject scoreText = null;
    private GameObject addText = null;
    private Vector3 addTextDefPos;

    private bool oldFlag = false;
    private float timeScale;
    void Start()
    {
        SCORE_NUM = 0;
        DrawScore = 0;
        scoreText = gameObject.transform.GetChild(0).gameObject;
        addText = gameObject.transform.GetChild(1).gameObject;
        addTextDefPos = addText.transform.position;
    }

    void Update()
    {
        timeScale = Time.timeScale;
        // Debug用
        if (Input.GetKey(KeyCode.Space)&&(!oldFlag==true))
        {
            AddScore(150);
        }
        oldFlag = Input.GetKey(KeyCode.Space);
        Vector3 addtextPos = addText.transform.position;
        if (DrawScore != SCORE_NUM)
        {
            addText.transform.position = new Vector3(addtextPos.x, addtextPos.y+timeScale, addtextPos.z);
            DrawScore += (int)(addSpeed* timeScale);
            // 描画スコアが本来のスコアを超えないように補正
            DrawScore = (DrawScore > SCORE_NUM) ? SCORE_NUM : DrawScore;
            ChangeText();
        }
        else
        {
            addText.SetActive(false);
        }
    }

    // スコアテキストを変更する関数
    private void ChangeText()
    {
        Text ChangeText = scoreText.GetComponent<Text>();
        ChangeText.text = DrawScore + "てん";
    }

    // スコア加算をする関数
    // AddPoint:加算するポイント
    public void AddScore(int AddPoint)
    {
        DrawScore = SCORE_NUM;
        SCORE_NUM += AddPoint;
        addText.SetActive(true);
        addText.transform.position = addTextDefPos;
        Text ChangeText = addText.GetComponent<Text>();
        ChangeText.text = "+" + AddPoint + "てん";
    }
}
