using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject Player;
	public GameObject FlameElemental;
	private bool spawnEnemy = false;
	private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
		//FlameElemental = GameObject.FindGameObjectWithTag("FlameElemental");
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((spawnEnemy) && (i < 2)) {
		Instantiate(FlameElemental, new Vector3(i*50+730,-8,710), Quaternion.identity);
		i++;
		}
    }
	
	void OnTriggerEnter(Collider other) {
		if(other.tag=="Player") {
			spawnEnemy = true;
		}
	}
}
