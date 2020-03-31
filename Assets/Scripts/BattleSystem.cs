using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { NOCOMBAT, START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	
	public Transform playerBattleStation;
	public Transform enemyBattleStation;
	
	public Unit playerUnit;
	public Unit enemyUnit;
	
	public Text dialogueText;
	
	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;
	
	public BattleState state;
	

	public BattleState getState()
	{
		return state;
	}
	
	public void Start()
    {
        state = BattleState.START;
		StartCoroutine(SetupBattle());	
    }
	
	IEnumerator SetupBattle()
	{
		GameObject playerGO = playerPrefab;
		playerUnit = playerGO.GetComponent<Unit>();
		
		GameObject enemyGO = enemyPrefab;
		enemyUnit = enemyGO.GetComponent<Unit>();
		
		dialogueText.text = enemyUnit.unitName;
		//Instantiate(playerPrefab,playerBattleStation);
		int i = 0;
		while (i<2) {
		Instantiate(enemyPrefab, new Vector3(i*50+730,-8,710), Quaternion.identity);
		i++;
		}
		
		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);
		
		yield return new WaitForSeconds(2f);
		
		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}
	
	IEnumerator PlayerAttack()
	{
		enemyHUD.setHP(enemyUnit.currentHP);
		playerUnit.experience += enemyUnit.TakeDamage(playerUnit.Attack());
		
		yield return new WaitForSeconds(2f);
		
		
		if(!playerUnit.isAlive)
		{
			state = BattleState.LOST;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
		
	}
	
	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + "attacks!";
		
		yield return new WaitForSeconds(1f);
		
		enemyUnit.TakeDamage(playerUnit.Attack());
		playerHUD.setHP(playerUnit.currentHP);
		
		yield return new WaitForSeconds(1f);
		
		
		if (!enemyUnit.isAlive)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}
		
	}
	
	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		}
		else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}
	
	void PlayerTurn()
	{
		dialogueText.text = "Choose an action";
	}
	
	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;
		
		StartCoroutine(PlayerAttack());
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
