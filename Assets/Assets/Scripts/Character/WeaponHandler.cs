using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public int damage;
    public int trapLayer;
    public int enemyLayer;
    public int bossLayer;
    public int groundLayer;
    public int bombLayer;
    public int groundColCount;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            collision.GetComponentInParent<Benny>().TakeDamage(damage);
        }        
        if (collision.gameObject.layer == trapLayer)
        {
            collision.GetComponentInParent<NewSnapTrapScript>().TakeDamage(damage);   
            

        }
        if (collision.gameObject.layer == bossLayer)
        {
            if (collision.GetComponent<SunFlower>() != null)
            {
                collision.GetComponent<SunFlower>().TakeDamage(damage);

            }
            if (collision.GetComponent<PumpkinHead>() != null)
            {
                collision.GetComponent<PumpkinHead>().TakeDamage(damage);
            }
            if (collision.GetComponent<GhostMovement>() != null)
            {
                collision.GetComponent<GhostMovement>().TakeDamage(damage);

            }

        }
        if (collision.gameObject.layer == groundLayer)
        {

            groundColCount++;
            if (groundColCount == 1)
            {
                AudioManager.a_Instance.AlyxJetSwordClashWithTerrainAudio();
               
            }

        }
        if (collision.gameObject.layer == bombLayer)
        {
            Vector2 originPos = GameManager.instace.Player.transform.position;
            Vector2 dir = new Vector2(originPos.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);
            Rigidbody2D bomb = collision.gameObject.GetComponent<Rigidbody2D>();
            Debug.LogFormat("Dir: {0} \n Bomb: {1}",dir, bomb);
            StartCoroutine(pushBomb(dir, bomb));
        }
    }
    private IEnumerator pushBomb(Vector2 dir, Rigidbody2D bomb)
    {
        bomb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.1f);
        bomb.velocity = dir;

    }
}
