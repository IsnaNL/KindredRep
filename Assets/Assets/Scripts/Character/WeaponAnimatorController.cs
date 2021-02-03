using System.Collections.Generic;
using UnityEngine;


public class WeaponAnimatorController : MonoBehaviour
{
    public List<RuntimeAnimatorController> weaponAnimationControllers;
    public List<Sprite> DefaultSpriteRendererSprites;
    public CharacterController2D player;
    private SpriteRenderer sr;
    private HUD hud;
    public void Init()
    {
        hud = FindObjectOfType<HUD>();
        sr = GetComponent<SpriteRenderer>();
    }
   public void SetWeapon(int weaponNum)
    {
        player.animator.runtimeAnimatorController = weaponAnimationControllers[weaponNum];
        sr.sprite = DefaultSpriteRendererSprites[weaponNum];
        hud.setWeapon(weaponNum);
    }

  
}
