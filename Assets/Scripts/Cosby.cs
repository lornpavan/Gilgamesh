using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosby : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        unitName = "Flame Elemental";
        unitLevel = 3;
        damage = 6;
        maxHP = 20;
        currentHP = maxHP;
        experience = unitLevel * unitLevel;
		this.spell = new SpellAttack();
		this.spell.spellName = "Hot Cosby";
		this.spell.spellDamage = this.damage / 2;
		this.spell.spellEffect = "sleep";
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentHP <= 0){
            Destroy(this);
        }
    }
}
