using System.Collections;
using UnityEngine;

public class SunFlower : Health
{
    
    public CharacterController2D PlayerRef;
    public SunFlowerSword sword;
    private SpriteRenderer spriteRenderer;
    private BlinkRed blinkRed;
    private Rigidbody2D rb2d;
    public float slashAttackRange;
    public float slashAttackCooldown;
    public float slashAttackCounting;
    public bool slashAttackReady;
    private Vector2 scale;
    private Vector2 negscale;
    public float JumpAttackDes;
    public float JumpAttackRange;
    public bool JumpAttackReady;
    public float JumpAttackCooldown;
    public float JumpAttackCounting;
    private bool isjumpAttack;
    public Vector2 velocity;
    public float gravityScale;
    public int moveSpeed;
    public Vector2 jumpAcceleration;
    public int direction;
    public int groundLayerInt;
    public LayerMask groundLayerMask;
    public bool jumpAttackFollowUpReady;
    public int playerLayerInt;
    public LayerMask playerLayer;
    public float backWalkSpeed;
    private bool isWalkBack;









    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        scale = transform.localScale;
        negscale = new Vector2(-scale.x, scale.y);
        PlayerRef = FindObjectOfType<CharacterController2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        blinkRed = GetComponent<BlinkRed>();
        //hitDamage = 0;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (isHit && this.gameObject.activeInHierarchy)
        {
            EffectsManager.instance.CreateEffect(Effects.blood, transform);
            EffectsManager.instance.CreateEffect(Effects.hit, transform);
            blinkRed?.StartCoroutine(blinkRed?.BlinkRoutine());
        }
    
    }
    // Update is called once per frame
    void Update()
    {
        Flip();
        SlashCD();
        JumpAttackCD();
        Gravity();
        AttackCheck();
        if (!isjumpAttack)
        {
            SetWalk();
        }
        if (jumpAttackFollowUpReady && IsGrounded())
        {
            animator.SetTrigger("FollowUpAttack");

            jumpAttackFollowUpReady = false;

        }
        CheckJumpAttack();

    }

    private void CheckJumpAttack()
    {
        if (Vector2.Distance(PlayerRef.transform.position, transform.position) <= JumpAttackRange && Vector2.Distance(PlayerRef.transform.position, transform.position) >= JumpAttackRange * 0.5f)
        {
            if (JumpAttackReady)
            {
                JumpAttackCounting = 0;
                JumpAttackReady = false;
                StartCoroutine(JumpAttack());
                animator.SetTrigger("JumpAttack");
            }
        }
    }

    void SetWalk()
    {
        if (IsGrounded())
        {
            if (!IsTouchingPlayer() && !isWalkBack)
            {
                velocity = new Vector2(direction * moveSpeed * Time.deltaTime, 0f);

            }
            else
            {
                StartCoroutine(WalkBack());
                
            }

        }
    }
    IEnumerator WalkBack()
    {
        isWalkBack = true;
        velocity = new Vector2(-direction * backWalkSpeed * Time.deltaTime, 0f);

        yield return new WaitForSeconds(1.4f);
        isWalkBack = false;

    }
    private void AttackCheck()
    {
        if (Vector2.Distance(PlayerRef.transform.position, transform.position) <= slashAttackRange)
        {

            if (slashAttackReady && IsGrounded() && !jumpAttackFollowUpReady)
            {

                animator.SetTrigger("CloseAttack");
                slashAttackCounting = 0;
                slashAttackReady = false;
            }
        }
    }

    private void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y -= gravityScale * Time.deltaTime;
        }
      
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + velocity);
       
    }
    void Flip()
    {
        if (transform.position.x >= PlayerRef.transform.position.x)
        {

            transform.localScale = scale;
            direction = -1;
        }
        else
        {
            transform.localScale = negscale;
            direction = 1;

        }
    }
    void SlashCD()
    {

        if (slashAttackCounting <= slashAttackCooldown)
        {
            slashAttackReady = false;
            slashAttackCounting += Time.deltaTime;
        }
        else
        {
            slashAttackReady = true;
        }
    }
    void JumpAttackCD()
    {
        if (JumpAttackCounting <= JumpAttackCooldown)
        {
            JumpAttackCounting += Time.deltaTime;
        }
        else
        {
            JumpAttackReady = true;
        }
    }
    IEnumerator JumpAttack()
    {
        isjumpAttack = true;
       
        float Destination = transform.position.y + JumpAttackDes;
        float currentjumpAccelerationY = jumpAcceleration.y;
        float currentjumpAccelerationX = jumpAcceleration.x;
        Vector2 currentvelocity = velocity;

        while (isjumpAttack)
        {

            velocity += new Vector2(currentjumpAccelerationX * direction * Time.deltaTime * 0.99f, currentjumpAccelerationY * Time.deltaTime * 0.99f);
            if (transform.position.y >= Destination)
            {
                jumpAttackFollowUpReady = true;
                slashAttackCounting = 2.5f;
                velocity *= 0.5f;
                isjumpAttack = false;
            }
            yield return null;
        }
    }
    public bool IsGrounded()
    {
        if (!isjumpAttack)
        {
            RaycastHit2D groundCheckCol = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.5f, groundLayerMask);


            if (groundCheckCol)
            {
               // Debug.Log("StickManGrounded");
                return true;
            }
            
        }
        return
               false;
    }
    public bool IsTouchingPlayer()
    {
        RaycastHit2D PlayerCheck = Physics2D.CircleCast(transform.position, 0.5f,new Vector2(direction, 0),0.8f, playerLayer);
        if (PlayerCheck)
        {
            return true;
        }
        return 
            false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.5f);
        Gizmos.DrawLine(new Vector2 (transform.position.x + JumpAttackRange * 0.5f * direction,transform.position.y) , new Vector2(transform.position.x + JumpAttackRange * direction, transform.position.y));
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, new Vector2(direction * 1.5f, 0));
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + direction * 1f, transform.position.y), 0.5f);
      
    }
  
}

