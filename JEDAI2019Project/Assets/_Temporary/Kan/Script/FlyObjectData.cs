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
    public string ObjName;
    public ObjType ObjType;
    public float Speed;
    public int Point;

    public Sprite sprite;
    public GameObject FlyObjPrefab;

}
