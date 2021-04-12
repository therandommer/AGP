using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject[] PlayerSpawnPoints;
    [Header("UI")]
    public CanvasGroup theButtons;
    public Slider HealthBar;
    public Text HealthText;
    public Text BattleText;
    public CanvasGroup CombatTextCanvas;
    public TMP_Text CombatText;
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
    public GameObject selectionCircle;
    private bool canSelectEnemy;
    private bool HideUI = false;

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
    public List<GameObject> playerParty;

    public enum BattleState
    {
        Begin_Battle,
        Intro,
        Player_Move,
        Player_Attack,
        Change_Control,
        Resolve_Attacks,
        WaitForAttacks,
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
        for (int i = 0; i < GameState.PlayersToSpawn.Length; i++)
        {
            if (GameState.PlayersToSpawn[i].GetComponent<PlayerController>().stats.PlayerProfile == GameState.PlayerObject.GetComponent<PlayerController>().stats.PlayerProfile)
            {
                Debug.Log("Just moving player");
                GameObject.Find("Player").transform.SetParent(PlayerSpawnPoints[0].transform);
                GameObject.Find("Player").transform.position = PlayerSpawnPoints[0].transform.position;
                GameObject.Find("Player").GetComponent<PlayerMovement>().CantMove = true;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                Debug.Log(GameState.PlayersToSpawn[i].name + " is not the same as " + GameState.PlayersToSpawn[i].name);
                GameObject SpawnPlayer = Instantiate(GameState.PlayersToSpawn[i], Vector3.zero, Quaternion.identity);
                SpawnPlayer.name = GameState.PlayersToSpawn[i].name;
                SpawnPlayer.transform.SetParent(PlayerSpawnPoints[i].transform);
                SpawnPlayer.transform.position = PlayerSpawnPoints[i].transform.position;
                SpawnPlayer.GetComponent<PlayerMovement>().CantMove = true;
                SpawnPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                SpawnPlayer.GetComponent<SpriteRenderer>().flipX = true;
                ListOfEntities.Add(SpawnPlayer);
                playerParty.Add(SpawnPlayer);
            }

        }
        GameState.CurrentPlayer = GameObject.Find("Player").GetComponent<PlayerController>();
        GameState.PlayerParty = playerParty;
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
            controller.stats.Speed = Random.Range(5, 10);
            newEnemy.name = controller.EnemyProfile.Name + " " + Names[Random.Range(1, Names.Length)];
            controller.battleManager = this;
            newEnemy.transform.position = new Vector3(10, -1, 0);
            yield return StartCoroutine(
            MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
            newEnemy.transform.parent = EnemySpawnPoints[i].transform;
            Enemies.Add(controller);
        }
        battleStateManager.SetBool("BattleReady", true);

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

    IEnumerator ShowResults()//Add in wait for click then end the scene
    {
        yield return new WaitForSeconds(2);
        battleStateManager.SetBool("EndBattle", true);
    }

    IEnumerator ResolvesAttacks()
    {
        Debug.Log("Resolving attacks");
        attacking = true;
        foreach (GameObject Entity in ListOfEntities)
        {
            if (Entity != null)
            {
                if (Entity.GetComponent<StatsHolder>().isPlayer)
                {
                    StartCoroutine(AttackTarget(Entity));
                }
                else if (Entity.GetComponent<StatsHolder>().isEnemy)
                {
                    Entity.GetComponent<EnemyController>().AI();
                }

                bool done = false;
                while (!done)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        done = true;
                    }
                    yield return null;
                }
            }
        }

        battleStateManager.SetBool("FinishedAllAttacks", true);

        yield return new WaitForSeconds(1f);

        if (ContinueBattle())
        {
            attacking = false;
            battleStateManager.SetBool("ContinueBattle", true);
            CombatTextCanvas.alpha = 0;
            ShowMainButtons();
        }
        else
        {
            attacking = false;
            battleStateManager.SetBool("EndBattle", true);
        }
    }

    IEnumerator AttackTarget(GameObject PlayerToAttack)
    {
        attacking = true;

        PlayerController CurrentPlayer = PlayerToAttack.GetComponent<PlayerController>();
        for (int i = 0; i < CurrentPlayer.EnemiesToDamage.Count; i++)
        {
            if (CurrentPlayer.EnemiesToDamage[i] != null)
            {
                int damageAmount = CalculateDamage(CurrentPlayer, CurrentPlayer.selectedTarget);
                Debug.Log(CurrentPlayer.EnemiesToDamage[i].gameObject.name + " has " + CurrentPlayer.EnemiesToDamage[i].stats.Health + " before damage");
                //GameState.CurrentPlayer.transform.position = new Vector2(Mathf.PingPong(Time.deltaTime / 50, EnemiesToDamage[i].gameObject.transform.position.x - GameState.CurrentPlayer.transform.position.x), Mathf.PingPong(Time.time, EnemiesToDamage[i].gameObject.transform.position.y - GameState.CurrentPlayer.transform.position.y));
                posA = CurrentPlayer.transform.position;
                posB = CurrentPlayer.EnemiesToDamage[i].gameObject.transform.position;
                float tmpTimer = 0.0f;
                if (CurrentPlayer.selectedAttack.isMelee)
                {
                    posB = new Vector2(posB.x - attackGap, posB.y); //adds attack gap to currently selected attack
                    CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                    CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
                    while (tmpTimer < moveSpeed)
                    {
                        CurrentPlayer.transform.position = Vector2.Lerp(posA, posB, tmpTimer / moveSpeed);
                        tmpTimer += Time.deltaTime;
                        yield return null;
                    }
                    CurrentPlayer.transform.position = new Vector2(posB.x, posB.y + 0.365f); //ensures player is actually at the position required
                    CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                }
                if (!CurrentPlayer.selectedAttack.isMelee)
                {
                    CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
                    CurrentPlayer.transform.position = new Vector2(posA.x, posA.y + 0.365f);
                    attackParticle = GameObject.Instantiate(CurrentPlayer.selectedAttack.CastEffect);
                    attackParticle.GetComponent<Effects>().SetPosA(posA);
                    attackParticle.GetComponent<Effects>().SetPosB(posB);
                    attackParticle.GetComponent<Effects>().InitiateProjectile();
                }
                CurrentPlayer.EnemiesToDamage[i].stats.Health -= damageAmount;
                CombatText.text = "The Player attacked " + CurrentPlayer.EnemiesToDamage[i].gameObject.name + " with " + damageAmount + " damage";
                Debug.Log("Attacked " + CurrentPlayer.EnemiesToDamage[i].gameObject.name + " with " + damageAmount + " damage");
                Debug.Log(CurrentPlayer.EnemiesToDamage[i].gameObject.name + " has " + CurrentPlayer.EnemiesToDamage[i].stats.Health + " health left");
                CurrentPlayer.EnemiesToDamage[i].UpdateUI(); //updates health slider to be accurate with current health bar + other things
                CurrentPlayer.EnemiesToDamage[i].gameObject.SendMessage("UpdateAnimState", "isHit");
                yield return new WaitForSeconds(stopTime + 0.3f);
                CurrentPlayer.EnemiesToDamage[i].gameObject.SendMessage("EnableDamageValues", damageAmount);
                tmpTimer = 0.0f;
                if (CurrentPlayer.selectedAttack.isMelee)
                {
                    CurrentPlayer.transform.position = new Vector2(posB.x, posB.y); //resetting player position to adjust for animation offset
                    CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                    while (tmpTimer < moveSpeed)
                    {
                        CurrentPlayer.transform.position = Vector2.Lerp(posB, posA, tmpTimer / moveSpeed);
                        tmpTimer += Time.deltaTime;
                        yield return null;
                    }
                    CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                }
                CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
                CurrentPlayer.transform.position = posA; //fully resets player position
                                                         //might need to change this for ranged and have enemy intereaction on projectile hit?
                attackParticle = GameObject.Instantiate(CurrentPlayer.selectedAttack.CastEffect, CurrentPlayer.EnemiesToDamage[i].gameObject.transform.position, Quaternion.identity); //should instantiate the correct effect which does its thing then destroys itself
            }
            ///Add in a coroutine to slowly move the slider to the value instead of just setting it
            attack.EnemyHealthSlider.value = CurrentPlayer.selectedTarget.stats.Health / (float)CurrentPlayer.selectedTarget.EnemyProfile.maxHealth;
            //Set the health text
            attack.HealthText.text = CurrentPlayer.selectedTarget.stats.Health + "/" + CurrentPlayer.selectedTarget.EnemyProfile.maxHealth;

            if (attackParticle != null)
            {
                attackParticle.transform.position = CurrentPlayer.selectedTarget.transform.position;
            }
        }


        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < CurrentPlayer.EnemiesToDamage.Count; i++)
        {
            if (CurrentPlayer.EnemiesToDamage[i].stats.Health < 1)
            {
                CurrentPlayer.EnemiesToDamage[i].Die();
            }
        }
        attack.ResetRange();
        attack.ResetHighlightSquares();
        attack.ResetTargetRecticle();
        attack.ResetEnemiesToDamage();
        attack.ResetSelectionCircle();
        attack.EnemyPopupCanvas.alpha = 0;
        Destroy(attackParticle);
        attacking = false;
        battleStateManager.SetBool("PlayerReady", false);
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
        GameState.CurrentPlayer.selectedTarget = enemy;
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
                ShowMainButtons();
                break;
            case BattleState.Player_Move:
                attack.ReadPlayersSkills();
                battleStateManager.SetBool("FinishedAllAttacks", false);
                battleStateManager.SetBool("AllPlayersReady", false);
                battleStateManager.SetBool("ContinueBattle", false); //Reset bool from previous check to stop looping
                HideUI = true;
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
                        //StartCoroutine(AttackTarget());
                    }
                }
                else
                {
                    GameState.CurrentPlayer.Attacking = true;
                    battleStateManager.SetBool("PlayerReady", false);
                }
                break;
            case BattleState.Change_Control:

                for (int i = 0; i < GameState.PlayersToSpawn.Length; i++)
                {
                    if (!GameState.PlayerParty[i].GetComponent<PlayerController>().Attacking)//If one of the party hasn't acted yet
                    {
                        Debug.Log("Changing player");
                        GameState.ChangeCurrentPlayer();
                        HideUI = false;
                        battleStateManager.SetBool("PlayerReady", false);
                        battleStateManager.SetBool("AllPlayersReady", false);
                        return;
                    }
                }

                if (HideUI)
                {
                    HideButtons();
                    CombatTextCanvas.alpha = 1;
                    battleStateManager.SetBool("AllPlayersReady", true);
                }
                //Set buttons to inactive and change bottom pannel potencially
                break;
            case BattleState.Resolve_Attacks: //Move to the Enemies controller for turn
                if (!attacking)
                {
                    StartCoroutine(ResolvesAttacks());
                }
                break;
            case BattleState.WaitForAttacks:
                break;
            case BattleState.Battle_Result:
                //After each enemies is defeated add to a resulting pool to give to the player
                StartCoroutine(ShowResults());
                break;
            case BattleState.Battle_End:
                NavigationManager.NavigateTo("Overworld");
                //Any animation and move back to Overworld
                break;
            default:
                break;
        }
    }

    bool ContinueBattle()
    {
        //Check whether there are no players or enemies alive, if either end the battle

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
        theButtons.alpha = 0;
        theButtons.interactable = false;
        theButtons.blocksRaycasts = false;

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

        MainButtons.alpha = 1;
        MainButtons.interactable = true;
        MainButtons.blocksRaycasts = true;
    }

    public float GetStopTime()
    {
        return stopTime;
    }
}
