using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSnapTrapSides : MonoBehaviour
{
    public int health;
    private SnapTrapTriggerHandler TriggerRef;
    public SnapTrapSIde1L Side1;
    public SnapTrapSIde2R Side2;
    public float SnapSpeed;
    public bool successfulAttack;
    public float trappedTime;
    public float currentTrapTime;
    public int damage;
    private Vector3 startingPosSide1;
    private Vector3 startingPosSide2;
    public bool unsuccessfulAttack;
  
   

    // Start is called before the first frame update
    void Start()
    {
        TriggerRef = GetComponentInChildren<SnapTrapTriggerHandler>();
        startingPosSide1 = Side1.transform.position;
        startingPosSide2 = Side2.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerRef.SnapAttackTrigger)
        {
            Side1.gameObject.SetActive(true);
            Side2.gameObject.SetActive(true);
            Side1.transform.Translate(new Vector2(SnapSpeed * Time.deltaTime, 0));
            Side2.transform.Translate(new Vector2(-SnapSpeed * Time.deltaTime, 0));
            if (Side1.IsTouchingPlayer && Side2.IsTouchingPlayer)
            {
                //SnapSpeed = 0;
               
                successfulAttack = true;
            }
            if (Vector2.Distance(Side1.transform.position, Side2.transform.position) <= 0.1f)
            {
                unsuccessfulAttack = true;
            }
        }
        if (successfulAttack)
        {
            TriggerRef.playerRef.enabled = false;
            TriggerRef.SnapAttackTrigger = false;
            currentTrapTime += Time.deltaTime;
            if (currentTrapTime >= trappedTime)
            {
                TriggerRef.playerRef.enabled = true;
                TriggerRef.playerRef.TakeDamage(damage);
                TriggerRef.TriggerCount = 1;
                currentTrapTime = 0;
                Side1.transform.position = startingPosSide1;
                Side2.transform.position = startingPosSide2;
                Side1.gameObject.SetActive(false);
                Side2.gameObject.SetActive(false);
                successfulAttack = false;

            }
        }
        if (unsuccessfulAttack)
        {

            TriggerRef.SnapAttackTrigger = false;
            currentTrapTime += Time.deltaTime;
            if (currentTrapTime >= trappedTime)
            {
                TriggerRef.TriggerCount = 1;
                currentTrapTime = 0;
                Side1.transform.position = startingPosSide1;
                Side2.transform.position = startingPosSide2;
                Side1.gameObject.SetActive(false);
                Side2.gameObject.SetActive(false);
                unsuccessfulAttack = false;
            }
        }

    }

    public void TakeDamage(int damage)
    {

        if (unsuccessfulAttack)
        {
            health -= damage;
            Debug.Log(this.gameObject.name + "takendamage" + "health" + "  " + health);
        }
       
       
        
        //hitDamage = 0;


        // StartCoroutine(HITSLOWTIME());
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }


    }
}
