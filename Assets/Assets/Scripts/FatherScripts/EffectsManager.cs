using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effects
{
    blood, hit, alyx_checkpoint, alyx_step, dragun_attack, dragun_mobility, bomb
}
public class EffectsManager : MonoBehaviour
{
    public static EffectsManager instance;
    public GameObject Blood;
    public GameObject Hit;
    public GameObject AlyxCheckpoint;
    public GameObject AlyxStep;
    public GameObject DragunAttack;
    public GameObject DragunMobility;
    public GameObject Bomb;
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreateEffect(Effects effect, Transform parent)
    {
        Transform tempGO = GetFromEnum(effect).transform;
        Instantiate(tempGO.gameObject, parent);
        tempGO.localPosition = Vector2.zero;
        tempGO.localRotation = Quaternion.identity;
        //tempGO.scale = Vector2.one;
    }
    public void CreateEffect(Effects effect, Transform parent, bool isBound)
    {
        if (isBound)
        {
            CreateEffect(effect, parent);
        }
        else
        {
            Transform tempGO = Instantiate(GetFromEnum(effect).transform, parent.transform.position,Quaternion.identity);
            tempGO.SetParent(null);
        }
    }
    private GameObject GetFromEnum(Effects effect)
    {
        switch (effect)
        {
            case Effects.blood: return Blood;
            case Effects.hit: return Hit;
            case Effects.alyx_checkpoint: return AlyxCheckpoint;
            case Effects.alyx_step: return AlyxStep;
            case Effects.dragun_attack: return DragunAttack;
            case Effects.dragun_mobility: return DragunMobility;
            case Effects.bomb: return Bomb;
            default: return null;
        }
    }
}
