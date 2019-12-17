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

    [SerializeField]
    List<FlyObjectData> BirdLevels = new List<FlyObjectData>();
    
    [SerializeField]
    List<FlyObjectData> RingLevels = new List<FlyObjectData>();



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
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateFlyObj(RingLevels[0],0);
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


        //if (flyObject.StartPosition == 999f)
        //{
        //    float position = Random.Range(-4.5f, 4.5f);
        //    obj.transform.position = new Vector2(12, position);
        //}
        //else
        //{
        //    obj.transform.position = new Vector2(12, flyObject.StartPosition);
        //}

        obj.transform.position = new Vector2(12, startPositionY);


        if (flyObject.sprite != null)
        {
            obj.GetComponent<SpriteRenderer>().sprite = flyObject.sprite;
        }



    }


}
