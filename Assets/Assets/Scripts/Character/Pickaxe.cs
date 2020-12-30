using System.Collections;
using UnityEngine;

public class Pickaxe : Weapon
{ 
    public bool isPickaxeClawing = false;
    public bool isPickaxeClawed = false;
    public Vector2 pickaxeJump;
    public float pickaxeRange;

    public float takeControlDur;
  
   public override void GetInput()
    {
        if (Input.GetKeyDown(mobilityAbility) && !player.IsGrounded)
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
            player.animator.SetTrigger("Clawing");
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

            RaycastHit2D checkforwall = Physics2D.CircleCast(new Vector2(transform.position.x,transform.position.y), pickaxeRange, dir, 0.22f,player.groundLayerMask);

            isPickaxeClawed = checkforwall;
            isPickaxeClawing = false;
          //  StartCoroutine( SetClawingFalse());



        }
        if (isPickaxeClawed)
        {
            player.animator.SetBool("Clawed",true);
            player.IsGrounded = false;
            player.velocity = Vector2.zero;
            player.rb.velocity = Vector2.zero;
            player.canMove = false;
            isPickaxeClawing = false;
          

            
                if (Input.GetButtonDown("Jump"))
            {
                player.canMove = true;
                player.animator.SetBool("Clawed", false);
                player.animator.SetTrigger("WallJump");
                isPickaxeClawed = false;
                player.islookingright = !player.islookingright;
                player.Flip();
                

                if (player.islookingright)
                {
                    player.velocity = new Vector2(pickaxeJump.x,pickaxeJump.y);

                }
                else
                {
                    player.velocity = new Vector2(-pickaxeJump.x, pickaxeJump.y);

                   
                }
              
              
            }
            else
            {
            
            }
        }
    }
    IEnumerator SetClawingFalse() 
    {


        yield return new WaitForSeconds(0.5f);
        isPickaxeClawing = false;

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
