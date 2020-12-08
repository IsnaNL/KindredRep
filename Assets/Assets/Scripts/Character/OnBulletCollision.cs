using UnityEngine;

public class OnBulletCollision : MonoBehaviour
{
    private Animator animator;
    public Vector2 velocity;
    public int damage;
    public int enemyLayer;
    public int trapLayer;
    public int bossLayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            collision.GetComponentInParent<Benny>().TakeDamage(damage);


        }
        if (collision.gameObject.layer == trapLayer)
        {
            collision.GetComponent<MovingSnapTrapSides>().TakeDamage(damage);


        }
        if (collision.gameObject.layer == bossLayer)
        {
            if (collision.GetComponent<SunFlower>() != null)
            {
                collision.GetComponent<SunFlower>().TakeDamage(damage);

            }
            if(collision.GetComponent<PumpkinHead>() != null)
            {
                collision.GetComponent<PumpkinHead>().TakeDamage(damage);
            }
            if (collision.GetComponent<GhostMovement>() != null)
            {
                collision.GetComponent<GhostMovement>().TakeDamage(damage);

            }

        }
        AudioManager.a_Instance.AlyxShotGunclashAudio();
        animator.SetTrigger("Hit");
        velocity = Vector2.zero;
        Destroy(gameObject, 0.3f);
    }
    void FixedUpdate()
    {
        transform.Translate(velocity * Time.fixedDeltaTime);

    }

}
