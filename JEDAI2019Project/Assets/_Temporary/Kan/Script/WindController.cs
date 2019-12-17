using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{

    Vector3 rotZ = Vector3.zero;
    [SerializeField]
    float rotSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rotZ = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.I))
        {
            rotZ.z -= rotSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.K))
        {
            rotZ.z += rotSpeed * Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(rotZ);

        if (Input.GetKey(KeyCode.O))
        {
            var main = GetComponentInChildren<ParticleSystem>().main;
            main.simulationSpeed *= 1.1f;
        }
        if (Input.GetKey(KeyCode.L))
        {
            var main = GetComponentInChildren<ParticleSystem>().main;
            main.simulationSpeed *= 0.9f;
            if (main.simulationSpeed < 1f)
            {
                main.simulationSpeed = 1f;
            }
        }
    }
}
