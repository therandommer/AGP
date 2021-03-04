using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public BattleManager battleManager;
    public Enemy EnemyProfile;
    Animator enemyAI;
    public Player targetPlayer;
    private bool selected;
    GameObject selectionCircle;
    public GameObject TargetReticle;
    public int Health;
    public int Strength;

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
        /*
        if (battleManager.CanSelectEnemy)
        {
            selectionCircle = (GameObject)GameObject.Instantiate(battleManager.selectionCircle);
            selectionCircle.transform.parent = transform;
            selectionCircle.transform.localPosition = new Vector3(0f, -0.2f, 0f);
            selectionCircle.transform.localScale = new Vector3(4f, 4f, 1f);
            StartCoroutine("SpinObject", selectionCircle);
            battleManager.SelectEnemy(this, EnemyProfile.name);
            battleManager.GetComponent<Attack>().attackSelected = false;
            battleManager.battleStateManager.SetBool("PlayerReady", true);
        }
        else
        {
            TargetReticle.SetActive(true);
        }
        */
        battleManager.attack.ResetHighlightSquares();
        battleManager.attack.ResetTargetRecticle();
        if(battleManager.selectedTarget == this && battleManager.LockEnemyPopup)
        {
            battleManager.LockEnemyPopup = false;
            battleManager.attack.ResetTargetRecticle();
        }
        else
        {
            battleManager.LockEnemyPopup = true;
            TargetReticle.SetActive(true);
        }
        battleManager.attack.ShowEnemyInfo(this.gameObject);
        battleManager.SelectEnemy(this, EnemyProfile.name);
        if(battleManager.selectedAttack != null)
            battleManager.attack.HighlightEnemies();

    }

    public void AI()
    {
        //Check state of play
        //Desinate the target
        //Depending on difficulty choose action
        //Do said action 
        //Move to battlestate to change UI/GameState

        StartCoroutine(DoAiTurn());

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
        while (AttackPlayer())
        {
            yield return null;
        }
        battleManager.attacking = false;
    }

    bool AttackPlayer()
    {
        targetPlayer = GameState.CurrentPlayer;

        targetPlayer.Health -= battleManager.CalculateDamage(this, targetPlayer);

        battleManager.HealthBar.value = targetPlayer.Health / targetPlayer.ActualHealth;

        Debug.Log(targetPlayer.name + " has " + targetPlayer.Health);

        return false;
    }

    void OnMouseEnter()
    {
        if(!battleManager.LockEnemyPopup)
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
        battleManager.Enemies.Remove(this);
        battleManager.enemyCount--;
        Destroy(this.gameObject);
    }

}
