﻿using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool IsSwordWeapon;
    public bool IsShotgunWeapon;
    public bool IsPickaxeWeapon;
    public GameObject shotgunBullet;
    public LayerMask enemylayer;
    public SunFlower sunflowerRef;
    public int enemyLayerValue;
    public int bossLayerValue;
    private float timeBtwAttacks;
    public float startTimeBtwAttacks;
    public float AttackRange;
    public SpriteRenderer sr;
    public CharacterController2D player;
    public ParticleSystem shotGunParticals;
    public bool PickaxeFirstAttack;
    public bool PickaxeSecondAttack;
    public float secondAttackBoost;
    public float currentTimeTillSecondAttack;
    public float currentTimeTillThirdAttack;
    public float SecondAttackCooldown;
    public float ThirdAttackCooldown;
    public Transform weaponCollider;
    public float pickaxeDashTiming;
    public int TrapsLayerValue;
    public float bulletSpeed;
    public Vector2 ShotgunShotDir;


    //public bool IsFoilWeapon;
    //public bool foilFirstAttack;
    //public bool foilSecondAttack;
    //public bool foilThirdAttack;
    //private bool foilSecondAttackTrigger;
    //private bool foilThirdAttackTrigger;
    //private bool foilFirstAttackTrigger;

    //private Vector2 foilAttackCapsuleDir;
    //public bool isClawedOnWall;
    //private Vector2 foilHitPointForGizmos;
    //public float foilAttackCapsuleDis;

    public void Init()
    {

        PickaxeFirstAttack = false;
        Debug.Log("weaponinited");
      

    }
    void Update()
    {
      SwapWeapon();
      Attack();





    }

    

    void Attack()
    {
        if (IsSwordWeapon)
        {
            
            if (Input.GetButtonDown("Fire1"))
            {
                if (timeBtwAttacks <= 0)
                {


                   // AudioManager.a_Instance.AlixAttackAudio();
                    //  Debug.Log("attacking");
                    SlashAnimation();
                    //SwordAttackGizmo();
                }
                timeBtwAttacks = startTimeBtwAttacks;
                
            }
            else
            {
                timeBtwAttacks -= Time.deltaTime;
            }
        }
        if (IsShotgunWeapon)
        {
           //aimcheck

            if (Input.GetButtonDown("Fire1"))
            { 
                if (timeBtwAttacks <= 0)
                {


                    //shotGunParticals.transform.position = player.transform.position;

                    ShotgunShot();

                    // Instantiate(shotgunBullet, transform.position, transform.rotation);
                    timeBtwAttacks = startTimeBtwAttacks;

                }
            }
            else
            {
                timeBtwAttacks -= Time.deltaTime;

            }
        }
        if (IsPickaxeWeapon)
        {
           
            if (Input.GetButtonDown("Fire1"))
            {
                if (PickaxeFirstAttack)
                {
                currentTimeTillSecondAttack += Time.deltaTime;
                if (currentTimeTillSecondAttack >= SecondAttackCooldown)
                {
                    PickaxeSecondAttack = false;
                    PickaxeFirstAttack = false;
                    currentTimeTillSecondAttack = 0;
                }
                else
                {

                    PickaxeSecondAttack = true;
                }
            }

            if (PickaxeSecondAttack)
            {
                transform.localPosition = new Vector3(0.5f, 0f, 0f);
                AttackRange = 1f;
                StartCoroutine(PickaxeAttackTiming());
                currentTimeTillSecondAttack = SecondAttackCooldown;
                }
                if (timeBtwAttacks <= 0)
                {




                    if (!PickaxeFirstAttack)
                    {
                        Debug.Log("firstattack");
                        //ClawAttackGizmo();
                        PickaxeFirstAttack = true;
                    }

                }

                    timeBtwAttacks = startTimeBtwAttacks;
                

            }
            else
            {
                timeBtwAttacks -= Time.deltaTime;

            }
           
            
            //    if (clawSecondAttack)
            //    {
            //        //if (player.islookingright && player.IsGrounded)
            //        //{
            //        //    player.velocity.x = secondAttackBoost;

            //        //}
            //        //else if (!player.islookingright && player.IsGrounded)
            //        //{
            //        //    player.velocity.x = -secondAttackBoost;


            //        //}
                  

            //        //Debug.Log("secondattack");
            //        //Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(transform.position, AttackRange, enemylayer);
            //        //for (int i = 0; i < enemiesToHit.Length; i++)
            //        //{
            //        //    if (enemiesToHit[i].gameObject.layer == enemyLayerValue)
            //        //    {
            //        //        Enemy enemyInstance = enemiesToHit[i].GetComponent<Enemy>();
            //        //        //enemyInstance.TakeDamage(damage);
            //        //        //enemyInstance.TakeDamage();

            //        //    }
            //        //    if (enemiesToHit[i].gameObject.layer == bossLayerValue)
            //        //    {
            //        //        //  Debug.Log("hit");
            //        //        //   boss = enemiesToHit[i].GetComponent<SunFlower>();
            //        //      //  sunflowerRef.TakeDamage(damage);
            //        //    }
            //        //    if (enemiesToHit[i].gameObject.layer == TrapsLayerValue)
            //        //    {

            //        //        MovingSnapTrapSides trap = enemiesToHit[i].GetComponent<MovingSnapTrapSides>();
            //        //       // trap.TakeDamage(damage);



            //        //    }
            //        //}
                  
                   
            //        //firstAttackDone = false;
            //        //secondAttack = false;


            //}
        }

        //if (IsFoilWeapon)
        //{
        //    if (!foilFirstAttack && !foilSecondAttack && !foilThirdAttack)
        //    {
        //        foilFirstAttackTrigger = true;

        //    }
        //    if (foilFirstAttack)
        //    {


        //        currentTimeTillSecondAttack += Time.deltaTime;
        //        if (currentTimeTillSecondAttack >= SecondAttackCooldown)
        //        {

        //            foilFirstAttack = false;
        //            foilSecondAttack = false;
        //            foilThirdAttack = false;
        //            foilSecondAttackTrigger = false;
        //            foilThirdAttackTrigger = false;
        //            FoilWeaponCollider.enabled = false;
        //            currentTimeTillSecondAttack = 0;

        //        }
        //        else
        //        {
        //            foilSecondAttackTrigger = true;
        //        }

        //    }
        //    if (foilSecondAttack)
        //    {

        //        currentTimeTillSecondAttack += Time.deltaTime;
        //        if (currentTimeTillSecondAttack >= SecondAttackCooldown)
        //        {
        //            foilFirstAttack = false;
        //            foilSecondAttack = false;
        //            foilThirdAttack = false;
        //            foilSecondAttackTrigger = false;
        //            foilThirdAttackTrigger = false;
        //            currentTimeTillSecondAttack = 0;
        //        }
        //        else
        //        {

        //            foilThirdAttackTrigger = true;
        //        }
        //    }
        //    if (foilThirdAttack)
        //    {
        //        //currentTimeTillSecondAttack += Time.deltaTime;
        //        //if (currentTimeTillSecondAttack >= SecondAttackCooldown)
        //        //{
        //            foilFirstAttack = false;
        //            foilSecondAttack = false;
        //            foilThirdAttack = false;
        //            foilSecondAttackTrigger = false;
        //            foilThirdAttackTrigger = false;
        //            currentTimeTillSecondAttack = 0;
        //        //}
        //    }


        //    if (timeBtwAttacks <= 0)
        //    {
        //        //FoilWeaponCollider.enabled = false;
        //        if (Input.GetButtonDown("Fire1"))
        //        {


        //            if (foilFirstAttackTrigger)
        //            {
        //                foilFirstAttack = true;
        //                StartCoroutine(FoilTiming());
        //                FoilWeaponCollider.size = new Vector2(1, 0.5f);
                        
        //            }



        //            if (foilSecondAttackTrigger)
        //            {
        //                StartCoroutine(FoilTiming());

        //                foilSecondAttackTrigger = false;
        //                foilSecondAttack = true;
        //                foilFirstAttack = false;
        //                FoilWeaponCollider.size = new Vector2(2, 0.5f);
                        
        //            }
        //            if (foilThirdAttackTrigger)
        //            {
        //                StartCoroutine(FoilTiming());

        //                foilThirdAttackTrigger = false;
        //                foilFirstAttack = false;
        //                foilSecondAttack = false;
        //                foilThirdAttack = true;
        //                FoilWeaponCollider.size = new Vector2(4, 0.5f);
                        

        //            }

        //            //ContactFilter2D contactFilter2D = new ContactFilter2D();
        //            //Collider2D[] enemyColliderArray = new Collider2D[5];
        //            //contactFilter2D.layerMask = enemylayer;
        //            //int numOfEnemiesHit = Physics2D.OverlapCollider(FoilWeaponCollider, contactFilter2D, enemyColliderArray);
        //            //for (int i = 0; i < enemyColliderArray.Length; i++)
        //            //{
        //            //    enemyColliderArray[i].transform.GetComponent<Enemy>().TakeDamage(damage);
        //            //}
        //            //if (player.islookingright)
        //            //{
        //            //    foilAttackCapsuleDir = new Vector2(transform.position.x + 1, transform.position.y);

        //            //}
        //            //else
        //            //{
        //            //    foilAttackCapsuleDir = new Vector2(transform.position.x - 1, transform.position.y);

        //            //}
        //            //FoilWeaponCollider.transform.localPosition = new Vector2(0.5f, 0);  
        //            //FoilWeaponCollider.Cast(foilAttackCapsuleDir, enemiesToHit, foilAttackCapsuleDis);


        //            //StartCoroutine(foilGizmoTiming());
        //            //transform.localPosition = new Vector3(0.2f, 0f, 0f);
        //            //AttackRange = 0.3f;
        //            ////Collider2D[] enemiesToHit = Physics2D.OverlapCapsuleAll(transform.position, foilAttackCapsuleRange, CapsuleDirection2D.Horizontal, 0);
        //            ////RaycastHit2D[] enemiesToHit = Physics2D.CircleCastAll(transform.position, AttackRange, foilAttackCapsuleDir, foilAttackCapsuleDis, enemylayer);
        //            ////RaycastHit2D[] enemiesToHit = Physics2D.LinecastAll(transform.position, foilHitPointForGizmos, enemylayer);
        //            ////RaycastHit2D[] enemiesToHit = Physics2D.RaycastAll(transform.position, foilAttackCapsuleDir, foilAttackCapsuleDis, enemylayer);/*(transform.position, foilHitPointForGizmos, enemylayer);*/
        //            //for (int i = 0; i < enemiesToHit.Length; i++)
        //            //{
        //            //    enemiesToHit[i].collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);


        //            //}
        //            //timeBtwAttacks = startTimeBtwAttacks;


        //        }
        //    }
        //    else
        //    {
        //        timeBtwAttacks -= Time.deltaTime;

        //    }
        //}
    }
   
    // void SwordAttackGizmo()
    //{
    //    Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(transform.position, AttackRange);
    //    for (int i = 0; i < enemiesToHit.Length; i++)
    //    {
    //        if (enemiesToHit[i].gameObject.layer == enemyLayerValue)
    //        {
    //            Enemy enemyInstance = enemiesToHit[i].GetComponent<Enemy>();
    //            enemyInstance.TakeDamage(damage);
    //            //enemyInstance.TakeDamage();

    //        }
    //        if (enemiesToHit[i].gameObject.layer == bossLayerValue)
    //        {
    //            //  Debug.Log("hit");
    //            //   boss = enemiesToHit[i].GetComponent<SunFlower>();
    //            //if(enemiesToHit[i].gameObject == sunflowerRef.gameObject)

    //            sunflowerRef.TakeDamage(damage);


    //        }
    //        if (enemiesToHit[i].gameObject.layer == TrapsLayerValue)
    //        {
    //            //  Debug.Log("hit");
    //            //   boss = enemiesToHit[i].GetComponent<SunFlower>();
    //            //if(enemiesToHit[i].gameObject == sunflowerRef.gameObject)
    //            MovingSnapTrapSides trap = enemiesToHit[i].GetComponentInParent<MovingSnapTrapSides>();
    //            trap.TakeDamage(damage);
    //            //sunflowerRef.TakeDamage(damage);


    //        }





    //    }
    //}

    // void ClawAttackGizmo()
    //{
    //    Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(transform.position, AttackRange);
    //    for (int i = 0; i < enemiesToHit.Length; i++)
    //    {
    //        if (enemiesToHit[i].gameObject.layer == enemyLayerValue)
    //        {
    //            Enemy enemyInstance = enemiesToHit[i].GetComponent<Enemy>();
    //            enemyInstance.TakeDamage(damage);
    //            //enemyInstance.TakeDamage();

    //        }
    //        if (enemiesToHit[i].gameObject.layer == bossLayerValue)
    //        {
    //            //  Debug.Log("hit");
    //            //   boss = enemiesToHit[i].GetComponent<SunFlower>();
    //            sunflowerRef.TakeDamage(damage);
    //        }
    //        if (enemiesToHit[i].gameObject.layer == TrapsLayerValue)
    //        {

    //            MovingSnapTrapSides trap = enemiesToHit[i].GetComponent<MovingSnapTrapSides>();
    //            trap.TakeDamage(damage);



    //        }
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        //if (IsFoilWeapon)
        //{
        //    if (foilFirstAttack)
        //    {

        //        Gizmos.color = Color.red;
        //        Gizmos.DrawLine(transform.position, foilHitPointForGizmos);
        //        Gizmos.DrawRay(transform.position,foilHitPointForGizmos)
        //        Gizmos.DrawSphere(foilHitPointForGizmos, AttackRange);
        //    }  

        //}

    }
    void SlashAnimation()
    {
      
        player.animator.SetTrigger("JetSwordAttack");
     
    }
    void ShotgunShot()
    {

        
        
         GameObject bulletInstance = Instantiate(shotgunBullet,weaponCollider.position, Quaternion.identity);
        if (player.islookingright)
        {
            bulletInstance.GetComponent<OnBulletCollision>().velocity = ShotgunShotDir * bulletSpeed;

        }
        else
        {
            bulletInstance.GetComponent<OnBulletCollision>().velocity = new Vector2(-ShotgunShotDir.x, ShotgunShotDir.y) * bulletSpeed;

        }
        //shotGunParticals.transform.rotation = Quaternion.Euler(0, -90, 0);
        //shotGunParticals.Play();
        //player.animator.SetTrigger("ShotgunShot");


        //GameObject Clone = Instantiate(shotGunParticals, player.transform.position, Quaternion.Euler(0, -90, 0));
        //Destroy(Clone, 2f);



        //ShotGunAttackGizmo();

        //void ShotGunAttackGizmo()
        //{
        //    Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(transform.position, AttackRange, enemylayer);
        //    for (int i = 0; i < enemiesToHit.Length; i++)
        //    {
        //        if (enemiesToHit[i].gameObject.layer == enemyLayerValue)
        //        {
        //            Enemy enemyInstance = enemiesToHit[i].GetComponent<Enemy>();
        //            enemyInstance.TakeDamage(damage);
        //            //enemyInstance.TakeDamage();

        //        }
        //        if (enemiesToHit[i].gameObject.layer == bossLayerValue)
        //        {
        //            //  Debug.Log("hit");
        //            //   boss = enemiesToHit[i].GetComponent<SunFlower>();
        //            sunflowerRef.TakeDamage(damage);
        //        }
        //        if (enemiesToHit[i].gameObject.layer == TrapsLayerValue)
        //        {

        //            MovingSnapTrapSides trap = enemiesToHit[i].GetComponent<MovingSnapTrapSides>();
        //            trap.TakeDamage(damage);



        //        }
        //    }
        //}
    }
     IEnumerator PickaxeAttackTiming()
    {
        float currentvelocity; 
        currentvelocity = player.velocity.x; 
        if (player.islookingright && player.IsGrounded)
        {
           
            player.velocity.x = secondAttackBoost;
            yield return new WaitForSeconds(pickaxeDashTiming);
            player.velocity.x = currentvelocity;
            PickaxeSecondAttack = false;
        }
        else if (!player.islookingright && player.IsGrounded)
        {
            player.velocity.x = -secondAttackBoost;
            yield return new WaitForSeconds(pickaxeDashTiming);
            player.velocity.x = currentvelocity;
            PickaxeSecondAttack = false;
        }
        else
        {
            player.velocity.x = currentvelocity;
            yield return null;
        }
       
    }
    //IEnumerator FoilTiming()
    //{
    //    FoilWeaponCollider.enabled = true;
    //    yield return new WaitForSeconds(0.2f);
    //    FoilWeaponCollider.enabled = false;

    //}
    //IEnumerator ClawAttackseq()
    //{

    //    secondAttack = true;
    //    yield return new WaitForSeconds(4f);
    //    secondAttack = false;


    //}
    private void SwapWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IsSwordWeapon = true;
            IsShotgunWeapon = false;
            IsPickaxeWeapon = false;
            //IsFoilWeapon = false;

            //if (IsSwordWeapon)
            //{
            //    IsSwordWeapon = false;
            //    IsShotgunWeapon = true;
            //    IsClawWeapon = false;
            //}

            //else if (IsShotgunWeapon)
            //{
            //    IsSwordWeapon = false;
            //    IsShotgunWeapon = false;
            //    IsClawWeapon = true;

            //} else if (IsClawWeapon)
            //{
            //    IsSwordWeapon = true;
            //    IsShotgunWeapon = false;
            //    IsClawWeapon = false;
            //}
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            IsSwordWeapon = false;
            IsShotgunWeapon = true;
            IsPickaxeWeapon = false;
            //IsFoilWeapon = false;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            IsSwordWeapon = false;
            IsShotgunWeapon = false;
            IsPickaxeWeapon = true;
            //IsFoilWeapon = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            IsSwordWeapon = false;
            IsShotgunWeapon = false;
            IsPickaxeWeapon = false;
            //IsFoilWeapon = true;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (IsFoilWeapon)
    //    {
    //        if(collision.gameObject.layer == enemylayer){
    //            collision.GetComponent<Enemy>().TakeDamage(damage);
    //        }
    //    }
    //}
}