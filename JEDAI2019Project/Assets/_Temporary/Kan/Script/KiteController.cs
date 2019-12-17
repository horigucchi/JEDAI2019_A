using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bounds
{
    public float xL, xR, yU, yD;
}



public class KiteController : AFlyObject
{
    [SerializeField]
    private float acceleration = 1f;

    [SerializeField]
    Bounds bounds = new Bounds();

    public Bounds Bounds { get => bounds; private set => bounds = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }

    void Start()
    {
        
    }


    void Update()
    {

        BoundCheck();
        AnimationCheck();
    }

    private void FixedUpdate()
    {

#if UNITY_EDITOR

        MoveCheck();

#endif
    }


    public void AddForce(Vector2 force)
    {
        
        Vector2 velocity = rb.velocity;

        velocity +=  force * Acceleration * Time.deltaTime;
       
        rb.velocity = velocity;
    }


    void MoveCheck()
    {
        float h, v;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector2 velocity = rb.velocity;

        velocity += new Vector2(h, v) * Acceleration * Time.deltaTime;
        if (h == 0)
        {
            velocity.x *= 0.98f;
        }
        if (v == 0)
        {
            velocity.y *= 0.98f;
        }
        rb.velocity = velocity;
    }


    void BoundCheck()
    {
        Vector2 position = rb.position;
        if (position.y > bounds.yU)
        {
            position.y = bounds.yU;
        }
        else if (position.y < bounds.yD)
        {
            position.y = bounds.yD;
        }

        if (position.x > bounds.xR)
        {
            position.x = bounds.xR;
        }
        else if (position.x < bounds.xL)
        {
            position.x = bounds.xL;
        }

        rb.position = position;
    }

    void AnimationCheck()
    {
        Vector3 rotZ = Vector3.zero;
        rotZ.z = -30f;
        if (rb.velocity.y > 3)
        {
            rotZ.z = 15f;
        }
        if (rb.velocity.y < -3)
        {
            rotZ.z = -75f;
        }
        transform.rotation = Quaternion.Euler(rotZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AFlyObject obj;
        obj = collision.GetComponent<AFlyObject>();

        if (obj == null)
        {
            return;
        }

        switch (obj.Status.ObjType)
        {
            case ObjType.Kite:

                break;
            case ObjType.Bird:
                Debug.Log("鳥！");
                break;
            case ObjType.Ring:
                Debug.Log(obj.Status.Point + "Point!");
                break;
            default:
                break;
        }
    }

}
