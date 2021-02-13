using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Health : MonoBehaviour
{
    public int health;
    public Animator animator;
    public bool isHit;
    public bool isVulnerable;
   virtual public void Start()
    {
        isVulnerable = true;
    }



    public virtual void TakeDamage(int damage)
    {
        if (isVulnerable)
        {
            health -= damage;
            Debug.Log(this.gameObject.name + "takendamage" + "health" + "  " + health);
            isHit = true;
          
 

        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }
   
}
