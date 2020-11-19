using System.Collections;
using UnityEngine;

public class SunFlower : Health
{
    public Collider2D Player;
    public SunFlowerSword Sword;
    public CharacterController2D PlayerRef;
    public float SlashAttackRange;
    public float SlashAttackCooldown;
    public float SlashAttackCounting;
    public bool SlashAttackReady;
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
    public float BackWalkSpeed;









    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        scale = transform.localScale;
        negscale = new Vector2(-scale.x, scale.y);
        //hitDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        flip();
        SlashCD();
        JumpAttackCD();
        
        if (!IsGrounded())
        {
            velocity.y -= gravityScale * Time.deltaTime;
        }
        else
        {
            if (!isTouchingPlayer())
            {
                velocity = new Vector2(direction * moveSpeed * Time.deltaTime, 0f);

            }
            else
            {
                velocity = new Vector2(-direction * moveSpeed * Time.deltaTime * BackWalkSpeed, 0f);
            }

        }
        if (Vector2.Distance(Player.transform.position, transform.position) <= SlashAttackRange)
        {

            if (SlashAttackReady && IsGrounded() && !jumpAttackFollowUpReady)
            {

                animator.SetTrigger("SlashAttackSunflower");
                SlashAttackCounting = 0;
                SlashAttackReady = false;
            }
        }
        if (jumpAttackFollowUpReady && IsGrounded())
        {
            animator.SetTrigger("FollowUpAttackSunflower");

            jumpAttackFollowUpReady = false;

        }
        if (Vector2.Distance(Player.transform.position, transform.position) <= JumpAttackRange && Vector2.Distance(Player.transform.position, transform.position) >= JumpAttackRange * 0.5f)
        {
            if (JumpAttackReady)
            {
                JumpAttackCounting = 0;
                JumpAttackReady = false;
                StartCoroutine(JumpAttack());

            }
        }

    }

    void FixedUpdate()
    {
        transform.Translate(velocity);
    }
    void flip()
    {
        if (transform.position.x + 1 >= Player.transform.position.x)
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

        if (SlashAttackCounting <= SlashAttackCooldown)
        {
            SlashAttackReady = false;
            SlashAttackCounting += Time.deltaTime;
        }
        else
        {
            SlashAttackReady = true;
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
                SlashAttackCounting = 2.5f;
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
            RaycastHit2D groundCheckCol = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 1.6f, groundLayerMask);


            if (groundCheckCol)
            {
                Debug.Log("StickManGrounded");
                return true;
            }
            
        }
        return
               false;
    }
    public bool isTouchingPlayer()
    {
        RaycastHit2D PlayerCheck = Physics2D.CircleCast(transform.position, 1f,new Vector2(direction, 0),1.6f, playerLayer);
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
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - 1.5f), 0.5f);
        Gizmos.DrawLine(new Vector2 (transform.position.x + JumpAttackRange * 0.5f * direction,transform.position.y) , new Vector2(transform.position.x + JumpAttackRange * direction, transform.position.y));
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, new Vector2(direction * 1.5f, 0));
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + direction * 1.5f, transform.position.y), 1f);
      
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.layer == groundLayerInt)
    //    {
    //        velocity = Vector2.zero;
    //        isGrounded = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    isGrounded = false;
    //}
}

