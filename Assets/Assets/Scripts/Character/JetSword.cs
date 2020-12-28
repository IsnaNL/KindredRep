using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetSword : Weapon
{
    public bool isSwordDashing;
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

            if (player.islookingright)
            {
                 des = Physics2D.CircleCast(transform.position, 0.2f,Vector2.right,10f, player.groundLayerMask);

            }
            else
            {
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
            }
            else
            {
                dashRight = false;
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
           
            if (!player.TopWallCheck && !player.BottomWallCheck)
            {
                if (dashRight)
                {
                    if (transform.position.x <= des.point.x - 0.4f)
                    {
                        player.velocity.x += dashForce * Time.deltaTime;
                    }
                    else
                    {
                        player.velocity.x = 0;
                        MidDashCurrentTime = 0f;
                        player.gravityScale = 25;
                    }
                }
                else
                {
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
                }
            }
            else
            {
                player.velocity.x = 0;
                MidDashCurrentTime = 0f;
                player.gravityScale = 25;
                isSwordDashing = false;

            }
            MidDashCurrentTime += Time.deltaTime;
            if (MidDashCurrentTime >= dashTargetLengthTime )
            {
                player.velocity.x *= 0.15f;
                player.gravityScale = 25;
                MidDashCurrentTime = 0f;
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
}
