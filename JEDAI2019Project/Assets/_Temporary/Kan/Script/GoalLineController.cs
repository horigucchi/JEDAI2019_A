using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLineController : RingController
{

    public RingExController Ring;

    //くぐって判定距離
    public float centerDistance;
    //外れ距離
    public float boundDistance;

    public int HitCenterPoint;

    public int HitSubsetPoint;

    public int MissPoint;

    bool startFX;

    [SerializeField]
    GameObject RingSprite;
    [SerializeField]
    GameObject LeftRingSprite;
    [SerializeField]
    GameObject RightRingSprite;

    Transform kitetrans;

    void Start()
    {
        animator = GetComponent<Animator>();
        startFX = false;
    }

    void Update()
    {
        rb.velocity = MoveDirection.normalized * Status.Speed;
        if (startFX)
        {
            //RingSprite.transform.localScale = 0.95 * RingSprite.transform.scale;
            transform.position = Vector3.MoveTowards(transform.position, kitetrans.position,Time.deltaTime);
        }
    }

    public override void HitCheck(Transform kitetransform, Vector3 pointOffset)
    {
        float hitdistance = Mathf.Abs(transform.position.y - kitetransform.position.y);
        Debug.Log(hitdistance);
        if (hitdistance <= centerDistance)
        {
            Debug.Log("hit!!");
            HitCenterEffect(kitetransform);
            ScoreController.Instance.AddScore(HitCenterPoint, transform.position + pointOffset);
        }
        else if(hitdistance > boundDistance)
        {
            MissEffect();
            ScoreController.Instance.AddScore(MissPoint, transform.position + pointOffset);
        }
        else
        {
            HitSubsetEffect();
            ScoreController.Instance.AddScore(HitSubsetPoint, transform.position + pointOffset);
        }
    }

    void HitCenterEffect(Transform playertrans)
    {
        //PlayAnim();
        //animator.SetTrigger("GoalRingHit");
        StartCoroutine(GoalRingHit());
        kitetrans = playertrans;
        Ring.animator.SetTrigger("ExMiss");
        
        AudioController.PlaySnd("one23", Camera.main.transform.position, 0.5f);
    }

    void HitSubsetEffect()
    {
        Ring.animator.SetTrigger("ExMiss");
        AudioController.PlaySnd("one23", Camera.main.transform.position, 0.5f);
    }

    void MissEffect()
    {
        PlayAnim();
        animator.SetTrigger("GoalRingMiss");
    }

    IEnumerator GoalRingHit()
    {
        yield return new WaitForSeconds(0.2f);
        MoveDirection = Vector2.zero;
        Ring.MoveDirection = Vector2.zero;
        //yield return new WaitForSeconds(0.3f);
        PlayAnim();
        animator.SetTrigger("GoalRingHit");
        startFX = true;
        yield return new WaitForSeconds(0.1f);
        DestroySelf();
        //LeftRingSprite.SetActive(false);
        //RightRingSprite.SetActive(false);
        //RingSprite.SetActive(true);
    }


    void setScale()
    {

    }
}
