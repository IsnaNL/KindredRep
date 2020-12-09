
using System.Collections;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float jumpforce;
    public float dashForce;
    public float ClampDashForce;
    public float targetWaitTime;


    Vector2 savedVelocity;
    internal Vector3 targetmousepos;
    Vector3 mousePos;
    Vector3 direction;
    private float currentWaitTime;
    //private bool islookingright;
    private bool ismoving;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GroundCheck groundCheck;
    private Animator animator;
    private BoxCollider2D BoxCollider2D;
    private Weapon weapon;
    private bool isJumping;
    private bool isDashing;
    //private int health;


    public void Init()
    {
        //health = 4;
        Debug.Log("inited");
        targetmousepos = Vector3.zero;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        groundCheck.Init();
        weapon = GetComponentInChildren<Weapon>();
        weapon.Init();
        //islookingright = true;
        isDashing = false;


    }

    private void Update()
    {
        
        ismoving = Input.GetButton("Horizontal");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (transform.position - mousePos);
        Debug.DrawRay(transform.position, mousePos, Color.red);
        if (Input.GetButtonDown("Jump") && groundCheck.IsGrounded)
        {
            Jump();
        }
        if (Input.GetButtonDown("Fire2") && groundCheck.IsGrounded)
        {

            savedVelocity = rb.velocity;
            if (savedVelocity == Vector2.zero)
            {
                savedVelocity = new Vector2(direction.normalized.x, direction.normalized.y);
                //Debug.Log("direction X: " + direction.x + " " + "direction Y: " + direction.y);
                //Debug.Log("direction Xnorm: " + direction.normalized.x + " " + "direction YXnorm: " + direction.normalized.y);




            }
            if (weapon.IsSwordWeapon)
            {
                isDashing = true;

            }

        }
        if (isDashing)
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime >= targetWaitTime)
            {
                isDashing = false;
                currentWaitTime = 0f;
                targetmousepos = Vector3.zero;
                rb.velocity = savedVelocity;
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {

            if (weapon.IsSwordWeapon)
            {
                weapon.IsSwordWeapon = false;
                weapon.IsShotgunWeapon = true;


            }
            else
            {


                weapon.IsShotgunWeapon = false;
                weapon.IsSwordWeapon = true;

            }

        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {


      
        
            MoveCharacter();
        


        if (isDashing)
        {
            MobilityAbility();
        }





    }

    void MoveCharacter()
    {
        float movementHorizontal = Input.GetAxis("Horizontal");


        if (ismoving)
        {
            
            if (groundCheck.IsGrounded && !isDashing)
            {
                animator.SetBool("IsMoving", true);
                Vector2 movementVector = new Vector2(movementHorizontal * speed, 0f);
                if (movementVector.x <= maxSpeed)
                {
                    rb.velocity += (movementVector);
                }
            }
           
            //Debug.Log(rb.velocity);

        }
        else if(!ismoving)
        {
            
            animator.SetBool("IsMoving", false);
            
            Vector2 movementVector = new Vector2(movementHorizontal * speed * 0.95f, 0f);
            if (movementVector.x > 0)
            {
                rb.velocity += (movementVector);
            }



        }

    }
   




    //float extraheight = 0.1f;
    //RaycastHit2D hitgroundinfo = Physics2D.BoxCast(BoxCollider2D.bounds.center, BoxCollider2D.bounds.size, 0f, Vector2.down, extraheight, groundLayerMask);
    //Color raycolorgizmo;


    //if (hitgroundinfo.collider != null)
    //{


    //    raycolorgizmo = Color.green;


    //    Debug.DrawRay(BoxCollider2D.bounds.center + new Vector3(BoxCollider2D.bounds.extents.x, 0), Vector2.down * (BoxCollider2D.bounds.extents.y + extraheight), raycolorgizmo);
    //    Debug.DrawRay(BoxCollider2D.bounds.center - new Vector3(BoxCollider2D.bounds.extents.x, 0), Vector2.down * (BoxCollider2D.bounds.extents.y + extraheight), raycolorgizmo);
    //    Debug.DrawRay(BoxCollider2D.bounds.center - new Vector3(BoxCollider2D.bounds.extents.x, BoxCollider2D.bounds.extents.y + extraheight), Vector2.right * (BoxCollider2D.bounds.extents.x + 0.25f), raycolorgizmo);



    //    return true;
    //}
    //else
    //{


    //    raycolorgizmo = Color.red;
    //    Debug.DrawRay(BoxCollider2D.bounds.center + new Vector3(BoxCollider2D.bounds.extents.x, 0), Vector2.down * (BoxCollider2D.bounds.extents.y + extraheight), raycolorgizmo);
    //    Debug.DrawRay(BoxCollider2D.bounds.center - new Vector3(BoxCollider2D.bounds.extents.x, 0), Vector2.down * (BoxCollider2D.bounds.extents.y + extraheight), raycolorgizmo);
    //    Debug.DrawRay(BoxCollider2D.bounds.center - new Vector3(BoxCollider2D.bounds.extents.x, BoxCollider2D.bounds.extents.y + extraheight), Vector2.right * (BoxCollider2D.bounds.extents.x + 0.25f), raycolorgizmo);

    //    return false;

    //}

   


    void MobilityAbility()
    {
        if (targetmousepos == Vector3.zero)
        {
            //targetmousepos = new Vector3(direction.x, direction.y, 0f);
            targetmousepos = new Vector3(mousePos.x, mousePos.y, 0f);

        }
        Debug.Log("indashing");
        if (weapon.IsSwordWeapon && isDashing)
        {

            rb.velocity = new Vector2(targetmousepos.x - transform.position.x, targetmousepos.y - transform.position.y) * dashForce;
            if (rb.velocity.magnitude >= 1f)
            {


                rb.velocity = Vector2.ClampMagnitude(rb.velocity, ClampDashForce);
                Debug.Log("rb.velocity: " + rb.velocity);
                Debug.Log("magnitude: " + rb.velocity.magnitude);
            }


            //Debug.Log(rb.velocity + "velocity");

            //rb.AddForce(direction * dashForce * Time.deltaTime, ForceMode2D.Impulse);
        }

    }
    void Jump()
    {

        if (groundCheck.IsGrounded)
        {
            Debug.Log("jumping");
            rb.AddForce(Vector2.up * jumpforce * Time.deltaTime, ForceMode2D.Impulse);
           
        }


    }
   

}
