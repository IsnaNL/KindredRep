using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public float bulletSpeed;
    public Vector2 ShotgunShotDir;
    public GameObject shotgunBullet;
    public ParticleSystem shotGunParticals;
    
    
    // Start is called before the first frame update
    public override void Attack()
    {

       // base.Attack();
        if (Input.GetButtonDown("Fire1"))
        {
            if (runningCooldown <= 0)
            {


                //shotGunParticals.transform.position = player.transform.position;

                ShotgunShot();

                // Instantiate(shotgunBullet, transform.position, transform.rotation);
                runningCooldown = Cooldown;

            }
        }
        else
        {
            runningCooldown -= Time.deltaTime;

        }
    }
    void ShotgunShot()
    {

        AudioManager.a_Instance.AlyxShotGunShotAudio();


        GameObject bulletInstance = Instantiate(shotgunBullet, weaponCollider.position, Quaternion.identity);
        if (player.islookingright)
        {
            bulletInstance.GetComponent<OnBulletCollision>().velocity = ShotgunShotDir * bulletSpeed;

        }
        else
        {
            bulletInstance.GetComponent<OnBulletCollision>().velocity = new Vector2(-ShotgunShotDir.x, ShotgunShotDir.y) * bulletSpeed;

        }
        
    }
}
