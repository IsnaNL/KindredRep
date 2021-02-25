using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager e_Instance;
    public GameObject BloodEffect;
    public GameObject hitImpact;
    public GameObject smokeEffect;
    public void Start()
    {
       
        e_Instance = this;   
        
    }
    public void BloodHitEffect(Vector2 pos)
    {     
            Instantiate(BloodEffect, pos,Quaternion.identity,transform);   
    }
    public void HitEffect(Vector2 pos)
    {

        Instantiate(hitImpact, pos, Quaternion.identity, transform);
    }
    public void SmokeEffect(Vector2 pos)
    {

        Instantiate(smokeEffect, pos, Quaternion.identity, transform);
    }
    public IEnumerator DeleteEffect(GameObject gameObject, float EffectTime)
    {
     yield return new WaitForSeconds(EffectTime);
        Destroy(gameObject);
    }
}
