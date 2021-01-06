using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSnapTrapScript : Health
{

    public LayerMask playerLayerMask;
    public float RangeToTrigger;
    private CharacterController2D player;
    public Collider2D hitColldier;
    public int damage;
    public float delayBetweenTriggers;
    public float SnapTrapTriggerConditionDelay;
    public bool canTrigger;
    private bool canHit;
    private Vector2 posForCheck;
    public bool playerCaught;



    // Start is called before the first frame update
    public void Init()
    {
        delayBetweenTriggers = SnapTrapTriggerConditionDelay;
        posForCheck = new Vector2(transform.position.x, transform.position.y - 0.7f);
    }
    void Update()
    {
        CheckForTrapTriggerCondition();
        if (delayBetweenTriggers <= SnapTrapTriggerConditionDelay)
        {
            canTrigger = false;
            delayBetweenTriggers += Time.deltaTime;
        }
        else
        {
            canTrigger = true;
            

        }
    }
    public void CheckForTrapTriggerCondition()
    {
        if (canTrigger)
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, RangeToTrigger, Vector2.up, 0.1f, playerLayerMask);
            if (hit)
            {
                delayBetweenTriggers = 0;
                player = hit.transform.GetComponent<CharacterController2D>();
                ExecuteSnapTrap();


            }
            else
            {
                canHit = true;
            }
        }
      
    }
    public void ExecuteSnapTrap()
    {
        animator.SetTrigger("SnapTrapTrigger");
       

    }
    // Update is called once per frame
   
   
    public void BackToIdle()
    {
        animator.SetTrigger("BackToIdle");
       // Invoke("ResetDelay", 1.5f); 
    }
    void ResetDelay()
    {
        delayBetweenTriggers = 0;
      //  hitColldier.enabled = true;

    }
    public void FreezeCharacter()
    {
        playerCaught = true;
        player.transform.position = new Vector2(transform.position.x, player.transform.position.y);
        player.animator.speed = 0;
        player.canMove = false;
        player.rb.velocity = Vector2.zero;
        player.velocity = Vector2.zero;
       
        //player.velocity = Vector2.zero;
        //player.enabled = false;
       

    }
    public void UnfreezeCharacter()
    {
        if (playerCaught)
        {
            player.animator.speed = 1;
            player.canMove = true;
            player.TakeDamage(damage);
        }
        playerCaught = false;
        //player.velocity = new Vector2(-2,2);
        //player.enabled = true;
        //player.animator.speed = 1;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            if (canHit)
            {
                FreezeCharacter();
                canHit = false;
            }
           

        }
       

    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeToTrigger);
    }
}
