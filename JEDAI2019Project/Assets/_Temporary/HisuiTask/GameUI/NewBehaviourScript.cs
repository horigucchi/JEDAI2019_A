using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            pos.y+=0.1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= 0.1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= 0.1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += 0.1f;
        }
        transform.position = new Vector3(pos.x,pos.y, transform.position.z);
    }
}
