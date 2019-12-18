﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadStage
{
    
    public static void LoadStageCSV(string fileName,List<WaveData> waves)
    {
        string dirStageFile = "Stages/" + fileName;
        string flyObjectDataFolder = "FlyObjects/"; 
        TextAsset stagedata = Resources.Load<TextAsset>(dirStageFile);

        string[] data = stagedata.text.Split(new char[] { '\n' });
        //Debug.Log(data.Length);

        string[] frow = data[0].Split(new char[] { ',' });

        for (int j = 0; j < frow.Length; j++)
        {
            waves.Add(item: ScriptableObject.CreateInstance<WaveData>());
            for (int k = 0; k < 5; k++)
            {
                waves[j].flyObjects.Add(ScriptableObject.CreateInstance<FlyObjectData>());
                //Debug.Log(waves[j].flyObjects.Count);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            
            for (int j = 0; j < row.Length; j++)
            {
                //Debug.Log(j + "  " + row[j]);
                int f;
                int.TryParse(row[j], out f);

                if (f == 1)
                {
                    waves[j].flyObjects[i] = Resources.Load<FlyObjectData>(flyObjectDataFolder + "Bird");
                }
                else if(f == 2)
                {
                    waves[j].flyObjects[i] = Resources.Load<FlyObjectData>(flyObjectDataFolder + "Ring");
                }
                
            }

        }
        
    }

}
