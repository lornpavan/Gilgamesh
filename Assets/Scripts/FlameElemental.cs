using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameElemental : Unit
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
                
        unitName = "Flame Elemental";
        unitLevel = 1;
        damage = 2;
        maxHP = 10;
        currentHP = maxHP;
        experience = unitLevel * unitLevel;
		this.spell = new SpellAttack();
		this.spell.spellName = "Fireball";
		this.spell.spellDamage = this.damage * 2;
		this.spell.spellEffect = "burn";
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentHP <= 0){
            anim = this.GetComponent<Animator>();
            anim.Play("die");
            StartCoroutine(delay());
           // Destroy(this);
        }
    }

    IEnumerator delay()
    {
        //paused = true;
        yield return new WaitForSeconds(2f);
        //paused = false;
    }
}
