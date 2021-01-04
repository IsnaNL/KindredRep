using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetSword : Weapon
{
    public bool isSwordDashing;
    public bool canDash;
    public float maxLength;
   

    public float dashForce;

    float axis;
 
    public float dashwaitTime;

    public bool dashRight;
    RaycastHit2D des;
    public float dashCooldown;
    float startingPos;
    public override void Init()
    {
        canDash = true;
        base.Init();
    }
    public override void Attack()
    {
     
        if (Input.GetKeyDown(attack) && !isSwordDashing)
        {
            if (runningCooldown >= Cooldown)
            {
                weaponHandler.groundColCount = 0;
                StartCoroutine(SlashAnimation());
            }
            runningCooldown = 0;

        }
        if (Input.GetKeyDown(attack) && isSwordDashing) {

            if (runningCooldown >= Cooldown)
            {
                weaponHandler.groundColCount = 0;
                StartCoroutine(SlashDashAnimation());
            }
            runningCooldown = 0;

        }
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
            axis = player.islookingright ? 1 : -1;
            des = Physics2D.CircleCast(player.transform.position, 0.1f, Vector2.right * axis, maxLength, player.groundLayerMask) ;
           
            if(des.transform == null)
            {
                des.point = new Vector2(transform.position.x + (maxLength * axis), transform.position.y);
            }

            player.rb.velocity = new Vector3(0, 0, 0);
            player.velocity = Vector2.zero;
            player.gravityScale = 0;
            dashwaitTime = 0f;
            AudioManager.a_Instance.AlyxJetSwordDashAudio();
            player.animator.SetTrigger("SwordDash");    
            isSwordDashing = true;
            StartCoroutine(TakePlayerControl(0.5f));
            canDash = false;
           


        }
        if (isSwordDashing)
        {
            this.MobilityAbility();
        }

    }
    public override void MobilityAbility()
    {
       
        
           
            player.velocity.y = 0;
            player.rb.velocity = Vector2.zero;
            float cur = Mathf.MoveTowards(player.transform.position.x, des.point.x, dashForce *0.1f);
           
            
            
           if(axis == 1)
            {
                if (transform.position.x <= des.point.x - 0.75f)
                {
                    player.transform.position = new Vector2(cur, player.transform.position.y);
                }
                else
                {
                    player.velocity.x = 0;
                    player.gravityScale = 25;
                    isSwordDashing = false;
                }
            }
            else
            {
                if (transform.position.x >= des.point.x +0.75f)
                {
                    player.transform.position = new Vector2(cur, player.transform.position.y);
                }
                else
                {
                    player.velocity.x = 0;
                    player.gravityScale = 25;
                    isSwordDashing = false;
                }
            }
            
            
           
            
        
    }
    IEnumerator SlashAnimation()
    {
        StartCoroutine(TakePlayerControl(0.5f));
        player.animator.SetTrigger("JetSwordAttack");
        yield return new WaitForSeconds(0.3f);
        AudioManager.a_Instance.AlyxJetSwordAttackAudio();
    }
    IEnumerator SlashDashAnimation()
    {
        StartCoroutine(TakePlayerControl(0.5f));
        player.animator.SetTrigger("SwordDashAttack");
        yield return new WaitForSeconds(0.3f);
        AudioManager.a_Instance.AlyxJetSwordAttackAudio();
    }
    protected override IEnumerator TakePlayerControl(float time)
    {
        StartCoroutine(base.TakePlayerControl(time));
        yield break;
    }
        /* if (dashRight)
         {
             if (transform.position.x <= des.point.x - 1f)
             {

             }
             else
             {
                 player.velocity.x = 0;
                 player.gravityScale = 25;
                 isSwordDashing = false;
             }
         }
         else
         {
             if (transform.position.x >= des.point.x + 1f)
             {
                 player.velocity.x -= dashForce * Time.deltaTime;
             }else
             {
                 player.velocity.x = 0;
                 player.gravityScale = 25;
                 isSwordDashing = false;
             }
         }
     */

        /* if (!player.FrontWallCheck)
         {
         else
         {
             player.velocity.x = 0;  
             player.gravityScale = 25;
             isSwordDashing = false;

         }
       */



}
