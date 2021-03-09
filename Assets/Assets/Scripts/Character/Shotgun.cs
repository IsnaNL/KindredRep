using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shotgun : Weapon
{
    public float bulletSpeed;
    public Vector2 ShotDir;
    public GameObject shotgunBullet;
    public float MobilityAbilityCoolDown;
    public float MobilityAbilityCoolDownCurrentTime;
    public bool canShotGunBlast;
    public Vector2 shotgunBlastForce;
    public bool IsShotgunKnockback;
    private bool blastTrigger;

    public Transform BarrelDown;
    public Transform BarrelSide;

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
        player.animator.SetTrigger("Shoot");
        GameObject bulletInstance = Instantiate(shotgunBullet, weaponCollider.position, Quaternion.identity);
        EffectsManager.instance.CreateEffect(Effects.dragun_attack, weaponCollider, false);

        if (bulletInstance != null)
        {
            bulletInstance.GetComponent<OnBulletCollision>().velocity = ShotDir * bulletSpeed;
        }
    }
    public override void GetInput()
    {
        blastTrigger = true;
        ShotDir = player.islookingright ? Vector2.right : Vector2.left;
        if (Input.GetKeyDown(mobilityAbility) && canShotGunBlast)
        {
            player.velocity.y *= 0.1f;
            MobilityAbilityCoolDownCurrentTime = 0f;
            canShotGunBlast = false;
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
            AudioManager.a_Instance.AlyxShotGunMobilityAudio();
            player.velocity += Vector2.up * shotgunBlastForce;
            EffectsManager.instance.CreateEffect(Effects.dragun_mobility, BarrelDown);
        }
    }
    
}
