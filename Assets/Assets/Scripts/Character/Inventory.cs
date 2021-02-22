using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public  class Inventory : MonoBehaviour
{
    public int weaponCheck = 0;
    public List<Weapon> weaponList = new List<Weapon>();
    public WeaponAnimatorController WAC;
    public Pickaxe pickaxe;
    public Shotgun shotgun;
    public JetSword sword;
    public KeyCode Sword;
    public KeyCode Shotgun;
    public KeyCode Pickaxe;
    public KeyCode swapKeyMappingKey = KeyCode.X;
    public KeyCode nextWeapon;
    public KeyCode prevWeapon;
    public bool swapKeyMapping;
    public void Init()
    {
        swapKeyMapping = false;
        CheckKeyMapping();
        weaponList.Clear();
        sword = GetComponentInChildren<JetSword>();
        weaponList.Add(sword);
        shotgun = GetComponentInChildren<Shotgun>();
        weaponList.Add(shotgun);
        pickaxe = GetComponentInChildren<Pickaxe>();
        weaponList.Add(pickaxe);
        EnableCurrentWeapon(weaponCheck);
        WAC.SetWeapon(weaponCheck);
    }

    private void SwapWeapon()
    {
        int startingWeaponCheck = weaponCheck;
        CheckKeyMapping();
        if (!swapKeyMapping)
        {
            if (Input.GetKeyDown(Sword) && sword.enabled == false)
            {
                weaponCheck = 0;
            }
            else if (Input.GetKeyDown(Shotgun) && shotgun.enabled == false)
            {
                weaponCheck = 1;

            }
            else if (Input.GetKeyDown(Pickaxe) && pickaxe.enabled == false)
            {
                weaponCheck = 2;
            }
        }
        else
        {
            if (Input.GetKeyDown(nextWeapon))
            {
                weaponCheck++;
            }
            else if (Input.GetKeyDown(prevWeapon))
            {
                weaponCheck--;
            }
        }

        if (weaponCheck < 0)
        {
            weaponCheck = weaponList.Count - 1;
        }
        else if (weaponCheck > weaponList.Count - 1)
        {
            weaponCheck = 0;
        }
        if(weaponCheck != startingWeaponCheck)
        {
            EnableCurrentWeapon(weaponCheck);
            WAC.SetWeapon(weaponCheck);
        }
   
    }

    private void CheckKeyMapping()
    {
        if (!swapKeyMapping)
        {
            foreach (Weapon w in weaponList)
            {
                w.attack = KeyCode.D;
                w.mobilityAbility = KeyCode.F;

            }
            Sword = KeyCode.Q;
            Shotgun = KeyCode.W;
            Pickaxe = KeyCode.E;
            prevWeapon = KeyCode.None;
            nextWeapon = KeyCode.None;
        }
        else
        {
            foreach (Weapon w in weaponList)
            {
                w.attack = KeyCode.Mouse0;
                w.mobilityAbility = KeyCode.Mouse1;

            }
            Sword = KeyCode.None;
            Shotgun = KeyCode.None;
            Pickaxe = KeyCode.None;
            prevWeapon = KeyCode.Q;
            nextWeapon = KeyCode.E;
        }
    }

    void EnableCurrentWeapon(int weaponIndex)
    {   
        foreach (Weapon w in weaponList)
        {
            w.enabled = false;
            if (weaponList.IndexOf(w) == weaponIndex)
            {
                w.enabled = true;
             
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(Sword) || Input.GetKeyDown(Shotgun) || Input.GetKeyDown(Pickaxe) || Input.GetKeyDown(nextWeapon) || Input.GetKeyDown(prevWeapon))
        {
            SwapWeapon();
        }
        if (Input.GetKeyDown(swapKeyMappingKey))
        {
            swapKeyMapping = !swapKeyMapping;
            CheckKeyMapping();
           
        }
    }
}