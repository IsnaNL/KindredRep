using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Weapon
{ 
    public bool isPickaxeClawing = false;
    public bool isPickaxeClawed;
    public Vector2 pickaxeJump;
    public float pickaxeRange;

    public float takeControlDur;
  
   public override void GetInput()
    {
        if (Input.GetKeyDown(mobilityAbility))
        {

            if(!isPickaxeClawing)
            {
                isPickaxeClawing = true;
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
        Vector2 dir;
        if (isPickaxeClawing)
        {
            if (player.islookingright)
            {
                dir = Vector2.right;
            }
            else
            {
                dir = Vector2.left;

            }

            RaycastHit2D checkforwall = Physics2D.CircleCast(new Vector2(transform.position.x,transform.position.y), pickaxeRange, dir, 0.2f,player.groundLayerMask);

            isPickaxeClawed = checkforwall;

            isPickaxeClawing = false;


        }
        if (isPickaxeClawed)
        {
          
            player.IsGrounded = false;
            player.velocity = Vector2.zero;
            player.rb.velocity = Vector2.zero;
            player.canMove = false;
           
           
            if (Input.GetButtonDown("Jump"))
            {
                player.islookingright = !player.islookingright;
                player.Flip();
                player.canMove = true;

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
            player.animator.SetTrigger("PickaxeAttack");
            Debug.Log("Pickaxe Attack");
            StartCoroutine(TakePlayerControl(takeControlDur));
        }
    }
    protected override IEnumerator TakePlayerControl(float time)
    {
        StartCoroutine(base.TakePlayerControl(time));
        yield break;
    }
    void OnDisable()
    {
        player.canMove = true;
        isPickaxeClawed = false;
        isPickaxeClawing = false;
    }
}
