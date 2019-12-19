using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RingController : AFlyObject
{
    public Vector2 MoveDirection = Vector2.left;
    
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public ParticleSystem childparticle;
    void Start()
    {
        animator = GetComponent<Animator>();
        childparticle = GetComponentInChildren<ParticleSystem>();
    }

    
    void Update()
    {
        rb.velocity = MoveDirection.normalized * Status.Speed;
    }

    /// <summary>
    /// 
    /// </summary>
    public void DestroySelf()
    {
        Destroy(this.gameObject,0.4f);
    }

    public void PlayParticle()
    {
        animator.enabled = true;
    }

    public void PlayAnim()
    {
        animator.enabled = true;
    }

    public virtual void HitCheck(Vector3 pointOffset)
    {
        //Debug.Log(obj.Status.Point + "Point!");
        ScoreController.Instance.AddScore(Status.Point, transform.position + pointOffset);
        //Destroy(obj.gameObject,0.15f);

        PlayAnim();
        PlayParticle();
        DestroySelf();
        AudioController.PlaySnd("button04", Camera.main.transform.position, 0.5f);
    }

    public virtual void HitCheck(Transform kitetransform, Vector3 pointOffset)
    {
        //HitEffect
    }

}
