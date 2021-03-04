using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [Header("Enemy Spawn Information")]
    public GameObject[] EnemyPrefabs;
    public GameObject[] EnemySpawnPoints;
    public List<EnemyController> Enemies;
    public AnimationCurve SpawnAnimationCurve;
    public int enemyCount;
    private int playerCount;
    [Header("Intro")]
    public Animator battleStateManager;
    public GameObject introPanel;
    Animator introPanelAnim;
    [Header("UI")]
    public CanvasGroup theButtons;
    public Slider HealthBar;
    public Text HealthText;
    public Text BattleText;
    public CanvasGroup MainButtons;
    public CanvasGroup AttackButtons;
    [Tooltip("Used to lock enemy popup")]
    public bool LockEnemyPopup = false;
    [Header("Attack/Abilities")]
    [Tooltip("The attack script attached to the same gameObject")]
    public Attack attack;

    public string selectedTargetName;
    public Abilities selectedAttack;
    public EnemyController selectedTarget;
    public GameObject selectionCircle;
    private bool canSelectEnemy;

    public bool attacking = false;



    public enum BattleState
    {
        Begin_Battle,
        Intro,
        Player_Move,
        Player_Attack,
        Change_Control,
        Enemy_Attack,
        Battle_Result,
        Battle_End
    }
    private Dictionary<int, BattleState> battleStateHash = new Dictionary<int, BattleState>();

    public BattleState currentBattleState;


    [Header("AttackParticles")]
    public GameObject Attack1Particle;
    public GameObject Attack2Particle;
    public GameObject Attack3Particle;
    public GameObject Attack4Particle;
    private GameObject attackParticle;

    string[] Names = new string[] { "Arnita", "Kristal", "Maryjane", "Minda", "Tanner", "Beaulah", "Myrtle", "Deon", "Reggie", "Jalisa", "Myong", "Denna", "Jayson", "Mafalda"};

    public bool CanSelectEnemy
    {
        get
        {
            return canSelectEnemy;
        }
    }

    public int EnemyCount
    {
        get
        {
            return enemyCount;
        }
    }

    public int PlayerCount
    {
        get
        {
            return playerCount;
        }
    }

    void Awake()
    {
        battleStateManager = GetComponent<Animator>();
        if (battleStateManager == null)
        {
            Debug.LogError("No battleStateMachine Animator found.");
        }
        introPanelAnim = introPanel.GetComponent<Animator>();

    }

    void Start()
    {
        GameState.CurrentPlayer.ActualHealth = GameState.CurrentPlayer.Health;
        HealthText.text = GameState.CurrentPlayer.Health + "/" + GameState.CurrentPlayer.Health;
        // Calculate how many enemies 
        //enemyCount = Random.Range(1, EnemySpawnPoints.Length); //Dynamically set enemy numbers based on level/party members, stops swarming
        enemyCount = 9;
        // Spawn the enemies in 
        StartCoroutine(SpawnEnemies());
        
        GetAnimationStates();
    }

    IEnumerator SpawnEnemies()
    {
        //Spawn enemies in over time 
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy = (GameObject)Instantiate(EnemyPrefabs[0]);
            var controller = newEnemy.GetComponent<EnemyController>();
            newEnemy.name = controller.EnemyProfile.Name + " " + Names[Random.Range(1, Names.Length)];
            controller.battleManager = this;
            newEnemy.transform.position = new Vector3(10, -1, 0);
            yield return StartCoroutine(
            MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
            newEnemy.transform.parent = EnemySpawnPoints[i].transform;
            Enemies.Add(controller);
        }
        battleStateManager.SetBool("BattleReady", true);
    }

    IEnumerator MoveCharacterToPoint(GameObject destination, GameObject character)
    {
        float timer = 0f;
        Vector3 StartPosition = character.transform.position;
        if (SpawnAnimationCurve.length > 0)
        {
            while (timer < SpawnAnimationCurve.keys[SpawnAnimationCurve.length - 1].time)
            {
                character.transform.position = Vector3.Lerp(StartPosition, destination.transform.position, SpawnAnimationCurve.Evaluate(timer));

                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            character.transform.position = destination.transform.position;
        }
    }

    IEnumerator AttackTarget()
    {
        attacking = true;
        Debug.Log("Passed");
        int damageAmount = CalculateDamage(GameState.CurrentPlayer, selectedTarget);

        selectedTarget.Health -= damageAmount;

        Debug.Log(selectedTarget.name + " has " + selectedTarget.Health);
        
        switch (damageAmount) //Spawn graphic/FX here bigger damage bigger damage effect
        {
            case 5:
                attackParticle = (GameObject)GameObject.Instantiate(Attack1Particle);
                break;
            case 10:
                attackParticle = (GameObject)GameObject.Instantiate(Attack2Particle);
                break;
            case 15:
                attackParticle = (GameObject)GameObject.Instantiate(Attack3Particle);
                break;
            case 20:
                attackParticle = (GameObject)GameObject.Instantiate(Attack4Particle);
                break;
        }

        if (attackParticle != null)
        {
            attackParticle.transform.position = selectedTarget.transform.position;
        }

        yield return new WaitForSeconds(1f);
        battleStateManager.SetBool("PlayerReady", false);
        GetComponent<Attack>().hitAmount = 0;
        if (selectedTarget.Health < 1)
        {
            selectedTarget.Die();
        }
        Destroy(attackParticle);
        attacking = false;
    }

    public int CalculateDamage(Player currentPlayer, EnemyController Target)
    {
        int DamageCalc = currentPlayer.Strength + attack.hitAmount;

        return DamageCalc;
    }

    public int CalculateDamage(EnemyController currentAI, Player TargetPlayer)
    {
        int DamageCalc = currentAI.Strength + attack.hitAmount - TargetPlayer.Defense;

        Debug.Log("Dealt " + DamageCalc + " to " + TargetPlayer.name);
        return DamageCalc;
    }

    public void RunAway()
    {
        GameState.justExitedBattle = true;
        NavigationManager.NavigateTo("Overworld");
    }

    void GetAnimationStates()
    {
        foreach (BattleState state in (BattleState[])System.Enum.GetValues(typeof(BattleState)))
        {
            battleStateHash.Add(Animator.StringToHash(state.ToString()), state);
        }
    }

    public void SelectEnemy(EnemyController enemy, string name)
    {
        Debug.Log("Selected" + name);
        selectedTarget = enemy;
        selectedTargetName = name;
    }

    void DeactivateButtons() //Deactiveate the buttons, maybe change display to show enemy actions instead
    {
        //currentBattleState = BattleState.Enemy_Attack;
    }

    // Update is called once per frame
    void Update()
    {
        currentBattleState = battleStateHash[battleStateManager.GetCurrentAnimatorStateInfo(0).shortNameHash]; //Use for using animations with the battle system

        switch (currentBattleState)
        {
            case BattleState.Intro:
                introPanelAnim.SetTrigger("Intro");
                BattleText.text = "Choose an attack, Use an Item or run away";
                break;
            case BattleState.Player_Move:
                ShowMainButtons();
                if (GetComponent<Attack>().attackSelected == true)
                {
                    BattleText.text = "Now choose an enemy to attack";
                    canSelectEnemy = true;
                }
                break;
            case BattleState.Player_Attack:
                canSelectEnemy = false;
                if (!attacking)
                {
                    StartCoroutine(AttackTarget());
                }
                break;
            case BattleState.Change_Control:
                HideButtons();
                //Set buttons to inactive and change bottom pannel potencially
                break;
            case BattleState.Enemy_Attack: //Move to the Enemies controller for turn
                if (!attacking)
                {
                    if(enemyCount > 0)
                    {
                        for (int i = 0; i < enemyCount; i++)
                        {
                            Enemies[i].AI();
                        }
                    }
                    if (ContinueBattle())
                    {
                        battleStateManager.SetBool("ContinueBattle", true);
                    }
                    else
                    {
                        battleStateManager.SetBool("EndBattle", true);
                    }

                }
                break;
            case BattleState.Battle_Result:
                //After each enemies is defeated add to a resulting pool to give to the player
                break;
            case BattleState.Battle_End:
                NavigationManager.NavigateTo("Overworld");
                //Any animation and move back to Overworld
                break;
            default:
                break;
        }

        if (currentBattleState != BattleState.Player_Move)
        {
            theButtons.alpha = 0;
            theButtons.interactable = false;
            theButtons.blocksRaycasts = false;
        }
    }

    bool ContinueBattle()
    {
        //Check whether there are no players or enemies alive, if either end the battle
        attack.ResetRange();
        attack.ResetHighlightSquares();
        if (enemyCount <= 0 || playerCount <= 0)
        {
            return false;
        }
        return true;
    }

    public void HideButtons()
    {
        MainButtons.alpha = 0;
        MainButtons.interactable = false;
        MainButtons.blocksRaycasts = false;

        AttackButtons.alpha = 0;
        AttackButtons.interactable = false;
        AttackButtons.blocksRaycasts = false;
    }

    public void ShowMainButtons()
    {
        theButtons.alpha = 1;
        theButtons.interactable = true;
        theButtons.blocksRaycasts = true;
    }



}
