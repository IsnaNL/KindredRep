using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Benny : Health
{
    public CharacterController2D player;
    private SpriteRenderer SR;
    public LayerMask GroundLayerMask;
    public LayerMask playerLayerMask;
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
    private bool IdleAudioTrigger;
    public float RangeForIdleAudio;


    public void Init()
    {
        SR = GetComponent<SpriteRenderer>();  
        startingJumpAccelerationX = jumpAcceleration.x;
        isVulnerable = false;
    }
    void LateUpdate()
    {
        animator.SetBool("IsLand", !isJumping);
        healthBar.value = health;
        if (!IsAttacking)
        {
            SR.flipX = player.transform.position.x >= transform.position.x;
            Direction = player.transform.position.x >= transform.position.x ? 1 : -1;
        }
    }

    private void WhenHit()
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
                velocity = new Vector2(HitForce.x, HitForce.y);
            }
            isVulnerable = false;
            AudioManager.a_Instance.BennyHurtAudio();
            isHit = false;
    }

     void FixedUpdate()
    {
        if (isHit)
        {
          WhenHit();
        }
        CheckPeekCondition();
        
        if (!isGrounded)

        {
            velocity.y -= GravityScale * Time.deltaTime;
        }
        else
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= RangeForIdleAudio)
            {
                if (!IdleAudioTrigger)
                {
                    AudioManager.a_Instance.BennyIdleAudio();
                    IdleAudioTrigger = true;
                }
            }
            else
            {
                IdleAudioTrigger = false;
            }
        }
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.fixedDeltaTime;
     }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        groundTriggerCount++;
        if (groundTriggerCount == 1)
        {
            if (collision.gameObject.layer == GroundLayerInt)
            {
                isJumping = false;         
                isGrounded = true;
                IsAttacking = false;
                isVulnerable = false;
                bodyCollider.gameObject.layer = defaultLayer;
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
            AttackExecute(dir);
        }

    }
    public void AttackExecute(Vector2 dir)
    {
        jumpAcceleration.x = startingJumpAccelerationX * dir.x;

        animator.SetTrigger("AttackTrigger");
        StartCoroutine((AttackJumpCoRou()));
    }
 
    public IEnumerator AttackJumpCoRou()
    {
        AudioManager.a_Instance.BennyJumpAudio();
        isVulnerable = true;
        bodyCollider.gameObject.layer = EnemyLayer;
        velocity = new Vector2(jumpAcceleration.x,jumpAcceleration.y);
        IsAttacking = true;
        GroundCheckCollider.enabled = false;
        isJumping = true;
        yield return new WaitForSeconds(0.1f);  
        GroundCheckCollider.enabled = true;
      
    }
 
  
}
