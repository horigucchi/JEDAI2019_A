using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSprite : MonoBehaviour
{

    public Transform SilkTrans;


    public void SetRotZ(float z)
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = z;
        transform.rotation = Quaternion.Euler(rot);
    }


    public void AddRotZ(float addZ)
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z += addZ;
        transform.rotation = Quaternion.Euler(rot);
    }

    public void SetScale(float scale)
    {
        Vector3 targeScale = SilkTrans.localScale;
        targeScale.x = scale;
        targeScale.y = scale;

        SilkTrans.localScale = targeScale;
    }



}
