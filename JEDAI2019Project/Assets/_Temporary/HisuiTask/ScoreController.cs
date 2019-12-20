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
    // 上がる最低ライン(上限)
    private const float upperSize = 25;
    // 最大桁数
    private const float maxDigit = 5;
    // 経過時間
    private float time = 0;
    // スプライト
    private List<List<GameObject>> score;
    private List<List<GameObject>> addScore;
    // ポップアップスコア用座標
    public Vector2 pos;
    public GameObject gb;

    private void OnEnable()
    {
        SCORE_NUM = 0;
        DrawScore = 0;
    }

    void Start()
    {
        // 各種変数の取得と初期化
        score = new List<List<GameObject>>();
        addScore = new List<List<GameObject>>();
        SCORE_NUM = 0;
        DrawScore = 0;
        scoreText = gameObject.transform.GetChild(1).gameObject;
        addText = gameObject.transform.GetChild(2).gameObject;
        addTextDefPos = addText.transform.position;
        addTextNum = addText.transform.GetChild(0).gameObject;
        var pos = gameObject.transform.position;
        // 数字のスプライトの複製と初期化
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
        addTextNum.transform.localScale *= 0.5f;// スケールの調整
    }
       
    private bool oldFlag = false; // Debug用

    void Update()
    {
        // Debug用
        if (Input.GetKey(KeyCode.Space)&&(!oldFlag==true))
        {
            AddScore(150, gb.transform.position);
        }
        oldFlag = Input.GetKey(KeyCode.Space);

        // 加算ポイントの現座標を取得する
        Vector3 addtextPos = addText.transform.position;

        // 描画スコアとスコア総計が違っていたとき
        if (DrawScore != SCORE_NUM)
        {
            // 描画スコアを加算する
            DrawScore += (int)(addSpeed * Time.timeScale);
            // 描画スコアが本来のスコアを超えないように補正
            DrawScore = (DrawScore > SCORE_NUM) ? SCORE_NUM : DrawScore;
            // テキストの変更
            ChangeText();
        }

        // テキストが登っていく高さの上限を超えたとき
        if (upperSize < (addtextPos.y - addTextDefPos.y))
        {
            // テキストを非アクティブにする
            addText.SetActive(false);
        }

        // 加算テキストがアクティブのとき
        if (addText.activeSelf)
        {
            // 上へ登っていく
            addText.GetComponent<RectTransform>().position = new Vector3(pos.x, addtextPos.y + Time.timeScale, pos.y + Time.timeScale);
        }
    }

    // スコアテキストを変更
    private void ChangeText()
    {
        for (int j = 0; j < maxDigit; ++j)
        {
            // 一旦すべてのスプライトを非アクティブ状態にする
            for (int i = 0; i < 10; ++i)
            {
                // アクティブのスプライトかどうかのチェック
                if (score[j][i].activeSelf)
                {
                    score[j][i].SetActive(false);
                }
            }
            // 現在の位の数値を取得する
            int num = (DrawScore/(int)Mathf.Pow(10,j)) % 10;
            // 取得した数値のスプライトのみをアクティブにする
            score[j][num].SetActive(true);
            // ゼロ埋めになってしまっているスプライトを非アクティブにする
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

    // スコアを消す
    public void ScoreClear()
    {
        SCORE_NUM = 0;
        DrawScore = 0;
    }

    // スコア加算をする
    // AddPoint:加算するポイント
    // Position:当たったオブジェクトの座標(この座標から獲得ポイントがポップアップする)
    public void AddScore(int AddPoint,Vector3 Position)
    {
        // 取得したワールド座標をスクリーン座標へ変換し、ポップアップ基準値にする
        pos = RectTransformUtility.WorldToScreenPoint(Camera.main, Position);
        addTextDefPos = pos;

        AddScore(AddPoint);
    }

    // スコア加算をする
    // AddPoint:加算するポイント
    // 第二引数なしの場合のスコア加算(非推奨)
    // そのまま使うと加算テキストがポップアップなしになる
    public void AddScore(int AddPoint)
    {
        // 1F当たりの加算値を算出
        addSpeed = AddPoint / addSecond;

        DrawScore = SCORE_NUM;
        SCORE_NUM += AddPoint;

        // 加算されたため加算テキストをアクティブ状態にし、ポップアップ対象の場所へ移動
        addText.SetActive(true);
        addText.transform.position = addTextDefPos;

        // スプライトの更新
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
