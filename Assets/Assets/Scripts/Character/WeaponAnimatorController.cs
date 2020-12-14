using System.Collections.Generic;
using UnityEngine;


public class WeaponAnimatorController : MonoBehaviour
{
    public List<RuntimeAnimatorController> weaponAnimationControllers;
    public List<Sprite> DefaultSpriteRendererSprites;
    private CharacterController2D player;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    // Update is called once per frame
    public void Init()
    {
        player = GetComponentInParent<CharacterController2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (player.inventory.weaponCheck == 0)
        {
            player.animator.runtimeAnimatorController = weaponAnimationControllers[0];
            sr.sprite = DefaultSpriteRendererSprites[0];

        }
        if (player.inventory.weaponCheck == 1)
        {
            player.animator.runtimeAnimatorController = weaponAnimationControllers[1];
            sr.sprite = DefaultSpriteRendererSprites[1];

        }
        if (player.inventory.weaponCheck == 2)
        {
            player.animator.runtimeAnimatorController = weaponAnimationControllers[2];
            sr.sprite = DefaultSpriteRendererSprites[0];

        }
    }
    /*public void flipScale()//for animator
    {
        if(transform.localScale.x == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
    }*/
}
