using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Health
{
    private CharacterController2D player;
    private SpriteRenderer sr;
    private Vector3 scale;
    private Vector3 negScale;
    public Vector2 jumpAcceleration;
    public Vector2 platformCheckAngle;
    private Vector2 velocity;
    private Rigidbody2D rb2d;
    private int GroundLayerInt;
    public int damage;
    private int Direction;
    public float runSpeed;
    private float minx;
    private float maxX;
    public float JumpCoolDown;
    public float JumpCountingTime;
    private float moveDis;
    public float moveAmount;
    public float wallCheckRange;
    public float ViewRange;
    public float groundCheckRadius;
    public float GroundCheckRange;
    public float JumpForce;
    public float JumpTimingDistance;
    public float enemyCheckRange;
    public float gravityScale;
    public float moveSpeed;
    public bool JumpRangeReady;
    public bool isGrounded;
    public bool IsAttacking;
    public bool detectingPlayer;
    public bool ismovingenemy;
    private bool isAccelerating;
    public bool JumpReady;
    private bool IsJumpOnCooldown;
    public LayerMask EnemyLayer;
    public LayerMask playerLayer;
    public LayerMask GroundLayer;
    public Slider healthBar;
    public float hitKnockBack;

    public void Init()
    {
        velocity = Vector2.zero;
        platformCheckAngle = new Vector3(transform.position.x + Direction * 0.5f, transform.position.y - 1) - transform.position;
        GroundLayerInt = 8;
        isGrounded = false;
        player = FindObjectOfType<CharacterController2D>();
        minx = transform.position.x - moveAmount;
        maxX = transform.position.x + moveAmount;
        moveDis = 0f;
        Debug.Log("enemyinit e");
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        Direction = -1;
        scale = transform.localScale;
        negScale = new Vector3(-scale.x, scale.y, scale.z);
    }
    void Update()
    {

        healthBar.value = health;
        CheckForPlayer();
        if (!isGrounded)
        {
            velocity.y -= gravityScale * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }
        
        if (ismovingenemy)
        {

            if (JumpCountingTime <= JumpCoolDown)
            {
                IsJumpOnCooldown = false;
                JumpCountingTime += Time.deltaTime;
            }
            else
            {
                IsJumpOnCooldown = true;

            }
            

            switch (Direction)
            {
                case -1:
                    if (transform.position.x >= minx)
                    {
                        transform.localScale = scale;
                        moveDis = -1;

                    }
                    else
                    {
                        Direction = 1;

                    }
                    break;
                case 1:
                    if (transform.position.x <= maxX)
                    {

                        transform.localScale = negScale;


                        moveDis = 1f;

                    }
                    else
                    {
                        Direction = -1;

                    }

                    break;

            }
            if (ObsticaleCheck() && Direction == -1)
            {
                minx = transform.position.x;
                maxX = transform.position.x + moveAmount;


            }
            else if (ObsticaleCheck() && Direction == 1)
            {
                maxX = transform.position.x;
                minx = transform.position.x - moveAmount;

            }


            if (!detectingPlayer && isGrounded && !isHit)
            {

                velocity = new Vector2(moveDis * moveSpeed * Time.deltaTime, 0);


            }
            else if (detectingPlayer && isGrounded && !isHit)
            {

                velocity = new Vector2(moveDis * runSpeed * Time.deltaTime, 0);

            }
            if (JumpRangeReady && JumpReady && IsJumpOnCooldown && !isHit)
            {



                StartCoroutine(JumpCoRou());
                AudioManager.a_Instance.BennyJumpAudio();
            }
            if (isHit)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.05f);

                if (player.transform.position.x >= transform.position.x)
                {
                   velocity += new Vector2(-hitKnockBack * Time.deltaTime, 0f);
                }
                else
                {
                    velocity += new Vector2(hitKnockBack * Time.deltaTime, 0f);

                }
                EffectsManager.instance.CreateEffect(effects.blood, transform);
                EffectsManager.instance.CreateEffect(effects.hit, transform);


                isHit = false;
            }

            transform.Translate(velocity * Time.deltaTime);


        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject )
        {
            if (IsAttacking)
            {
                player.TakeDamage(damage);
                if (player.transform.position.x >= transform.position.x)
                {
                    player.velocity += new Vector2(player.hitKnockBack.x * Time.deltaTime, 0);
                }
                else
                {
                    player.velocity += new Vector2(-player.hitKnockBack.x * Time.deltaTime, 0);

                }
                AudioManager.a_Instance.BennyAttackAudio();
                IsAttacking = false;
            }
            if (!isHit)
            {
                velocity.x = 0f;

            }

        }
        if(collision.gameObject.layer == GroundLayerInt && !isGrounded)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.05f);
        }
    }
    private void CheckForPlayer()
    {
        RaycastHit2D playerColCheck = Physics2D.CircleCast(transform.position, 0.6f, new Vector2(Direction, 0), ViewRange, playerLayer);
      


        if (playerColCheck)
        {
            RaycastHit2D wallColCheck = Physics2D.CircleCast(playerColCheck.point, 0.5f, new Vector2(-Direction, 0), Vector2.Distance(playerColCheck.point, transform.position), GroundLayer);

            if (!wallColCheck)
            {
                detectingPlayer = true;
            }
            else
            {

                detectingPlayer = false;

            }
            Debug.Log("detectingPlayer" + detectingPlayer);
        }
        else
        {

            detectingPlayer = false;

        }
        RaycastHit2D JumpTimingDisCheck = Physics2D.CircleCast(transform.position + new Vector3(Direction * 0.5f, 0, 0), 0.6f, new Vector2(Direction, 0), JumpTimingDistance, playerLayer);
        if (JumpTimingDisCheck && JumpReady && detectingPlayer)
        {

            JumpRangeReady = true;
        }
        else
        {
            JumpRangeReady = false;

        }


    }

    public bool ObsticaleCheck()
    {

        if (!detectingPlayer)
        {
            RaycastHit2D WallCheck = Physics2D.Raycast(transform.position, new Vector2(Direction, 0), wallCheckRange, GroundLayer);
            RaycastHit2D EnemyCheck = Physics2D.Raycast(new Vector2(transform.localPosition.x + Direction * 0.7f, transform.position.y), new Vector2(Direction, 0), enemyCheckRange, EnemyLayer);
            if (isGrounded)
            {
                RaycastHit2D PlatformEdgeCheck = Physics2D.Raycast(new Vector2(transform.position.x + Direction * 0.3f, transform.position.y), platformCheckAngle, 1.5f, GroundLayer);
                if (WallCheck || EnemyCheck || !PlatformEdgeCheck)
                {
                    return true;
                }
            }


        }
        return false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GroundLayerInt)
        {
            isGrounded = true;
            velocity.y = 0f;
            JumpReady = true;
            IsAttacking = false;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        JumpReady = false;
        isGrounded = false;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, new Vector3(Direction * ViewRange, 0, 0));
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, new Vector3(Direction * wallCheckRange, 0, 0));
        Gizmos.DrawRay(new Vector2(transform.position.x + Direction * 0.3f, transform.position.y), platformCheckAngle);
        Gizmos.DrawWireSphere(new Vector3(minx, transform.position.y, transform.position.z), 0.3f);
        Gizmos.DrawWireSphere(new Vector3(maxX, transform.position.y, transform.position.z), 0.3f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, -GroundCheckRange, transform.position.z), groundCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, new Vector3(Direction * JumpTimingDistance, 0, 0));
        
    }
    public IEnumerator JumpCoRou()
    {
        IsAttacking = true;
        isAccelerating = true;
        JumpCountingTime = 0;
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.05f);
        float Destination = transform.position.y + JumpForce;
        float negDestination = transform.position.y - 0.01f;
        float currentjumpAccelerationY = jumpAcceleration.y;
        float currentjumpAccelerationX = jumpAcceleration.x;
    
        while (isAccelerating)
        {
    
            velocity += new Vector2(currentjumpAccelerationX * Direction * Time.deltaTime, currentjumpAccelerationY * Time.deltaTime);
            if (transform.position.y >= Destination || transform.position.y < negDestination)
            {
                velocity.y *= 0.5f;
                velocity.x *= 0.5f;
                isAccelerating = false;
            }
            yield return null;
        }

    }
  


}


