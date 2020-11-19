using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public int damage;
    public int trapLayer;
    public int enemyLayer;
    public int bossLayer;
   
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
           collision.GetComponentInParent<Benny>().TakeDamage(damage);
           

        }
        if (collision.gameObject.layer == trapLayer)
        {
            collision.GetComponent<MovingSnapTrapSides>().TakeDamage(damage);
            

        }
        if (collision.gameObject.layer == bossLayer)
        {
            collision.GetComponent<SunFlower>().TakeDamage(damage);
          

        }
    }
}
