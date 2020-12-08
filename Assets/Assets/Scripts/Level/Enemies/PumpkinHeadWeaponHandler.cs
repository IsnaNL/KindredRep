using UnityEngine;

public class PumpkinHeadWeaponHandler : MonoBehaviour
{
    // Start is called before the first frame update
  
        public int damage;
        public int PlayerLayer;
        public CharacterController2D player;
   

        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D collision)
        {
          if (collision.gameObject.layer == PlayerLayer)
           {
                collision.GetComponentInParent<CharacterController2D>().TakeDamage(damage);
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
        }
            
        }
    }

