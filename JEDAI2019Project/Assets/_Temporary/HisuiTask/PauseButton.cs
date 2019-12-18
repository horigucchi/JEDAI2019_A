using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private bool pushFlag = false;
    public void OnClick()
    {
        pushFlag = !pushFlag;
        gameObject.SetActive(false);
        Debug.Log("Push");
    }

    public bool GetFlag()
    {
        return pushFlag;
    }

    public bool SetFlag()
    {
        return pushFlag = !pushFlag;
    }
}
