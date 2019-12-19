using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    Kan.BGscroller Background;

    [SerializeField]
    float scrollSpeed = 10f;

    public float ScrollSpeed { get => scrollSpeed; private set => scrollSpeed = value; }

    public bool SpawnFlag { get; set; }



    public FlyObjectData GoalLine;
    public float goalLineSpawnDelay;


    public string StageDataName;


    LevelData level;

    int waveNumber;
    int waveCount;
    float spawnrate = .5f;
    float spawntime;

    float totalLevelTime;
    float leftLevelTime;
    float levelTime;

    private void Awake()
    {
        if(Background == null)
        {
            try
            {
                Background = GameObject.Find("Background").GetComponent<Kan.BGscroller>();
            }
            catch (System.NullReferenceException)
            {
                Debug.LogError("背景を指定してください。");
            }
        }

        level = ScriptableObject.CreateInstance<LevelData>();


        if (level != null)
        {
            LoadStage.LoadStageCSV(StageDataName, level.Waves, 5);
        }
        
        
    }
    void Start()
    {
        Background.scrollSpeed = scrollSpeed;
        waveNumber = 0;
        waveCount = level.Waves.Count;
        totalLevelTime = spawnrate * waveCount + goalLineSpawnDelay;
        leftLevelTime = totalLevelTime;
        levelTime = 0f;
        spawntime = 0f;
        SpawnFlag = false;
        //SpawnGoalLine();
    }

    
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //CreateFlyObj(BirdLevels[0],0);
            SpawnWave(level.Waves[waveNumber]);
            waveNumber += 1;
            if (waveNumber > waveCount - 1)
            {
                waveNumber = 0;
            }
        }
#endif


        if (SpawnFlag == false)
        {
            if(waveNumber > waveCount - 1)
            {
                levelTime += Time.deltaTime;
                leftLevelTime -= Time.deltaTime;
            }
            return;
        }

        spawntime += Time.deltaTime;
        levelTime += Time.deltaTime;
        leftLevelTime -= Time.deltaTime;

        if (spawntime>=spawnrate)
        {
            //CreateFlyObj(BirdLevels[0],0);
            //SpawnWave(waves[waveNumber]);
            SpawnWave(level.Waves[waveNumber]);
            waveNumber += 1;
            spawntime = 0f;
            if (waveNumber > waveCount - 1)
            {
                //Debug.Log("StageEnd");
                StartCoroutine(SpawnGoalLine(goalLineSpawnDelay));
                //waveNumber = 0;
                SpawnFlag = false;
            }
        }

       

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="waitTime"></param>
    IEnumerator SpawnGoalLine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //三番目の列を中心にして生成する
        int goalLinePositionY = 3;
        CreateFlyObj(GoalLine, goalLinePositionY);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="speed"></param>
    public void SetScrollSpeed(float speed)
    {
        ScrollSpeed = speed;
        Background.scrollSpeed = scrollSpeed;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="flyObject"></param>
    /// <param name="startPositionY"></param>
    public void CreateFlyObj(FlyObjectData flyObject,float startPositionY)
    {
        if (flyObject.FlyObjPrefab == null)
        {
            return;
        }

        GameObject obj = Instantiate(flyObject.FlyObjPrefab);
        obj.name = flyObject.ObjName;
        
        obj.GetComponent<AFlyObject>().Status = flyObject;

        obj.transform.position = new Vector2(11, startPositionY);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="flyObject"></param>
    /// <param name="gridNumber">上からの列番</param>
    public void CreateFlyObj(FlyObjectData flyObject, int gridNumber)
    {
        if (flyObject.FlyObjPrefab == null)
        {
            return;
        }
        GameObject obj = Instantiate(flyObject.FlyObjPrefab);
        obj.name = flyObject.ObjName;
        
        obj.GetComponent<AFlyObject>().Status = flyObject;


        float positionY = -gridNumber * 2 + 5.5f;

        obj.transform.position = new Vector2(11, positionY);


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="wave"></param>
    void SpawnWave(WaveData wave)
    {
        for (int i = 0; i < wave.flyObjects.Count; i++)
        {
            if(wave.flyObjects[i] != null)
            {
                CreateFlyObj(wave.flyObjects[i], i);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ResetStage()
    {
        waveNumber = 0;
        waveCount = level.Waves.Count;
        totalLevelTime = spawnrate * waveCount + goalLineSpawnDelay;
        leftLevelTime = totalLevelTime;
        levelTime = 0f;
        spawntime = 0f;
    }


    public float GetStageLeftTime()
    {
        return Mathf.Clamp(leftLevelTime,0,totalLevelTime);
    }
}
