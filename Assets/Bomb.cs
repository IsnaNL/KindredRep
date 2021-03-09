using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float ExplosionDelay;
    private bool isTriggered = false;
    private Rigidbody2D rb;
    public Collider2D myColl;
    private List<GameObject> Collisions;
    [SerializeField] private int Damage;

    public LayerMask GroundLayerMask;
    public LayerMask HitLayerMask;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        myColl.isTrigger = false;
        Collisions = new List<GameObject>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            StartCoroutine(Trigger());
        }
    }
    private IEnumerator Trigger()
    {
        anim.SetTrigger("Trigger");
        anim.speed = 1 / ExplosionDelay;
        yield return new WaitForSeconds(ExplosionDelay);
        anim.SetTrigger("Explode");
        anim.speed = 1;
        rb.isKinematic = true;
        myColl.isTrigger = true;
        yield return new WaitForSeconds(0.161616f);
        Collider2D[] overlaps;
        overlaps = Physics2D.OverlapCircleAll(myColl.transform.position, 3);
        Vector2 bombCenter = myColl.transform.position;
        foreach (var item in overlaps)
        {
            if (item.GetComponent<Health>() != null || item.GetComponentInParent<Health>() != null)
            {
                bool isParent = item.GetComponentInParent<Health>() != null;
                Vector2 pos = item.transform.position;
                Vector2 direction = new Vector2(pos.x - bombCenter.x, pos.y - bombCenter.y);

                RaycastHit2D hit = Physics2D.Raycast(bombCenter, direction, direction.magnitude, GroundLayerMask);
                if (hit)
                {
                    Debug.Log(hit.transform.name + " " + (bool)hit);
                }
                else
                {
                    hit = Physics2D.Raycast(bombCenter, direction, direction.magnitude, HitLayerMask);
                }
                if (hit)
                {

                }
                {
                    //Debug.Log(hit.transform.name + " " + (bool)hit);
                    if (isParent)
                    {
                        item.gameObject.GetComponentInParent<Health>().TakeDamage(Damage);
                    }
                    else
                    {
                        item.gameObject.GetComponent<Health>().TakeDamage(Damage);
                    }
                }
            }
        }
        EffectsManager.instance.CreateEffect(Effects.bomb, myColl.transform, false);
        gameObject.SetActive(false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(myColl.transform.position, 3);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
 
    }
}