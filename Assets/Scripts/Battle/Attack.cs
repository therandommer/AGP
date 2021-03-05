using TMPro;
using UnityEngine;
using UnityEngine.UI;


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
    public CanvasGroup EnemyPopupCanvas;
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
        PopupCanvas.interactable = true;
    }

    public void HidePopup()
    {
        PopupCanvas.alpha = 0;
        PopupCanvas.interactable = false;
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
    /// <summary>
    /// Highjack these systems to add in the targeting as they get the right squares
    /// </summary>
    void HighlightBottomRow()///Will highlight the first row
    {
        for (int i = 0; i < battleManager.EnemySpawnPoints.Length; i++)
        {
            Debug.Log("Highlight " + battleManager.EnemySpawnPoints[i].name);
            ShowHighlightSquare(battleManager.EnemySpawnPoints[i]);
            HighlightSquare(battleManager.EnemySpawnPoints[i], Color.red);
            if (battleManager.Enemies[i] != null)
            {
                battleManager.Enemies[i].TargetReticle.SetActive(true);
                battleManager.EnemiesToDamage.Add(battleManager.Enemies[i]);
            }
            i++;
            i++;
        }
    }

    void HighlightMiddleRow()///Will highlight the first row
    {
        for (int i = 1; i < battleManager.EnemySpawnPoints.Length; i++)
        {
            Debug.Log("Highlight " + battleManager.EnemySpawnPoints[i].name);
            ShowHighlightSquare(battleManager.EnemySpawnPoints[i]);
            HighlightSquare(battleManager.EnemySpawnPoints[i], Color.red);
            if (battleManager.Enemies[i] != null)
            {
                battleManager.Enemies[i].TargetReticle.SetActive(true);
                battleManager.EnemiesToDamage.Add(battleManager.Enemies[i]);
            }
            i++;
            i++;
        }
    }

    void HighlightTopRow()///Will highlight the first row
    {
        for (int i = 2; i < battleManager.EnemySpawnPoints.Length; i++)
        {
            Debug.Log("Highlight " + battleManager.EnemySpawnPoints[i].name);
            ShowHighlightSquare(battleManager.EnemySpawnPoints[i]);
            HighlightSquare(battleManager.EnemySpawnPoints[i], Color.red);
            if (battleManager.Enemies[i] != null)
            {
                battleManager.Enemies[i].TargetReticle.SetActive(true);
                battleManager.EnemiesToDamage.Add(battleManager.Enemies[i]);
            }
            i++;
            i++;
        }
    }

    void HighlightLeftColumn()
    {
        for (int i = 0; i < battleManager.EnemySpawnPoints.Length - 6; i++)
        {
            ShowHighlightSquare(battleManager.EnemySpawnPoints[i]);
            HighlightSquare(battleManager.EnemySpawnPoints[i], Color.red);
            if (battleManager.Enemies[i] != null)
            {
                battleManager.Enemies[i].TargetReticle.SetActive(true);
                battleManager.EnemiesToDamage.Add(battleManager.Enemies[i]);
            }
        }
    }

    void HighlightMiddleColumn()
    {
        for (int i = 3; i < battleManager.EnemySpawnPoints.Length - 3; i++)
        {
            ShowHighlightSquare(battleManager.EnemySpawnPoints[i]);
            HighlightSquare(battleManager.EnemySpawnPoints[i], Color.red);
            if (battleManager.Enemies[i] != null)
            {
                battleManager.Enemies[i].TargetReticle.SetActive(true);
                battleManager.EnemiesToDamage.Add(battleManager.Enemies[i]);
            }
        }
    }

    void HighlightRightColumn()
    {
        for (int i = 6; i < battleManager.EnemySpawnPoints.Length; i++)
        {
            ShowHighlightSquare(battleManager.EnemySpawnPoints[i]);
            HighlightSquare(battleManager.EnemySpawnPoints[i], Color.red);
            if (battleManager.Enemies[i] != null)
            {
                battleManager.Enemies[i].TargetReticle.SetActive(true);
                battleManager.EnemiesToDamage.Add(battleManager.Enemies[i]);
            }
        }
    }
    public void HighlightEnemies()
    {
        /*
         * Enemies are layed out as such, the actual position vs the array position
         * 3-2 6-5 9-8
         * 2-1 5-4 8-7
         * 1-0 4-3 7-6
         */

        for (int i = 0; i < battleManager.Enemies.Count; i++)
        {
            if (battleManager.Enemies[i] == battleManager.selectedTarget)
            {
                //Found enemy
                switch (battleManager.selectedAttack.abilityRange)
                {
                    case AbilityRange.OneEnemy:
                        ShowHighlightSquare(battleManager.EnemySpawnPoints[i]);
                        HighlightSquare(battleManager.EnemySpawnPoints[i], Color.red);
                        if (battleManager.Enemies[i] != null)
                        {
                            battleManager.Enemies[i].TargetReticle.SetActive(true);
                            battleManager.EnemiesToDamage.Add(battleManager.Enemies[i]);
                        }
                        break;
                    case AbilityRange.AllEnemies:
                        for (int j = 0; j < battleManager.EnemySpawnPoints.Length; j++)
                        {
                            ShowHighlightSquare(battleManager.EnemySpawnPoints[j]);
                            HighlightSquare(battleManager.EnemySpawnPoints[j], Color.red);
                            if (battleManager.Enemies[j] != null)
                            {
                                battleManager.Enemies[j].TargetReticle.SetActive(true);
                                battleManager.EnemiesToDamage.Add(battleManager.Enemies[j]);
                            }

                        }
                        break;
                    case AbilityRange.RowOfEnemies:
                        /*
                         * Enemies are layed out as such, the actual position vs the array position
                         * 3-2 6-5 9-8
                         * 2-1 5-4 8-7
                         * 1-0 4-3 7-6
                         */
                        switch (i)
                        {
                            case 0:
                                HighlightBottomRow();
                                break;
                            case 1:
                                HighlightMiddleRow();
                                break;
                            case 2:
                                HighlightTopRow();
                                break;
                            case 3:
                                HighlightBottomRow();
                                break;
                            case 4:
                                HighlightMiddleRow();
                                break;
                            case 5:
                                HighlightTopRow();
                                break;
                            case 6:
                                HighlightBottomRow();
                                break;
                            case 7:
                                HighlightMiddleRow();
                                break;
                            case 8:
                                HighlightTopRow();
                                break;
                        }
                        break;
                    case AbilityRange.ColumnOfEnemies:
                        /*
                         * Enemies are layed out as such, the actual position vs the array position
                         * 3-2 6-5 9-8
                         * 2-1 5-4 8-7
                         * 1-0 4-3 7-6
                         */
                        switch (i)
                        {
                            case 0:
                                HighlightLeftColumn();
                                break;
                            case 1:
                                HighlightLeftColumn();
                                break;
                            case 2:
                                HighlightLeftColumn();
                                break;
                            case 3:
                                HighlightMiddleColumn();
                                break;
                            case 4:
                                HighlightMiddleColumn();
                                break;
                            case 5:
                                HighlightMiddleColumn();
                                break;
                            case 6:
                                HighlightRightColumn();
                                break;
                            case 7:
                                HighlightRightColumn();
                                break;
                            case 8:
                                HighlightRightColumn();
                                break;
                        }
                        break;
                }
            }
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
        for (int i = 0; i < battleManager.EnemySpawnPoints.Length; i++)
        {
            DisableHighlightSquare(battleManager.EnemySpawnPoints[i]);
        }
    }

    public void ResetTargetRecticle()
    {
        for (int i = 0; i < battleManager.Enemies.Count; i++)
        {
            if (battleManager.Enemies[i] != null)
            {
                battleManager.Enemies[i].TargetReticle.SetActive(false);
            }
        }
    }
    public void ResetEnemiesToDamage()
    {
        battleManager.EnemiesToDamage.Clear();
    }
    public void ResetSelectionCircle()
    {
        for(int i = 0; i < battleManager.Enemies.Count; i++)
        {
            Destroy(battleManager.Enemies[i].selectionCircle);
        }
    }

    public void Attack1()
    {
        ResetRange();
        ResetHighlightSquares();
        ResetTargetRecticle();
        ResetEnemiesToDamage();
        ResetSelectionCircle();

        hitAmount = Ability1.AbilityAmount;
        ReadAbiltiesInfo(Ability1);
        SelectAttack(Ability1);
    }

    public void Attack2()
    {
        ResetRange();
        ResetHighlightSquares();
        ResetTargetRecticle();
        ResetEnemiesToDamage();
        ResetSelectionCircle();

        hitAmount = Ability2.AbilityAmount;
        ReadAbiltiesInfo(Ability2);
        SelectAttack(Ability2);
    }

    public void Attack3()
    {
        ResetRange();
        ResetHighlightSquares();
        ResetTargetRecticle();
        ResetEnemiesToDamage();
        ResetSelectionCircle();

        hitAmount = Ability3.AbilityAmount;
        ReadAbiltiesInfo(Ability3);
        SelectAttack(Ability3);
    }

    public void Attack4()
    {
        ResetRange();
        ResetHighlightSquares();
        ResetTargetRecticle();
        ResetEnemiesToDamage();
        ResetSelectionCircle();

        hitAmount = Ability4.AbilityAmount;
        ReadAbiltiesInfo(Ability4);
        SelectAttack(Ability4);
    }

    public void SelectAttack(Abilities Attack)
    {
        battleManager.selectedAttack = Attack;
        attackSelected = true;
    }
}
