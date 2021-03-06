using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager e_Instance;
    public GameObject BloodEffect;
    public GameObject hitImpact;
    public GameObject checkPointEffect;
    public GameObject StepEffect;
    public void Start()
    {
        e_Instance = this;
    }
    public void CreateEffect(Vector2 pos, GameObject Effect, Transform parent, bool flip)
    {
        Instantiate(Effect, pos, Quaternion.Euler(Vector3.forward * (flip ? 180 : 0)), parent);
    }
    public void CreateEffect(Vector2 pos, GameObject Effect, Transform parent)
    {
        Instantiate(Effect, pos, Quaternion.identity, parent);
    }
    public void CreateEffect(Vector2 pos, GameObject Effect)
    {
        Instantiate(Effect, pos, Quaternion.identity);
    }
}
