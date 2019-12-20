using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// クリアイメージを動かすスクリプト
public class ClearImage : MonoBehaviour
{
    [SerializeField]
    private GameObject newScore;
    [SerializeField]
    private List<GameObject> clearImages;

    private bool afterFlag = false;
    public int highScore = 0;
    [SerializeField]
    private int nowScore = 0;
    private float flame;
    private Vector3 pos;
    private bool isHighScore;
    private const int subPosX = 15;

    private void OnEnable()
    {
        isHighScore = false;
        for (int i = 0; i < transform.childCount; ++i) 
        {
            clearImages.Add(transform.GetChild(i).gameObject);
        }
        pos = transform.position;
        transform.position = new Vector3(pos.x - subPosX, pos.y, pos.z);
        highScore = HighScoreDataIO.Instance.HighScoreLoad("/Score/HighScore.txt");
        if (highScore < (nowScore = ScoreController.Instance.GetScore()))
        {
            isHighScore = true;
            HighScoreDataIO.Instance.HighScoreSave("/Score/HighScore.txt", nowScore);
        }
    }

    void Update()
    {
        if (!afterFlag)
        {
            NeutralUpdate();
        }
        else
        {
            AfterUpdate();
        }
        for (int i = 0; i < clearImages.Count; ++i)
        {
            clearImages[i].transform.position = new Vector3(transform.position.x + i * 3, transform.position.y + Mathf.Sin((++flame + i * 30) * Mathf.PI / 180) / 3, transform.position.z);
        }
    }

    public void OnClick()
    {
        afterFlag = true;
    }

    private void NeutralUpdate()
    {
        if (transform.position.x < pos.x)
        {
            transform.position += new Vector3(6 * Time.deltaTime, 0, 0);
        }
        else
        {
            if((isHighScore)&&(newScore!=null))
            {
                newScore.SetActive(true);
            }
        }
    }

    private void AfterUpdate()
    {
        transform.position += new Vector3(6 * Time.deltaTime, 0, 0);
    }
}
