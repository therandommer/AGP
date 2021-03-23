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
    public CanvasGroup FinalAttackButton;
    [Tooltip("Used to lock enemy popup")]
    public bool LockEnemyPopup = false;
    [Header("Attack/Abilities")]
    [Tooltip("The attack script attached to the same gameObject")]
    public Attack attack;
    [Header("Debug variables")]
    public string selectedTargetName;
    public Abilities selectedAttack;
    public EnemyController selectedTarget;
    public GameObject selectionCircle;
    public List<EnemyController> EnemiesToDamage;
    private bool canSelectEnemy;

    public bool attacking = false;
    [Tooltip("Use if you want to have player go first in the fight")]
    public bool DebugPlayerAttack = false;

    //used for attack movement lerps
    Vector2 posA = Vector2.zero;
    Vector2 posB = Vector2.zero;
    [SerializeField]
    float moveSpeed = 1.0f; //time taken to move back or forward
    [SerializeField]
    float stopTime = 1.0f; //time taken before moving back
    [SerializeField]
    float attackGap = 0.35f; //distance in X axis between player and enemy

    public enum BattleState
    {
        Begin_Battle,
        Intro,
        Player_Move,
        Player_Attack,
        Change_Control,
        Resolve_Attacks,
        Battle_Result,
        Battle_End
    }
    private Dictionary<int, BattleState> battleStateHash = new Dictionary<int, BattleState>();

    public BattleState currentBattleState;


    public List<GameObject> ListOfEntities = new List<GameObject>();

    [Header("AttackParticles")]
    public GameObject Attack1Particle;
    public GameObject Attack2Particle;
    public GameObject Attack3Particle;
    public GameObject Attack4Particle;
    private GameObject attackParticle;

    string[] Names = new string[] { "Arnita", "Kristal", "Maryjane", "Minda", "Tanner", "Beaulah", "Myrtle", "Deon", "Reggie", "Jalisa", "Myong", "Denna", "Jayson", "Mafalda" };

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
        HealthText.text = GameState.CurrentPlayer.stats.Health + "/" + GameState.CurrentPlayer.stats.MaxHealth;
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
            ListOfEntities.Add(newEnemy);

            var controller = newEnemy.GetComponent<EnemyController>();
            controller.stats.Speed = Random.Range(5, 30);
            newEnemy.name = controller.EnemyProfile.Name + " " + Names[Random.Range(1, Names.Length)];
            controller.battleManager = this;
            newEnemy.transform.position = new Vector3(10, -1, 0);
            yield return StartCoroutine(
            MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
            newEnemy.transform.parent = EnemySpawnPoints[i].transform;
            Enemies.Add(controller);
        }
        battleStateManager.SetBool("BattleReady", true);

        ListOfEntities.Add(GameState.PlayerObject);

        ListOfEntities.Sort(delegate (GameObject a, GameObject b)
        {

            return (a.GetComponent<StatsHolder>().CompareTo(b.GetComponent<StatsHolder>()));

        });

        foreach (GameObject entity in ListOfEntities)
        {
            Debug.Log(entity.name + " has " + entity.GetComponent<StatsHolder>().Speed + " Speed");
        }

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
        int damageAmount = CalculateDamage(GameState.CurrentPlayer, selectedTarget);

        for (int i = 0; i < EnemiesToDamage.Count; i++)
        {
            Debug.Log(EnemiesToDamage[i].gameObject.name + " has " + EnemiesToDamage[i].stats.Health + " before damage");
            //GameState.CurrentPlayer.transform.position = new Vector2(Mathf.PingPong(Time.deltaTime / 50, EnemiesToDamage[i].gameObject.transform.position.x - GameState.CurrentPlayer.transform.position.x), Mathf.PingPong(Time.time, EnemiesToDamage[i].gameObject.transform.position.y - GameState.CurrentPlayer.transform.position.y));
            posA = GameState.CurrentPlayer.transform.position;
            posB = EnemiesToDamage[i].gameObject.transform.position;
            float tmpTimer = 0.0f;
            if (selectedAttack.isMelee)
            {
                posB = new Vector2(posB.x - attackGap, posB.y); //adds attack gap to currently selected attack
                GameState.CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                GameState.CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
                while (tmpTimer < moveSpeed)
                {
                    GameState.CurrentPlayer.transform.position = Vector2.Lerp(posA, posB, tmpTimer / moveSpeed);
                    tmpTimer += Time.deltaTime;
                    yield return null;
                }
                GameState.CurrentPlayer.transform.position = new Vector2(posB.x, posB.y + 0.365f); //ensures player is actually at the position required
                GameState.CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
            }
            if (!selectedAttack.isMelee)
            {
                GameState.CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
                GameState.CurrentPlayer.transform.position = new Vector2(posA.x, posA.y + 0.365f);
                attackParticle = GameObject.Instantiate(selectedAttack.CastEffect);
                attackParticle.GetComponent<Effects>().SetPosA(posA);
                attackParticle.GetComponent<Effects>().SetPosB(posB);
                attackParticle.GetComponent<Effects>().InitiateProjectile();
            }
            EnemiesToDamage[i].stats.Health -= damageAmount;
            Debug.Log("Attacked " + EnemiesToDamage[i].gameObject.name + " with " + damageAmount + " damage");
            Debug.Log(EnemiesToDamage[i].gameObject.name + " has " + EnemiesToDamage[i].stats.Health + " health left");
            EnemiesToDamage[i].UpdateUI(); //updates health slider to be accurate with current health bar + other things
            EnemiesToDamage[i].gameObject.SendMessage("UpdateAnimState", "isHit");
            yield return new WaitForSeconds(stopTime + 0.3f);
            EnemiesToDamage[i].gameObject.SendMessage("EnableDamageValues", damageAmount);
            tmpTimer = 0.0f;
            if (selectedAttack.isMelee)
            {
                GameState.CurrentPlayer.transform.position = new Vector2(posB.x, posB.y); //resetting player position to adjust for animation offset
                GameState.CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                while (tmpTimer < moveSpeed)
                {
                    GameState.CurrentPlayer.transform.position = Vector2.Lerp(posB, posA, tmpTimer / moveSpeed);
                    tmpTimer += Time.deltaTime;
                    yield return null;
                }
                GameState.CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
            }
            GameState.CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
            GameState.CurrentPlayer.transform.position = posA; //fully resets player position
            //might need to change this for ranged and have enemy intereaction on projectile hit?
            attackParticle = GameObject.Instantiate(selectedAttack.CastEffect, EnemiesToDamage[i].gameObject.transform.position, Quaternion.identity); //should instantiate the correct effect which does its thing then destroys itself
        }
        ///Add in a coroutine to slowly move the slider to the value instead of just setting it
        attack.EnemyHealthSlider.value = selectedTarget.stats.Health / (float)selectedTarget.EnemyProfile.maxHealth;
        //Set the health text
        attack.HealthText.text = selectedTarget.stats.Health + "/" + selectedTarget.EnemyProfile.maxHealth;

        /*
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
        }*/

        if (attackParticle != null)
        {
            attackParticle.transform.position = selectedTarget.transform.position;
        }

        yield return new WaitForSeconds(1f);
        battleStateManager.SetBool("PlayerReady", false);
        attack.ResetRange();
        attack.ResetHighlightSquares();
        attack.ResetTargetRecticle();
        attack.ResetEnemiesToDamage();
        attack.ResetSelectionCircle();
        attack.EnemyPopupCanvas.alpha = 0;
        GetComponent<Attack>().hitAmount = 0;
        for (int i = 0; i < EnemiesToDamage.Count; i++)
        {
            if (EnemiesToDamage[i].stats.Health < 1)
            {
                EnemiesToDamage[i].Die();
            }
        }
        //Destroy(attackParticle);
        attacking = false;
    }

    public int CalculateDamage(PlayerController currentPlayer, EnemyController Target)
    {
        int DamageCalc = currentPlayer.stats.Strength + attack.hitAmount;

        return DamageCalc;
    }

    public int CalculateDamage(EnemyController currentAI, PlayerController TargetPlayer)
    {
        int DamageCalc = currentAI.stats.Strength + attack.hitAmount - TargetPlayer.stats.Defense;

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

    public void ShowFinalAttackButton()
    {
        FinalAttackButton.alpha = 1;
        FinalAttackButton.interactable = true;
        FinalAttackButton.blocksRaycasts = true;
    }
    public void HideFinalAttackButton()
    {
        FinalAttackButton.alpha = 0;
        FinalAttackButton.interactable = false;
        FinalAttackButton.blocksRaycasts = false;
    }

    public void ChangeStateToAttack()//Link this to button 
    {
        attack.attackSelected = false; //Deselect attack to stop potencial loop later
        battleStateManager.SetBool("PlayerReady", true); //Player is ready to attack chosen targets with chosen ability
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
                if (GetComponent<Attack>().attackSelected == true)
                {
                    BattleText.text = "Now choose an enemy to attack";
                    canSelectEnemy = true;
                }
                break;
            case BattleState.Player_Attack:
                canSelectEnemy = false;
                if (DebugPlayerAttack)
                {
                    if (!attacking)
                    {
                        StartCoroutine(AttackTarget());
                    }
                }
                else
                {
                    battleStateManager.SetBool("PlayerReady", false);
                }
                break;
            case BattleState.Change_Control:
                HideButtons();
                //Set buttons to inactive and change bottom pannel potencially
                break;
            case BattleState.Resolve_Attacks: //Move to the Enemies controller for turn
                if (!attacking)
                {

                    foreach (GameObject Entity in ListOfEntities)
                    {
                        if(Entity.GetComponent<StatsHolder>().isPlayer)
                        {
                            StartCoroutine(AttackTarget());
                        }
                        else if (Entity.GetComponent<StatsHolder>().isEnemy)
                        {
                            Entity.GetComponent<EnemyController>().AI();
                        }
                    }

                    /*
                    if (enemyCount > 0)
                    {
                        for (int i = 0; i < Enemies.Count; i++)
                        {
                            if (Enemies[i] != null)
                                Enemies[i].AI();
                        }
                    }
                    */
                    if (ContinueBattle())
                    {
                        ShowMainButtons();
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
        else if (currentBattleState == BattleState.Player_Move)
        {
            theButtons.alpha = 1;
            theButtons.interactable = true;
            theButtons.blocksRaycasts = true;
        }

    }

    bool ContinueBattle()
    {
        //Check whether there are no players or enemies alive, if either end the battle
        battleStateManager.SetBool("ContinueBattle", false); //Reset bool from previous check to stop looping

        attack.ResetRange();
        attack.ResetHighlightSquares();
        attack.HidePopup();

        if (enemyCount == 0)
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
        MainButtons.alpha = 1;
        MainButtons.interactable = true;
        MainButtons.blocksRaycasts = true;
    }

    public float GetStopTime()
    {
        return stopTime;
    }
}
