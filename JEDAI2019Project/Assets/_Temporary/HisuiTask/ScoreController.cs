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
    private int addSpeed = 11;
    // 上昇時間(フレーム)
    private int addSecond = 60;
    // Textオブジェクト
    private GameObject scoreText = null;// スコアポイント
    private GameObject addText = null;// 加算ポイント
    private GameObject addTextNum = null;// 加算ポイント用の数字オブジェクト
    // 加算ポイントの初期座標
    private Vector3 addTextDefPos;
    // タイムスケール入れる変数
    private float timeScale;
    // 上がる最低ライン(上限)
    private const float upperSize = 25;
    // 最大桁数
    private const float maxDigit = 5;
    // スプライト
    private List<List<GameObject>> score;
    private List<List<GameObject>> addScore;
    // ポップアップスコア用座標
    public Vector2 pos;
    public GameObject gb;

    void Start()
    {
        // 各種変数の取得
        score = new List<List<GameObject>>();
        addScore = new List<List<GameObject>>();
        SCORE_NUM = 0;
        DrawScore = 0;
        scoreText = gameObject.transform.GetChild(1).gameObject;
        addText = gameObject.transform.GetChild(2).gameObject;
        addTextDefPos = addText.transform.position;
        addTextNum = addText.transform.GetChild(0).gameObject;
        var pos = gameObject.transform.position;
        for (int j = 0;j < maxDigit; ++j)
        {
            var numList = new List<GameObject>();
            var numList2 = new List<GameObject>();
            for (int i = 0; i < 10; ++i)
            {
                numList.Add(Instantiate(scoreText.transform.GetChild(0).GetChild(i).gameObject));
                numList[i].SetActive(false);
                numList[i].transform.position += new Vector3(pos.x + 45 * (maxDigit - j) + 50, pos.y - 100, pos.z);
                numList[i].transform.parent = scoreText.transform;

                numList2.Add(Instantiate(addText.transform.GetChild(1).GetChild(i).gameObject));
                numList2[i].SetActive(false);
                numList2[i].transform.position += new Vector3(pos.x + 45 * (maxDigit - j) + 50, pos.y - 100, pos.z);
                numList2[i].transform.parent = addTextNum.transform;
            }
            score.Add(numList);
            addScore.Add(numList2);
        }
        score[0][0].SetActive(true);
        addTextNum.transform.localScale *= 0.5f;
    }
       
    private bool oldFlag = false; // Debug用

    void Update()
    {
        timeScale = Time.timeScale;
        // Debug用
        if (Input.GetKey(KeyCode.Space)&&(!oldFlag==true))
        {
            AddScore(120, gb.transform.position);
        }
        oldFlag = Input.GetKey(KeyCode.Space);
        Vector3 addtextPos = addText.transform.position;


        if (DrawScore != SCORE_NUM)
        {
            DrawScore += (int)(addSpeed * timeScale);
            // 描画スコアが本来のスコアを超えないように補正
            DrawScore = (DrawScore > SCORE_NUM) ? SCORE_NUM : DrawScore;
            ChangeText();
        }
        if (upperSize < (addtextPos.y - addTextDefPos.y))
        {
            addText.SetActive(false);
        }

        if (addText.activeSelf)
        {
            addText.GetComponent<RectTransform>().position = new Vector3(pos.x, addtextPos.y + timeScale, pos.y + timeScale);
        }
    }

    // スコアテキストを変更
    private void ChangeText()
    {        
        for (int j = 0; j < maxDigit; ++j)
        {
            int num = (DrawScore/(int)Mathf.Pow(10,j)) % 10;
            for (int i = 0; i < 10; ++i)
            {
                score[j][i].SetActive(false);
            }
            score[j][num].SetActive(true);
            if ((DrawScore / (int)Mathf.Pow(10, j))==0)
            {
                score[j][num].SetActive(false);
            }
        }
    }

    // スコア総計を返す
    public int GetScore()
    {
        return SCORE_NUM;
    }

    // スコア加算をする
    // AddPoint:加算するポイント
    // Position:当たったオブジェクトの座標(この座標から獲得ポイントがポップアップする)
    public void AddScore(int AddPoint,Vector3 Position)
    {
        pos = RectTransformUtility.WorldToScreenPoint(Camera.main, Position);
        addTextDefPos = pos;
        addSpeed = AddPoint / addSecond;
        DrawScore = SCORE_NUM;
        SCORE_NUM += AddPoint;
        addText.SetActive(true);
        addText.transform.position = addTextDefPos;
        for (int j = 0; j < maxDigit; ++j)
        {
            int num = (AddPoint / (int)Mathf.Pow(10, j)) % 10;
            for (int i = 0; i < 10; ++i)
            {
                addScore[j][i].SetActive(false);
            }
            addScore[j][num].SetActive(true);
            if ((AddPoint / (int)Mathf.Pow(10, j)) == 0)
            {
                addScore[j][num].SetActive(false);
            }
        }
    }

    public void AddScore(int AddPoint)
    {
        addSpeed = AddPoint / addSecond;
        DrawScore = SCORE_NUM;
        SCORE_NUM += AddPoint;
        addText.SetActive(true);
        addText.transform.position = addTextDefPos;
        for (int j = 0; j < maxDigit; ++j)
        {
            int num = (AddPoint / (int)Mathf.Pow(10, j)) % 10;
            for (int i = 0; i < 10; ++i)
            {
                addScore[j][i].SetActive(false);
            }
            addScore[j][num].SetActive(true);
            if ((AddPoint / (int)Mathf.Pow(10, j)) == 0)
            {
                addScore[j][num].SetActive(false);
            }
        }
    }
}
