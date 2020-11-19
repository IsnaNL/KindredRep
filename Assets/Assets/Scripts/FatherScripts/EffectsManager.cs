using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager e_Instance;
    public GameObject BloodEffect;
   // public GameObject ShotGunEffect;
    
    // Start is called before the first frame update
    public void Start()
    {
       
        e_Instance = this;   
        
    }

    // Update is called once per frame
    public void BloodHitEffect(Transform transform)
    {
    
     GameObject currentBlood = Instantiate(BloodEffect, transform);
     
     StartCoroutine(DeleteEffect(currentBlood,1f));
    }
    public IEnumerator DeleteEffect(GameObject gameObject, float EffectTime)
    {
     yield return new WaitForSeconds(EffectTime);
     Destroy(gameObject);
    }
}
