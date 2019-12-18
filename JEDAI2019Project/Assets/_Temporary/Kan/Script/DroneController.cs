using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : AFlyObject
{
    [SerializeField]
    Vector2 MoveDirection = Vector2.left;

    void Start()
    {

    }

    
    void Update()
    {
        MoveDirection.y = Mathf.Sin(Time.unscaledTime);

        rb.velocity = MoveDirection * Status.Speed;
    }
}
