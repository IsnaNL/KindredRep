using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Benny : Health
{
    public CharacterController2D player;
    private SpriteRenderer SR;
    public bool isGrounded;
    public LayerMask GroundLayerMask;
    public int GroundLayerInt;
    public LayerMask playerLayerMask;
    public int playerLayerInt;
    public float RangeToPeek;
    //private bool TriggerLandingAnimationBool;
    Rigidbody2D RB2D;
    public float JumpForce;
    public int Direction;
    public int defaultLayer;
    public bool isAccelerating;
    public Vector2 jumpAcceleration;
    public Vector2 HitForce;
    public float KnockBackAfterHittingPlayer;
    public Vector2 velocity;
    public float GravityScale;
    public bool IsAttacking;
    public float startingJumpAccelerationX;
    public int damage;
    public Slider healthBar;
    //public float GroundCheckDistanceForLanding;
    public Collider2D bodyCollider;
    public Collider2D GroundCheckCollider;
    public int groundCheckLayer;
    public int EnemyLayer;
    private int groundTriggerCount;
    RaycastHit2D checkForGround;
    public bool isJumping;
    RaycastHit2D landingDesCheck;
    private float landingYDes;



    // Start is called before the first frame update
    public void Init()
    {
      //  player = FindObjectOfType<CharacterController2D>();
        SR = GetComponent<SpriteRenderer>();
        RB2D = GetComponent<Rigidbody2D>();
        //velocity = new Vector2(0, 0);
        startingJumpAccelerationX = jumpAcceleration.x;
        isVulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        //velocity *= 0.999f * Time.deltaTime; ;//smoother
       
        if (!isGrounded && isJumping)
        {
            velocity.y -= GravityScale * Time.deltaTime;
        }
        else
        {
           // velocity.y = 0;
        }
        healthBar.value = health;
       
        
        //Debug.Log(isGrounded);
        //if (!isGrounded)
        //{
        //    velocity.y -= GravityScale * Time.deltaTime;
        //}
        //else
        //{
        //    velocity.y = 0;
        //}
        //velocity.x *= 0.999f;
        if (!IsAttacking)
        {




            if (player.transform.position.x >= transform.position.x)
            {
                SR.flipX = true;
                Direction = 1;

            }
            else
            {
                SR.flipX = false;
                Direction = -1;



            }


        }

       
        //transform.Translate(velocity * Time.deltaTime);
    }

    private void WhenHit()
    {
        if (isHit)
        {
            EffectsManager.e_Instance.BloodHitEffect(transform);


            if (player.transform.position.x >= transform.position.x)
            {
                velocity = Vector2.zero;
                velocity = new Vector2(-HitForce.x, HitForce.y);

            }
            else
            {
                RB2D.velocity = Vector2.zero;

                velocity = new Vector2(HitForce.x, HitForce.y);


            }
            isVulnerable = false;
            isHit = false;

        }
    }

     void FixedUpdate()
    {
        WhenHit();
        CheckPeekCondition();
        checkForGround = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.1f, Vector2.down, 0.1f, GroundLayerMask);
        landingDesCheck = Physics2D.Raycast(transform.position, Vector2.down, 10, GroundLayerMask);
        if (velocity.y < 0)
        {
            //Debug.DrawRay()
            // GroundCheckCollider.enabled = true;
            if (checkForGround && isJumping)
            {
                isJumping = false;
                velocity.x = 0;
                animator.SetTrigger("Land");
            }
            if (landingDesCheck)
            {
                landingYDes = landingDesCheck.point.y + 0.1f;
               if(transform.position.y <= landingYDes)
                {
                    velocity *= 0.1f;
                }

            }
        }
        transform.Translate(velocity * Time.fixedDeltaTime);
        // CheckAttackCondition();
      //  if (/*IsAttacking &&*/ RB2D.velocity.y < 0)  
      //  {
            //CheckForLandingAnimation();

     //   }
        


    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        groundTriggerCount++;
        if (groundTriggerCount == 1)
        {
           
            if (collision.gameObject.layer == GroundLayerInt)
            {


              //  animator.SetTrigger("Land");

                isGrounded = true;
                IsAttacking = false;
                isVulnerable = false;
                bodyCollider.gameObject.layer = defaultLayer;
                RB2D.velocity = Vector2.zero;
                velocity = Vector2.zero;
                
                //velocity *= 0.5f;
                //animator.SetTrigger("BackToIdle");

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        groundTriggerCount--;
        if (groundTriggerCount == 0)
        {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            if (IsAttacking)
            { 
                player.TakeDamage(damage);

                if (player.transform.position.x >= transform.position.x)
                {
                    velocity = new Vector2(-HitForce.x, HitForce.y) * 0.2f;//benny
                    player.velocity = new Vector2(HitForce.x, HitForce.y);

                }
                else
                {
                    velocity = new Vector2(HitForce.x, HitForce.y) * 0.2f;//benny

                    player.velocity = new Vector2(-HitForce.x, HitForce.y);


                }
                AudioManager.a_Instance.BennyAttackAudio();
                IsAttacking = false;
            }
          
        }
        if(collision.gameObject.layer == GroundLayerInt && !isGrounded)
        {
            RB2D.velocity = Vector2.zero;
        }
    }
  /* public void CheckForLandingAnimation()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down, GroundCheckDistanceForLanding,GroundLayerMask);
        if (hit)
        {
           
            animator.SetTrigger("Land");

        }
    }*/
    public void CheckPeekCondition()
    {
       RaycastHit2D hit = Physics2D.CircleCast(transform.position, RangeToPeek,Vector2.up,0.1f,playerLayerMask);
       if(hit)
       {
            PeekExecute();
       }
       else
       {
            animator.SetTrigger("BackToIdle");
       }
    }
  
    public void PeekExecute()
    {
        
        animator.SetTrigger("PeekTrigger");
    }
    public void setBackToIdle()//called by animator in the end of the land animation
    {
        animator.SetTrigger("BackToIdle");

    }

    public void CheckAttackCondition() //called by animator
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, RangeToPeek, Vector2.up, 0.1f, playerLayerMask);
        if (hit)
        {
           
            AttackExecute();
        }
        else
        {
        animator.SetTrigger("BackToIdle");
        }
    }
    public void AttackExecute()
    {
        if (Direction == 1)
        {
            jumpAcceleration.x = startingJumpAccelerationX;
        }
        else
        {
            jumpAcceleration.x = -startingJumpAccelerationX;
        }
        animator.SetTrigger("AttackTrigger");
        AudioManager.a_Instance.BennyJumpAudio();
        StartCoroutine((JumpCoRou()));
    }
    /*public void StopAnimation()
    {
        animator.speed = 0;
    }*/
 
    public IEnumerator JumpCoRou()
    {
        isJumping = true;
     //   Vector2 savedjumpacc = jumpAcceleration;
       // GroundCheckCollider.enabled = false;
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(Direction, 0), 1, GroundLayerMask);
       // if (hit)
       // {
         //   jumpAcceleration = new Vector2(0, 10);
          //  jumpAcceleration.y = savedjumpacc * 2;
       // }
       // else
       // {
         //   jumpAcceleration = savedjumpacc;
           // jumpAcceleration.y = savedjumpacc;
      //  }

        isVulnerable = true;
        bodyCollider.gameObject.layer = EnemyLayer;
        
        velocity = new Vector2(jumpAcceleration.x,jumpAcceleration.y);
        IsAttacking = true;
        yield return null;
       // isGrounded = false;
        
        // RB2D.AddForce(jumpAcceleration, ForceMode2D.Impulse);
        //yield return new WaitForSeconds(0.5f);
        //isAccelerating = true;
        //float Destination = transform.position.y + JumpForce;
        //float negDestination = transform.position.y - 0.01f;
        //float currentjumpAccelerationY = jumpAcceleration.y;
        //float currentjumpAccelerationX = jumpAcceleration.x;

        //while (isAccelerating)
        //{
        //    velocity += new Vector2(currentjumpAccelerationX * Direction * Time.deltaTime, currentjumpAccelerationY * Time.deltaTime);

        //    if (transform.position.y >= Destination || transform.position.y < negDestination)
        //    {
        //        velocity.y *= 0.5f;
        //        velocity.x *= 0.5f;
        //        isAccelerating = false;
        //    }
        //    yield return null;
        //}

    }
   public void setGroundfromAnimator()
    {
        if (checkForGround)
        {
            isJumping = false;
            velocity.x = 0;
            animator.SetTrigger("Land");
        }
    }
    private void OnDrawGizmos()
    {
      //  Gizmos.color = Color.red;
      // Gizmos.DrawWireSphere(transform.position,0.1f);
        Gizmos.color = Color.yellow;
        //Gizmos.DrawRay(new Vector2(transform.position.x,transform.position.y -0.5f), Vector2.down *0.2f);
    }
}
