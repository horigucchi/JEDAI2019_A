using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Bounds
{
    public float xL, xR, yU, yD;
}



public class KiteController : AFlyObject
{
    [SerializeField]
    private float acceleration = 1f;

    [SerializeField]
    Bounds bounds = new Bounds();

    [SerializeField]
    Horiguchi.YarnController controller;

    [SerializeField]
    RollerSprite roller;

    [SerializeField]
    Text text;
    
    [SerializeField]
    Vector3 pointOffset;

    public Bounds Bounds { get => bounds; private set => bounds = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }

    public bool CanMove { get; set; }


    Animator animator;

    [SerializeField]
    Sprite DamagedSprite;

    SpriteRenderer rd;



    void Start()
    {
        if (controller == null)
        {
            try
            {
                controller = Horiguchi.YarnController.Instance;
            }
            catch (System.NullReferenceException)
            {
                Debug.LogError("Rollerを指定してください。");
            }
        }

        animator = GetComponentInChildren<Animator>();
        rd = GetComponentInChildren<SpriteRenderer>();

        CanMove = true;
    }


    void Update()
    {

        BoundCheck();
        AnimationCheck();
        RollerAnimationCheck();

        if (CanMove == true)
        {
            RollCheck();
#if UNITY_EDITOR

            MoveCheck();
#endif
        }

        if (rb.velocity.y < -15f)
        {
            GameManager.Instance.GameOver();        }

    }

    private void FixedUpdate()
    {
       
    }

    public void RollCheck()
    {


        switch (controller.YarnState)
        {
            case Horiguchi.EYarnState.forwardRolling:
                AddVelocity(new Vector2(0, -1));
                break;
            case Horiguchi.EYarnState.reverseRolling:
                AddVelocity(new Vector2(0, 1));
                break;
            case Horiguchi.EYarnState.hold:
                AddVelocity(new Vector2(0, -0.5f));
                break;
            case Horiguchi.EYarnState.letGo:
                AddVelocity(new Vector2(0, -0.5f));
                break;
            default:
                break;
        }
        
        
    }


    public void AddVelocity(Vector2 direction)
    {
        
        Vector2 velocity = rb.velocity;

        if (velocity.y < -5 && direction.y > 0)
        {
            velocity.y = 0;

        }
        if (velocity.y > 5 && direction.y < 0)
        {
            velocity.y = 0;
        }

        //Acceleration = force.y;

        //velocity.y += Acceleration * Time.deltaTime;

        velocity += direction * Acceleration * Time.deltaTime;

        if (direction.x == 0)
        {
            velocity.x *= 0.98f;
        }
        if (direction.y == 0)
        {
            velocity.y *= 0.98f;
        }

        velocity.y = Mathf.Clamp(velocity.y, -30f, 30f);

        rb.velocity = velocity;
        text.text = "Speed:" + (int)velocity.y + " " + /*"加速度:" + acceleration*/ ((int)controller.RollValue / 360f) + "巻く"; 

    }


    void MoveCheck()
    {
        float h, v;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector2 velocity = rb.velocity;

        velocity += new Vector2(h, v) * Acceleration * Time.deltaTime;
        //if (h == 0)
        //{
        //    velocity.x *= 0.98f;
        //}
        //if (v == 0)
        //{
        //    velocity.y *= 0.98f;
        //}
        rb.velocity = velocity;
    }


    void BoundCheck()
    {
        Vector2 position = rb.position;
        if (position.y > bounds.yU)
        {
            position.y = bounds.yU;
        }
        else if (position.y < bounds.yD)
        {
            position.y = bounds.yD;
        }

        if (position.x > bounds.xR)
        {
            position.x = bounds.xR;
        }
        else if (position.x < bounds.xL)
        {
            position.x = bounds.xL;
        }

        rb.position = position;
    }

    void AnimationCheck()
    {
        Vector3 rotZ = Vector3.zero;
        rotZ.z = -30f;
        if (rb.velocity.y > 3)
        {
            rotZ.z = 15f;
        }
        if (rb.velocity.y < -3)
        {
            rotZ.z = -75f;
        }
        transform.rotation = Quaternion.Euler(rotZ);
    }

    void RollerAnimationCheck()
    {
        //roller.AddRotZ(-rb.velocity.y * 3);

        switch (controller.YarnState)
        {
            case Horiguchi.EYarnState.forwardRolling:
            case Horiguchi.EYarnState.reverseRolling:
                roller.AddRotZ(-controller.RollingSpeed);
                break;
            case Horiguchi.EYarnState.hold:
            case Horiguchi.EYarnState.letGo:
                roller.AddRotZ(-rb.velocity.y);
                break;
            default:
                break;
        }

        //roller.SetRotZ(-controller.RollValue);
        //roller.SetRotZ((-transform.position.y + 10f) * 360);
        roller.SetScale((-transform.position.y + 10f) / 16);

        //roller.SetScale(Mathf.Clamp(3600f / controller.RollValue ,0.1f,1.0f));
    }



    public void GameOver()
    {
        CanMove = false;

        animator.SetTrigger("Dead");

        rd.sprite = DamagedSprite;

        bounds.yD = -10f;

        rb.velocity = Vector2.down;

        GetComponentInChildren<ParticleSystem>().Play();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanMove) return;

        AFlyObject obj;
        obj = collision.GetComponent<AFlyObject>();

        if (obj == null)
        {
            return;
        }

        switch (obj.Status.ObjType)
        {
            case ObjType.Goal:


                GameManager.Instance.GameClear();
                break;
            case ObjType.Bird:


                CanMove = false;
                bounds.yD = -12;

                //下に落ちる
                rb.velocity = new Vector2(0, -3);

                AudioController.PlaySnd("crowhit1", Camera.main.transform.position, 0.5f);
                //
                GameManager.Instance.GameOver();
                //Debug.Log("鳥！");
                break;
            case ObjType.Ring:

                obj.GetComponent<RingController>().HitCheck(pointOffset);
                break;
            case ObjType.RingEX:

                obj.GetComponent<RingExController>().HitCheck(transform,pointOffset);
                break;
            default:
                break;
        }
    }

}
