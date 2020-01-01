using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    public void OnClickToTitle()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void OnClickToGame()
    {
        // ステージ開始の処理
        GameManager.Instance.RestartStage();
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    
}
