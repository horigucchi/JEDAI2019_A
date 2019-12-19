using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingExController : RingController
{
    public float distance = 0.2f;

    //Hit Bonus Score
    public int BonusPoint;

    float speed;

    void Start()
    {
        speed = Status.Speed;
        animator = GetComponentInChildren<Animator>();
        childparticle = GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        rb.velocity = MoveDirection.normalized * speed;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetPositionY()
    {
        return transform.position.y;
    }

    public void HitCheck(Transform kitetransform,Vector3 pointOffset)
    {
        float hitdistance = Mathf.Abs(transform.position.y - kitetransform.position.y);
        Debug.Log(hitdistance);
        if (hitdistance > distance)
        {
            MissHit();
            ScoreController.Instance.AddScore(Status.Point, transform.position + pointOffset);
        }
        else
        {
            Hit();
            ScoreController.Instance.AddScore(BonusPoint, transform.position + pointOffset);
        }
    }

    private void Hit()
    {
        animator.SetTrigger("ExHit");
        AudioController.PlaySnd("one23", Camera.main.transform.position, 0.5f);
        speed *= 0.5f;
        childparticle.Play();
        Destroy(this.gameObject, 1f);
    }

    private void MissHit()
    {
        animator.SetTrigger("ExMiss");
        AudioController.PlaySnd("button04", Camera.main.transform.position, 0.5f);
        MoveDirection = Vector2.zero;
        Destroy(this.gameObject, 1f);
    }
}
