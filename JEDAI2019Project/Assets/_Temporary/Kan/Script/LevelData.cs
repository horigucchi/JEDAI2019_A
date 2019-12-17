using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyObjectStatus.asset", menuName = "FlyObject/LevelAsset", order = 30)]
public class LevelData : ScriptableObject
{

    public uint Level;

    List<WaveData> Waves;


}


