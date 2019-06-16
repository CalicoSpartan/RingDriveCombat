using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class LevelManager : MonoBehaviour {

    // Use this for initialization
    public GameObject enemy;
    int currentWave = 1;
    int enemiesThisWave = 0;
    int currentIndex = 0;
    bool spawningWave = true;

    public List<Bridge> bridges;
    public List<int> enemiesInWave = new List<int>(10);
    public List<int> maxVertTurnSpeed = new List<int>(10);
    public List<int> maxHorzTurnSpeed = new List<int>(10);
    public List<float> shotFrequency = new List<float>(10);
    public List<float> shotDelay = new List<float>(10);
    public List<float> startingHealth = new List<float>(10);

    

    void Start () {
       
        bridges = Object.FindObjectsOfType<Bridge>().ToList<Bridge>();
        Debug.Log(bridges.Count);
    }
	
	// Update is called once per frame
	void Update () {
		if (spawningWave)
        {
            if (bridges[currentIndex].occupied == false && Vector3.Magnitude(transform.position - bridges[currentIndex].transform.position) < 30f)
            {
                EnemyController en1 =  Instantiate(enemy, bridges[currentIndex].spawnPoint1.position, bridges[currentIndex].transform.rotation).GetComponent<EnemyController>();
                en1.transform.SetParent(bridges[currentIndex].transform, true);
                EnemyController en2 = Instantiate(enemy, bridges[currentIndex].spawnPoint2.position, bridges[currentIndex].transform.rotation).GetComponent<EnemyController>();
                en2.transform.SetParent(bridges[currentIndex].transform, true);
                EnemyController en3 = Instantiate(enemy, bridges[currentIndex].spawnPoint3.position, bridges[currentIndex].transform.rotation).GetComponent<EnemyController>();
                en3.transform.SetParent(bridges[currentIndex].transform, true);

                bridges[currentIndex].occupied = true;
                currentIndex += 1;
            }
            if (currentIndex == bridges.Count - 1)
            {
                spawningWave = false;
            }
            /*
            for (int i = 0; i < objects.Length;i++)
            {
                if (objects[i].occupied == false && Vector3.SqrMagnitude(transform.position - objects[i].transform.position) < 30f)
                {
                    Instantiate(enemy, objects[i].spawnPoint1.position, objects[i].transform.rotation, objects[i].transform);
                    Instantiate(enemy, objects[i].spawnPoint2.position, objects[i].transform.rotation, objects[i].transform);
                    Instantiate(enemy, objects[i].spawnPoint3.position, objects[i].transform.rotation, objects[i].transform);
                    objects[i].occupied = true;
                }

            }
            */
        }
	}

    public void SpawnWave()
    {

    }
}
