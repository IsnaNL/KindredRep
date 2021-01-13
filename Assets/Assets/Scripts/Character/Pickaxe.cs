using System.Collections;
using UnityEngine;

public class Pickaxe : Weapon
{
   
    public bool isPickaxeClawed = false;
    public bool isClawing;
    public Vector2 pickaxeJump;
    public float pickaxeRange;
    public float pickaxeCD;
    public float pickaxeRunningCD;
    public float takeControlDur;
    Vector2 curPlayerPos;
    public override void GetInput()
    {
        if(pickaxeRunningCD < pickaxeCD)
        {
            pickaxeRunningCD += Time.deltaTime;
        }
        if (Input.GetKeyDown(mobilityAbility) && !player.IsGrounded && pickaxeRunningCD >= pickaxeCD)
        {
            
            pickaxeRunningCD = 0;
           

            if (isPickaxeClawed)
            {
                isPickaxeClawed = false;
                player.canMove = true;
                isClawing = false;
                player.islookingright = !player.islookingright;
                player.Flip();
                player.animator.SetBool("Clawed", false);
                player.gravityScale = 25;
                isClawing = false;
            }
            else
            {
                player.animator.SetTrigger("Clawing");
                isClawing = true;
            }
    
          
           
        }

        if (isClawing)
        {

         
            CheckForWall();

            this.MobilityAbility();

        }
        if (Input.GetButtonDown("Jump"))
        {
            if (isPickaxeClawed)
            {
                isPickaxeClawed = false;
                player.canMove = true;
                player.islookingright = !player.islookingright;
                player.Flip();
                player.animator.SetBool("Clawed", false);
                player.animator.SetTrigger("WallJump");
                player.gravityScale = 25;
                if (player.islookingright)
                {
                    player.velocity = new Vector2(pickaxeJump.x, pickaxeJump.y);

                }
                else
                {
                    player.velocity = new Vector2(-pickaxeJump.x, pickaxeJump.y);


                }
            }
        }


    }
  
    public override void MobilityAbility()
    {
        if (isPickaxeClawed)
        {
            StartCoroutine( ClawedRoutine());
        }
       
    }

    private IEnumerator ClawedRoutine()
    {
       
        player.animator.speed = 1;
        yield return new WaitForSeconds(0.05f);
        if (player.islookingright)
        {
            player.transform.position = new Vector3(curPlayerPos.x -0.25f ,curPlayerPos.y);
        }
        else
        {
            player.transform.position = new Vector3(curPlayerPos.x +0.25f, curPlayerPos.y);
        }

        isClawing = false;
        player.canMove = false;
        player.IsGrounded = false;
        player.animator.SetBool("Clawed", true);
        player.velocity = Vector2.zero;
        player.rb.velocity = Vector2.zero;
        player.gravityScale = 0;

     



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

    public void CheckForWall()
    {
        Vector2 dir;
        if (player.islookingright)
        {
            dir = Vector2.right;
        }
        else
        {
            dir = Vector2.left;
        }


        if (isClawing)
        {
            isClawing = !isPickaxeClawed;
            RaycastHit2D checkforwall = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y), pickaxeRange, dir, 0.6f, player.groundLayerMask);
            isPickaxeClawed =  checkforwall;
            curPlayerPos = player.transform.position;
          
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
        player.gravityScale = 25;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector2.right * 0.6f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + 0.6f,transform.position.y), pickaxeRange);
    }
}


