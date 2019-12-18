using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RingController : AFlyObject
{
    [SerializeField]
    protected Vector2 MoveDirection = Vector2.left;
    // Start is called before the first frame update
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public ParticleSystem particle;
    void Start()
    {
        animator = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
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

    public void PlayAnim()
    {
        animator.enabled = true;
        particle.Play();
        DestroySelf();
    }

}
