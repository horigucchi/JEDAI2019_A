using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadStage
{
    /// <summary>
    /// CSVファイルのステージデータを
    /// </summary>
    /// <param name="fileName">リソースファイルの名前</param>
    /// <param name="waves">保存先</param>
    /// <param name="rowCount"></param>
    public static void LoadStageCSV(string fileName, List<WaveData> waves, int rowCount)
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
            for (int k = 0; k < rowCount; k++)
            {
                waves[j].flyObjects.Add(ScriptableObject.CreateInstance<FlyObjectData>());
                //Debug.Log(waves[j].flyObjects.Count);
            }
        }

        for (int i = 0; i < rowCount; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            
            for (int j = 0; j < row.Length; j++)
            {
                //Debug.Log(j + "  " + row[j]);
                int flyObjType;
                int.TryParse(row[j], out flyObjType);

                if (flyObjType == 1)
                {
                    waves[j].flyObjects[i] = Resources.Load<FlyObjectData>(flyObjectDataFolder + "Bird");
                }
                else if(flyObjType == 2)
                {
                    waves[j].flyObjects[i] = Resources.Load<FlyObjectData>(flyObjectDataFolder + "Ring");
                }
                else if(flyObjType == 3)
                {
                    waves[j].flyObjects[i] = Resources.Load<FlyObjectData>(flyObjectDataFolder + "RingEX");
                }
                
            }

        }
        
    }



}
