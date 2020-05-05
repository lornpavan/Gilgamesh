using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { NOCOMBAT, START, PLAYERTURN,PLAYERATTACKED, ENEMYTURN, WON, LOST, }

public class BattleSystem : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject enemySpawner2;
    public GameObject enemySpawner3;
    public GameObject enemyBattleHud;
    public GameObject playerBattleHud;
    private Animator[] flameElementalAnim;
    private Animator playerAnim;
    private int deathCount;

    public Transform enemyBattleStation;

    public Unit playerUnit;
    Unit[] enemyUnit;
    Unit selectedEnemy;

    public Text dialogueText;
    public Text levelText;

    BattleHUD playerHUD;
    BattleHUD enemyHUD;

    public static BattleState state;

    GameObject[] flameElementalArray;

    int spawnCounter = 0;
    int elementalNum;
    int createSpawner = 0;

    public bool enemyHasSpawned = false;

    public void Start()
    {
        state = BattleState.NOCOMBAT;
        Instantiate(enemySpawner2, new Vector3(746,0,740),Quaternion.identity);
        createSpawner++;
        enemyUnit = new Unit[5];
        deathCount = 0;

        GameObject playerGO = playerPrefab;
        playerUnit = playerGO.GetComponent<Unit>();

        playerHUD = playerBattleHud.GetComponent<BattleHUD>();
        playerHUD.SetHUD(playerUnit);

        enemyBattleHud.SetActive(false);
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //flameElementalAnim[] = enemyPrefab.GetComponent<Animator>();
    }

	IEnumerator SetupBattle()
	{
        dialogueText.text = "Prepare for battle!";

        playerAnim.Play("arthur_idle_01");

        if (createSpawner == 2 || createSpawner == 3 || createSpawner == 4)
        {
            mainCamera.GetComponent<Camera>().orthographicSize = 100;
        }

        elementalNum = EnemySpawner.elementalNum;
        SpawnEnemy(elementalNum);		       
		
		yield return new WaitForSeconds(2f);
		
		state = BattleState.PLAYERTURN;
		PlayerTurn(elementalNum);
    }

    public void SpawnEnemy(int elementalNum)
    {
        int i = 0;
        flameElementalArray = new GameObject[5];
        flameElementalAnim = new Animator[5];
        while (i < elementalNum)
        {
            if (elementalNum == 2)
            {
                GameObject flameElementalGO = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
                flameElementalGO.transform.parent = GameObject.FindGameObjectWithTag("EnemySpawner2").transform;
                flameElementalGO.transform.localPosition = new Vector3((i * 40) - 20, 0, 0);
                flameElementalArray[i] = flameElementalGO.gameObject;
                flameElementalAnim[i] = flameElementalArray[i].GetComponent<Animator>();
            }
            else if (elementalNum == 3)
            {
                GameObject flameElementalGO = Instantiate(enemyPrefab, transform.position, Quaternion.Euler(0,90,0)) as GameObject;
                flameElementalGO.transform.parent = GameObject.FindGameObjectWithTag("EnemySpawner3").transform;
                flameElementalGO.transform.localPosition = new Vector3((i * 40) - 20, 0, 0);
                flameElementalArray[i] = flameElementalGO.gameObject;
                flameElementalAnim[i] = flameElementalArray[i].GetComponent<Animator>();
            }            
            enemyUnit[i] = flameElementalArray[i].GetComponent<Unit>();
            //print(flameElementalArray[0]);
            i++;
        }
        enemyHasSpawned = true;
    } 
	
	IEnumerator PlayerAttack()
	{
        dialogueText.text = "Player 1 attacks!";
        playerAnim.Play("arthur_attack_01");
		selectedEnemy.TakeDamage(playerUnit.Attack());
		enemyHUD.setHP(selectedEnemy.currentHP);
		
		yield return new WaitForSeconds(2f);
        

        if (!playerUnit.isAlive)
		{
			state = BattleState.LOST;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn(elementalNum));
		}
		
	}

    IEnumerator PlayerHeal()
    {
        dialogueText.text = "Player 1 heals!";
        //selectedEnemy.TakeDamage(playerUnit.Attack());
        playerUnit.TakeHealing(playerUnit.Heal());
        playerHUD.setHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);


        if (!playerUnit.isAlive)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn(elementalNum));
        }

    }

    IEnumerator EnemyTurn(int elementalNum)
	{
        int x = 0;
        dialogueText.text = selectedEnemy.unitName + "attacks!";
		
		yield return new WaitForSeconds(1f);

        for (int i = 0; i < elementalNum; i++)
        {
            if (enemyUnit[i].isAlive)
            {
                flameElementalAnim[i].Play("Attack01");
                playerUnit.TakeDamage(enemyUnit[i].Attack());
                playerHUD.setHP(playerUnit.currentHP);
                yield return new WaitForSeconds(.5f);
            }
            else
            {
                x++;
                deathCount++;
            }
        }		
		
		if (x == elementalNum)
		{
			state = BattleState.WON;
            yield return new WaitForSeconds(1f);
			EndBattle();
		}
        else if(!selectedEnemy.isAlive)
        {
            selectedEnemy = null;
            state = BattleState.PLAYERTURN;
            PlayerTurn(elementalNum);
        }
		else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn(elementalNum);
		}
		
	}
	
	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
            EnemySpawner.spawnEnemy = false;
            spawnCounter = 0;            
            enemyHasSpawned = false;
            createSpawner += 1;
            if (createSpawner == 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            CreateSpawner(createSpawner);
            enemyBattleHud.SetActive(false);
            mainCamera.GetComponent<Camera>().orthographicSize = 60;
            delay();
            dialogueText.text = "";
            state = BattleState.NOCOMBAT;
        }
		else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
        
    }
	
	void PlayerTurn(int elementalNum)
	{
        int x = 0;
        for (int i = 0; i < elementalNum; i++)
        {
            if (!enemyUnit[i].isAlive)
            {
                x++;
                deathCount++;
            }
        }

        //yield return new WaitForSeconds(1f);
        if (deathCount == 4)
        {
            playerUnit.unitLevel += 1;
            dialogueText.text = "Level Up! Health, Damage, and Speed Increased";
            PlayerMovement.moveSpeed += 1;
            PlayerMovement.originalSpeed += 1;
            deathCount = 0;
        }



        if (x == elementalNum)
            state = BattleState.WON;
    }

        public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

        if (selectedEnemy != null)
        {
            StartCoroutine(PlayerAttack());
            state = BattleState.PLAYERATTACKED;
        }
	}

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if (selectedEnemy != null)
        {
            StartCoroutine(PlayerHeal());
            state = BattleState.PLAYERATTACKED;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHUD.setHP(playerUnit.currentHP);
        print(PlayerMovement.moveSpeed);
        levelText.text = "lvl " + playerUnit.unitLevel.ToString();

        if (EnemySpawner.spawnEnemy)
        {
            while (spawnCounter < 1)
            {
                state = BattleState.START;
                StartCoroutine(SetupBattle());
                spawnCounter++;
            }
        }

        if (enemyHasSpawned)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    float distance = Vector3.Distance(hit.transform.position, transform.position);

                    GameObject hitTag = hit.collider.transform.gameObject;
                    print(hitTag);
                    if (hitTag == flameElementalArray[0])
                    {
                        //Debug.Log(hit.transform.gameObject.name);
                        print("TARGET");
                        selectedEnemy = enemyUnit[0];

                    }
                    else if (hitTag == flameElementalArray[1])
                    {
                        //Debug.Log(hit.transform.gameObject.name);
                        print("TARGET");
                        selectedEnemy = enemyUnit[1];

                    }
                    else if (hitTag == flameElementalArray[2])
                    {
                        //Debug.Log(hit.transform.gameObject.name);
                        print("TARGET");
                        selectedEnemy = enemyUnit[2];

                    }
                }
            }
        }
       

        //Display on screen instructions
        if (state == BattleState.PLAYERTURN)
        {
            if (selectedEnemy == null)
                dialogueText.text = "Choose an enemy to attack:";
            else if (selectedEnemy != null)
            {
                dialogueText.text = "Choose an attack";
                enemyBattleHud.SetActive(true);
                enemyHUD = enemyBattleHud.GetComponent<BattleHUD>();
                enemyHUD.SetHUD(selectedEnemy);
            }
        }

        
    }

    void CreateSpawner(int track)
    {
        if (createSpawner == 2)
        {
            Instantiate(enemySpawner3, new Vector3(440, 0, 857), Quaternion.Euler(0, 90, 0));
        }
        if (createSpawner == 3)
        {
            Instantiate(enemySpawner3, new Vector3(218, -8, 416), Quaternion.Euler(0,45, 0));
        }
        if (createSpawner == 4)
        {
            Instantiate(enemySpawner3, new Vector3(748, -8, 207), Quaternion.Euler(0, 90, 0));
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2f);
    }
}
