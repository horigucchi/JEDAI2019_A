using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ObjType
{
    Kite, Bird, Ring
}

[CreateAssetMenu(fileName = "FlyObjectStatus.asset", menuName = "FlyObjectAsset", order = 30)]

public class FlyObjectData : ScriptableObject
{
    public string ObjName;
    public ObjType ObjType;
    public float Speed;
    public float Point;
    public float StartPosition = 999f;

    public Sprite sprite;
    public GameObject FlyObjPrefab;

}
