using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Weapon
{

  //  public Vector2 pickaxeVelocity = Vector2.zero;
    public bool isPickaxeClawing = false;
    public bool isPickaxeClawed;
    public Vector2 pickaxeJump;
    public float pickaxeRange;
    public bool shouldFlip = false;
    // Start is called before the first frame update
   public override void GetInput()
    {
        if (Input.GetKeyDown(mobilityAbility))
        {
            //isClawing = true;
            if (isPickaxeClawing)
            {
                isPickaxeClawing = false;
            }
            else
            {
                isPickaxeClawing = true;
                shouldFlip = true;
            }
            if (isPickaxeClawed)
            {
                isPickaxeClawed = false;
            }
        }
        if (isPickaxeClawing)
        {
            this.MobilityAbility();
        }
    }
    public override void MobilityAbility()
    {
        
        if (isPickaxeClawing)
        {
           

            Collider2D[] wallToHit = Physics2D.OverlapCircleAll(player.transform.position, pickaxeRange, player.groundLayerMask);
            for (int i = 0; i < wallToHit.Length; i++)
            {
                //    Debug.Log(wallToHit[i].gameObject.name);
                //    Debug.Log(wallToHit);
                if (wallToHit[0].gameObject.layer == player.groundLayer)
                {
                    //rb.gravityScale = 0f;

                    isPickaxeClawed = true;
                    Debug.Log(wallToHit[0].gameObject.layer);
                }
               

            }
            isPickaxeClawing = false;
        }
        if (isPickaxeClawed)
        {
            // if(shouldFlip == true)
            //  {
            //  player.Flip();
          //  shouldFlip = false;
       // }
            // player.islookingright = !player.islookingright;
            player.IsGrounded = false;
           
            player.velocity = Vector2.zero;
            player.rb.velocity = Vector2.zero;
           
            //rb.isKinematic = false;
            //animator.SetBool("IsMoving", false);
            // StartCoroutine(TrailTime());
            if (Input.GetButtonDown("Jump"))
            {
                if (player.islookingright)
                {
                    player.velocity = new Vector2(pickaxeJump.x,pickaxeJump.y);

                }
                else
                {
                    player.velocity = new Vector2(-pickaxeJump.x, pickaxeJump.y);

                   
                }
                isPickaxeClawed = false;
            }

           
           
        }
    }
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
