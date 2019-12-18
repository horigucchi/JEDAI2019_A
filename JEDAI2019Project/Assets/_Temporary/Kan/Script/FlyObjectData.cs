using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ObjType
{
    Kite, Bird, Ring, Drone, Goal,
}

[CreateAssetMenu(fileName = "FlyObjectStatus.asset", menuName = "FlyObject/FlyObjectAsset", order = 30)]
public class FlyObjectData : ScriptableObject
{
    [Tooltip("オブジェクトの名前")]
    public string ObjName;
    [Tooltip("オブジェクトの種類")]
    public ObjType ObjType;
    [Tooltip("移動速度")]
    public float Speed;
    [Tooltip("ゲットできる点数")]
    public int Point;

    
    [Tooltip("単位ののプレハブ")]
    public GameObject FlyObjPrefab;

}
