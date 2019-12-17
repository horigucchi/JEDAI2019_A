using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ObjType
{
    Kite, Bird, Ring
}

[CreateAssetMenu(fileName = "FlyObjectStatus.asset", menuName = "FlyObject/FlyObjectAsset", order = 30)]
public class FlyObjectData : ScriptableObject
{
    [Tooltip("単位の名前")]
    public string ObjName;
    [Tooltip("単位の種類")]
    public ObjType ObjType;
    [Tooltip("移動速度")]
    public float Speed;
    [Tooltip("")]
    public int Point;

    [Tooltip("")]
    public Sprite sprite;
    [Tooltip("")]
    public GameObject FlyObjPrefab;

}
