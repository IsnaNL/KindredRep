using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Benny : Health
{
    public CharacterController2D player;
    private SpriteRenderer SR;
    RaycastHit2D checkForGround;
    RaycastHit2D landingDesCheck;
    public LayerMask GroundLayerMask;
    public LayerMask playerLayerMask;
    Rigidbody2D RB2D;
    public Vector2 velocity;
    public Vector2 jumpAcceleration;
    public Vector2 HitForce;
    public Slider healthBar;
    public Collider2D bodyCollider;
    public Collider2D GroundCheckCollider;
    public int groundCheckLayer;
    public bool isGrounded;
    public int GroundLayerInt;
    public int playerLayerInt;
    public float RangeToAttack;
    public float JumpForce;
    public int Direction;
    public int defaultLayer;
    public bool isAccelerating;
    public float KnockBackAfterHittingPlayer;
    public float GravityScale;
    public bool IsAttacking;
    public float startingJumpAccelerationX;
    public int damage;
    public int EnemyLayer;
    private int groundTriggerCount;
    public bool isJumping;
    private float landingYDes;
    private bool IdleAudioTrigger;
    public float RangeForIdleAudio;


    public void Init()
    {
        SR = GetComponent<SpriteRenderer>();
        RB2D = GetComponent<Rigidbody2D>();
        startingJumpAccelerationX = jumpAcceleration.x;
        isVulnerable = false;
    }

   
    void Update()
    {
        if (!isGrounded && isJumping)
        {
            velocity.y -= GravityScale * Time.deltaTime;
        }
        else
        {
         

          
            if (Vector2.Distance(transform.position, player.transform.position) <= RangeForIdleAudio)
            {
                if (!IdleAudioTrigger)
                {
                    Debug.Log(Vector2.Distance(transform.position, player.transform.position));
                    AudioManager.a_Instance.BennyIdleAudio();
                    IdleAudioTrigger = true;
                }
               
            }else
            {
               
                    
                    IdleAudioTrigger = false;
                
            }
            
           
        }
        healthBar.value = health;
       
        if (IsAttacking)
        {

        }
        else
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
        
    }

    private void WhenHit()
    {
        if (isHit)
        {
            EffectsManager.e_Instance.BloodHitEffect(transform.position);
            EffectsManager.e_Instance.HitEffect(transform.position);

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
            AudioManager.a_Instance.BennyHurtAudio();
            isHit = false;

        }

    }

     void FixedUpdate()
    {
        WhenHit();
        CheckPeekCondition();

        checkForGround = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y - 0.2f), 0.1f, Vector2.down, 0.3f, GroundLayerMask);
        landingDesCheck = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down,10, GroundLayerMask);
        if (velocity.y < 0)
        {
            if (checkForGround && isJumping)
            {

                isJumping = false;
                velocity.x = 0;
                animator.SetTrigger("Land");
            }
            
        }
        if (landingDesCheck)
        {
            landingYDes = landingDesCheck.point.y ; // small revision in place
            if (transform.position.y <= landingYDes)
            {
                isGrounded = true;
                IsAttacking = false;
                isVulnerable = false;
                bodyCollider.gameObject.layer = defaultLayer;
                RB2D.velocity = Vector2.zero;
                velocity = Vector2.zero;
                // RB2D.velocity = Vector2.zero;
            }

        }
        transform.Translate(velocity * Time.fixedDeltaTime);



    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        groundTriggerCount++;
        if (groundTriggerCount == 1)
        {
           
            if (collision.gameObject.layer == GroundLayerInt)
            {


             

                isGrounded = true;
                IsAttacking = false;
                isVulnerable = false;
                bodyCollider.gameObject.layer = defaultLayer;
                RB2D.velocity = Vector2.zero;
                velocity = Vector2.zero;
                
               

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
            else
            {

               
                  
                 //   player.velocity = new Vector2(player.velocity.x, 5);

               
            }
          
        }
        if(collision.gameObject.layer == GroundLayerInt && !isGrounded)
        {
            RB2D.velocity = Vector2.zero;
        }
    }
  
    public void CheckPeekCondition()
    {
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.transform.position.y - transform.transform.position.y).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,dir,3); 
     

        if (hit.transform == player.transform)
        {
            animator.SetTrigger("PeekTrigger");
        }
        else
        {
            animator.SetTrigger("BackToIdle");
        }
    }
    public void PlayPeakSound()
    {
        AudioManager.a_Instance.BennyPeekAudio();
    }
    public void SetBackToIdle()
    {
        animator.SetTrigger("BackToIdle");
    }

    public void CheckAttackCondition() //called by animator
    {
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.transform.position.y - transform.transform.position.y).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 3);
        if (hit.transform == player.transform)
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
   
 
    public IEnumerator JumpCoRou()
    {
        isJumping = true;
     

        isVulnerable = true;
        bodyCollider.gameObject.layer = EnemyLayer;
        
        velocity = new Vector2(jumpAcceleration.x,jumpAcceleration.y);
        IsAttacking = true;
        yield return null;
       
    }
   public void SetGroundfromAnimator()
    {
        if (checkForGround)
        {
            isJumping = false;
            velocity.x = 0;
            animator.SetTrigger("Land");
           
        }
    }
  
}
