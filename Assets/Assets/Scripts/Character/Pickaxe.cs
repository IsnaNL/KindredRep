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
        if (player.IsGrounded)
        {
            player.animator.SetBool("Clawing", false);
            player.animator.SetBool("WallJump", false);


        }
        if (pickaxeRunningCD < pickaxeCD)
        {
            pickaxeRunningCD += Time.deltaTime;
        }
        if (Input.GetKeyDown(mobilityAbility) && !player.IsGrounded && pickaxeRunningCD >= pickaxeCD)
        {
            player.animator.SetBool("Clawed", false);
            pickaxeRunningCD = 0;       
            if (isPickaxeClawed)
            {
                isClawing = false;
                player.canMove = true;  
                player.islookingright = !player.islookingright;
                player.Flip();
                player.animator.SetBool("Clawed", false);
                player.animator.SetBool("WallJump", false);
                player.gravityScale = 25;
                isPickaxeClawed = false;
            }
            else
            {
                player.animator.SetBool("Clawing",true);    
                isClawing = true;
            }                      
        }
        if (isClawing)
        {
           StartCoroutine(CheckForWall());
        }
      
        if (Input.GetButtonDown("Jump"))
        { 
            if (isPickaxeClawed)
            { 
                player.canMove = true;
                player.islookingright = !player.islookingright;
                player.Flip();
                player.animator.SetBool("Clawed", false);
                player.animator.SetBool("WallJump",true);
                player.gravityScale = 25;
                if (player.islookingright)
                {
                    player.velocity = new Vector2(pickaxeJump.x, pickaxeJump.y);
                }
                else
                {
                    player.velocity = new Vector2(-pickaxeJump.x, pickaxeJump.y);
                }
                isPickaxeClawed = false;
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
            player.transform.position =  new Vector2( curPlayerPos.x -0.9f,curPlayerPos.y);
        }
        else
        {
            player.transform.position = new Vector2(curPlayerPos.x + 0.9f, curPlayerPos.y);
        }
        isClawing = false;
        player.canMove = false;
        player.IsGrounded = false;
        player.animator.SetBool("Clawing", false);
        player.animator.SetBool("WallJump", false);
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
    public IEnumerator CheckForWall()
    {
        Vector2 dir;
        dir = player.islookingright ? Vector2.right : Vector2.left;
        yield return new WaitForSeconds(0.1f);
        if (isClawing)
        {
            RaycastHit2D checkforwall = Physics2D.CircleCast(new Vector2(player.transform.position.x, player.transform.position.y), pickaxeRange, dir, 0.59f, player.groundLayerMask);
            isPickaxeClawed =  checkforwall;
            isClawing = !isPickaxeClawed;
            curPlayerPos = checkforwall.point;
            this.MobilityAbility();       
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
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + 0.6f, transform.position.y), pickaxeRange);
    }
}


