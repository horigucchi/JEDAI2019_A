using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNumber : MonoBehaviour
{
    // スプライト
    [SerializeField]
    private List<List<GameObject>> nowScore;
    [SerializeField]
    private List<List<GameObject>> highScore;
    private const int maxDigit = 5;
    [SerializeField]
    private GameObject Numbers_N = null;
    [SerializeField]
    private GameObject Numbers_R = null;

    private Vector3 pos;
    private int DrawScore;
    private int DrawHighScore;

    private void Start()
    {
        DrawScore = 0;
        DrawHighScore = HighScoreDataIO.Instance.HighScoreLoad("/Score/HighScore.txt");
        // 各種変数の取得と初期化
        nowScore = new List<List<GameObject>>();
        highScore = new List<List<GameObject>>();
        pos = transform.position;
        // 数字のスプライトの複製
        for (int j = 0; j < maxDigit; ++j)
        {
            var numList = new List<GameObject>();
            var numList2 = new List<GameObject>();
            for (int i = 0; i < 10; ++i)
            {
                numList.Add(Instantiate(Numbers_N.transform.GetChild(i).gameObject));
                numList[i].SetActive(false);
                numList[i].transform.parent = transform.GetChild(0).transform;
                numList[i].transform.position = new Vector3((maxDigit - j)-2 , pos.y+2.5f, pos.z);
                numList[i].transform.localScale = new Vector3(0.4f,0.5f,1);
                numList[i].GetComponent<Renderer>().sortingOrder = 105;

                if (Numbers_R != null)
                {
                    numList2.Add(Instantiate(Numbers_R.transform.GetChild(i).gameObject));
                    numList2[i].SetActive(false);
                    numList2[i].transform.parent = transform.GetChild(1).transform;
                    numList2[i].transform.position = new Vector3((maxDigit - j) - 2, pos.y - 2.5f, pos.z);
                    numList2[i].transform.localScale = new Vector3(0.4f, 0.5f, 1);
                    numList2[i].GetComponent<Renderer>().sortingOrder = 105;
                }
            }
            nowScore.Add(numList);
            if (Numbers_R != null)
            {
                highScore.Add(numList2);
            }
        }
    }

    private void Update()
    {
        Draw();
    }

    private void Draw()
    {
        DrawScore = ScoreController.Instance.GetScore();

        for (int j = 0; j < maxDigit; ++j)
        {
            // 一旦すべてのスプライトを非アクティブ状態にする
            for (int i = 0; i < 10; ++i)
            {
                // アクティブのスプライトかどうかのチェック
                if (nowScore[j][i].activeSelf)
                {
                    nowScore[j][i].SetActive(false);
                }
                if (highScore[j][i].activeSelf)
                {
                    highScore[j][i].SetActive(false);
                }

            }
            // 現在の位の数値を取得する
            int num = (DrawScore / (int)Mathf.Pow(10, j)) % 10;
            int num2 = (DrawHighScore / (int)Mathf.Pow(10, j)) % 10;
            // 取得した数値のスプライトのみをアクティブにする
            nowScore[j][num].SetActive(true);
            highScore[j][num2].SetActive(true);
            // ゼロ埋めになってしまっているスプライトを非アクティブにする
            if ((DrawScore / (int)Mathf.Pow(10, j)) == 0)
            {
                nowScore[j][num].SetActive(false);
            }
            if ((DrawHighScore / (int)Mathf.Pow(10, j)) == 0)
            {
                highScore[j][num2].SetActive(false);
            }
        }
    }
}
