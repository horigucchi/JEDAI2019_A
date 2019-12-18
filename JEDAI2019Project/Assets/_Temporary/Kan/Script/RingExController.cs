using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingExController : RingController
{
    public float distance = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = MoveDirection.normalized * Status.Speed;
    }

    public float GetPositionY()
    {
        return transform.position.y;
    }

    public void HitCheck(Transform kitetransform)
    {
        float hitdistance = Mathf.Abs(transform.position.y - kitetransform.position.y);

        if (hitdistance > distance)
        {
            MissHit();
        }
        else
        {
            Hit();
        }
    }

    private void Hit()
    {
        particle.Play();
    }

    private void MissHit()
    {
        animator.enabled = true;
    }
}
