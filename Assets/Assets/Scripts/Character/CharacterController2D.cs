using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : Health
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;
    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    Vector2 jumpHeight;
    public int groundLayer;
    private Vector2 currentScale;
    private int coinLayer;
    public RaycastHit2D BottomWallCheck;
    public RaycastHit2D TopWallCheck;
    private float savedGravityScale;
    private WeaponAnimatorController animationControllerSwapper;
    public bool canMove;
    public bool isJumping;
    public float moveInput;
    public float shrinkTime;
    public float maxMagnitude;
    public bool islookingright;
    public Vector2 velocity;
    public Inventory inventory;
    public LayerMask groundLayerMask;
    public LayerMask playerLayerMask;
    public bool IsGrounded;
    public Vector2 hitKnockBack;
    public Vector2 jumpAcceleration;
    public float ceilingCheckDis;
    public float gravityScale;
    public int groundTriggerCount;
    public Rigidbody2D rb;
    public float verInput;
    public bool isFalling;
    public float secondMaxAccelrationModifier;
    public float secondMaxSpeedModifier;
   

    public override void Start()
    {
        base.Start();
    }
    public void Init()
    {
        Debug.Log("characterinit");   
        groundLayer = 8;  
        coinLayer = 14;
        IsGrounded = false;
        animationControllerSwapper = GetComponentInChildren<WeaponAnimatorController>();
        rb = GetComponent<Rigidbody2D>();
        islookingright = true;
        currentScale = new Vector2(transform.localScale.x, transform.localScale.y);
        savedGravityScale = gravityScale;
        inventory = GetComponentInChildren<Inventory>();
        inventory.Init();
        animationControllerSwapper.Init();
        canMove = true;
      
    }
    private void Update()
    {  
        float acceleration = IsGrounded ? walkAcceleration : airAcceleration;
        float deceleration = IsGrounded ? groundDeceleration : 0;
        moveInput = canMove ? Input.GetAxisRaw("Horizontal") : 0;
        verInput = canMove ? Input.GetAxisRaw("Vertical") : 0;
        HorizontalMovement(acceleration, deceleration);
        HitImpact();
        GetInputJumpMethod();
        CheckFlip();
        if (!IsGrounded && !inventory.pickaxe.isPickaxeClawed)//gravity
        {
            if (velocity.y >= -10)
            {
                velocity.y -= gravityScale * Time.deltaTime;

            }
           if (velocity.y < 0)
            {
                isFalling = true;
                animator.SetBool("IsFalling", true);
                isJumping = false;
                animator.SetBool("Idle", false);
                
            }

        }
    
    }
    private void FixedUpdate()
    {
        CeilingCheck();
        WallCol();
        transform.Translate(velocity * Time.fixedDeltaTime);
        
    }
    private void HorizontalMovement(float acceleration, float deceleration)
    {
        if (moveInput != 0)
        {
           animator.SetBool("Idle", false);

           animator.SetBool("IsRunning", IsGrounded);
            animator.speed =  Mathf.Abs(velocity.x) / speed * secondMaxSpeedModifier;

            if (!inventory.sword.isSwordDashing)
            {
                if (inventory.shotgun.enabled && verInput != 0)
                {
                    StopPlayer(deceleration);
                    return;
                }

                MovePlayer(acceleration);
            }
            if (velocity.x >= speed * 0.5f && IsGrounded || velocity.x <= -speed * 0.5f && IsGrounded)
            {

                if (!TopWallCheck && BottomWallCheck)
                {
                    velocity.y = 5.1f;
                }

            }
            else
            {
                animator.SetBool("IsRunning", false);

            }
            if(moveInput == 1)
            {
                if(velocity.x < 0)
                {
                    velocity.x += 1;
                }
            }else if (moveInput == -1)
            {
                if (velocity.x > 0)
                {
                    velocity.x -= 1;

                }

            }
        }
        else
        {
         
            StopPlayer(deceleration);
          
            animator.SetBool("IsRunning", false);
            animator.speed = 1;
            animator.SetBool("Idle", true);
        }
    }
    private void StopPlayer(float deceleration)
    {
        velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
    }
    private void MovePlayer(float acceleration)
    {
        velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        if (velocity.x == speed * moveInput)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed *secondMaxSpeedModifier * moveInput, acceleration * secondMaxAccelrationModifier * Time.deltaTime);

        }
    }
    private void GetInputJumpMethod()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded && canMove)
            {

                StartCoroutine("JumpCoroutine");
             
            }
        
        }
    }
    private IEnumerator JumpCoroutine()
    {
        isJumping = true;
        animator.SetBool("IsJumping", true);
        animator.SetTrigger("JumpAnim");   
        float Destination = transform.position.y + jumpHeight.y; 
        velocity.y = 4;
        while (Input.GetButton("Jump") && isJumping)
        {
            if (!inventory.sword.isSwordDashing)
            {
                velocity += new Vector2(jumpAcceleration.x * moveInput * Time.deltaTime * 0.99f, jumpAcceleration.y * Time.deltaTime * 0.99f);

            }   
            if (transform.position.y >= Destination)
            {
                animator.SetBool("IsJumping", false);
                velocity *= 0.99f;
                isJumping = false;
            }
            yield return null;
        }
    }
    private void WallCol()
    {

        if (islookingright)
        {

            BottomWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.right, 0.5f, groundLayerMask);
            TopWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.right, 0.5f, groundLayerMask);
        }
       else
        {
            BottomWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.left, 0.5f, groundLayerMask);
            TopWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.left, 0.5f, groundLayerMask);
        }
    
        if (TopWallCheck && BottomWallCheck)
        {
            velocity.x = 0f;
        }
        
    }
    void CheckFlip()
    {
        if (moveInput > 0)
        {
            islookingright = true;
            Flip();
        }
        else if (moveInput < 0)
        {
            islookingright = false;
            Flip();
        }   
    }
    public void Flip()
    {
        if (islookingright)
        {
            
            transform.localScale = new Vector3(currentScale.x, currentScale.y, 1);
        }
        else
        {
           
            transform.localScale = new Vector3(-currentScale.x, currentScale.y, 1);
        }
      
    }
    private void HitImpact()
    {
        if (isHit)
        {
            EffectsManager.e_Instance.BloodHitEffect(transform.position);
            EffectsManager.e_Instance.HitEffect(transform.position);
            AudioManager.a_Instance.AlyxHitAudio();
            isHit = false;

        }
    }
    void CeilingCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.up, ceilingCheckDis, groundLayerMask);
        if (hit)
        {
            StopCoroutine("JumpCoroutine");
            isFalling = true;
            inventory.shotgun.IsShotgunKnockback = false;
            if (velocity.y >= 0)
            {
                velocity.y = 0;
            }
            Debug.Log("Ceilinghit");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.gameObject.layer == groundLayer)
        {
            animator.SetTrigger("Land");
            groundTriggerCount++;
            if (groundTriggerCount == 1)
            {
                IsGrounded = true;
                animator.speed = 1;
                gravityScale = 0f;
                rb.velocity = new Vector3(0, 0, 0); 
                velocity.y = 0;
                animator.SetBool("IsFalling", false);
                isJumping = false;
                isFalling = false;
                StopCoroutine("JumpCoroutine");


            }
        }
        if (collision.gameObject.layer == coinLayer)
        {
            Destroy(collision.gameObject, 0.3f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            groundTriggerCount--;
            if (groundTriggerCount == 0)
            {
                gravityScale = savedGravityScale;        
                IsGrounded = false;
                animator.SetBool("Idle", false);
            
            }
        }
      
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
       // Gizmos.DrawWireSphere(weapon.transform.position, ClawRange);
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, dashWallCheckRange);

        if (islookingright)
        {
            Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.right * 0.5f);
            Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.right * 0.5f);
        }else
        {
            Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.left * 0.5f);
            Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.left * 0.5f);
        }
            
         

        

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, ceilingCheckDis, 0));
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, ceilingCheckDis, 0), 0.35f);
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, enemyCheckRange);
    }
}


