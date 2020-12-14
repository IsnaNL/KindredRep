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
    private Vector2 mouseDirNormalized;


    private Vector2 dashVelocity;
    private Vector2 ShotgunBlastVelocity;
    private Vector2 clawVelocity;
    private bool isSwordDashing;
    private bool IsShotgunKnockback;
    private float dashCurrentWaitTime;
    private float blastCurrentWaitTime;
    private float shotGunBlastWaitTime;
    private float dashwaitTime;
    private bool canDash;
    private bool dashRight;
    private bool canShotGunBlast;
    private Vector2 saveMouseDir;
    private int groundLayer;
    private Vector2 mousePos;
    private TrailRenderer trail;
    //private int enemyLayer;
    private Vector2 currentScale;
    public bool isJumping;
    public float moveInput;
    private RaycastHit2D BottomWallCheck;
    private int coinLayer;
    private float savedGravityScale;
    private RaycastHit2D TopWallCheck;
    private SwapWeaponAnimatorController animationControllerSwapper;
    private float mouseDeltaCounter;
    public float MouseYDelta;
    public float shrinkTime;
    public float JumpXBoost;
    public bool isPickaxeClawing;
    public bool isPickaxeClawed;
    public float maxMagnitude;
    public Vector2 clawJump;
    public float ClawRange;
    public float shotGunBlastCoolDown;
    public float dashCooldown;
    public float blastTargetWaitTime;
    public float dashTargetWaitTime;
    public bool islookingright;
    public float dashForce;
    public Vector2 velocity;
    public ParticleSystem FireBoostFromShotGunParitcals;
    public Inventory inventory;
    public Vector2 shrinkSize;
    public LayerMask groundLayerMask;
    public LayerMask playerLayerMask;
    public bool IsGrounded;
    public Vector2 hitKnockBack;
    public Vector2 shotgunBlastForce;
    public Vector2 jumpAcceleration;
    public float ceilingCheckDis;
    public float dashWallCheckRange;
    public float gravityScale;
    public int groundTriggerCount;
    public Rigidbody2D rb;
    public float DeltaCap;
    public float verInput;

    public bool isFalling { get; private set; }

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


    public override void Start()
    {
        base.Start();

    }
    public void Init()
    {
        Debug.Log("characterinit");
        clawVelocity = Vector2.zero;
        isPickaxeClawing = false;
        groundLayer = 8;
        //enemyLayer = 9;
        coinLayer = 14;
        canDash = true;
        IsGrounded = false;
        animationControllerSwapper = GetComponentInChildren<SwapWeaponAnimatorController>();
        trail = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        islookingright = true;
        currentScale = new Vector2(transform.localScale.x, transform.localScale.y);
        savedGravityScale = gravityScale;
        inventory = GetComponentInChildren<Inventory>();
        inventory.Init();
        //weapon.Invoke("FakeInit",0);
        // weapon.Init();
        animationControllerSwapper.Init();

        //groundcheck = GetComponentInChildren<GroundCheck>();
        //boxCollider = GetComponent<BoxCollider2D>();
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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirNormalized = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;
        MouseYDelta =  Input.GetAxis("Mouse Y");
        Debug.DrawRay(transform.position, mouseDirNormalized, Color.red);
        Debug.DrawRay(transform.position, -mouseDirNormalized, Color.blue);
        float acceleration = IsGrounded ? walkAcceleration : airAcceleration;
        float deceleration = IsGrounded ? groundDeceleration : 0;
        moveInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");
      
        //Debug.Log(mouseDirNormalized);
        // velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        HorizontalMovement(acceleration, deceleration);
        HitImpact();
        GetInputJumpMethod();
        GetInputSetCoditionsForSwordDash();
        GetInputSetConditionsForShotgunBlast();
        GetInputSetConditionsForPickaxeClawing();
        CheckFlip();
       
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
        if (dashwaitTime <= dashCooldown)
        {
            dashwaitTime += Time.deltaTime;
        }
        else
        {
            canDash = true;
        }
        if (shotGunBlastWaitTime <= shotGunBlastCoolDown)
        {
            shotGunBlastWaitTime += Time.deltaTime;
        }
        else
        {
            canShotGunBlast = true;
        }
        if (isSwordDashing)
        {
            SwordMovementAbility();
        }
        if (IsShotgunKnockback)
        {
            ShotgunMovementAbility();
        }
        if (isPickaxeClawing)
        {
            PickaxeMovementAbility();
        }
        //if (isShrink)
        //{
        //    SwordMovementAbility();
        //}


        if (inventory.weaponCheck == 2)
        {
            isPickaxeClawed = false;
            isPickaxeClawing = false;
            //rb.isKinematic = false;
        }
        if (isPickaxeClawed)
        {
            IsGrounded = false;
        }

        if (!IsGrounded && !isPickaxeClawed)//gravity
        {
            if (velocity.y >= -10)
            {
                velocity.y -= gravityScale * Time.deltaTime;

            }
           if (velocity.y < 0)
            {
                isFalling = true;
                animator.SetBool("IsFalling", true);
                animator.SetBool("Idle", false);
                
            }

        }
       

        //if (velocity.magnitude >= maxMagnitude)
        //{
        //    velocity = Vector2.ClampMagnitude(velocity, maxMagnitude);
        //}
    }
    void FixedUpdate()
    {
        ceilingCheck();
        WallCol();
        if (isSwordDashing && !IsShotgunKnockback && !isPickaxeClawing && !TopWallCheck && !BottomWallCheck)
        {
            transform.Translate(dashVelocity * Time.fixedDeltaTime);
        }
        else if (IsShotgunKnockback && !isSwordDashing && !isPickaxeClawing)
        {
            transform.Translate(ShotgunBlastVelocity * Time.fixedDeltaTime);
        }
        else if (isPickaxeClawed && !isSwordDashing && !IsShotgunKnockback)
        {
            transform.Translate(clawVelocity * Time.fixedDeltaTime);
        }
        else
        {
            transform.Translate(velocity * Time.fixedDeltaTime);
        }
    }
    private void HorizontalMovement(float acceleration, float deceleration)
    {
        if (moveInput != 0)
        {
            animator.SetBool("Idle", false);

            if (IsGrounded)
            {
                animator.SetBool("IsRunning", true);

            }
            else
            {
                animator.SetBool("IsRunning", false);
            }

            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
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
           velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
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
            animator.SetBool("Idle",true);
        }
    }
    private void GetInputSetConditionsForPickaxeClawing()
    {
        if (Input.GetButtonDown("Fire2") && inventory.weaponCheck == 2)
        {
            //isClawing = true;
            if (isPickaxeClawing)
            {
                isPickaxeClawing = false;
            }
            else
            {
                isPickaxeClawing = true;
            }
            if (isPickaxeClawed)
            {
                isPickaxeClawed = false;
            }
        }
    }
    private void GetInputJumpMethod()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded)
            {

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

                StartCoroutine("JumpCoroutine");
                StartCoroutine(TrailTime());
                //AudioManager.a_Instance.AlixJumpAudio();

                //LeanTween.move(gameObject, new Vector3(velocity.x, jumpHeight * Time.deltaTime), 0.8f );
                //velocity.y = Mathf.Sqrt(Time.deltaTime * jumpHeight);
                //else
                //{
                //    velocity.x -= 1;
                //}
                //animator.SetBool("IsMoving", false);
            }
            if (isPickaxeClawed)
            {

                isPickaxeClawed = false;
                //rb.isKinematic = false;
                //animator.SetBool("IsMoving", false);
                StartCoroutine(TrailTime());
                if (islookingright)
                {
                    velocity.y += clawJump.y;
                    velocity.x += clawJump.x;
                }
                else
                {
                    velocity.y += clawJump.y;
                    velocity.x += -clawJump.x;
                }
            }



        }
    }

    private void GetInputSetConditionsForShotgunBlast()
    {
        if (Input.GetButtonDown("Fire2") && canShotGunBlast && inventory.weaponCheck == 1)
        {
            //  rb.velocity = new Vector3(0, 0, 0);
            saveMouseDir = mouseDirNormalized;
            //Debug.Log("ShotGUnBLAsT");
            //ShotgunBlastVelocity.x = velocity.x;
            //ShotgunBlastVelocity.y = velocity.y;
            FireBoostFromShotGunParitcals.transform.position = transform.position;
            FireBoostFromShotGunParitcals.Play();
            shotGunBlastWaitTime = 0f;
            canShotGunBlast = false;
            AudioManager.a_Instance.AlyxShotGunMobilityAudio();
            IsShotgunKnockback = true;
        }
    }
    private void GetInputSetCoditionsForSwordDash()
    {
        if (Input.GetButtonDown("Fire2") && canDash && inventory.weaponCheck == 0)
        {
            //rb.gravityScale = -1.5f;
            StartCoroutine(TrailTime());
            dashVelocity.x = velocity.x;
            dashVelocity.y = velocity.y;
            //dashVelocity.y = 0f;
            rb.velocity = new Vector3(0, 0, 0);

            //savedMousePos = mousePos;
            dashwaitTime = 0f;
            canDash = false;
            isSwordDashing = true;
            AudioManager.a_Instance.AlyxJetSwordDashAudio();
            animator.SetTrigger("SwordDash");
            //savedvelocity = rb.velocity;
            if (islookingright/*savedMousePos.x >= transform.position.x*/ )
            {
                dashRight = true;
                //velocity.x = dashForce;
            }
            else
            {
                dashRight = false;
                //velocity.x = -dashForce;
            }
           
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

        if (isPickaxeClawed)
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
    void Flip()
    {

        if (islookingright)
        {
            //  transform.rotation = Quaternion.Euler(0, 0, 0);
            BottomWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.right, 0.5f, groundLayerMask);
            TopWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.right, 0.5f, groundLayerMask);
            transform.localScale = new Vector3(currentScale.x, currentScale.y, 1);
        }
        else
        {

            // transform.rotation = Quaternion.Euler(0, 180, 0);
            BottomWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.left, 0.5f, groundLayerMask);
            TopWallCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Vector2.left, 0.5f, groundLayerMask);
            transform.localScale = new Vector3(-currentScale.x, currentScale.y, 1);
        }
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
    public void SwordMovementAbility()
    {

        if (isSwordDashing)
        {
          /*  if (islookingright)
            {
                RaycastHit2D destination = Physics2D.Raycast(transform.position, Vector2.right, groundLayerMask);

            }
            else
            {
                RaycastHit2D destination = Physics2D.Raycast(transform.position, Vector2.left, groundLayerMask);

            }

            //RaycastHit2D checkForWallRight = Physics2D.Raycast(transform.position, Vector2.right,10, groundLayerMask);
            //RaycastHit2D checkForWallLeft = Physics2D.Raycast(transform.position, Vector2.left,10, groundLayerMask);
            //rb.gravityScale = 0;
            //Debug.Log("velocity :" + dashVelocity);
            //dashVelocity.y = currentVelocityY;
            //velocity.y = currentVelocityY;
            /* dashVelocity.y = transform.position.y + 1f;*/ /// To change or 0f or freeze Rb.y  
                //Debug.Log(transform.position.x);
            //int WallCheckDir;
            if (!TopWallCheck && !BottomWallCheck)
            {
                if (dashRight)
                {
                    RaycastHit2D destination = Physics2D.Raycast(transform.position, Vector2.right,100f, groundLayerMask);

                    if(transform.position.x >= destination.point.x)
                    {
                        velocity.x = dashVelocity.x * 0.15f;

                        isSwordDashing = false;
                        dashCurrentWaitTime = 0f;
                    }
                    else
                    {
                        dashVelocity.x += dashForce * Time.deltaTime;

                    }
                    //WallCheckDir = 1;
                    //rb.AddForce(new Vector2(dashForce*Time.deltaTime , 0),ForceMode2D.Force);
                }
                else
                {
                    //rb.AddForce(new Vector2(-dashForce*Time.deltaTime, 0),ForceMode2D.Force);
                    RaycastHit2D destination = Physics2D.Raycast(transform.position, Vector2.left, 100f, groundLayerMask);

                    if (transform.position.x <= destination.point.x)
                    {
                        velocity.x = dashVelocity.x * 0.15f;

                        isSwordDashing = false;
                        dashCurrentWaitTime = 0f;
                    }
                    else
                    {
                        dashVelocity.x += -dashForce * Time.deltaTime;

                    }
                    //WallCheckDir = -1;
                }
            }
            else
            {
                velocity.x = 0;
                isSwordDashing = false;
                dashCurrentWaitTime = 0f;

            }



            //Collider2D wallCheck = Physics2D.OverlapCircle(transform.position, dashWallCheckRange, groundLayerMask);
            dashCurrentWaitTime += Time.deltaTime;
            if (dashCurrentWaitTime >= dashTargetWaitTime || TopWallCheck || BottomWallCheck)/*||transform.position.x >= checkForWallRight.point.x || transform.position.x <= checkForWallLeft.point.x*/
            {
                //Debug.Log(wallCheck.gameObject.name);
                Debug.Log("DashTimeDone");
                //rb.velocity = savedvelocity;


                //  Debug.Log("clicked");

                //Finish dash added velocity
                //if (dashRight)
                //{
                //    velocity.x = 2f;
                //}
                //else
                //{
                //    velocity.x = -2f;
                //}
                //rb.gravityScale = 1.5f;
                //if (IsGrounded)
                //{
                //    rb.gravityScale = 1;
                //}
                //rb.velocity = Vector2.zero;
                velocity.x = dashVelocity.x * 0.15f;
                dashCurrentWaitTime = 0f;
                isSwordDashing = false;
            }

            //   Debug.Log("click");
        }
       
        //if (isShrink)
        //{
        //    StartCoroutine(ShrinkTime());
        //}

    }
    private void PickaxeMovementAbility()
    {
        if (isPickaxeClawing)
        {
            isPickaxeClawing = false;

            Collider2D[] wallToHit = Physics2D.OverlapCircleAll(inventory.transform.position, ClawRange, groundLayerMask);
            for (int i = 0; i < wallToHit.Length; i++)
            {
                //    Debug.Log(wallToHit[i].gameObject.name);
                //    Debug.Log(wallToHit);
                if (wallToHit[i].gameObject.layer == groundLayer)
                {
                    //rb.gravityScale = 0f;

                    isPickaxeClawed = true;
                    Debug.Log(wallToHit[i].gameObject.layer);
                }
                if (isPickaxeClawed)
                {
                    islookingright = !islookingright;
                    velocity = Vector2.zero;
                    rb.velocity = Vector2.zero;
                    //rb.isKinematic = true;
                }

            }

        }
    }
    private void ShotgunMovementAbility()
    {
        if (IsShotgunKnockback)
        {
            // rb.gravityScale = 0.7f;
            // trail.enabled = false;
        
            //ShotgunBlastVelocity.y = transform.position.y;
            blastCurrentWaitTime += Time.deltaTime;
            //knockBackOverTime += Time.deltaTime;
            if (blastCurrentWaitTime >= blastTargetWaitTime)
            {

                //Debug.Log(IsGrounded + "IsGrounded");
                blastCurrentWaitTime = 0f;
                IsShotgunKnockback = false;

                velocity = ShotgunBlastVelocity;

                
                
             
                // rb.gravityScale = 1.5f;
                //if (ShotgunBlastVelocity.x >= 0)
                //{
                //    velocity.x += afterBlastVelocityX;
                //    Debug.Log(velocity.x);
                //}
                //else
                //{
                //    velocity.x += -afterBlastVelocityX;
                //    Debug.Log(velocity.x);
                //}
                //knockBackOverTime >= knockBackOverTargetTime 
                /*IsGrounded &&*/
                /* knockBackOverTime >= knockBackOverTargetTime ||*/
                //knockBackOverTime = 0f;
            }

            //velocity = ShotgunBlastVelocity;
            if (islookingright)
            {
                ShotgunBlastVelocity.x = -inventory.shotgun.ShotDir.x * shotgunBlastForce.x;
                ShotgunBlastVelocity.y = -inventory.shotgun.ShotDir.y * shotgunBlastForce.y;
            }
            else
            {
                ShotgunBlastVelocity.x = inventory.shotgun.ShotDir.x * shotgunBlastForce.x;
                ShotgunBlastVelocity.y = -inventory.shotgun.ShotDir.y * shotgunBlastForce.y;
            }
           
           // animator.SetBool("IsFalling", true);
            //if (IsGrounded)
            //{
            //    ShotgunBlastVelocity.x = -mouseDir.x * ShotBlastForceX;
            //    ShotgunBlastVelocity.y = -mouseDir.y * ShotBlastForceY;
            //}
            //else
            //{
            //    ShotgunBlastVelocity.x = -mouseDir.x * ShotBlastForceX;
            //    ShotgunBlastVelocity.y = -mouseDir.y * ShotBlastForceY;
            //}
            //if (IsGrounded)
            // {
            //   rb.AddForce(ShotgunBlastVelocity * -mouseDir * Time.fixedDeltaTime, ForceMode2D.Impulse);
            // }
            // else
            // {
           
            //     //rb.AddForce(ShotBlastAirForce * -mouseDir * Time.fixedDeltaTime, ForceMode2D.Impulse);
            // }
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
    IEnumerator TrailTime()
    {
        trail.enabled = true;
        yield return new WaitForSeconds(1f);
        trail.enabled = false;
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
        float currentjumpAccelerationY = jumpAcceleration.y;
        float currentjumpAccelerationX = jumpAcceleration.x;
        Vector2 currentvelocity = velocity;
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
            velocity += new Vector2(jumpAcceleration.x * moveInput * Time.deltaTime * 0.99f, jumpAcceleration.y * Time.deltaTime * 0.99f);

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
    void ceilingCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.up, ceilingCheckDis, groundLayerMask);
        if (hit)
        {

            StopCoroutine("JumpCoroutine");
            isFalling = true;
            IsShotgunKnockback = false;
            if (velocity.y >= 0)
            {
                velocity.y = 0;
            }

            //velocity.x *= 0.9f;
            Debug.Log("Ceilinghit");
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