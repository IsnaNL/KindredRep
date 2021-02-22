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
    private bool blastTrigger;

    public override void Init()
    {
        base.Init();
    }
    private void OnEnable()
    {
        MobilityAbilityCoolDownCurrentTime = MobilityAbilityCoolDown;
    }
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
        player.animator.SetTrigger("Shoot");

        if (bulletInstance != null)
        {
            bulletInstance.GetComponent<OnBulletCollision>().velocity = ShotDir * bulletSpeed;
        }
    }
    public override void GetInput()
    {
        GetAimAngleForShotgun();
        if (Input.GetKeyDown(mobilityAbility) && canShotGunBlast)
        {
            player.velocity.y *= 0.1f;
            FireBoostFromShotGunParitcals.transform.position = transform.position;
            FireBoostFromShotGunParitcals.transform.parent = null;
            FireBoostFromShotGunParitcals.Play();
            MobilityAbilityCoolDownCurrentTime = 0f;
            canShotGunBlast = false;
            AudioManager.a_Instance.AlyxShotGunMobilityAudio();
            IsShotgunKnockback = true;
            if (IsShotgunKnockback)
            {
                this.MobilityAbility();
             
                IsShotgunKnockback = false;
            }
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
        if (IsShotgunKnockback && blastTrigger)
        {
            player.velocity += Vector2.up * shotgunBlastForce;  
        }

    }
    void GetAimAngleForShotgun()
    {
        blastTrigger = true;
        ShotDir = player.islookingright ? Vector2.right : Vector2.left;
        
    }
  
}
