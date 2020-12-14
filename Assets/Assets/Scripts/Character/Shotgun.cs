using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public float bulletSpeed;
    public Vector2 ShotDir;
    public GameObject shotgunBullet;
   // public ParticleSystem shotGunParticals;
    public float MobilityAbilityCoolDown;
    public ParticleSystem FireBoostFromShotGunParitcals;
    public float MobilityAbilityCoolDownCurrentTime;
    public bool canShotGunBlast;
    public Vector2 shotgunBlastForce;
    public bool IsShotgunKnockback;
  //  public Vector2 ShotgunBlastVelocity;
   // public float blastTargetWaitTime;
  //  public float blastCurrentWaitTime;
    // Start is called before the first frame update
    public override void Attack()
    {
        GetAimAngleForShotgun();
        // base.Attack();
        if (Input.GetKeyDown(attack))
        {
            if (runningCooldown >= Cooldown)
            {


                //shotGunParticals.transform.position = player.transform.position;

                ShotgunShot();

                // Instantiate(shotgunBullet, transform.position, transform.rotation);
                runningCooldown = 0;

            }
        }
        if(runningCooldown < Cooldown)
        {
            runningCooldown += Time.deltaTime;

        }
    }
    void ShotgunShot()
    {

        AudioManager.a_Instance.AlyxShotGunShotAudio();


        GameObject bulletInstance = Instantiate(shotgunBullet, weaponCollider.position, Quaternion.identity);
       
        if(bulletInstance != null)
        {
            bulletInstance.GetComponent<OnBulletCollision>().velocity = ShotDir * bulletSpeed;

        }



    }
    public override void GetInput()
    {
        if (Input.GetKeyDown(mobilityAbility) && canShotGunBlast)
        {
            //  rb.velocity = new Vector3(0, 0, 0);
            //saveMouseDir = mouseDirNormalized;
            //Debug.Log("ShotGUnBLAsT");
            //ShotgunBlastVelocity.x = velocity.x;
            //ShotgunBlastVelocity.y = velocity.y;
            FireBoostFromShotGunParitcals.transform.position = transform.position;
            FireBoostFromShotGunParitcals.Play();
            MobilityAbilityCoolDownCurrentTime = 0f;
            canShotGunBlast = false;
            AudioManager.a_Instance.AlyxShotGunMobilityAudio();
            IsShotgunKnockback = true;
        }

        if (IsShotgunKnockback)
        {
            this.MobilityAbility();
        }
        if (MobilityAbilityCoolDownCurrentTime <= MobilityAbilityCoolDown)
        {
            MobilityAbilityCoolDownCurrentTime += Time.deltaTime;
        }
        else
        {
            canShotGunBlast = true;
        }
    }
    public override void MobilityAbility()
    {
      

        if (IsShotgunKnockback)
        {
            // rb.gravityScale = 0.7f;
            // trail.enabled = false;

            //ShotgunBlastVelocity.y = transform.position.y;
         //   blastCurrentWaitTime += Time.deltaTime;
            //knockBackOverTime += Time.deltaTime;
           // {

                //Debug.Log(IsGrounded + "IsGrounded");
             //   blastCurrentWaitTime = 0f;
                

              // player.velocity = ShotgunBlastVelocity;




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
          //  }

            //velocity = ShotgunBlastVelocity;


            player.velocity = -ShotDir * shotgunBlastForce;
            IsShotgunKnockback = false;





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
    void GetAimAngleForShotgun()//on a scale of -1 to 1 divided by 5
    {

        /*
        mouseDeltaCounter += MouseYDelta;
        mouseDeltaCounter *= 0.99f;


        if (mouseDeltaCounter > DeltaCap)
        {
            animator.SetTrigger("AngleUp");
            mouseDeltaCounter = 0;
        }
        else if (mouseDeltaCounter < -DeltaCap)
        {
            animator.SetTrigger("AngleDown");

            mouseDeltaCounter = 0;
        }
        */



        if (player.moveInput == 0 && player.verInput == 0f)
        {
            if (player.islookingright)
            {
                ShotDir = Vector2.right;
                player.animator.SetTrigger("G3");
            }
            else
            {
                player.animator.SetTrigger("G3");

                ShotDir = Vector2.left;

            }
            if (player.isFalling)
            {
                player.animator.SetTrigger("F3");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J3");
            }
        }
        else if (player.moveInput == 0 && player.verInput == 1)
        {
            ShotDir = Vector2.up;
            player.animator.SetTrigger("G1");

            if (player.isFalling)
            {
                player.animator.SetTrigger("F1");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J1");

            }
        }
        else if (player.moveInput == 0 && player.verInput == -1)
        {
            ShotDir = Vector2.down;
            player.animator.SetTrigger("G5");

            if (player.isFalling)
            {
                player.animator.SetTrigger("F5");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J5");

            }
        }
        else if (player.moveInput == 1 && player.verInput == 0)
        {
            ShotDir = Vector2.right;
            player.animator.SetTrigger("G3");

            if (player.isFalling)
            {
                player.animator.SetTrigger("F3");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J3");

            }
        }
        else if (player.moveInput == 1 && player.verInput == 1)
        {
            ShotDir = new Vector2(0.5f, 0.5f);
            player.animator.SetTrigger("G2");

            if (player.isFalling)
            {
                player.animator.SetTrigger("F2");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J2");

            }
        }
        else if (player.moveInput == 1 && player.verInput == -1)
        {
            ShotDir = new Vector2(0.5f, -0.5f);
            player.animator.SetTrigger("G4");

            if (player.isFalling)
            {
                player.animator.SetTrigger("F4");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J4");

            }

        }
        else if (player.moveInput == -1 && player.verInput == 0)
        {
            ShotDir = Vector2.left;
            player.animator.SetTrigger("G3");

            if (player.isFalling)
            {
                player.animator.SetTrigger("F3");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J3");

            }
        }
        else if (player.moveInput == -1 && player.verInput == 1)
        {

            ShotDir = new Vector2(-0.5f, 0.5f);
            player.animator.SetTrigger("G2");
            if (player.isFalling)
            {
                player.animator.SetTrigger("F2");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J2");

            }
        }
        else if (player.moveInput == -1 && player.verInput == -1)
        {
            ShotDir = Vector2.down;
            player.animator.SetTrigger("G5");

            if (player.isFalling)
            {
                player.animator.SetTrigger("F5");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J5");

            }

        }

        //if (MouseYDelta >= 0.6f)
        //{
        //    ShotgunShotDir = Vector2.up;
        //}
        //if (MouseYDelta >= 0.2f && MouseYDelta <= 0.6)
        //{
        //    if (islookingright)
        //    {
        //        ShotgunShotDir = new Vector2(1, 0.7f);
        //    }
        //    else
        //    {
        //        ShotgunShotDir = new Vector2(-1, 0.7f);
        //    }
        //}
        //if (MouseYDelta >= -0.2f && MouseYDelta <= 0.2)
        //{
        //    if (islookingright)
        //    {
        //        ShotgunShotDir = Vector2.right;
        //    }
        //    else
        //    {
        //        ShotgunShotDir = Vector2.left;
        //    }
        //}
        //if (MouseYDelta >= -0.6f && MouseYDelta <= -0.2)
        //{
        //    if (islookingright)
        //    {
        //        ShotgunShotDir = new Vector2(1, -0.7f);
        //    }
        //    else
        //    {
        //        ShotgunShotDir = new Vector2(-1, -0.7f);
        //    }
        //}
        //if (MouseYDelta <= -0.6f)
        //{
        //    ShotgunShotDir = Vector2.down;
        //}





    }
}
