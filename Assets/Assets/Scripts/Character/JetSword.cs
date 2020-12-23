using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetSword : Weapon
{
    public bool isSwordDashing;
   // public Vector2 dashVelocity;
    public bool canDash;

    public float MidDashCurrentTime;

    public float dashForce;

    public float dashTargetLengthTime;

    public float dashwaitTime;

    public bool dashRight;

    public float dashCooldown;
    RaycastHit2D des;
    public override void Init()
    {
        canDash = true;
        base.Init();
    }
    // Start is called before the first frame update
    public override void Attack()
    {
        if (Input.GetKeyDown(attack) && !isSwordDashing)
        {
            if (runningCooldown >= Cooldown)
            {
                weaponHandler.groundColCount = 0;


                // AudioManager.a_Instance.AlixAttackAudio();
                //  Debug.Log("attacking");
                StartCoroutine(SlashAnimation());
                //SwordAttackGizmo();
            }
            runningCooldown = 0;

        }
        if (Input.GetKeyDown(attack) && isSwordDashing) {

            if (runningCooldown >= Cooldown)
            {
                weaponHandler.groundColCount = 0;


                // AudioManager.a_Instance.AlixAttackAudio();
                //  Debug.Log("attacking");
                StartCoroutine(SlashDashAnimation());
                //SwordAttackGizmo();
            }
            runningCooldown = 0;

        }
        /*else if (Input.GetKeyDown(attack) && Input.GetKeyDown(mobilityAbility))
        { 
            if (runningCooldown >= Cooldown)
            {
                weaponHandler.groundColCount = 0;


                // AudioManager.a_Instance.AlixAttackAudio();
                //  Debug.Log("attacking");
                StartCoroutine(SlashDashAnimation());
                //SwordAttackGizmo();
            }
            runningCooldown = 0;
        }*/
            if ( runningCooldown < Cooldown)
         {
            runningCooldown += Time.deltaTime;
         }
    }
    public override void GetInput()
    {
        if (dashwaitTime <= dashCooldown)
        {
            dashwaitTime += Time.deltaTime;
        }
        else
        {
            canDash = true;
        }
        if (Input.GetKeyDown(mobilityAbility) && canDash)
        {

            //StartCoroutine(TrailTime());
            // dashVelocity.x = player.velocity.x;
            //dashVelocity.y = player.velocity.y;
            if (player.islookingright)
            {
                //  transform.rotation = Quaternion.Euler(0, 0, 0);
                 des = Physics2D.CircleCast(transform.position, 0.2f,Vector2.right,10f, player.groundLayerMask);
               // Debug.DrawRay(transform.position, Vector2.right *10);

            }
            else
            {

                // transform.rotation = Quaternion.Euler(0, 180, 0);
                 des = Physics2D.CircleCast(transform.position, 0.2f, Vector2.left, 10f, player.groundLayerMask);


            }
            player.rb.velocity = new Vector3(0, 0, 0);
            player.velocity = Vector2.zero;
            player.gravityScale = 0;
            dashwaitTime = 0f;
            canDash = false;
            isSwordDashing = true;
            AudioManager.a_Instance.AlyxJetSwordDashAudio();
            player.animator.SetTrigger("SwordDash");
          
            if (player.islookingright )
            {
                dashRight = true;
                //velocity.x = dashForce;
            }
            else
            {
                dashRight = false;
                //velocity.x = -dashForce;
            }
            if (isSwordDashing)
            {
                this.MobilityAbility();
            }
        }

    }
    public override void MobilityAbility()
    {
        
        if (isSwordDashing)
        {
            player.velocity.y = 0;
            player.rb.velocity = Vector2.zero;
           
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
            if (!player.TopWallCheck && !player.BottomWallCheck)
            {
                if (dashRight)
                {
                   // RaycastHit2D destination = Physics2D.Raycast(player.transform.position, Vector2.right, 100f, player.groundLayerMask);
                   if(transform.position.x <= des.point.x - 0.4f)
                    {
                        player.velocity.x += dashForce * Time.deltaTime;
                    }
                    else
                    {
                        player.velocity.x = 0;
                        MidDashCurrentTime = 0f;
                        player.gravityScale = 25;
                        isSwordDashing = false;
                    } 
                   // {
                   //     player.velocity.x = 0f;
                //        player.gravityScale = 25;
               //         isSwordDashing = false;
              //          MidDashCurrentTime = 0f;
               //     }
                  //  else
                 //   {
                       
                  //  }
                    //WallCheckDir = 1;
                    //rb.AddForce(new Vector2(dashForce*Time.deltaTime , 0),ForceMode2D.Force);
                }
                else
                {
                    //rb.AddForce(new Vector2(-dashForce*Time.deltaTime, 0),ForceMode2D.Force);
                    //  RaycastHit2D destination = Physics2D.Raycast(player.transform.position, Vector2.left, 100f, player.groundLayerMask);

                    //  if (player.transform.position.x <= destination.point.x)
                    //   {
                    //      player.velocity.x *= 0.15f;
                    //player.gravityScale = 25;
                    //      isSwordDashing = false;
                    //     MidDashCurrentTime = 0f;



                    if (transform.position.x >= des.point.x + 0.4f)
                    {
                        player.velocity.x -= dashForce * Time.deltaTime;
                    }else
                    {
                        player.velocity.x = 0;
                        MidDashCurrentTime = 0f;
                        player.gravityScale = 25;
                        isSwordDashing = false;
                    }




                    //WallCheckDir = -1;
                }
            }
            else
            {
                player.velocity.x = 0;
                MidDashCurrentTime = 0f;
                player.gravityScale = 25;
                isSwordDashing = false;

            }



            //Collider2D wallCheck = Physics2D.OverlapCircle(transform.position, dashWallCheckRange, groundLayerMask);
            MidDashCurrentTime += Time.deltaTime;
            if (MidDashCurrentTime >= dashTargetLengthTime )/*||transform.position.x >= checkForWallRight.point.x || transform.position.x <= checkForWallLeft.point.x*/
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
                player.velocity.x *= 0.15f;
                player.gravityScale = 25;
                MidDashCurrentTime = 0f;
                isSwordDashing = false;
            }

            //   Debug.Log("click");
        }
    

        //if (isShrink)
        //{
        //    StartCoroutine(ShrinkTime());
        //}

    }
    IEnumerator SlashAnimation()
    {

        player.animator.SetTrigger("JetSwordAttack");
        yield return new WaitForSeconds(0.3f);
        AudioManager.a_Instance.AlyxJetSwordAttackAudio();
    }
    IEnumerator SlashDashAnimation()
    {
        player.animator.SetTrigger("SwordDashAttack");
        yield return new WaitForSeconds(0.3f);
        AudioManager.a_Instance.AlyxJetSwordAttackAudio();
    }

}
