using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D),typeof(SpriteRenderer))]
public class AFlyObject : MonoBehaviour
{
    [SerializeField]
    FlyObjectData status;

    public FlyObjectData Status { get => status; set => status = value; }


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
