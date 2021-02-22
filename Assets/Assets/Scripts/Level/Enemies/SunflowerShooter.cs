using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerShooter : MonoBehaviour
{
   
    public float shootCooldown;
    public float shootCoolDownCounting;
    public SunFlowerBullet sunflowerBullet;
    private SunFlower sunflowerRef;
    public float bulletShootTriggerRangeFar;
  
    

    // Start is called before the first frame update
    void Start()
    {
        sunflowerRef = GetComponentInParent<SunFlower>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shootCooldown > shootCoolDownCounting)
        {
            shootCoolDownCounting += Time.deltaTime;
        }
        if ((Vector2.Distance(sunflowerRef.PlayerRef.transform.position, transform.position) <= bulletShootTriggerRangeFar && Vector2.Distance(sunflowerRef.PlayerRef.transform.position, transform.position) >= bulletShootTriggerRangeFar * 0.5f))
        {
            if(shootCooldown <= shootCoolDownCounting)
            {
                Instantiate(sunflowerBullet.gameObject, new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
                sunflowerBullet.direction = sunflowerRef.direction;
                shootCoolDownCounting = 0f;

                
            }
           
        }

      
    }
   
    private void OnDrawGizmos()
    {
    //   Gizmos.DrawLine(new Vector2(transform.position.x + bulletShootTriggerRangeFar * 0.5f * sunflowerRef.direction, transform.position.y), new Vector2(transform.position.x + bulletShootTriggerRangeFar * sunflowerRef.direction, transform.position.y));
    }
}
