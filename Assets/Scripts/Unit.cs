using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	public int experience;
    public string unitName;
	public int unitLevel;
	public bool isAlive;
	public int damage; //Attack Damage
	protected SpellAttack spell;
	
	public int maxHP;
	public int currentHP;
	/**
	* Reduces HP of unit.
	* Returns true if unit dies as a result.
	* @param dmg - (integer) damage taken
	*/
	public int TakeDamage(int dmg)
	{
		this.currentHP -= dmg;
		
		//Update will take care of removing the bugger when dead.
		//This can be chain called:
		//Player.experience += Critter.TakeDamage(Player.Attack());
		
		if(currentHP <= 0) {
			//If critter dies, return experience.
			isAlive = false;
			return experience;
		}
		else {
			//Otherwise return 0
			return 0;
		}
		
	}
	/**
	* Returns a random number between 0 and 'damage' variable
	*/
	public int Attack(){
		System.Random RNG = new System.Random();
		return RNG.Next(0, damage);
	}
	
	public object[] SpecialAttack() {
		object[] returnArray = new object[3];
		returnArray[0] = this.spell.spellName;
		returnArray[1] = this.spell.spellDamage;
		returnArray[2] = this.spell.spellEffect;
		return returnArray;
	}
	void update(){
		if (this.currentHP <= 0){
			Destroy(this);
		}
	}
}
