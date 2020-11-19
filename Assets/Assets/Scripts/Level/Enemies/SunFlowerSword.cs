using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerSword : MonoBehaviour

{
    public SunFlower Handler;
    public int damage;
    public int PlayerLayer;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("SunFlowerswordTrigger");
       
        if(collision.gameObject.layer == PlayerLayer)
        {
            Handler.PlayerRef.TakeDamage(damage);

        }

    }// Start is called before the first frame update

}
