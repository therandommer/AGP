using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    public GameObject[] EnemySpawnPoints;
    public GameObject[] EnemyPrefabs;
    public AnimationCurve SpawnAnimationCurve;
    public CanvasGroup theButtons;
    public Animator battleStateManager;
    public GameObject introPanel;
    Animator introPanelAnim;

    private int enemyCount;

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

    private BattleState currentBattleState;

    private string selectedTargetName;
    private EnemyController selectedTarget;
    public GameObject selectionCircle;
    private bool canSelectEnemy;

    bool attacking = false;

    public GameObject Attack1Particle;
    public GameObject Attack2Particle;
    public GameObject Attack3Particle;
    public GameObject Attack4Particle;
    private GameObject attackParticle;


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
        GetAnimationStates();
        // Calculate how many enemies 
        enemyCount = Random.Range(1, EnemySpawnPoints.Length);
        // Spawn the enemies in 
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        //Spawn enemies in over time 
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy = (GameObject)Instantiate(EnemyPrefabs[0]);
            newEnemy.transform.position = new Vector3(10, -1, 0);
            yield return StartCoroutine(
            MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
            newEnemy.transform.parent = EnemySpawnPoints[i].transform;

            var controller = newEnemy.GetComponent<EnemyController>();

            controller.BattleManager = this;

            var EnemyProfile = ScriptableObject.CreateInstance<Enemy>(); ///Move all of this to GameState with random Generation 
            EnemyProfile.Class = EnemyClass.Dragon;
            EnemyProfile.Level = 1;
            EnemyProfile.Damage = 1;
            EnemyProfile.Health = 20;
            EnemyProfile.name = EnemyProfile.Class + " " + i.ToString();

            controller.EnemyProfile = EnemyProfile;
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
        var damageAmount = GetComponent<Attack>().hitAmount;
        switch (damageAmount)
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

        selectedTarget.EnemyProfile.Health -= damageAmount;
        yield return new WaitForSeconds(1f);
        attacking = false;
        GetComponent<Attack>().hitAmount = 0;
        battleStateManager.SetBool("PlayerReady", false);
        Destroy(attackParticle);
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
        selectedTarget = enemy;
        selectedTargetName = name;
    }


    // Update is called once per frame
    void Update()
    {
        currentBattleState = battleStateHash[battleStateManager.GetCurrentAnimatorStateInfo(0).shortNameHash];

        switch (currentBattleState)
        {
            case BattleState.Intro:
                introPanelAnim.SetTrigger("Intro");
                break;
            case BattleState.Player_Move:
                if (GetComponent<Attack>().attackSelected == true)
                {
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
                break;
            case BattleState.Enemy_Attack:
                break;
            case BattleState.Battle_Result:
                break;
            case BattleState.Battle_End:
                break;
            default:
                break;
        }


        if (currentBattleState == BattleState.Player_Move)
        {
            theButtons.alpha = 1;
            theButtons.interactable = true;
            theButtons.blocksRaycasts = true;
        }
        else
        {
            theButtons.alpha = 0;
            theButtons.interactable = false;
            theButtons.blocksRaycasts = false;
        }
    }
}
