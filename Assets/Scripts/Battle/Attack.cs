using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class Attack : MonoBehaviour
{
    public BattleManager battleManager;
    [Header("Abilities")]
    public Text Ability1ButtonText;
    public Abilities Ability1;
    public Text Ability2ButtonText;
    public Abilities Ability2;
    public Text Ability3ButtonText;
    public Abilities Ability3;
    public Text Ability4ButtonText;
    public Abilities Ability4;
    [Header("Attack Pop Up")]
    public CanvasGroup PopupCanvas;
    public Image Skill_Image;
    public TMP_Text Skill_Name;
    public TMP_Text Skill_Description;
    public Image Skill_Type_Background;
    public TMP_Text Skill_Type;
    [Header("Attack Range")]
    public Image Spot1;
    public Image Spot2;
    public Image Spot3;
    public Image Spot4;
    public Image Spot5;
    public Image Spot6;
    public Image Spot7;
    public Image Spot8;
    public Image Spot9;
    public Image Target;
    public Image Target1;
    [Header("EnemyPopup")]
    public List<EnemyController> Enemies;
    public CanvasGroup EnemyPopupCanvas;
    public bool CanHoverOver = false;//Used to act as a guard for hover over UI
    public bool EnemySelected = false;//Used to activate the selection UI
    public EnemyController EnemyHoveredOver;
    public Image EnemyImage;
    public TMP_Text EnemyName;
    public Slider EnemyHealthSlider;
    public TMP_Text HealthText;
    [Header("AttackSelected")]
    public bool attackSelected = false;
    public int hitAmount = 0;


    public void ShowEnemyInfo(GameObject Enemy)
    {
        Enemy EnemyInfo = Enemy.GetComponent<EnemyController>().EnemyProfile;

        EnemyImage.sprite = EnemyInfo.EnemySprite;
        EnemyName.text = Enemy.name;
        HealthText.text = EnemyInfo.Health + "/" + EnemyInfo.MaxHealth;
        EnemyHealthSlider.value = EnemyInfo.Health / EnemyInfo.MaxHealth;
        EnemyPopupCanvas.alpha = 1;
    }


    void Start()
    {
        ReadAbilitiesNames();
    }

    void ReadAbilitiesNames()
    {
        Ability1ButtonText.text = Ability1.name;
        Ability2ButtonText.text = Ability2.name;
        Ability3ButtonText.text = Ability3.name;
        Ability4ButtonText.text = Ability4.name;
    }

    void ReadAbiltiesInfo(Abilities ChosenAbility)
    {
        Skill_Image.sprite = ChosenAbility.AbilityImage;
        Skill_Name.text = ChosenAbility.name;
        Skill_Description.text = ChosenAbility.AbilityDescription;
        Skill_Type.text = ChosenAbility.AbilityType.ToString();
        switch (ChosenAbility.AbilityType)
        {
            case AbilityTypes.Normal:
                break;
            case AbilityTypes.Slashing:
                Skill_Type_Background.color = Color.gray;
                break;
            case AbilityTypes.Blunt:
                Skill_Type_Background.color = Color.cyan;
                break;
            case AbilityTypes.Holy:
                Skill_Type_Background.color = Color.yellow;
                break;
            case AbilityTypes.Dark:
                Skill_Type_Background.color = Color.magenta;
                break;
            case AbilityTypes.Fire:
                Skill_Type_Background.color = Color.red;
                break;
            case AbilityTypes.Water:
                Skill_Type_Background.color = Color.blue;
                break;
        }
        ReadRange(ChosenAbility);
        PopupCanvas.alpha = 1;
    }

    void ShowHighlightSquare(GameObject SpawnPoint)
    {
        SpawnPoint.GetComponent<SpriteRenderer>().enabled = true;
    }

    void HighlightSquare(GameObject SpawnPoint, Color HighlightColour)
    {
        SpawnPoint.GetComponent<SpriteRenderer>().color = HighlightColour;
    }

    void DisableHighlightSquare(GameObject SpawnPoint)
    {
        SpawnPoint.GetComponent<SpriteRenderer>().color = Color.white;
        SpawnPoint.GetComponent<SpriteRenderer>().enabled = false;
    }

    void HighlightEnemies(Abilities Attack)
    {
        /*
         * Enemies are layed out as such
         * 3 6 9
         * 2 5 8
         * 1 4 7
         */
        switch (Attack.abilityRange)
        {
            case AbilityRange.OneEnemy:
                ShowHighlightSquare(battleManager.EnemySpawnPoints[0]);
                HighlightSquare(battleManager.EnemySpawnPoints[0], Color.red);
                break;
            case AbilityRange.AllEnemies:
                for (int i = 0; i < battleManager.EnemySpawnPoints.Length; i++)
                {
                    ShowHighlightSquare(battleManager.EnemySpawnPoints[i]);
                    HighlightSquare(battleManager.EnemySpawnPoints[i], Color.red);
                }
                break;
            case AbilityRange.RowOfEnemies:
                for (int i = 0; i < battleManager.EnemySpawnPoints.Length; i++)
                {
                    Debug.Log("Highlight " + battleManager.EnemySpawnPoints[i].name);
                    ShowHighlightSquare(battleManager.EnemySpawnPoints[i]);
                    HighlightSquare(battleManager.EnemySpawnPoints[i], Color.red);
                    i++;
                    i++;
                }
                break;
            case AbilityRange.ColumnOfEnemies:
                ShowHighlightSquare(battleManager.EnemySpawnPoints[0]);
                HighlightSquare(battleManager.EnemySpawnPoints[0], Color.red);                
                
                ShowHighlightSquare(battleManager.EnemySpawnPoints[1]);
                HighlightSquare(battleManager.EnemySpawnPoints[1], Color.red);

                ShowHighlightSquare(battleManager.EnemySpawnPoints[2]);
                HighlightSquare(battleManager.EnemySpawnPoints[2], Color.red);
                break;
        }
    }

    void ReadRange(Abilities Attack)
    {
        switch (Attack.abilityRange)
        {
            case AbilityRange.OneEnemy:
                Spot2.enabled = true;
                break;
            case AbilityRange.AllEnemies:
                Spot1.enabled = true;
                Spot2.enabled = true;
                Spot3.enabled = true;
                Spot4.enabled = true;
                Spot5.enabled = true;
                Spot6.enabled = true;
                Spot7.enabled = true;
                Spot8.enabled = true;
                Spot9.enabled = true;
                break;
            case AbilityRange.RowOfEnemies:
                Spot2.enabled = true;
                Spot5.enabled = true;
                Spot8.enabled = true;
                break;
            case AbilityRange.ColumnOfEnemies:
                Spot1.enabled = true;
                Spot2.enabled = true;
                Spot3.enabled = true;
                break;
        }
        if (Attack.Targetable)
        {
            Target.enabled = true;
            Target1.enabled = true;
        }
        HighlightEnemies(Attack);
    }

    public void ResetRange()
    {
        Spot1.enabled = false;
        Spot2.enabled = false;
        Spot3.enabled = false;
        Spot4.enabled = false;
        Spot5.enabled = false;
        Spot6.enabled = false;
        Spot7.enabled = false;
        Spot8.enabled = false;
        Spot9.enabled = false;
        Target.enabled = false;
        Target1.enabled = false;
    }

    public void ResetHighlightSquares()
    {
        for(int i = 0; i < battleManager.EnemySpawnPoints.Length; i++)
        {
            DisableHighlightSquare(battleManager.EnemySpawnPoints[i]);
        }
    }
    public void Attack1()
    {
        ResetRange();
        ResetHighlightSquares();
        hitAmount = Ability1.AbilityAmount;
        ReadAbiltiesInfo(Ability1);
        AttackTheEnemy();
    }

    public void Attack2()
    {
        ResetRange();
        ResetHighlightSquares();
        hitAmount = Ability2.AbilityAmount;
        ReadAbiltiesInfo(Ability2);
        AttackTheEnemy();
    }

    public void Attack3()
    {
        ResetRange();
        ResetHighlightSquares();
        hitAmount = Ability3.AbilityAmount;
        ReadAbiltiesInfo(Ability3);
        AttackTheEnemy();
    }

    public void Attack4()
    {
        ResetRange();
        ResetHighlightSquares();
        hitAmount = Ability4.AbilityAmount;
        ReadAbiltiesInfo(Ability4);
        AttackTheEnemy();
    }

    public void AttackTheEnemy()
    {
        attackSelected = true;
    }
}
