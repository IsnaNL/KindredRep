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
    public KeyCode Sword = KeyCode.Q;
    public KeyCode Shotgun = KeyCode.W;
    public KeyCode Pickaxe = KeyCode.E;
    public void Init()
    {
       
        Sword = KeyCode.Q;
        Shotgun = KeyCode.W;
        Pickaxe = KeyCode.E;
        weaponList.Clear();
        sword = GetComponentInChildren<JetSword>();
        weaponList.Add(sword);
        shotgun = GetComponentInChildren<Shotgun>();
        weaponList.Add(shotgun);
        pickaxe = GetComponentInChildren<Pickaxe>();
        weaponList.Add(pickaxe);
        enableCurrentWeapon(weaponCheck);
        WAC.SetWeapon(weaponCheck);
    }

    private void SwapWeapon()
    {

     
        if (Input.GetKeyDown(Sword))
        {
            weaponCheck = 0;
        }
        else if (Input.GetKeyDown(Shotgun))
        {
            weaponCheck = 1;

        }
        else if (Input.GetKeyDown(Pickaxe))
        {
            weaponCheck = 2;
        }
        if (weaponCheck < 0)
        {
            weaponCheck = weaponList.Count-1;
        }
        else if (weaponCheck > weaponList.Count-1)
        {
            weaponCheck = 0;
        }             
            enableCurrentWeapon(weaponCheck);
            WAC.SetWeapon(weaponCheck);
    }
    void enableCurrentWeapon(int weaponIndex)
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
        if (Input.GetKeyDown(Sword) || Input.GetKeyDown(Shotgun) || Input.GetKeyDown(Pickaxe))
        {
            SwapWeapon();
        }
    }
}