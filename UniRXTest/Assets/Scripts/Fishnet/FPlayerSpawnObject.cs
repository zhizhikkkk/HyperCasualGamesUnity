using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class FPlayerSpawnObject : NetworkBehaviour
{
    public GameObject objtoSpawn;
    [HideInInspector]public GameObject spawnedObject;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            GetComponent<FPlayerSpawnObject>().enabled = false;
        }
    }

    private void Update()
    {
        if(spawnedObject == null && Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawObject(objtoSpawn, transform, this);
        }
        if(spawnedObject != null && Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
    }

    [ServerRpc]

    public void SpawObject(GameObject obj,Transform player, FPlayerSpawnObject script)
    {
        GameObject spawned = Instantiate(obj,player.position + player.forward,Quaternion.identity);
        ServerManager.Spawn(spawned);
        SetSpawnedObject(spawned,script);
    }

    [ObserversRpc]
    public void SetSpawnedObject(GameObject spawned, FPlayerSpawnObject script)
    {
        script.spawnedObject = spawned;
    }

    [ServerRpc(RequireOwnership=false)]
    public void DespawnObject(GameObject obj)
    {
        ServerManager.Despawn(obj);
    }

}
