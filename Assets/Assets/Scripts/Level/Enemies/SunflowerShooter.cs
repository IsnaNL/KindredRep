using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerShooter : MonoBehaviour
{
   
    public float shootCooldown;
    public float shootCoolDownCounting;
    public SunFlowerBullet sunflowerBullet;
    private SunFlower sunflowerRef;
    public float BulletShootTriggerRangeFar;
    public Transform Player;
    

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
        if ((Vector2.Distance(Player.position, transform.position) <= BulletShootTriggerRangeFar && Vector2.Distance(Player.position, transform.position) >= BulletShootTriggerRangeFar * 0.5f))
        {
            if(shootCooldown <= shootCoolDownCounting)
            {
                Instantiate<GameObject>(sunflowerBullet.gameObject, new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
                sunflowerBullet.direction = sunflowerRef.direction;
                shootCoolDownCounting = 0f;

                
            }
           
        }

      
    }
   
    private void OnDrawGizmos()
    {
       Gizmos.DrawLine(new Vector2(transform.position.x + BulletShootTriggerRangeFar * 0.5f * sunflowerRef.direction, transform.position.y), new Vector2(transform.position.x + BulletShootTriggerRangeFar * sunflowerRef.direction, transform.position.y));
    }
}
