using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopulateQuestList : MonoBehaviour
{
    [Header("Quest Icons")]
    public Sprite KillQuestSprite;
    public Sprite FetchQuestSprite;
    public Sprite TalkQuestSprite;

    [Header("Item Icons")]
    public Sprite HelmetTypeImage;
    public Sprite ChestTypeImage;
    public Sprite ArmsTypeImage;
    public Sprite BootsTypeImage;

    [Header("Weapon Icons")]
    public Sprite AxeTypeImage;
    public Sprite StaffTypeImage;
    public Sprite DaggerTypeImage;
    public Sprite SwordTypeImage;
    public Sprite MaceTypeImage;

    [Header("Button Prefab")]
    public Button QuestTabPrefab;

    [Header("Selected Quest Ui")]
    public TMP_Text SelectedQuestTitle;
    public Image SelectedQuestTypeImage;
    public TMP_Text SelectedQuestType;
    public Image TargetImage;
    public TMP_Text TargetTypeName;
    public TMP_Text QuestDescription;
    public TMP_Text ExpReward;
    public TMP_Text MoneyReward;
    public TMP_Text ItemReward;
    public Image ItemRewardImage;
    public TMP_Text AbilityReward;
    public Image AbilityRewardImage;

    int TotalRewardMoney;
    int TotalRewardExperience;

    public Button CompleteQuestButton;

    public Color32 LegendaryColor;
    public Color32 EpicColor;
    public Color32 RareColor;
    public Color32 CommonColor;

    public Quest SelectedQuest;
    public void SelectQuest(Quest QuestSelected)
    {
        SelectedQuest = QuestSelected;
        ReadSelectedQuest(SelectedQuest);
    }

    public void populateQuestList()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Quest quest in GameState.CurrentPlayer.QuestLog)
        {
            Button NewQuest = Instantiate(QuestTabPrefab, Vector3.zero, Quaternion.identity);
            NewQuest.onClick.AddListener(() => SelectQuest(quest));

            NewQuest.transform.SetParent(transform);

            ItemUIManager UI = NewQuest.GetComponent<ItemUIManager>();

            UI.Title.text = quest.name;
            UI.Amount.text = "Exp: " + quest.ExperienceReward;
            switch (quest.questType)
            {
                case QuestType.KillQuest:
                    UI.ItemTypeImage.sprite = KillQuestSprite;
                    break;
                case QuestType.FetchQuest:
                    UI.ItemTypeImage.sprite = FetchQuestSprite;
                    break;
                case QuestType.TalkingQuest:
                    UI.ItemTypeImage.sprite = TalkQuestSprite;
                    break;
            }
        }
    }

    public void ReadSelectedQuest(Quest SelQ)
    {
        SelectedQuestTitle.text = SelQ.name;
        switch (SelQ.questType)
        {
            case QuestType.KillQuest:
                SelectedQuestTypeImage.sprite = KillQuestSprite;
                TargetTypeName.text = SelQ.EnemyToKill.ToString();
                break;
            case QuestType.FetchQuest:
                SelectedQuestTypeImage.sprite = FetchQuestSprite;
                TargetTypeName.text = SelQ.ItemNeeded.ToString();
                break;
            case QuestType.TalkingQuest:
                SelectedQuestTypeImage.sprite = TalkQuestSprite;
                break;
        }
        TargetImage.sprite = SelQ.TargetSprite;

        SelectedQuestType.text = SelQ.questType.ToString();
        QuestDescription.text = SelQ.QuestDescription;

        foreach (QuestReward reward in SelQ.Reward)
        {
            switch (reward.questReward)
            {
                case Reward.Money:
                    TotalRewardMoney += reward.RewardAmount;
                    ExpReward.text = "+ " + TotalRewardMoney + " Exp";
                    break;
                case Reward.Exp:
                    TotalRewardExperience += reward.RewardAmount;
                    ExpReward.text = "+ " + TotalRewardExperience + " Money";
                    break;
                case Reward.Item:
                    ItemReward.text = reward.RewardItem.itemName;
                    switch (reward.RewardItem.rarity)
                    {
                        case RarityOptions.Common:
                            ItemReward.color = CommonColor;
                            break;
                        case RarityOptions.Rare:
                            ItemReward.color = RareColor;
                            break;
                        case RarityOptions.Epic:
                            ItemReward.color = EpicColor;
                            break;
                        case RarityOptions.Legendary:
                            ItemReward.color = LegendaryColor;
                            break;
                    }
                    if (reward.RewardItem.isArmour)
                    {
                        switch (reward.RewardItem.armourItem)
                        {
                            case ArmourItems.Helmet:
                                ItemRewardImage.sprite = HelmetTypeImage;
                                break;
                            case ArmourItems.Arms:
                                ItemRewardImage.sprite = ArmsTypeImage;
                                break;
                            case ArmourItems.Chest:
                                ItemRewardImage.sprite = ChestTypeImage;
                                break;
                            case ArmourItems.Boots:
                                ItemRewardImage.sprite = BootsTypeImage;
                                break;
                        }
                    }
                    else if (reward.RewardItem.isWeapon)
                    {
                        switch (reward.RewardItem.weaponItem)
                        {
                            case WeaponItem.Axe:
                                ItemRewardImage.sprite = AxeTypeImage;
                                break;
                            case WeaponItem.Dagger:
                                ItemRewardImage.sprite = DaggerTypeImage;
                                break;
                            case WeaponItem.Hammer:
                                ItemRewardImage.sprite = MaceTypeImage;
                                break;
                            case WeaponItem.Staff:
                                ItemRewardImage.sprite = StaffTypeImage;
                                break;
                            case WeaponItem.Sword:
                                ItemRewardImage.sprite = SwordTypeImage;
                                break;
                        }
                    }
                    break;
                case Reward.Ability:
                    AbilityReward.text = reward.RewardAbility.name;
                    switch (reward.RewardAbility.AbilityType)
                    {
                        case AbilityTypes.Slashing:
                            AbilityReward.color = Color.gray;
                            break;
                        case AbilityTypes.Blunt:
                            AbilityReward.color = Color.cyan;
                            break;
                        case AbilityTypes.Holy:
                            AbilityReward.color = Color.yellow;
                            break;
                        case AbilityTypes.Dark:
                            AbilityReward.color = Color.magenta;
                            break;
                        case AbilityTypes.Fire:
                            AbilityReward.color = Color.red;
                            break;
                        case AbilityTypes.Water:
                            AbilityReward.color = Color.blue;
                            break;
                    }
                    AbilityRewardImage.sprite = reward.RewardAbility.AbilityImage;
                    break;
            }
        }
        CompleteQuestButton.interactable = true;
    }

    public void CheckIfQuestCompleted()
    {
        if(SelectedQuest.Status == QuestStatus.Complete)
        {
            GameState.CurrentPlayer.ClaimQuest(SelectedQuest);
        }
        else
        {
            Debug.Log("Quest is not completed");
        }
    }


}
