using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingExController : RingController
{
    public float distance = 0.2f;

    //Hit Bonus Score
    public int BonusPoint;
    

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        rb.velocity = MoveDirection.normalized * Status.Speed;
    }


    public float GetPositionY()
    {
        return transform.position.y;
    }

    public void HitCheck(Transform kitetransform,Vector3 pointOffset)
    {
        float hitdistance = Mathf.Abs(transform.position.y - kitetransform.position.y);

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
        AudioController.PlaySnd("one23", Camera.main.transform.position, 0.5f);
        particle.Play();
    }

    private void MissHit()
    {
        animator.enabled = true;
        AudioController.PlaySnd("button04", Camera.main.transform.position, 0.5f);
    }
}
