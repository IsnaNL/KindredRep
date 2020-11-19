using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCol : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController2D player;
    public int damage;
    public void Init()
    {
        Debug.Log("TrapsInited");
        player = FindObjectOfType<CharacterController2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(player.gameObject.name);
        player.TakeDamage(damage);
        
    }
}
