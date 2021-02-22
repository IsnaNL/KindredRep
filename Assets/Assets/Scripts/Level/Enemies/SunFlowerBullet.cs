using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerBullet : MonoBehaviour
{
    public Vector2 velocity;
    public float Gravity;
    public int GroundLayer;
    public int PlayerLayer;
    public bool isShoot;
    public float BulletDes;
    public Vector2 BulletAcceleration;
    public int direction;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector2.zero;
      
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity.y -= Gravity * Time.deltaTime;
        transform.Translate(velocity);

        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, 0.5f);
        if (collider2D)
        {
            if (collider2D.CompareTag("Player"))
            {

                velocity = Vector2.zero;
                collider2D.GetComponent<CharacterController2D>().TakeDamage(damage);
                Destroy(gameObject);

            }
            if (collider2D.CompareTag("Ground"))
            {
                velocity = Vector2.zero;
                Destroy(gameObject);
            }

        }


    }
 
    
    IEnumerator Shoot()
    {
        isShoot = true;

        float Destination = transform.position.y + BulletDes;
        float BulletAccelerationY = BulletAcceleration.y;
        float BulletAccelerationX = BulletAcceleration.x;


        while (isShoot)
        {

            velocity += new Vector2(BulletAccelerationX * direction * Time.deltaTime, BulletAccelerationY * Time.deltaTime);
            if (transform.position.y >= Destination)
            {


               velocity *= 0.5f;
             
                isShoot = false;
            }
            yield return null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
