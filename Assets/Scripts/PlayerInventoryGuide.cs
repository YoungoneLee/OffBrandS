using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerInventoryGuide : NetworkBehaviour
{
    [Header("Inventory settings")]
    public List<InventoryObject> inventoryObjects = new List<InventoryObject>();
    GameObject invPanel;
    Transform invObjectHolder;
    [SerializeField] GameObject invCanvasObject;
    [SerializeField] KeyCode inventoryButton = KeyCode.Tab;

    [Header("Pickup settings")]
    [SerializeField] LayerMask pickupLayer;
    [SerializeField] float pickupDistance;
    [SerializeField] KeyCode pickupButton = KeyCode.E;

    Camera cam;
    Transform worldObjectHolder;

    public override void OnStartClient()
    {

        base.OnStartClient();

        if (!base.IsOwner)
        {
            enabled = false;
            return;
        }

        cam = Camera.main;
        worldObjectHolder = GameObject.FindGameObjectWithTag("WorldObjects").transform;
        invPanel = GameObject.FindGameObjectWithTag("InventoryPanel");
        invObjectHolder = GameObject.FindGameObjectWithTag("InventoryObjectHolder").transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pickupButton))
            Pickup();

        if (Input.GetKeyDown(inventoryButton))
            ToggleInventory();

    }

    void Pickup()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, pickupDistance, pickupLayer))
        {
            if(hit.transform.GetComponent<GroundItemGuide>() == null)
                return;

            AddToInventory(hit.transform.GetComponent<GroundItemGuide>().itemScriptable);
            DespawnObject(hit.transform.gameObject);
        }
    }

    void AddToInventory(Item newItem)
    {
        foreach(InventoryObject invObj in inventoryObjects)
        {
            if(invObj.item == newItem)
            {
                invObj.amount++;
                return;
            }
        }

        inventoryObjects.Add(new InventoryObject() {item = newItem, amount = 1 });
    }

    [ServerRpc(RequireOwnership = false)]
    void DespawnObject(GameObject objToDespawn)
    {
        ServerManager.Despawn(objToDespawn, DespawnType.Destroy);
    }

    void ToggleInventory()
    {
        if(invPanel.activeSelf)
        {
            invPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!invPanel.activeSelf)
        {
            invPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    [System.Serializable]
    public class InventoryObject
    {
        public Item item;
        public int amount;
    }
}
