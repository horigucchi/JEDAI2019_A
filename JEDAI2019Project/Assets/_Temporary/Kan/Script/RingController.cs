using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RingController : AFlyObject
{
    [SerializeField]
    Vector2 MoveDirection = Vector2.left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = MoveDirection.normalized * Status.Speed;
    }

    /// <summary>
    /// AnimationEvent用
    /// </summary>
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

}
