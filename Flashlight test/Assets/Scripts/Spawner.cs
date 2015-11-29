using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Transform[] spawnLocations;
	public float spawnTime = 5f;
	public float spawnDelay = 3f;
	public GameObject[] whatToSpawnPrefab;
	public GameObject[] whatToSpawnClone;
	int enemyCounter = -1;
	
	void Start() {
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn() 
	{
		whatToSpawnClone[0] = Instantiate(whatToSpawnPrefab[0], spawnLocations[0].transform.position, Quaternion.Euler(0,0,0)) as GameObject;

	}
}
