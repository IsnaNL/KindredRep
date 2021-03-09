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
    public RaycastHit2D FrontWallCheck;
    public RaycastHit2D TopWallCheck;
    public float savedGravityScale;
    private WeaponAnimatorController animationControllerSwapper;
    private BlinkRed blinkRed;
    public bool canMove;
    public bool isJumping;
    public float moveInput;
    public float maxMagnitude;
    public bool islookingright;
    public Vector2 velocity;
    public Inventory inventory;
    public LayerMask groundLayerMask;
    public LayerMask playerLayerMask;
    public bool isGrounded;
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
    public float JumpDelaytime;
    public float dirAxis = 1;
    public Transform Step;
    public override void Start()
    {
        base.Start();
    }
    public void Init()
    {
        Debug.Log("characterinit");   
        groundLayer = 8;  
        coinLayer = 14;
        isGrounded = false;
        animationControllerSwapper = GetComponentInChildren<WeaponAnimatorController>();
        blinkRed = GetComponentInChildren<BlinkRed>();
        rb = GetComponent<Rigidbody2D>();
        islookingright = true;
        currentScale = new Vector2(transform.localScale.x, transform.localScale.y);
        savedGravityScale = gravityScale;
        animationControllerSwapper.Init();
        animationControllerSwapper.player = this;
        inventory = GetComponentInChildren<Inventory>();
        inventory.Init();
        canMove = true;
        
    }
    private void Update()
    {  
        float acceleration = isGrounded ? walkAcceleration : airAcceleration;
        float deceleration = isGrounded ? groundDeceleration : 0;
        if (inventory.swapKeyMapping)
        {
            moveInput = canMove ? Input.GetAxisRaw("Horizontal") : 0;
            verInput = canMove ? Input.GetAxisRaw("Vertical") : 0;
        }
        else
        {
            moveInput = canMove ? Input.GetAxisRaw("HorizontalKeys") : 0;
            verInput = canMove ? Input.GetAxisRaw("VerticalKeys") : 0;
        }
       
        dirAxis = islookingright ? 1f : -1f;
        CheckFlip();
        HorizontalMovement(acceleration, deceleration);
        GetInputJumpMethod();
       

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        CeilingCheck();
        WallCol();
        if (!isGrounded && !inventory.pickaxe.isPickaxeClawed)//gravity set
        {
            SetGravity();
            SetFalling();

        }

    }
    private void SetFalling()
    {
        if (velocity.y < 0f && !isGrounded)
        {
            isFalling = true;
            isJumping = false;
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsRunning", false);
            animator.SetBool("Idle", false);

        }
    }

    private void SetGravity()
    {
        if (velocity.y >= -9.5f)
            velocity.y -= gravityScale * Time.deltaTime;
        
    }
    private void HorizontalMovement(float acceleration, float deceleration)
    {
        if (moveInput != 0 && isGrounded /*&& !TopWallCheck && !FrontWallCheck*/)
        {
          
            animator.SetBool("Idle", false); 
         //   animator.speed = Mathf.Abs(velocity.x) / speed * secondMaxSpeedModifier;
        }
        else if(moveInput == 0 && isGrounded)
        {
            animator.SetBool("Idle", true);
        }
        if (moveInput!= 0)
        {
            if (!inventory.sword.isSwordDashing)
            {
              
                MovePlayer(acceleration);
            }
            SmallLedgeInteraction();
            Turn();
        }
        else
        {
            animator.speed = 1;
            animator.SetBool("IsRunning", false);
            StopPlayer(deceleration);
        }
    }

    private void Turn()
    {
        if (moveInput == 1)
        {
            if (velocity.x < 0)
            {
                velocity.x += 1;
            }
        }
        else if (moveInput == -1)
        {
            if (velocity.x > 0)
            {
                velocity.x -= 1;

            }

        }
    }

    private void StopPlayer(float deceleration)
    {
        velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
    }
    private void MovePlayer(float acceleration)
    {
        animator.SetBool("IsRunning", true);

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
            if (isGrounded && canMove)
            {
                StartCoroutine("JumpCoroutine");
            }
        }
    }
    private IEnumerator JumpCoroutine()
    {
        float startVel = velocity.x;
        animator.speed = 1;
        canMove = false;
        animator.SetTrigger("JumpAnim");
        canMove = true;
        velocity.x = startVel;
        isJumping = true;
        float Destination = transform.position.y + jumpHeight.y; 
        velocity.y = 8;
        while (Input.GetButton("Jump") && isJumping)
        {

          
             velocity += new Vector2(jumpAcceleration.x * moveInput * Time.deltaTime * 0.99f, jumpAcceleration.y * Time.deltaTime * 0.99f);

            if (transform.position.y >= Destination || inventory.sword.isSwordDashing)
            {           
                velocity *= 0.99f;
                isJumping = false;
            }
            yield return null;
        }
    }
    private void WallCol()
    {
        TopWallCheck = Physics2D.Raycast(new Vector2(transform.position.x , transform.position.y+0.2f), Vector2.right * dirAxis, 0.6f, groundLayerMask);
        if (TopWallCheck)
        {
            velocity.x *= 0f;
            animator.speed = 1;
        }else
        {
            FrontWallCheck = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.4f * dirAxis,transform.localPosition.y + 0.2f), Vector2.down, 0.5f, groundLayerMask);          
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
 
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (isHit)
        {
            EffectsManager.instance.CreateEffect(Effects.blood, transform, false);
            EffectsManager.instance.CreateEffect(Effects.hit, transform, false);
            AudioManager.a_Instance.AlyxHitAudio();
            StartCoroutine(blinkRed?.BlinkRoutine());
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
            animator.SetBool("IsFalling", true);
            inventory.shotgun.IsShotgunKnockback = false;
            if (velocity.y >= 0)
            {
                velocity.y = 0;
            }

        }
        
    }
    private void SmallLedgeInteraction()
    {
        if (velocity.x >= speed * 0.5f && isGrounded || velocity.x <= -speed * 0.5f && isGrounded)
        {

            if (!TopWallCheck && FrontWallCheck)
            {
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsLedgeJump", true);
                //transform.position = new Vector2(transform.position.x + 0.2f,FrontWallCheck.point.y + 0.5f);
                velocity  = new Vector2(0,5.4f);
            }

        }
      
    }
    private void OnDisable()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance?.StartCoroutine(GameManager.instance?.ReviveCharacter());
        }
    }
    private void OnEnable()
    {
        velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.gameObject.layer == groundLayer)
        {
            animator.SetTrigger("Land");
            groundTriggerCount++;
            if (groundTriggerCount == 1)
            {
                StopCoroutine("JumpCoroutine");
                isGrounded = true;
                canMove = true;
                animator.speed = 1;
                gravityScale = 0f;
                rb.velocity = new Vector3(0, 0, 0); 
                velocity.y = 0;
                isJumping = false;
                isFalling = false;
                inventory.pickaxe.isClawing = false;
                animator.SetBool("IsFalling", false);
                animator.SetBool("IsLedgeJump", false);
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
                isGrounded = false;
                animator.SetBool("Idle", false);
                animator.SetBool("IsRunning", false);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  
        Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.4f), Vector2.right * dirAxis * 0.6f);
        Gizmos.DrawRay(new Vector2(transform.localPosition.x + 0.6f * dirAxis, transform.localPosition.y + 0.2f), Vector2.down);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + (0.2f * dirAxis), transform.position.y), 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, ceilingCheckDis, 0));
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, ceilingCheckDis, 0), 0.35f);
        Gizmos.color = Color.blue;
    }
    
}