using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerShoot : NetworkBehaviour
{
    public int damage;
    public float timeBetweenFire;
    public ParticleSystem bulletImpactPrefab; // Prefab for the impact particle effect
    public ParticleSystem muzzleFlashPrefab; // Prefab for the muzzle flash particle effect
    float fireTimer;

    private void Update()
    {
        if (!base.IsOwner)
            return;

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (fireTimer <= 0)
            {
                ShootServer(damage, Camera.main.transform.position, Camera.main.transform.forward);
            }
        }

        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ShootServer(int damageToGive, Vector3 position, Vector3 direction)
    {
        // Instantiate the muzzle flash particle effect at the shooting position

        if (Physics.Raycast(position, direction, out RaycastHit hit))
        {
            Instantiate(muzzleFlashPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            if (hit.transform.TryGetComponent(out PlayerHealthGuide enemyHealth))
            {
                Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                enemyHealth.health -= damageToGive;
            }
        }
    }
}
