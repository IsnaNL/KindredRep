using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public float bulletSpeed;
    public Vector2 ShotDir;
    public GameObject shotgunBullet;
    public float MobilityAbilityCoolDown;
    public ParticleSystem FireBoostFromShotGunParitcals;
    public float MobilityAbilityCoolDownCurrentTime;
    public bool canShotGunBlast;
    public Vector2 shotgunBlastForce;
    public bool IsShotgunKnockback;
    public override void Attack()
    {
       
        // base.Attack();
        if (Input.GetKeyDown(attack))
        {
            if (runningCooldown >= Cooldown)
            {

                ShotgunShot();
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
        GetAimAngleForShotgun();
        if (Input.GetKeyDown(mobilityAbility) && canShotGunBlast)
        {
            FireBoostFromShotGunParitcals.transform.position = transform.position;
            FireBoostFromShotGunParitcals.Play();
            MobilityAbilityCoolDownCurrentTime = 0f;
            canShotGunBlast = false;
            AudioManager.a_Instance.AlyxShotGunMobilityAudio();
            IsShotgunKnockback = true;
        }

        if (IsShotgunKnockback )
        {
            this.MobilityAbility();
            IsShotgunKnockback = false;
            
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

          //  RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, player.groundLayerMask);
          //  Debug.DrawRay(transform.position, Vector2.left * 0.5f);
            {
              //  if (hit)
              //  {
               //     player.velocity = 0.75f;
               // }
            }
            player.velocity += -ShotDir * shotgunBlastForce;
            IsShotgunKnockback = false;
           

        }

    }
    void GetAimAngleForShotgun()//on a scale of -1 to 1 divided by 5
    {

        if (player.moveInput == 0 && player.verInput == 0f && !player.isFalling && !player.isJumping)
        {
           
            if (player.islookingright && player.IsGrounded)
            {
                ShotDir = Vector2.right;
                player.animator.SetTrigger("G3");
               
            }
            else
            {
                player.animator.SetTrigger("G3");
                

                ShotDir = Vector2.left;

            }
        }else if (player.moveInput == 0 && player.verInput == 0f && player.isFalling && !player.isJumping)
        {
           
            player.animator.SetTrigger("F3");
            if (player.islookingright)
            {
                ShotDir = Vector2.right;
                

            }
            else
            {
                


                ShotDir = Vector2.left;

            }
        }
        else if (player.moveInput == 0 && player.verInput == 0f && !player.isFalling && player.isJumping)
        {
           
            player.animator.SetTrigger("J3");
            if (player.islookingright)
            {
                ShotDir = Vector2.right;


            }
            else
            {



                ShotDir = Vector2.left;

            }
        }
        else if (player.moveInput == 0 && player.verInput == 1)
        {
            ShotDir = Vector2.up;


            if (player.isFalling)
            {
               
                player.animator.SetTrigger("F1");
               

            }
            if (player.isJumping)
            {
               
                player.animator.SetTrigger("J1");
                

            }
            if (player.IsGrounded)
            {
               
                player.animator.SetTrigger("G1");
               

            }
        }
        else if (player.moveInput == 0 && player.verInput == -1)
        {
            ShotDir = Vector2.down;
           
            if (player.isFalling)
            {
               
                player.animator.SetTrigger("F5");

            }
            if (player.isJumping)
            {
              

                player.animator.SetTrigger("J5");

            }
            if (player.IsGrounded) {
               
                player.animator.SetTrigger("G5");

            }
        }
        else if (player.moveInput == 1 && player.verInput == 0)
        {
            ShotDir = Vector2.right;
            



            if (player.isFalling)
            {
                player.animator.SetTrigger("F3");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J3");

            }
            if (player.IsGrounded)
            {
                player.animator.SetTrigger("G3");
            }
        }
        else if (player.moveInput == 1 && player.verInput == 1)
        {
            ShotDir = Vector2.up;

            if (player.isFalling)
            {
                player.animator.SetTrigger("F1");
                
            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J1");
                
            }
            if (player.IsGrounded)
            {

               
                player.animator.SetTrigger("G1"); 
            }
        }
        else if (player.moveInput == 1 && player.verInput == -1)
        {
            ShotDir = Vector2.down;
            if (player.isFalling)
            {
                player.animator.SetTrigger("F5");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J5");

            }
            if (player.IsGrounded)
            {
                player.animator.SetTrigger("G5");
            }
              

        }
        else if (player.moveInput == -1 && player.verInput == 0)
        {
            ShotDir = Vector2.left;

            if (player.isFalling)
            {
                player.animator.SetTrigger("F3");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J3");

            }
            if (player.IsGrounded)
            {
                player.animator.SetTrigger("G3");
            }
        }
        else if (player.moveInput == -1 && player.verInput == 1)
        {

           
            if (player.IsGrounded)
            {
                ShotDir = Vector2.zero;
                player.animator.SetTrigger("G1");
            }
         
            if (player.isFalling)
            {
                ShotDir = Vector2.up;
                player.animator.SetTrigger("F1");

            }
            if (player.isJumping)
            {
                ShotDir = Vector2.up;
                player.animator.SetTrigger("J1");

            }
        }
        else if (player.moveInput == -1 && player.verInput == -1)
        {
            ShotDir = Vector2.down;

            if (player.isFalling)
            {
                player.animator.SetTrigger("F5");

            }
            if (player.isJumping)
            {
                player.animator.SetTrigger("J5");

            }
            if (player.IsGrounded)
            {
                player.animator.SetTrigger("G5");
            }
        }

    }
}
