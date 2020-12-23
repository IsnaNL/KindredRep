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
    //  private Vector2 savedMousePos;
    //private bool isShrink;
    //private Vector2 mouseDirNormalized;


   
    
   // private TrailRenderer trail;
    //public float MouseYDelta;
    public int groundLayer;
    private Vector2 currentScale;
    private int coinLayer;
    public RaycastHit2D BottomWallCheck;
    public RaycastHit2D TopWallCheck;
    private float savedGravityScale;
    private WeaponAnimatorController animationControllerSwapper;
    public bool isJumping;
    public float moveInput;
    public float shrinkTime;
    public float JumpXBoost;
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
    //private float runAnimSpeed;
    //private float counter;
    public float secondMaxAccelrationModifier;
    public float secondMaxSpeedModifier;

    //public float runAnimAcceleration;


    // private bool oneStep;
    //private SpriteRenderer sr;
    //public float dashWallCheckRange;
    //  public float DeltaCap;
    //public Vector2 shrinkSize;


    //private float playerToMousePosAngle;
    //private Vector2 mouseDir;




    //public BoxCollider2D groundCheckCollider;
    //public float afterBlastVelocityX;
    //public float knockBackOverTargetTime;
    //private float knockBackOverTime;
    //private GroundCheck groundcheck;
    //public float afterBlastVelocityY;
    //private bool ShotBlastAirForce;
    //private BoxCollider2D boxCollider;
    //private Vector2 savedvelocity;
    //private float currentVelocityY;
    // private Vector2 saveMouseDir;
    //private Vector2 mousePos;
    // private float mouseDeltaCounter;


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
       // trail = GetComponent<TrailRenderer>();
       // sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {


        //if (weapon.IsShotgunWeapon)
        //{


        //    //mouseDir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        //    //playerToMousePosAngle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        //}
        //if (weapon.IsSwordWeapon)
        //{

        //}
        //if (weapon.IsClawWeapon)
        //{

        //}
     //   mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      //  mouseDirNormalized = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;
       // MouseYDelta =  Input.GetAxis("Mouse Y");
     //   Debug.DrawRay(transform.position, mouseDirNormalized, Color.red);
      //  Debug.DrawRay(transform.position, -mouseDirNormalized, Color.blue);
        float acceleration = IsGrounded ? walkAcceleration : airAcceleration;
        float deceleration = IsGrounded ? groundDeceleration : 0;
        moveInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");
        HorizontalMovement(acceleration, deceleration);
        HitImpact();
        GetInputJumpMethod();
        CheckFlip();
      
        //Debug.Log(mouseDirNormalized);
        // velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
      //  GetInputSetCoditionsForSwordDash();
      //  GetInputSetConditionsForShotgunBlast();
       // GetInputSetConditionsForPickaxeClawing();
       
        //animator.SetFloat("MouseDir", MouseYDelta);
        //else
        //{
        //    if (!isDashing)
        //    {
        //        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        //    }
        //}
        //if (Input.GetButtonDown("Fire2") && weapon.IsFoilWeapon)
        //{
        //    if (isShrink)
        //    {
        //        isShrink = false;
        //    }
        //    else
        //    {
        //        isShrink = true;
        //    }
        //}
        
      
        
        //if (isShrink)
        //{
        //    SwordMovementAbility();
        //}


       // if (inventory.weaponCheck == 2)
        //{
        //    isPickaxeClawed = false;
        //    isPickaxeClawing = false;
            //rb.isKinematic = false;
      //  }
       

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
       

        //if (velocity.magnitude >= maxMagnitude)
        //{
        //    velocity = Vector2.ClampMagnitude(velocity, maxMagnitude);
        //}
    }
   private void FixedUpdate()
    {
        CeilingCheck();
        WallCol();
      /*  if (inventory.sword.isSwordDashing && !!inventory.shotgun.IsShotgunKnockback && !inventory.pickaxe.isPickaxeClawing && !TopWallCheck && !BottomWallCheck)
        {
            transform.Translate(inventory.sword.dashVelocity * Time.fixedDeltaTime);
        }
        else if (inventory.shotgun.IsShotgunKnockback && !inventory.sword.isSwordDashing && !inventory.pickaxe.isPickaxeClawing)
        {
            transform.Translate(inventory.shotgun.ShotgunBlastVelocity * Time.fixedDeltaTime);
        }
        else if (inventory.pickaxe.isPickaxeClawed && !inventory.sword.isSwordDashing && !inventory.shotgun.IsShotgunKnockback)
        {
            transform.Translate(inventory.pickaxe.pickaxeVelocity * Time.fixedDeltaTime);
        }
        else*/
      
        
           transform.Translate(velocity * Time.fixedDeltaTime);
        
    }
    private void HorizontalMovement(float acceleration, float deceleration)
    {
        if (moveInput != 0)
        {
           animator.SetBool("Idle", false);

           animator.SetBool("IsRunning", IsGrounded);

            // if (velocity.x != speed * moveInput)
            // {
            //    counter += Time.deltaTime;

            //  }


            // runAnimSpeed = 0.1f;
            //  runAnimSpeed = Mathf.MoveTowards(runAnimSpeed, 1, runAnimAcceleration * Time.deltaTime);
            // animator.speed = Mathf.MoveTowards(, 1, runAnimAcceleration * Time.deltaTime);
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
            //animator.SetBool("IsMoving", false);
            //counter = 0;
            StopPlayer(deceleration);
            /*  if(velocity.x >= 0)
               {
                   velocity.x -= deceleration * Time.deltaTime;

               }
              if(velocity.x < 0)
               {
                   velocity = 0;
               }*/
            //animator.SetBool("IsMoving", false);
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
            if (IsGrounded)
            {

                StartCoroutine("JumpCoroutine");
                //if (islookingright)
                //{
                //    velocity.x += 1;
                //}
                //if (moveInput > 0)
                //{
                //    velocity += jumpHeight;

                //}
                //else if (moveInput < 0)
                //{
                //    velocity.y += jumpHeight.y;
                //    velocity.x -= jumpHeight.x;
                //}else
                //{
                //    velocity.y = Mathf.MoveTowards(velocity.y, jumpHeight.y, jumpAcceleration);
                //}
                //StartCoroutine("JumpCoroutine");

                //StartCoroutine(TrailTime());
                //AudioManager.a_Instance.AlixJumpAudio();

                //LeanTween.move(gameObject, new Vector3(velocity.x, jumpHeight * Time.deltaTime), 0.8f );
                //velocity.y = Mathf.Sqrt(Time.deltaTime * jumpHeight);
                //else
                //{
                //    velocity.x -= 1;
                //}
                //animator.SetBool("IsMoving", false);
            }
           



        }
    }

    private IEnumerator JumpCoroutine()
    {

        //if (!IsGrounded)
        //{

        //    yield return null;
        //}


        isJumping = true;
        animator.SetBool("IsJumping", true);
        animator.SetTrigger("JumpAnim");

        //float startingpos = transform.position.y;
        float Destination = transform.position.y + jumpHeight.y;
       /// float currentjumpAccelerationY = jumpAcceleration.y;
       // float currentjumpAccelerationX = jumpAcceleration.x;
       // Vector2 currentvelocity = velocity;
        velocity.y = 5;
        //if (islookingright)
        //{
        //    JumpXBoost = 1;
        //}
        //else
        //{
        //    JumpXBoost = -1;
        //}
        while (Input.GetButton("Jump") && isJumping)
        {

            //currentjumpAcceleration *= 0.99f;
            //currentJumpX *= 0.99f;
            if (!inventory.sword.isSwordDashing)
            {
                velocity += new Vector2(jumpAcceleration.x * moveInput * Time.deltaTime * 0.99f, jumpAcceleration.y * Time.deltaTime * 0.99f);

            }

            //transform.position += new Vector3(currentJumpX * moveInput * Time.deltaTime * 0.8f, currentjumpAcceleration * Time.deltaTime * 0.8f, 0);
            if (transform.position.y >= Destination)
            {
                animator.SetBool("IsJumping", false);
                

                //  animator.SetBool("IsFalling", true);

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
            //  transform.rotation = Quaternion.Euler(0, 0, 0);
            BottomWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.right, 0.5f, groundLayerMask);
            TopWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.right, 0.5f, groundLayerMask);
           
        }
        else
        {

            // transform.rotation = Quaternion.Euler(0, 180, 0);
            BottomWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.left, 0.5f, groundLayerMask);
            TopWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.left, 0.5f, groundLayerMask);
            
        }
        if (TopWallCheck && BottomWallCheck)
        {
            velocity.x = 0f;
        }
    }
    //private void FixedUpdate()
    //{
    //    //if (IsShotgunKnockback)
    //    //{

    //    //    MovementAbility();
    //    //}
    //    //if (IsShotgunKnockback)
    //    //{
    //    //    transform.Translate(dashVelocity * Time.deltaTime);
    //    //}
    //    //else 


    //}
    void CheckFlip()
    {
        if (inventory.pickaxe.isPickaxeClawed && inventory.pickaxe.shouldFlip)
        {
            Flip();
           
            return;

        }
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
            //  transform.rotation = Quaternion.Euler(0, 0, 0);
            BottomWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.right, 0.5f, groundLayerMask);
            TopWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.right, 0.5f, groundLayerMask);
            //sr.flipX = false;
           // animationControllerSwapper.flipScale();
            transform.localScale = new Vector3(currentScale.x, currentScale.y, 1);
        }
        else
        {

            // transform.rotation = Quaternion.Euler(0, 180, 0);
            BottomWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.left, 0.5f, groundLayerMask);
            TopWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.left, 0.5f, groundLayerMask);
           // sr.flipX = true;
           // animationControllerSwapper.flipScale();

            transform.localScale = new Vector3(-currentScale.x, currentScale.y, 1);
        }
        inventory.pickaxe.shouldFlip = false;
    }
    private void HitImpact()
    {
        if (isHit)
        {
            EffectsManager.e_Instance.BloodHitEffect(transform);
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

            //velocity.x *= 0.9f;
            Debug.Log("Ceilinghit");
        }
    }
    
   
   
    //private IEnumerator ShrinkTime()
    //{
    //    currentScale = shrinkSize;
    //    //transform.localScale = shrinkSize;
    //    yield return new WaitForSeconds(shrinkTime);
    //    isShrink = false;
    //    currentScale = savedScale;
    //    //transform.localScale = currentScale;
    //}
   /* void LandingAnimationCall()
    {

        RaycastHit2D hitGround = Physics2D.CircleCast(transform.position, 0.4f, Vector2.down, 0.6f, groundLayerMask);
        if (hitGround && animator.speed != 1)
        {
            animator.speed = 1;
            //animator.SetTrigger("Land");
        }
    }*/
   
    /*void SetIsGroundedTofalse()//called from Invoke
    {
        IsGrounded = false;

    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //IsGrounded = collision != null && (((1 << collision.gameObject.layer) & groundLayer) != 0);
        if (collision.gameObject.layer == groundLayer)
        {
            animator.SetTrigger("Land");

            groundTriggerCount++;
            if (groundTriggerCount == 1)
            {
                IsGrounded = true;
                animator.speed = 1;
                gravityScale = 0f;
                rb.velocity = new Vector3(0, 0, 0); //Debug.Log(collision.gameObject.layer);
                velocity.y = 0;
                //gravityScale = 0f;
                //rb.gravityScale = 1f;
                animator.SetBool("IsFalling", false);
                animator.SetBool("IsJumping", false);
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
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (IsGrounded && collision.gameObject.layer == groundLayer)
    //    {


    //        if (velocity.x >= speed || velocity.x <= -speed)
    //        {

    //            //else if (TopWallCheck && BottomWallCheck)
    //            //{
    //            //    velocity = Vector2.zero;
    //            //}

    //        }
    //        //else
    //        //{ 
    //        //   velocity = Vector2.zero;
    //        //}



    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.layer != groundLayerMask || collision == null)
        if (collision.gameObject.layer == groundLayer)
        {
            groundTriggerCount--;
            if (groundTriggerCount == 0)
            {
                gravityScale = savedGravityScale;
               // animator.SetBool("Idle", false);
                //Invoke("SetIsGroundedTofalse",0.1f);
                IsGrounded = false;
                animator.SetBool("Idle", false);
                //Debug.Log("NOTGrounded" + IsGrounded);
            }
        }
        //    if (collision.gameObject.layer == enemyLayer)
        //    {
        //        collision.GetComponent<Enemy>().TakeDamage(weapon.damage);
        //    }
    }
   /* public void FallingAnimationCall()
    {
        animator.speed = 0;
        //if (IsGrounded)
        //{
        //    animator.speed = 1;
        //}
    }*/
    /*IEnumerator TrailTime()
    {
        trail.enabled = true;
        yield return new WaitForSeconds(1f);
        trail.enabled = false;
    }*/
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

        }
        else
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


//velocity = new Vector2(mouseDir.x * dashforce, mouseDir.y * dashforce);
//if (velocity.magnitude >= 1)
//{
//    velocity = Vector2.ClampMagnitude(velocity, ClampDashForce);

//}


//Debug.Log(velocity.magnitude);