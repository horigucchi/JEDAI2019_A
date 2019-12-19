using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // 座標を入れておく
    Vector3 pos;
    // 経過フレーム
    private int flame;
    // 最初に下げておくY値
    private const int SubPosY = 15;

    private bool isMoveEnd;

    void Start()
    {
        flame = 0;
        isMoveEnd = false;
        pos = transform.position;
        transform.position = new Vector3(pos.x,pos.y - SubPosY, pos.z);
    }
    
    void Update()
    {
        if (isMoveEnd)
        {
            transform.position += new Vector3(0, Mathf.Sin(++flame * Mathf.PI / 180)/150,0);
        }
        else
        {
            if (transform.position.y >= pos.y)
            {
                isMoveEnd = true;
            }
            else if (!isMoveEnd)
            {
                transform.position += new Vector3(0, 3 * Time.deltaTime, 0);
            }
        }
    }
}
