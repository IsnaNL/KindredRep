using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkRed : MonoBehaviour
{
    public  SpriteRenderer sr;
    public  float blinkSpeed;

    // Start is called before the first frame update
    private void OnEnable()
    {
     
    }
    public  IEnumerator BlinkRoutine()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(blinkSpeed);
        sr.color = Color.white;
        yield return new WaitForSeconds(blinkSpeed);
        sr.color = Color.red;
        yield return new WaitForSeconds(blinkSpeed);
        sr.color = Color.white;
        yield return new WaitForSeconds(blinkSpeed);
        sr.color = Color.red;
        yield return new WaitForSeconds(blinkSpeed);
        sr.color = Color.white;
    }
    private void OnDisable()
    {
        sr.color = Color.white;
    }
}
