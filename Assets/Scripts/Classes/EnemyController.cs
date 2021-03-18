using System.Collections;
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

    public BattleManager battleManager;
    public Enemy EnemyProfile;
    Animator enemyAI;
    public PlayerController targetPlayer;
    private bool selected;
    public GameObject selectionCircle;
    public GameObject TargetReticle;
    public bool attacking = false;
    private int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    private int health = 0;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    private int maxHealth = 0;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    private int strength = 1;
    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    private int magic = 0;
    public int Magic
    {
        get { return magic; }
        set { magic = value; }
    }
    private int defense = 0;
    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }
    private int speed = 1;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private int damage = 1;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private int armor = 0;
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    private int noOfAttacks = 1;

    private string Weapon; //Again switch, adds in bonus damage

    public Abilities[] Skills;
    public void UpdateUI()
	{
        if(healthSlider != null)
		{
            float startSliderValue = healthSlider.value;
            float newSliderValue = (health / maxHealth);
            Debug.Log("Slider max health = " + maxHealth);
            Debug.Log("Slider current health = " + health);
            Debug.Log("New slider value: " + newSliderValue);
            healthSlider.value = Mathf.Lerp(startSliderValue, newSliderValue, sliderLerpTime);
        }
	}
    void Awake()
    {
        if(sliderObject != null)
		{
            sliderObject.SetActive(true);
            healthSlider = sliderObject.GetComponent<Slider>();
		}
        enemyAI = GetComponent<Animator>();
        if (enemyAI == null)
        {
            Debug.LogError("No AI System Found");
        }
        ///Copy across all details, much easier to handle plus better for saving
        Level = EnemyProfile.level;
        Health = EnemyProfile.maxHealth;
        MaxHealth = EnemyProfile.maxHealth;
        Strength = EnemyProfile.strength;
        Magic = EnemyProfile.magic;
        Defense = EnemyProfile.defense;
        Speed = EnemyProfile.speed;
        Damage = EnemyProfile.BonusDamage;
        Armor = EnemyProfile.armor;
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
        if (enemyAI != null && EnemyProfile != null && battleManager.EnemyCount == 0)
        {
            enemyAI.SetInteger("EnemyHealth", EnemyProfile.health);
            enemyAI.SetInteger("PlayerHealth", GameState.CurrentPlayer.Health);
            enemyAI.SetInteger("EnemiesInBattle", battleManager.EnemyCount);
        }
    }

    void OnMouseDown()
    {
        if (battleManager.CanSelectEnemy)
        {

            battleManager.attack.ResetHighlightSquares();
            battleManager.attack.ResetTargetRecticle();
            battleManager.attack.ResetEnemiesToDamage();
            battleManager.attack.ResetSelectionCircle();

            if (battleManager.selectedTarget == this && battleManager.LockEnemyPopup)
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
            battleManager.SelectEnemy(this, EnemyProfile.name);
            if (battleManager.selectedAttack != null)
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
    }

    bool AttackPlayer()
    {
        targetPlayer = GameState.CurrentPlayer;
        
        targetPlayer.Health -= battleManager.CalculateDamage(this, targetPlayer);

        float HealthBarValue = targetPlayer.Health / (float)targetPlayer.MaxHealth;
        Debug.Log(HealthBarValue);
        battleManager.HealthBar.value = HealthBarValue;

        battleManager.HealthText.text = GameState.CurrentPlayer.Health + "/" + GameState.CurrentPlayer.MaxHealth;
        Debug.Log(targetPlayer.name + " has " + targetPlayer.Health);

        return false;
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
        //battleManager.Enemies.Remove(this);
        battleManager.enemyCount--;
        Destroy(this.gameObject);
    }

}
