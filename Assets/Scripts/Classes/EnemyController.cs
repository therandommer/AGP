using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public BattleManager battleManager;
    public Enemy EnemyProfile;
    Animator enemyAI;
    public Player targetPlayer;
    public GameObject playerObject;
    private bool selected;
    public GameObject selectionCircle;
    public GameObject TargetReticle;
    public int Health;
    public int Strength;
    public bool attacking = false;

    public void Awake()
    {
        enemyAI = GetComponent<Animator>();
        if (enemyAI == null)
        {
            Debug.LogError("No AI System Found");
        }
        Health = EnemyProfile.Health;
        Strength = EnemyProfile.Strength;
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
            enemyAI.SetInteger("EnemyHealth", EnemyProfile.Health);
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
        playerObject = GameState.PlayerParty[0];
        int damageAmount = battleManager.CalculateDamage(this, targetPlayer);
        targetPlayer.Health -= damageAmount;
        float HealthBarValue = targetPlayer.Health / (float)targetPlayer.MaxHealth;
        Debug.Log(HealthBarValue);
        battleManager.HealthBar.value = HealthBarValue;

        battleManager.HealthText.text = GameState.CurrentPlayer.Health + "/" + GameState.CurrentPlayer.MaxHealth;
        Debug.Log(targetPlayer.name + " has " + targetPlayer.Health);

        playerObject.SendMessage("UpdateAnimState", "isHit");
        playerObject.SendMessage("EnableDamageValues", damageAmount);
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
