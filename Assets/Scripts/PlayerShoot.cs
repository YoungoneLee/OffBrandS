using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerShoot : NetworkBehaviour
{
    public int damage;
    public float timeBetweenFire;
    float fireTimer;


    private void Update()
    {
        if (!base.IsOwner)
            return;

        //if(Input.GetButton("Fire1"))
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            if(fireTimer <= 0)
            {
                ShootServer(damage, Camera.main.transform.position, Camera.main.transform.forward);
            }
        }

        if(fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }

    [ServerRpc (RequireOwnership = false)]

    private void ShootServer(int damageToGive, Vector3 position, Vector3 direction)
    {
        if(Physics.Raycast(position, direction, out RaycastHit hit) && hit.transform.TryGetComponent(out PlayerHealthGuide enemyHealth))
        {
            enemyHealth.health -= damageToGive;
        }
    }
}
