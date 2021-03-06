using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shotgun : Weapon
{
    public float bulletSpeed;
    public Vector2 ShotDir;
    public GameObject shotgunBullet;
    public float MobilityAbilityCoolDown;
    public ParticleSystem MobilityEffect;
    public ParticleSystem AttackEffect;
    public float MobilityAbilityCoolDownCurrentTime;
    public bool canShotGunBlast;
    public Vector2 shotgunBlastForce;
    public bool IsShotgunKnockback;
    private bool blastTrigger;

    private SpriteRenderer GFX;

    public override void Init()
    {
        base.Init();
        GFX = GameManager.instace.Player.GetComponentInChildren<SpriteRenderer>();
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
        float dir = player.islookingright ? 0f : 180f;
        AudioManager.a_Instance.AlyxShotGunShotAudio();
        GameObject bulletInstance = Instantiate(shotgunBullet, weaponCollider.position, Quaternion.identity);
        player.animator.SetTrigger("Shoot");

        PlayParticle(AttackEffect);
        AttackEffect.transform.rotation = Quaternion.Euler(Vector3.forward * dir);
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
            PlayParticle(MobilityEffect);
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
    private void PlayParticle(ParticleSystem particle)
    {
        particle.transform.position = transform.position;
        particle.transform.parent = null;
        particle.Play();
    }
    protected override void TakePlayerControl(float time)
    {
        base.TakePlayerControl(time);
    }
}
