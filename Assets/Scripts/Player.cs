using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
	private Inventory inventory;
	private int nextLevel;
	private SpellAttack[] spellList;
    private int x;
    // Start is called before the first frame update
    void Start()
    {
		unitName = "Gilgamesh";
		unitLevel = 1;
        inventory = new Inventory();
		this.experience = 0;
		this.nextLevel = 5;
		this.spellList = new SpellAttack[4];
        x = 0;

    }

    // Update is called once per frame
    void Update()
    {
        /*if (this.experience > this.nextLevel){
			this.unitLevel += 1;
			System.Random RNG = new System.Random();
			this.damage += RNG.Next(1, this.unitLevel);
			this.maxHP += RNG.Next(1, this.unitLevel * 3);
			this.currentHP = this.maxHP;
			this.nextLevel = (unitLevel * unitLevel) + ((unitLevel + 1) * (unitLevel + 1));
		}*/
        if (BattleSystem.state == BattleState.NOCOMBAT && x < 1)
        {
            StartCoroutine(regenHealth());
            x++;
        }

        if (BattleSystem.state == BattleState.START)
            x = 0;
    }

    IEnumerator regenHealth()
    {
        while (this.currentHP <= this.maxHP)
        {
            this.currentHP += 1;
            yield return new WaitForSeconds(2f);
        }
    }
}
