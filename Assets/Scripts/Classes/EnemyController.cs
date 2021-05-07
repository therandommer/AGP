using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //UI Updates for each enemy, they all require the slider bar.
    [SerializeField]
    GameObject sliderObject = null;
    Slider healthSlider = null;
    [SerializeField]
    float sliderLerpTime = 0.5f;

    public Sprite ExpSprite;

    AnimationManager anim;
    public BattleManager battleManager;
    Animator enemyAI;
    private bool selected;
    public GameObject selectionCircle;
    public GameObject TargetReticle;
    public bool attacking = false;

    [Header("BattleScene stuff")]
    public Abilities selectedAttack;
    public PlayerController selectedTarget;
    public List<EnemyController> EnemiesToDamage;

    [Header("Battle ABilities - NEEDED FOR BATTLESCENE")]
    public List<Abilities> AbilitiesList;

    Vector2 posA = Vector2.zero; //default enemy position
    [SerializeField]
    float attackGap = 0.35f; //distance in X axis between player and enemy
    private GameObject attackParticle = null;

    [Header("Stats Holder")]
    public StatsHolder stats;
    [Header("Experience given on kill, Total experience = Exp*Level*5")]
    public int ExperienceToGive;

    public void UpdateUI()
    {
        if (healthSlider != null)
        {
            //float startSliderValue = healthSlider.value;
            //float newSliderValue = Health;
            Debug.Log("Slider max health = " + stats.MaxHealth);
            Debug.Log("Slider current health = " + stats.Health);
            //Debug.Log("New slider value: " + newSliderValue);
            healthSlider.value = stats.Health;
        }
    }

    public int ExperienceOnKill()
    {
        return ExperienceToGive * stats.Level * 5;
    }

    public int GoldOnKill()
    {
        return stats.Level * 5;
    }

    void Awake()
    {
        anim = GetComponent<AnimationManager>();

        int levelDifference = GameState.CurrentPlayer.stats.Level - stats.Level;

        for(int i = 0; i < levelDifference; i++)
        {
            stats.LevelUp();
        }

        if (sliderObject != null)
        {
            sliderObject.SetActive(true);
            healthSlider = sliderObject.GetComponent<Slider>();
            healthSlider.maxValue = stats.MaxHealth;
            healthSlider.value = healthSlider.maxValue;
        }

        enemyAI = GetComponent<Animator>();

        if (enemyAI == null)
        {
            Debug.LogError("No AI System Found");
        }

        for (int i = 0; i < stats.EnemyProfile.StartingSkills.Length; i++)
        {
            AbilitiesList.Add(stats.EnemyProfile.StartingSkills[i]);
        }
    }

    public BattleManager BattleManager
    {
        get
        {
            return battleManager;
        }
        set
        {
            battleManager = value;
        }
    }

    IEnumerator SpinObject(GameObject target)
    {
        while (true)
        {
            target.transform.Rotate(0, 0, 180 * Time.deltaTime);
            yield return null;
        }
    }

    public void UpdateAI()//To use with the animator/controller
    {
        if (enemyAI != null && stats.EnemyProfile != null && battleManager.EnemyCount == 0)
        {
            enemyAI.SetInteger("EnemyHealth", stats.EnemyProfile.health);
            enemyAI.SetInteger("PlayerHealth", GameState.CurrentPlayer.stats.Health);
            enemyAI.SetInteger("EnemiesInBattle", battleManager.EnemyCount);
        }
    }

    void OnMouseDown()
    {
        if (battleManager.CanSelectEnemy)
        {

            battleManager.attack.ResetHighlightSquares();
            battleManager.attack.ResetTargetRecticle();
            battleManager.attack.ResetEnemiesToDamage(GameState.CurrentPlayer);
            battleManager.attack.ResetSelectionCircle();

            if (GameState.CurrentPlayer.selectedTarget == this && battleManager.LockEnemyPopup)
            {
                battleManager.LockEnemyPopup = false;
                battleManager.attack.ResetTargetRecticle();
                Destroy(selectionCircle);
            }
            else
            {
                battleManager.LockEnemyPopup = true;
                TargetReticle.SetActive(true);
                if (selectionCircle == null)
                {
                    selectionCircle = (GameObject)GameObject.Instantiate(battleManager.selectionCircle);
                    selectionCircle.transform.parent = transform;
                    selectionCircle.transform.localPosition = new Vector3(-0.1f, -0.2f, 0f);
                    selectionCircle.transform.localScale = new Vector3(2f, 2f, 1f);
                }
                //StartCoroutine("SpinObject", selectionCircle);
            }
            battleManager.attack.ShowEnemyInfo(this.gameObject);
            battleManager.SelectEnemy(this, stats.EnemyProfile.name);
            if (GameState.CurrentPlayer.selectedAttack != null)
                battleManager.attack.HighlightEnemies();

            battleManager.ShowFinalAttackButton();
        }

    }

    public void AI()
    {
        //Check state of play
        //Desinate the target
        //Depending on difficulty choose action
        //Do said action 
        //Move to battlestate to change UI/GameState
        if (!attacking)
        {
            StartCoroutine(DoAiTurn());
        }

    }

    IEnumerator DoAiTurn()
    {
        //Add in a variable to chance between choices, such as even if at less than half health can attack
        /*
        if (EnemyProfile.Health > EnemyProfile.Health/2)
        {
            while (UseItem()) //return false to move on || return true to do the function again
            {
                yield return null;
            }
        }
        */
        attacking = true;
        while (AttackPlayer())
        {
            yield return null;
        }

        battleManager.attacking = false;
        attacking = false;
        if (selectedTarget.stats.Health <= 0)
        {
            battleManager.playerCount--;
            selectedTarget.KillPlayer();
        }
    }

    bool AttackPlayer()
    {
        if (battleManager.ListOfPlayers.Count > 0)
        {
            int rnd = UnityEngine.Random.Range(0, AbilitiesList.Count);
            selectedAttack = AbilitiesList[rnd];
            int rndPlayer = UnityEngine.Random.Range(0, battleManager.ListOfPlayers.Count);

            selectedTarget = battleManager.ListOfPlayers[rndPlayer].GetComponent<PlayerController>();

            StartCoroutine(AttackDelay());

            selectedTarget.stats.Health -= battleManager.CalculateDamage(this, selectedTarget);

            float HealthBarValue = selectedTarget.stats.Health / (float)selectedTarget.stats.MaxHealth;

            battleManager.HealthBar.value = HealthBarValue;

            battleManager.HealthText.text = GameState.CurrentPlayer.stats.Health + "/" + GameState.CurrentPlayer.stats.MaxHealth;
            Debug.Log(gameObject.name + " hit " + selectedTarget.name + " for " + battleManager.CalculateDamage(this, selectedTarget) + "\n" + selectedTarget.name + " has " + selectedTarget.stats.Health);
            battleManager.CombatText.text = gameObject.name + " dealt " + battleManager.CalculateDamage(this, selectedTarget) + " damage to " + selectedTarget.name;
        }
        else
        {
            Debug.Log("There are no players left");
        }

        return false;
    }

    IEnumerator AttackDelay()
    {
        bool hasAttacked = false;
        Vector2 posB = Vector2.zero;
        posB = new Vector2(selectedTarget.gameObject.transform.position.x, selectedTarget.gameObject.transform.position.y); //base target position
        if (!hasAttacked)
        {
            if (selectedAttack.isMelee && !hasAttacked)
            {
                posB = new Vector2(posB.x - attackGap, posB.y); //adds attack gap to currently selected attack
                //this.gameObject.SendMessage("UpdateAnimState", "isMoving");
                anim.UpdateAnimState("isAttacking");
                anim.UpdateAnimState("AttackType");
                Debug.Log("Melee attack anim");
                //float tmpTimer = 0.0f;
                float moveSpeed = battleManager.GetMoveSpeed();
            }
            if (!selectedAttack.isMelee)
            {
                anim.UpdateAnimState("isAttacking");
                anim.UpdateAnimState("AttackType");
                //this.gameObject.transform.position = new Vector2(posA.x, posA.y);
                attackParticle = GameObject.Instantiate(selectedAttack.CastEffect);
                attackParticle.GetComponent<Effects>().SetPosA(posA);
                attackParticle.GetComponent<Effects>().SetPosB(posB);
                attackParticle.GetComponent<Effects>().InitiateProjectile();
            }
            hasAttacked = true;
        }
        yield return new WaitForSeconds(battleManager.GetStopTime());
        if (hasAttacked)
        {
            Debug.Log("Returning");
            anim.UpdateAnimState("isAttacking");
        }
        yield return null;
    }
    void OnMouseEnter()
    {
        if (!battleManager.LockEnemyPopup)
        {
            battleManager.attack.ShowEnemyInfo(this.gameObject);
        }
    }

    void OnMouseExit()
    {
        if (!battleManager.LockEnemyPopup)
        {
            battleManager.attack.EnemyPopupCanvas.alpha = 0;
        }
        //TargetReticle.SetActive(false);
    }

    bool UseItem()
    {
        return true;
    }

    public void Die()
    {
        //battleManager.ListOfEntities.Remove(this.gameObject);

        GameState.CurrentPlayer.PingKillQuests(stats.EnemyProfile.Class);
        int Exp = ExperienceOnKill();
        GameState.CurrentPlayer.AddExperience(Exp);

        ShowMessage.Instance.StartCouroutineForMessage("Gained Experience!", "You have gained " + Exp + " Exp." + "\n" + GameState.CurrentPlayer.Experience + "/" + GameState.CurrentPlayer.ExperienceNeededToLevel, ExpSprite, 2f);

        if(stats.EnemyProfile.isBoss)
        {
            GameState.IncreaseNumberOfBossesDefeated();
            ShowMessage.Instance.StartCouroutineForMessage("Killed a Boss!", "You have defeated " + GameState.NumberOfBossesDefeated + " bosses. " + (GameState.NumberofBossesNeededToFightFinalBoss - GameState.NumberOfBossesDefeated) + " left", ExpSprite, 2f);

        }

        if (stats.EnemyProfile.isFinalBoss)
        {
            NavigationManager.NavigateTo("GameOver");
        }
        battleManager.enemyCount--;
        Destroy(this.gameObject);
    }

}
