using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum effects
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
    public void CreateEffect(effects effect, Transform parent)
    {
        Transform tempGO = GetFromEnum(effect).transform;
        Instantiate(tempGO.gameObject, parent);
        tempGO.localPosition = Vector2.zero;
        tempGO.localRotation = Quaternion.identity;
        //tempGO.scale = Vector2.one;
    }
    public void CreateEffect(effects effect, Transform parent, bool isBound)
    {
        if (isBound)
        {
            CreateEffect(effect, parent);
        }
        else
        {
            Transform tempGO = Instantiate(GetFromEnum(effect), parent).transform;
            tempGO.SetParent(null);
        }
    }
    private GameObject GetFromEnum(effects effect)
    {
        switch (effect)
        {
            case effects.blood: return Blood;
            case effects.hit: return Hit;
            case effects.alyx_checkpoint: return AlyxCheckpoint;
            case effects.alyx_step: return AlyxStep;
            case effects.dragun_attack: return DragunAttack;
            case effects.dragun_mobility: return DragunMobility;
            case effects.bomb: return Bomb;
            default: return null;
        }
    }
}
