using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class AFlyObject : MonoBehaviour
{
    [SerializeField]
    FlyObjectData status;

    //オブジェクトのステータス
    public FlyObjectData Status { get => status; set => status = value; }

    //RigidBody
    protected Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
