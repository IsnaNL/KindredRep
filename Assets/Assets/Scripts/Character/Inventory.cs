using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public  class Inventory : MonoBehaviour
{
    public int weaponCheck = 0;
    public List<Weapon> weaponList = new List<Weapon>();
    public Pickaxe pickaxe;
    public Shotgun shotgun;
    public JetSword sword;
    public KeyCode next = KeyCode.F;
    public KeyCode previous = KeyCode.D;
 


    public void Init()
    {
        weaponList.Clear();
        sword = GetComponentInChildren<JetSword>();
        weaponList.Add(sword);
        shotgun = GetComponentInChildren<Shotgun>();
        weaponList.Add(shotgun);
        pickaxe = GetComponentInChildren<Pickaxe>();
        weaponList.Add(pickaxe);
        GetCurrentWeapon(weaponCheck);
   
    }

    private void SwapWeapon()
    {
       
        if (Input.GetKeyDown(previous))
        {
            weaponCheck--;
        }
        if (Input.GetKeyDown(next))
        {
            weaponCheck++;
        }
       
        if (weaponCheck < 0)
        {
            weaponCheck = weaponList.Count-1;
        }
        if (weaponCheck > weaponList.Count-1)
        {
            weaponCheck = 0;
        }
       

     
        
            GetCurrentWeapon(weaponCheck);

    }
    void GetCurrentWeapon(int weaponIndex)
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
        if (Input.GetKeyDown(previous) || Input.GetKeyDown(next))
        {
            SwapWeapon();
        }
    }
}