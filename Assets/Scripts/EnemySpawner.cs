using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static bool spawnEnemy = false;
    int x;
    public static int elementalNum;
    public static string optionInterval
    {
        get
        {
            return elementalNum.ToString();
        }
        set
        {
            elementalNum = Convert.ToInt32(value);
        }
    }

    void Update()
    {
        if (BattleSystem.state == BattleState.WON)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        x = 0;
            if (other.tag == "Player")
            {
                if (x < 1)
                    {
                    spawnEnemy = true;
                    if (this.tag == "EnemySpawner2")
                    {
                        elementalNum = 2;
                    }
                    if (this.tag == "EnemySpawner3")
                    {
                        elementalNum = 3;
                    }
                    x++;
                }
            }
    }

    void createSpawner()
    {

    }  

}
