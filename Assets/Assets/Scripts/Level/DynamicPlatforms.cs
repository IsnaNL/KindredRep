using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatforms : MonoBehaviour
{
    public LayerMask playerLayer;
    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {   
       Invoke("DropPlatform",1f);           
    }

    private void DropPlatform()
    {
        rb.isKinematic = false;
        Destroy(gameObject, 2f);
    }
   
}
