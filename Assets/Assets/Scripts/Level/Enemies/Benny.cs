using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Benny : Health
{
    private CharacterController2D player;
    private SpriteRenderer SR;
    private bool isGrounded;
    public LayerMask GroundLayerMask;
    public int GroundLayerInt;
    public LayerMask playerLayerMask;
    public int playerLayerInt;
    public float RangeToPeek;
    Rigidbody2D RB2D;
    public float JumpForce;
    public int Direction;
    public int defaultLayer;
    public bool isAccelerating;
    public Vector2 jumpAcceleration;
    public Vector2 HitForce;
    public float KnockBackAfterHittingPlayer;
    //public Vector2 velocity;
    public float GravityScale;
    public bool IsAttacking;
    public float startingJumpAccelerationX;
    public int damage;
    public Slider healthBar;
    public float GroundCheckDistanceForLanding;
    public Collider2D bodyCollider;
    public int groundCheckLayer;
    public int EnemyLayer;
    private int groundTriggerCount;





    // Start is called before the first frame update
    public void Init()
    {
        player = FindObjectOfType<CharacterController2D>();
        SR = GetComponent<SpriteRenderer>();
        RB2D = GetComponent<Rigidbody2D>();
        //velocity = new Vector2(0, 0);
        startingJumpAccelerationX = jumpAcceleration.x;
        isVulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
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
                RB2D.velocity = Vector2.zero;
                RB2D.AddForce(new Vector2(-HitForce.x, HitForce.y), ForceMode2D.Impulse);

            }
            else
            {
                RB2D.velocity = Vector2.zero;

                RB2D.AddForce(HitForce, ForceMode2D.Impulse);

            }
            isVulnerable = false;
            isHit = false;

        }
    }

     void FixedUpdate()
    {
        WhenHit();
        CheckPeekCondition();

        // CheckAttackCondition();
        if (IsAttacking && RB2D.velocity.y < 0)  
        {
            CheckForLandingAnimation();

        }
        


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        groundTriggerCount++;
        if (groundTriggerCount == 1)
        {
            if (collision.gameObject.layer == GroundLayerInt)
            {
                isGrounded = true;
                //velocity *= 0.5f;
                IsAttacking = false;
                isVulnerable = false;
                bodyCollider.gameObject.layer = defaultLayer;
                //  animator.SetTrigger("Land");
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
                    //player.velocity *= 0.1f;
                    player.velocity += new Vector2(player.hitKnockBack.x, player.hitKnockBack.y);
                    RB2D.AddForce(new Vector2(-KnockBackAfterHittingPlayer,1), ForceMode2D.Impulse);
                   
                }
                else
                {
                    //player.velocity *= 0.1f;
                    player.velocity += new Vector2(-player.hitKnockBack.x, player.hitKnockBack.y);
                    RB2D.AddForce(new Vector2(KnockBackAfterHittingPlayer, 1), ForceMode2D.Impulse);
                   


                }
                AudioManager.a_Instance.BennyAttackAudio();
               // IsAttacking = false;
            }
          
        }
        if(collision.gameObject.layer == GroundLayerInt && !isGrounded)
        {
            RB2D.velocity = Vector2.zero;
        }
    }
   public void CheckForLandingAnimation()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down, GroundCheckDistanceForLanding,GroundLayerMask);
        if (hit)
        {
           
            animator.SetTrigger("Land");

        }
    }
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
     
        
        isVulnerable = true;
        bodyCollider.gameObject.layer = EnemyLayer;
        RB2D.AddForce(jumpAcceleration, ForceMode2D.Impulse);
        IsAttacking = true;
        yield return null;
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeToPeek);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector2.down * GroundCheckDistanceForLanding);
    }
}
