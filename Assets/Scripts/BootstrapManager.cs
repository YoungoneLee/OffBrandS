using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Managing.Scened;

public class BootstrapManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (!InstanceFinder.IsServer)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            LoadScene("Scene1");
            UnloadScene("Scene2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            LoadScene("Scene2");
            UnloadScene("Scene1");
        }

    }

    void LoadScene(string scenename)
    {
        if (!InstanceFinder.IsServer)
            return;

        SceneLoadData sld = new SceneLoadData(scenename);
        InstanceFinder.SceneManager.LoadGlobalScenes(sld);
    }

    void UnloadScene(string scenename)
    {
        if (!InstanceFinder.IsServer)
            return;

        SceneUnloadData sld = new SceneUnloadData(scenename);
        InstanceFinder.SceneManager.UnloadGlobalScenes(sld); 
    }
}
