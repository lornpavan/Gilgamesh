using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameElemental : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        unitName = "Flame Elemental";
        unitLevel = 1;
        damage = 2;
        maxHP = 10;
        currentHP = maxHP;
        experience = unitLevel * unitLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentHP <= 0){
            Destroy(this);
        }
    }
}
