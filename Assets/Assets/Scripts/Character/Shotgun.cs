using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public float bulletSpeed;
    public Vector2 ShotDir;
    public GameObject shotgunBullet;
    public ParticleSystem shotGunParticals;

    
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
