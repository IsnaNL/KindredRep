using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
 
    Vector2 mousepos;
    Vector2 targetMousePos;
    private Enemy enemy;
    public float bulletSpeed;
    Vector2 startingpositionfordebug;
    //Vector3 difference;
    //Vector3 shotDir;
    //private float distance;
    void Start()
    {
        startingpositionfordebug = transform.position;
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetMousePos = new Vector2(mousepos.x, mousepos.y);
        targetMousePos =  new Vector2(targetMousePos.x - transform.position.x, targetMousePos.y - transform.position.y) * bulletSpeed;
        //difference = mousepos - transform.position;
        //distance = difference.magnitude;
        //if (distance >= 2f)
        //{
        //    distance = 2f;
        //}
        //shotDir = difference / distance;
        //shotDir.Normalize();
        //shotDir.z = 0f;
    }
     void Update()
    {
        Debug.DrawRay(startingpositionfordebug, targetMousePos, Color.blue);
        Shoot();
    }
    void Shoot()
    {
        transform.Translate((targetMousePos.normalized * bulletSpeed * Time.deltaTime));
        if (targetMousePos.magnitude >= 1f)
        {

            targetMousePos = Vector2.ClampMagnitude(targetMousePos, bulletSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(1);
        Destroy(this.gameObject);
        
    }

}
