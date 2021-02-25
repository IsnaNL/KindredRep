using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager e_Instance;
    public GameObject BloodEffect;
    public GameObject hitImpact;
    public GameObject checkPointEffect;
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
    public void CheckPointEffect(Vector2 pos, Transform parent)
    {
        Instantiate(checkPointEffect, pos, Quaternion.identity, parent);
    }
    public IEnumerator DeleteEffect(GameObject gameObject, float EffectTime)
    {
     yield return new WaitForSeconds(EffectTime);
        Destroy(gameObject);
    }
}
