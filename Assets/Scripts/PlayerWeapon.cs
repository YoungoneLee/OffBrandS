using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] private List<APlayerWeapon> weapons = new List<APlayerWeapon>();

    [SerializeField] private APlayerWeapon currentWeapon;


    public override void OnStartClient()
    {
        base.OnStartClient();

        if(!base.IsOwner)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            FireWeapon();


        if (Input.GetKeyDown(KeyCode.Alpha1))
            initializeWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            initializeWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            initializeWeapon(2);
    }


    public void initializeWeapons(Transform parentOfWeapons)
    {
        for(int i = 0; i < weapons.Count; i++)
        {
            weapons[i].transform.SetParent(parentOfWeapons);
        }

        initializeWeapon(0);
    }

    private void initializeWeapon(int weaponIndex)
    {
        for(int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

        if (weapons.Count > weaponIndex)
        {
            currentWeapon = weapons[weaponIndex];
            currentWeapon.gameObject.SetActive(true);

        }
    }

    private void FireWeapon()
    {
        currentWeapon.Fire();
    }
}