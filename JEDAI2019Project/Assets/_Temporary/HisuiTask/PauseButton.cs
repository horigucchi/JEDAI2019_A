using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ポーズボタンのクラス(押されると消えるボタン)
public class PauseButton : MonoBehaviour
{
    private bool pushFlag = false;
    public void OnClick()
    {
        pushFlag = !pushFlag;
        gameObject.SetActive(false);
    }

    public bool GetFlag()
    {
        return pushFlag;
    }

    public void SetFlag(bool flag)
    {
        pushFlag = flag;
    }

    // フラグ反転
    public void ReverseFlag()
    {
        pushFlag = !pushFlag;
    }
}
