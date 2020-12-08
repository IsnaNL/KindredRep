using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Health : MonoBehaviour
{
    public int health;
    public Animator animator;
    public bool isHit;
    public bool isVulnerable;
   // public bool hit;
    //public int hitDamage;
    
   virtual public void Start()
    {
       // Debug.Log("AbstractHealthInit");
        isVulnerable = true;
    }



     public void TakeDamage(int damage)
    {
        if (isVulnerable)
        {
            health -= damage;
            Debug.Log(this.gameObject.name + "takendamage" + "health" + "  " + health);
            // animator.SetTrigger("Hit");
            //hitDamage = 0;
            isHit = true;
          
            // StartCoroutine(HITSLOWTIME());

        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }
   
  /*  public IEnumerator HITSLOWTIME()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;

    }*/
}
