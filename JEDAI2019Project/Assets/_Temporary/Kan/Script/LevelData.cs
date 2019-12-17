using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyObjectStatus.asset", menuName = "FlyObject/LevelAsset", order = 30)]
public class LevelData : ScriptableObject
{

    public uint Level;

    List<WaveData> Waves;

}


[CreateAssetMenu(fileName = "FlyObjectStatus.asset", menuName = "FlyObject/WaveAsset", order = 30)]
public class WaveData : ScriptableObject
{

    public uint WaveNomber;
    public uint SpawnTime;

    public FlyObjectData[] flyObjects = new FlyObjectData[5];





}