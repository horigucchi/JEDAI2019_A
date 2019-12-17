using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
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

    public bool SetFlag()
    {
        return pushFlag = !pushFlag;
    }
}
