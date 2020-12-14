using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetSword : Weapon
{
    public override void Init()
    {
        base.Init();
    }
    // Start is called before the first frame update
    public override void Attack()
    {
        if (Input.GetKeyDown(attack))
        {
            if (runningCooldown >= Cooldown)
            {
                weaponHandler.groundColCount = 0;


                // AudioManager.a_Instance.AlixAttackAudio();
                //  Debug.Log("attacking");
                StartCoroutine(SlashAnimation());
                //SwordAttackGizmo();
            }
            runningCooldown = 0;

        }
         if( runningCooldown <Cooldown)
        {
            runningCooldown += Time.deltaTime;
        }
    }
    IEnumerator SlashAnimation()
    {

        player.animator.SetTrigger("JetSwordAttack");
        yield return new WaitForSeconds(0.3f);
        AudioManager.a_Instance.AlyxJetSwordAttackAudio();
    }
}
