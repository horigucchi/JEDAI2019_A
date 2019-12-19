using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "FlyObjectStatus.asset", menuName = "FlyObject/WaveAsset", order = 30)]
public class WaveData : ScriptableObject
{
    //public WaveData()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        flyObjects[i] = CreateInstance<FlyObjectData>();
    //    }
    //}
    //public FlyObjectData[] flyObjects = new FlyObjectData[5];

    public List<FlyObjectData> flyObjects = new List<FlyObjectData>();
    
    
}
