using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : Health
{
    public Vector2 velocity;
    public float speed;
    private CharacterController2D player;
    public int GhostDamage;



    // Start is called before the first frame update
    public override void Start()
    {
        player = FindObjectOfType<CharacterController2D>();
        isVulnerable = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
        velocity = new Vector2(dir.x * speed,dir.y*speed);
    }
    private void FixedUpdate()
    {
        transform.Translate(velocity * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {

            player.TakeDamage(GhostDamage);
            if (player.transform.position.x >= transform.position.x)
            {
                //player.velocity *= 0.1f;
                player.velocity += new Vector2(player.hitKnockBack.x, player.hitKnockBack.y);
              

            }
            else
            {
                //player.velocity *= 0.1f;
                player.velocity += new Vector2(-player.hitKnockBack.x, player.hitKnockBack.y);



            }

            Die();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
        transform.localPosition = Vector2.zero;
    }
}
