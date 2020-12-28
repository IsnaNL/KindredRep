using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager e_Instance;
    public GameObject BloodEffect;
    public GameObject HitImpact;
   // public GameObject ShotGunEffect;
    
    // Start is called before the first frame update
    public void Start()
    {
       
        e_Instance = this;   
        
    }

    // Update is called once per frame
    public void BloodHitEffect(Vector2 pos)
    {     
            Instantiate(BloodEffect, pos,Quaternion.identity,transform);   
    }
    public void HitEffect(Vector2 pos)
    {

        Instantiate(HitImpact, pos, Quaternion.identity, transform);
    }
    public IEnumerator DeleteEffect(GameObject gameObject, float EffectTime)
    {
     yield return new WaitForSeconds(EffectTime);
        Destroy(gameObject);
    }
}
