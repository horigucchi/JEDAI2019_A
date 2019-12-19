using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// クリアイメージを動かすスクリプト
public class ClearImage : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> clearImages;

    private bool afterFlag = false;

    private float flame;
    private Vector3 pos;
    private const int subPosX = 15;

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; ++i) 
        {
            clearImages.Add(transform.GetChild(i).gameObject);
        }
        pos = transform.position;
        transform.position = new Vector3(pos.x - subPosX, pos.y, pos.z);
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
    }

    private void AfterUpdate()
    {
        transform.position += new Vector3(6 * Time.deltaTime, 0, 0);
    }
}
