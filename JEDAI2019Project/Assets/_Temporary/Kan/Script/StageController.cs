using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    Kan.BGscroller Background;

    [SerializeField]
    float scrollSpeed = 10f;

    public float ScrollSpeed { get => scrollSpeed; set => scrollSpeed = value; }

    public bool StageClear { get; set; }

    //[SerializeField]
    //List<FlyObjectData> BirdLevels = new List<FlyObjectData>();
    
    //[SerializeField]
    //List<FlyObjectData> RingLevels = new List<FlyObjectData>();

    [SerializeField]
    List<WaveData> waves = new List<WaveData>();

    int waveNumber;
    int waveCount;
    float spawnrate = 2f;
    float spawntime;

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
    }
    void Start()
    {
        Background.scrollSpeed = scrollSpeed;
        waveNumber = 0;
        waveCount = waves.Count;
        spawntime = 0f;
        StageClear = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //CreateFlyObj(BirdLevels[0],0);
            SpawnWave(waves[waveNumber]);
            waveNumber += 1;
            if (waveNumber > waveCount - 1)
            {
                waveNumber = 0;
            }
        }

        if (StageClear == true)
        {
            return;
        }

        spawntime += Time.deltaTime;
        if (spawntime>spawnrate)
        {
            //CreateFlyObj(BirdLevels[0],0);
            SpawnWave(waves[waveNumber]);
            waveNumber += 1;
            if (waveNumber > waveCount - 1)
            {
                waveNumber = 0;
            }
            spawntime = 0f;
        }

    }

    void SetScrollSpeed(float speed)
    {
        ScrollSpeed = speed;
        Background.scrollSpeed = scrollSpeed;
    }

    public void CreateFlyObj(FlyObjectData flyObject,float startPositionY)
    {
        GameObject obj = Instantiate(flyObject.FlyObjPrefab);
        obj.name = flyObject.ObjName;
        if(obj.GetComponent<AFlyObject>() == null)
        {
            switch (flyObject.ObjType)
            {
                case ObjType.Kite:
                    obj.AddComponent<KiteController>();
                    break;
                case ObjType.Bird:
                    obj.AddComponent<BirdController>();
                    break;
                case ObjType.Ring:
                    obj.AddComponent<RingController>();
                    break;
                default:
                    break;
            }
        }
        obj.GetComponent<AFlyObject>().Status = flyObject;


        

        obj.transform.position = new Vector2(12, startPositionY);


        if (flyObject.sprite != null)
        {
            obj.GetComponent<SpriteRenderer>().sprite = flyObject.sprite;
        }
    }

    public void CreateFlyObj(FlyObjectData flyObject, int gridNumber)
    {
        GameObject obj = Instantiate(flyObject.FlyObjPrefab);
        obj.name = flyObject.ObjName;
        if (obj.GetComponent<AFlyObject>() == null)
        {
            switch (flyObject.ObjType)
            {
                case ObjType.Kite:
                    obj.AddComponent<KiteController>();
                    break;
                case ObjType.Bird:
                    obj.AddComponent<BirdController>();
                    break;
                case ObjType.Ring:
                    obj.AddComponent<RingController>();
                    break;
                default:
                    break;
            }
        }
        obj.GetComponent<AFlyObject>().Status = flyObject;


        int positionY = gridNumber * 2 - 4;

        obj.transform.position = new Vector2(12, positionY);


        if (flyObject.sprite != null)
        {
            obj.GetComponent<SpriteRenderer>().sprite = flyObject.sprite;
        }
    }


    void SpawnWave(WaveData wave)
    {
        for (int i = 0; i < wave.flyObjects.Length; i++)
        {
            if(wave.flyObjects[i] != null)
            {
                CreateFlyObj(wave.flyObjects[i], i);
            }
        }
    }

}
