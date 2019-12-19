using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + "Destroyed");
        if (collision.GetComponent<KiteController>()!=null)
        {
            return;
        }
        Destroy(collision.gameObject);
    }
}
