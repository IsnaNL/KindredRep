using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public Weapon weapon;
    public LayerMask wallLayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (weapon.IsPickaxeWeapon)
        {
            if(collision != null && collision.gameObject.layer == wallLayer)
            {
                //weapon.isClawedOnWall = true;
                //Debug.Log(weapon.isClawedOnWall);

            }
         
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (weapon.IsPickaxeWeapon)
        {
            if (collision == null || collision.gameObject.layer != wallLayer) { 
            //{
            //    weapon.isClawedOnWall = false;
            //    Debug.Log(weapon.isClawedOnWall);

            }
        }
    }
    // Start is called before the first frame update

}
