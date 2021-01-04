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
                isClawing = true;
            }
    
            if (isClawing)
            {

                player.animator.SetTrigger("Clawing");
                CheckForWall();
                
                this.MobilityAbility();
                
            }
           
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
               
                // StopCoroutine("ClawedRoutine");
            }
        }


    }
  
    public override void MobilityAbility()
    {





      



        //  StartCoroutine( SetClawingFalse());




        if (isPickaxeClawed)
        {

           StartCoroutine(ClawedRoutine());
          
        }
       
    }

    private IEnumerator ClawedRoutine()
    {
       
        player.animator.speed = 1;

        yield return new WaitForSeconds(0.1f);
        if (player.islookingright)
        {
            player.transform.position = new Vector2(curPlayerPos.x - 0.4f, player.transform.position.y);
        }
        else
        {
            player.transform.position = new Vector2(curPlayerPos.x + 0.4f, player.transform.position.y);
        }


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
        

        
        RaycastHit2D checkforwall = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y), pickaxeRange, dir, 0.55f, player.groundLayerMask);
        isPickaxeClawed = checkforwall;
        curPlayerPos = player.transform.position;
        isClawing = false;

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
       
    }
}


