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
	
	Unit playerUnit;
	Unit enemyUnit;
	
	public Text dialogueText;
	
	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;
	
	public BattleState state;
	
	
    // Start is called before the first frame update
	/*void Start()
	{
		state = BattleState.NOCOMBAT;
	}*/

	public BattleState getState()
	{
		return state;
	}
	
    /*void Start()
    {
        state = BattleState.NOCOMBAT;
		//StartCoroutine(SetupBattle());
		int i = 0;
    }*/
	
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
		
		//dialogueText.text = enemyUnit.unitName;
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
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
		enemyHUD.setHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";
		
		yield return new WaitForSeconds(2f);
		
		if(isDead)
		{
			state = BattleState.WON;
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
		
		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
		playerHUD.setHP(playerUnit.currentHP);
		
		yield return new WaitForSeconds(1f);
		
		if (isDead)
		{
			state = BattleState.LOST;
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
