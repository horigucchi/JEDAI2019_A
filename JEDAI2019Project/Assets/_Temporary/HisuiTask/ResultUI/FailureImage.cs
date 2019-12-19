using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailureImage : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> failureImage;
    [SerializeField]
    private GameObject button;

    private float flame;
    private Vector3 pos;
    private const int addPosY = 5;


    private void Start()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            failureImage.Add(transform.GetChild(i).gameObject);
        }
        pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + addPosY, pos.z);
    }

    void Update()
    {
        if (transform.position.y >= pos.y)
        {
            transform.position -= new Vector3(0, 6 * Time.deltaTime, 0);
        }
        else
        {
            if (!button.activeSelf)
            {
                button.SetActive(true);
            }
        }
        for (int i = 0; i < failureImage.Count; ++i)
        {
            
            failureImage[i].transform.position = new Vector3(transform.position.x + i * 3, transform.position.y + Mathf.Sin((++flame + i * 30) * Mathf.PI / 180) / 3, transform.position.z);
        }
    }
}
