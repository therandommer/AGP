﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [Header("Enemy Spawn Information")]
    //public GameObject[] EnemyPrefabs;
    public GameObject[] EnemySpawnPoints;
    public List<EnemyController> Enemies;
    public AnimationCurve SpawnAnimationCurve;
    public int enemyCount;
    public int playerCount;
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
    public TMP_Text CurrentPlayer;
    public Image CurrentPlayerImage;
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
    public bool BattleSceneTest;
    public List<GameObject> BattleSceneTestList = new List<GameObject>();
    [Header("Player Pics")]
    public Image[] PlayerImages;
    [Header("Enemy Pics")]
    public Image[] EnemyImages;


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
    [SerializeField]
    float superEffective = 2f; //multiplier if move effective
    [SerializeField]
    float notEffective = 0.5f; //multiplier if move not effective
    [SerializeField]
    float stabBonus = 1.5f;

    //used to determine damage number outputs
    bool isEffective = false;
    bool isNotEffective = false;

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

    public List<GameObject> ListOfPlayers = new List<GameObject>();

    public static GameObject StorredPlayer;

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
        Instantiate(GameState.CheckTimeForMap());
        battleStateManager = GetComponent<Animator>();
        if (battleStateManager == null)
        {
            Debug.LogError("No battleStateMachine Animator found.");
        }
        introPanelAnim = introPanel.GetComponent<Animator>();


        GameState.CurrentPlayer.transform.position = new Vector3(40, 0, 1);
        GameState.CurrentPlayer.GetComponent<PlayerMovement>().CantMove = true;
        GameState.CurrentPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StorredPlayer = GameState.CurrentPlayer.gameObject;
    }

    void Start()
    {
        for (int i = 0; i < GameState.ActiveParty.Count; i++)
        {
            GameObject Player = Instantiate(GameState.ActiveParty[i]);
            PlayerImages[i].sprite = Player.GetComponent<PlayerController>().stats.PlayerProfile.PlayerImage;
            Player.transform.localScale = new Vector3(0.33f,0.33f,1);
            Player.SetActive(true);
            Player.transform.SetParent(PlayerSpawnPoints[i].transform);
            Player.transform.position = PlayerSpawnPoints[i].transform.position;
            Player.GetComponent<PlayerMovement>().CantMove = true;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Player.GetComponent<SpriteRenderer>().flipX = true;

            ListOfPlayers.Add(Player);
            ListOfEntities.Add(ListOfPlayers[i]);
        }
        playerCount = ListOfPlayers.Count;
        GameState.CurrentPlayer = ListOfPlayers[0].GetComponent<PlayerController>();

        HealthText.text = GameState.CurrentPlayer.stats.Health + "/" + GameState.CurrentPlayer.stats.MaxHealth;
        // Calculate how many enemies 
        if(!GameState.PreSetCombat)
        {
            enemyCount = Random.Range(1, EnemySpawnPoints.Length); //Dynamically set enemy numbers based on level/party members, stops swarming
        }
        else
        {
            enemyCount = GameState.EnemyPrefabsForBattle.Length - 1;
        }
        // Spawn the enemies in 
        StartCoroutine(SpawnEnemies(GameState.EnemyPrefabsForBattle));
        enemyCount = GameState.EnemyPrefabsForBattle.Length;
        GetAnimationStates();

        
    }

    public void ReadCurrentPlayer()
    {
        CurrentPlayer.text = "Now playing as: " + GameState.CurrentPlayer.stats.PlayerProfile.Name;
        CurrentPlayerImage.sprite = GameState.CurrentPlayer.stats.PlayerProfile.PlayerImage;
    }

    IEnumerator SpawnEnemies(GameObject[] EnemyPrefabs)
    {
        //Spawn enemies in over time 
        for (int i = 0; i < enemyCount; i++)
        {
            if(!BattleSceneTest)
            {
                var newEnemy = (GameObject)Instantiate(EnemyPrefabs[i]);
                ListOfEntities.Add(newEnemy);

                var controller = newEnemy.GetComponent<EnemyController>();
                newEnemy.name = controller.EnemyProfile.Name + " " + Names[Random.Range(1, Names.Length)];
                controller.battleManager = this;
                newEnemy.transform.position = new Vector3(10, -1, 0);
                yield return StartCoroutine(
                MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
                newEnemy.transform.parent = EnemySpawnPoints[i].transform;
                Enemies.Add(controller);
            }
            else
            {
                var newEnemy = (GameObject)Instantiate(BattleSceneTestList[Random.Range(0, BattleSceneTestList.Count - 1)]);
                ListOfEntities.Add(newEnemy);

                var controller = newEnemy.GetComponent<EnemyController>();
                newEnemy.name = controller.EnemyProfile.Name + " " + Names[Random.Range(1, Names.Length)];
                controller.battleManager = this;
                newEnemy.transform.position = new Vector3(10, -1, 0);
                yield return StartCoroutine(
                MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
                newEnemy.transform.parent = EnemySpawnPoints[i].transform;
                Enemies.Add(controller);
            }
        }
        battleStateManager.SetBool("BattleReady", true);

        ListOfEntities.Sort(delegate (GameObject a, GameObject b)
        {

            return (a.GetComponent<StatsHolder>().CompareTo(b.GetComponent<StatsHolder>()));

        });
        for (int i = 0; i < Enemies.Count; i++)
        {
            EnemyImages[i].sprite = Enemies[i].stats.EnemyProfile.EnemySprite;
        }

        for (int i = 0; i < PlayerImages.Length; i++)
        {
            if(PlayerImages[i].sprite == null)
            {
                PlayerImages[i].GetComponent<PopUpMenu>().DisableTheMenu();
            }
        }
        for (int i = 0; i < EnemyImages.Length; i++)
        {
            if (EnemyImages[i].sprite == null)
            {
                EnemyImages[i].GetComponent<PopUpMenu>().DisableTheMenu();
            }
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
                        IdlePlayers();
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
            IdlePlayers();
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
        Debug.Log("Target count " + CurrentPlayer.EnemiesToDamage.Count);
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
                    posB = new Vector2(posB.x - attackGap, posB.y); //adds attack gap to currently selected attack

                CurrentPlayer.GetComponent<AnimationManager>().UpdateAnimState("isMoving");
                CurrentPlayer.GetComponent<AnimationManager>().UpdateAnimState("isAttacking");
                while (tmpTimer < moveSpeed)
                {
                    posB = new Vector2(posB.x - attackGap, posB.y); //adds attack gap to currently selected attack
                    CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                    CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
                    //CurrentPlayer.transform.position = Vector2.Lerp(posA, posB, tmpTimer / moveSpeed);
                    tmpTimer += Time.deltaTime;
                    //CurrentPlayer.transform.position = new Vector2(posB.x, posB.y + 0.365f); //ensures player is actually at the position required
                    CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                }
                if (!CurrentPlayer.selectedAttack.isMelee)
                {
                    CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
                    //CurrentPlayer.transform.position = new Vector2(posA.x, posA.y + 0.365f);
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
                yield return new WaitForSeconds(stopTime);
                CurrentPlayer.EnemiesToDamage[i].gameObject.GetComponent<AnimationManager>().EnableDamageValues(damageAmount, isEffective, isNotEffective);
                //CurrentPlayer.EnemiesToDamage[i].gameObject.SendMessage("EnableDamageValues", damageAmount);
                tmpTimer = 0.0f;
                if (CurrentPlayer.selectedAttack.isMelee)
                {
                    //CurrentPlayer.transform.position = new Vector2(posB.x, posB.y); //resetting player position to adjust for animation offset
                    /*CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                    while (tmpTimer < moveSpeed)
                    {
                        CurrentPlayer.transform.position = Vector2.Lerp(posB, posA, tmpTimer / moveSpeed);
                        tmpTimer += Time.deltaTime;
                        yield return null;
                    }*/
                    CurrentPlayer.SendMessage("UpdateAnimState", "isMoving");
                }
                CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
                //CurrentPlayer.transform.position = posA; //fully resets player position
                //might need to change this for ranged and have enemy intereaction on projectile hit?
                attackParticle = GameObject.Instantiate(CurrentPlayer.selectedAttack.CastEffect, CurrentPlayer.EnemiesToDamage[i].gameObject.transform.position, Quaternion.identity); //should instantiate the correct effect which does its thing then destroys itself

                ///Add in a coroutine to slowly move the slider to the value instead of just setting it
                attack.EnemyHealthSlider.value = CurrentPlayer.selectedTarget.stats.Health / (float)CurrentPlayer.selectedTarget.EnemyProfile.maxHealth;
                //Set the health text
                attack.HealthText.text = CurrentPlayer.selectedTarget.stats.Health + "/" + CurrentPlayer.selectedTarget.EnemyProfile.maxHealth;
                //GameState.CurrentPlayer.transform.position = new Vector2(posB.x, posB.y + 0.365f); //ensures player is actually at the position required
                CurrentPlayer.GetComponent<AnimationManager>().UpdateAnimState("isMoving");

                if (!CurrentPlayer.selectedAttack.isMelee)
                {
                    CurrentPlayer.SendMessage("UpdateAnimState", "isAttacking");
                    //GameState.CurrentPlayer.transform.position = new Vector2(posA.x, posA.y + 0.365f);
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
                CurrentPlayer.EnemiesToDamage[i].GetComponent<AnimationManager>().UpdateAnimState("isHit");
                yield return new WaitForSeconds(stopTime);
                CurrentPlayer.EnemiesToDamage[i].GetComponent<AnimationManager>().EnableDamageValues(damageAmount, isEffective, isNotEffective);
                tmpTimer = 0.0f;
                if (CurrentPlayer.selectedAttack.isMelee)
                {
                    //GameState.CurrentPlayer.transform.position = new Vector2(posB.x, posB.y); //resetting player position to adjust for animation offset
                    CurrentPlayer.GetComponent<AnimationManager>().UpdateAnimState("isMoving");
                    /*while (tmpTimer < moveSpeed)
                    {
                        GameState.CurrentPlayer.transform.position = Vector2.Lerp(posB, posA, tmpTimer / moveSpeed);
                        tmpTimer += Time.deltaTime;
                    }*/
                    CurrentPlayer.GetComponent<AnimationManager>().UpdateAnimState("isMoving");
                }
                CurrentPlayer.GetComponent<AnimationManager>().UpdateAnimState("isAttacking");
                //GameState.CurrentPlayer.transform.position = posA; //fully resets player position
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
            if(CurrentPlayer.EnemiesToDamage[i] != null)
            {
                if (CurrentPlayer.EnemiesToDamage[i].stats.Health < 1)
                {
                    CurrentPlayer.EnemiesToDamage[i].Die();
                }
            }
        }
        attack.ResetRange();
        attack.ResetHighlightSquares();
        attack.ResetTargetRecticle();
        attack.ResetEnemiesToDamage(CurrentPlayer);
        attack.ResetSelectionCircle();
        attack.EnemyPopupCanvas.alpha = 0;
        Destroy(attackParticle);
        attacking = false;
        CurrentPlayer.Attacking = false;
        yield return null;
    }

    public int CalculateDamage(PlayerController CurrentPlayer, EnemyController Target)
    {
        int tmpRnd = 0; // used for dodge chance and crit chance
        int speedDif = CurrentPlayer.stats.PlayerProfile.speed - Target.stats.EnemyProfile.speed;
        bool canHit = true;
        isEffective = false;
        isNotEffective = false;
        if (speedDif <= Target.stats.EnemyProfile.speed * 0.1) //if player speed greater than or within 10% of enemy speed, can miss
        {
            tmpRnd = Random.Range(1, 100);
            if (tmpRnd <= 10) //10% chance to miss
            {
                canHit = false;
            }
            else
            {
                canHit = true;
            }
        }
        else
        {
            canHit = true;
        }
        if (canHit)
        {
            float DamageCalc = 0;
            float DamageModifier = 1.0f;
            for (int i = 0; i < Target.EnemyProfile.Elements.Count; ++i)
            {
                DamageModifier *= ElementalModifier(CurrentPlayer.selectedAttack.AbilityType, Target.EnemyProfile.Elements[i]);
            }
            for (int i = 0; i < CurrentPlayer.stats.PlayerProfile.Elements.Count; ++i)
            {
                if (CurrentPlayer.selectedAttack.AbilityType == CurrentPlayer.stats.PlayerProfile.Elements[i])
                {
                    DamageModifier *= stabBonus;
                }
            }
            if (CurrentPlayer.selectedAttack.BaseType == AbilityBaseType.Physical)
            {
                DamageCalc = (CurrentPlayer.stats.PlayerProfile.strength + attack.hitAmount) - (Target.stats.EnemyProfile.defense);
            }
            else if (CurrentPlayer.selectedAttack.BaseType == AbilityBaseType.Magical)
            {
                DamageCalc = CurrentPlayer.stats.PlayerProfile.magic + attack.hitAmount - (Target.stats.EnemyProfile.magic);
            }

            if (DamageModifier > 1)
            {
                isEffective = true;
                isNotEffective = false;
            }
            else if (DamageModifier < 1)
            {
                isNotEffective = true;
                isEffective = false;
            }
            else
            {
                isNotEffective = false;
                isEffective = false;
            }
            if (DamageCalc <= 0)
            {
                DamageCalc = 1;
            }

            Target.gameObject.GetComponent<AnimationManager>().EnableDamageValues(Mathf.RoundToInt(DamageCalc), isEffective, isNotEffective);
            return Mathf.RoundToInt(DamageCalc);
        }
        else
        {
            Target.gameObject.GetComponent<AnimationManager>().EnableDamageValues(0, isEffective, isNotEffective);
            return 0;
        }
    }

    public int CalculateDamage(EnemyController currentAI, PlayerController TargetPlayer)
    {
        int tmpRnd = 0; // used for dodge chance and crit chance
        int speedDif = currentAI.stats.Speed - TargetPlayer.stats.Speed;
        bool canHit = true;

        if (speedDif <= TargetPlayer.stats.PlayerProfile.speed * 0.1) //if player speed greater than or within 10% of player speed, can miss
        {
            tmpRnd = Random.Range(1, 100);
            if (tmpRnd <= 10) //10% chance to miss
            {
                canHit = false;
            }
            else
            {
                canHit = true;
            }
        }
        else
        {
            canHit = true;
        }

        if (canHit)
        {
            float DamageCalc = 0;
            float DamageModifier = 1.0f;
            for (int i = 0; i < TargetPlayer.stats.PlayerProfile.Elements.Count; ++i)
            {
                DamageModifier *= ElementalModifier(currentAI.selectedAttack.AbilityType, TargetPlayer.stats.PlayerProfile.Elements[i]);
            }

            for (int i = 0; i < currentAI.stats.EnemyProfile.Elements.Count; ++i)
            {
                if (currentAI.selectedAttack.AbilityType == currentAI.stats.EnemyProfile.Elements[i])
                {
                    DamageModifier *= stabBonus;
                }
            }

            if (currentAI.selectedAttack.BaseType == AbilityBaseType.Physical)
            {
                DamageCalc = (currentAI.stats.Strength + attack.hitAmount) - (TargetPlayer.stats.Defense);
            }
            else if (currentAI.selectedAttack.BaseType == AbilityBaseType.Magical)
            {
                DamageCalc = (currentAI.stats.Magic + attack.hitAmount) - (TargetPlayer.stats.Magic);
            }
            if (DamageModifier > 1)
            {
                isEffective = true;
                isNotEffective = false;
            }
            else if (DamageModifier < 1)
            {
                isNotEffective = true;
                isEffective = false;
            }
            else
            {
                isNotEffective = false;
                isEffective = false;
            }
            if (DamageCalc <= 0)
            {
                DamageCalc = 1;
            }
            //int DamageCalc = currentAI.stats.Strength + attack.hitAmount - TargetPlayer.stats.Defense;
            //Debug.Log("Dealt " + DamageCalc + " to " + TargetPlayer.name);
            //TargetPlayer.GetComponent<AnimationManager>().EnableDamageValues()
            TargetPlayer.gameObject.GetComponent<AnimationManager>().EnableDamageValues(Mathf.RoundToInt(DamageCalc), isEffective, isNotEffective);
            return Mathf.RoundToInt(DamageCalc);
        }
        else
        {
            TargetPlayer.gameObject.GetComponent<AnimationManager>().EnableDamageValues(0, isEffective, isNotEffective);
            return 0;
        }

    }

    float ElementalModifier(AbilityTypes attackType, AbilityTypes defenceType)
    {
        //Blunt>Slashing>Normal>Blunt
        //Fire>Electricity>Water>Fire
        //Holy>Dark>Holy
        switch (attackType)
        {
            case AbilityTypes.Blunt:
                switch (defenceType)
                {
                    case AbilityTypes.Blunt:
                        return notEffective;
                    case AbilityTypes.Slashing:
                        return superEffective;
                    case AbilityTypes.Normal:
                        return notEffective;
                    default:
                        return 1.0f;
                }
            case AbilityTypes.Slashing:
                switch (defenceType)
                {
                    case AbilityTypes.Slashing:
                        return notEffective;
                    case AbilityTypes.Blunt:
                        return notEffective;
                    case AbilityTypes.Normal:
                        return superEffective;
                    default:
                        return 1.0f;
                }
            case AbilityTypes.Normal:
                switch (defenceType)
                {
                    case AbilityTypes.Normal:
                        return notEffective;
                    case AbilityTypes.Blunt:
                        return superEffective;
                    case AbilityTypes.Slashing:
                        return notEffective;
                    default:
                        return 1.0f;
                }
            case AbilityTypes.Fire:
                switch (defenceType)
                {
                    case AbilityTypes.Fire:
                        return notEffective;
                    case AbilityTypes.Water:
                        return notEffective;
                    case AbilityTypes.Electricity:
                        return superEffective;
                    default:
                        return 1.0f;
                }
            case AbilityTypes.Water:
                switch (defenceType)
                {
                    case AbilityTypes.Fire:
                        return superEffective;
                    case AbilityTypes.Water:
                        return notEffective;
                    case AbilityTypes.Electricity:
                        return notEffective;
                    default:
                        return 1.0f;
                }
            case AbilityTypes.Electricity:
                switch (defenceType)
                {
                    case AbilityTypes.Fire:
                        return notEffective;
                    case AbilityTypes.Water:
                        return superEffective;
                    case AbilityTypes.Electricity:
                        return notEffective;
                    default:
                        return 1.0f;
                }
            case AbilityTypes.Dark:
                switch (defenceType)
                {
                    case AbilityTypes.Dark:
                        return notEffective;
                    case AbilityTypes.Holy:
                        return superEffective;
                    default:
                        return 1.0f;
                }
            case AbilityTypes.Holy:
                switch (defenceType)
                {
                    case AbilityTypes.Holy:
                        return notEffective;
                    case AbilityTypes.Dark:
                        return superEffective;
                    default:
                        return 1.0f;
                }
            default:
                Debug.LogWarning("No valid attack type for skill");
                return 1.0f;
        }
    }
    public void RunAway()
    {
        GameState.justExitedBattle = true;

        StorredPlayer.GetComponent<PlayerMovement>().CantMove = false;
        GameState.CurrentPlayer = StorredPlayer.GetComponent<PlayerController>();
        GameState.CurrentPlayer.gameObject.transform.position = GameState.CurrentPlayer.LastScenePosition;

        NavigationManager.NavigateTo(GameState.CurrentPlayer.LastSceneName);
    }

    public void TriggerEnding()
    {
        ///Trigger Convo for ending
        NavigationManager.NavigateTo("Ending");
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
                GameState.CurrentPlayer.CurrentPlayerPointer.enabled = true;
                Debug.Log("Trying to set " + GameState.CurrentPlayer.name);
                ReadCurrentPlayer();
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
                attack.ResetRange();
                attack.ResetHighlightSquares();
                attack.ResetTargetRecticle();
                attack.ResetSelectionCircle();
                attack.HidePopup();
                attack.EnemyPopupCanvas.alpha = 0;
                GameState.CurrentPlayer.CurrentPlayerPointer.enabled = false;
                //GameState.CurrentPlayer.CurrentPlayerPointer.enabled = false;
                for (int i = 0; i < ListOfPlayers.Count; i++)
                {
                    if (!ListOfPlayers[i].GetComponent<PlayerController>().Attacking)//If one of the party hasn't acted yet
                    {
                        Debug.Log("Changing player");
                        ChangeCurrentPlayerBattle();
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
                GameState.justExitedBattle = true;
                StorredPlayer.GetComponent<PlayerMovement>().CantMove = false;
                GameState.CurrentPlayer = StorredPlayer.GetComponent<PlayerController>();
                GameState.CurrentPlayer.gameObject.transform.position = GameState.CurrentPlayer.LastScenePosition;

                NavigationManager.NavigateTo(GameState.CurrentPlayer.LastSceneName);                
                //Any animation and move back to Overworld
                break;
            default:
                break;
        }
    }

    public void ChangeCurrentPlayerBattle()
    {
        for (int i = 0; i < ListOfPlayers.Count; i++)
        {
            if (ListOfPlayers[i].GetComponent<PlayerController>().stats.Id == GameState.PlayerObject.GetComponent<PlayerController>().stats.Id)
            {
                if ((i + 1) <= (ListOfPlayers.Count - 1))
                {
                    Debug.Log("Changing to: " + ListOfPlayers[i + 1].name);
                    GameState.PlayerObject = ListOfPlayers[i + 1];
                    GameState.CurrentPlayer = ListOfPlayers[i + 1].GetComponent<PlayerController>();
                    return;
                }
                else
                {
                    GameState.PlayerObject = ListOfPlayers[0];
                    GameState.CurrentPlayer = ListOfPlayers[0].GetComponent<PlayerController>();
                    return;
                }
            }
        }
    }
    bool ContinueBattle()
    {
        //Check whether there are no players or enemies alive, if either end the battle

        attack.ResetRange();
        attack.ResetHighlightSquares();
        attack.HidePopup();

        if (enemyCount == 0 || playerCount == 0)
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

    public void IdlePlayers()
    {
        for (int i = 0; i < playerParty.Count; ++i)
        {
            if (playerParty[i].GetComponent<PlayerController>().stats.Health > 0)
            {
                playerParty[i].GetComponent<AnimationManager>().IdleThis();
            }
        }
    }
    public float GetStopTime()
    {
        return stopTime;
    }
    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed; //lower values makes player and enemies move faster on melee attacks
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
