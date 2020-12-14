using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Weapon
{
    // Start is called before the first frame update
   
    public override void Attack()
    {
        if (Input.GetKeyDown(attack))
        {
            Debug.Log("Pickaxe Attack");
            /* if (PickaxeFirstAttack)
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

         }*/
        }
    }
}
