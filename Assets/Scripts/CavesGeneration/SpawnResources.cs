using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpawnResources : MonoBehaviour
{
    public List<GameObject> prefabs;
    private float spawnDelay = 240;
    private Vector3 position;
    private void Start()
    {
        System.Random rd = new System.Random();
        for (var i = 0; i < 2000; i++)
        {
            var y = rd.Next(-100, 100);
            var x = rd.Next(-50, 50);
            var z = rd.Next(-50, 50);
            var obj = Instantiate(prefabs[rd.Next(0, prefabs.Count)], new Vector3(x, y, z), Quaternion.identity) as GameObject;
            obj.gameObject.AddComponent<Falled>();
        }
        StartCoroutine("StartSpawn");

    }
    private void Update()
    {
        position = this.transform.position;
    }
    IEnumerator StartSpawn()
    {
        while (true)
        {
            System.Random rd = new System.Random();
            
            for (var i = 0; i < 1000; i++)
            {

                var y = rd.Next((int)position.y - 70, (int)position.y + 70);
                var x = rd.Next((int)position.x - 60, (int)position.x + 60);
                var z = rd.Next((int)position.z - 60, (int)position.z + 60);
                var obj = Instantiate(prefabs[rd.Next(0, prefabs.Count)], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                obj.gameObject.AddComponent<Falled>();
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
