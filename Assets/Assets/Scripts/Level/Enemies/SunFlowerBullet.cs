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
   

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector2.zero;
      
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y -= Gravity * Time.deltaTime;
        transform.Translate(velocity);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GroundLayer || collision.gameObject.layer == PlayerLayer)
        {
            velocity = Vector2.zero;
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
}
