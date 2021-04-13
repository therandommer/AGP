using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats Holder")]
    public StatsHolder stats;

    [Header("Is the character attacking")]
    public bool Attacking = false;

    [Header("Profile Details")]
    public List<InventoryItem> Inventory = new List<InventoryItem>();

    public List<InventoryItem> EquippedItems = new List<InventoryItem>();

    [Header("BattleScene stuff")]
    public Abilities selectedAttack;
    public EnemyController selectedTarget;
    public List<EnemyController> EnemiesToDamage;

    public Vector3 LastScenePosition;

    public void AddInventoryItem(InventoryItem item)
    {
        Inventory.Add(item);
    }


    public List<InventoryItem> GetItems(bool ShowItemsPlayerDoesNotOwn = false, bool IncludeAllRarities = false, bool IncludeAllArmour = false, bool IncludeAllWeapons = false,
        RarityOptions rarity = RarityOptions.Basic, ArmourItems Armour = ArmourItems.None, WeaponItem Weapon = WeaponItem.None)
    {
        InventoryItem[] itemArray = Inventory.ToArray();
        Debug.Log("IA " + itemArray.Length);
        var items = from item in itemArray select item;

        //if (!ShowItemsPlayerDoesNotOwn)
        //items = items.Where(item => ItemCollection.Instance.QuantityOfEachItem[item] > 0);

        //if (!IncludeAllRarities)
        //items = items.Where(item => item.rarity == rarity);

        if (!IncludeAllArmour)
            items = items.Where(item => item.armourItem == Armour);

        if (!IncludeAllWeapons)
            items = items.Where(item => item.weaponItem == Weapon);

        if (IncludeAllArmour)
            items = items.Where(item => item.isArmour);

        if (IncludeAllWeapons)
            items = items.Where(item => item.isWeapon);

        var returnList = items.ToList<InventoryItem>();
        Debug.Log("RL " + returnList.Count);

        //returnList.Sort();
        //returnList.OrderByDescending(a => a.isArmour).ThenBy(a => a.InitialEffectAmount);

        return returnList;
    }


    public List<InventoryItem> GetItemsWithRarity(RarityOptions rarity)
    {
        return GetItems(true, false, false, false, rarity);
    }

    public List<InventoryItem> GetAllArmourItems()
    {
        return GetItems(false, true, true);
    }
    public List<InventoryItem> GetArmourItemsOfType(ArmourItems ArmourPiece)
    {
        return GetItems(false, true, false, false, RarityOptions.Basic, ArmourPiece);
    }
    public List<InventoryItem> GetAllWeaponItems()
    {
        return GetItems(false, true, false, true);
    }

    public List<InventoryItem> GetWeaponItemsOfType(WeaponItem Weapon)
    {
        return GetItems(false, true, false, false, RarityOptions.Basic, ArmourItems.None, Weapon);
    }

    public List<Abilities> Skills;

    public List<Abilities> EquipedSkills;

    public int Money;

    public int Experience;

    public double ExperienceNeededToLevel;

    public void AddExperience(int experienceAmount)
    {
        Experience += experienceAmount;
        if(Experience >= ExperienceNeededToLevel)
        {
            stats.LevelUp();
            ExperienceNeededToLevel *= 1.5;
        }
    }


    public List<Quest> QuestLog = new List<Quest>();
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach (Abilities abilities in stats.PlayerProfile.StartingSkills)
        {
            Skills.Add(abilities);
        }
        foreach (Abilities abilities in stats.PlayerProfile.StartingSkills)
        {
            EquipedSkills.Add(abilities);
        }
        Money = stats.PlayerProfile.StartingMoney;

        QuestLog = stats.PlayerProfile.StartingQuestLog;
        foreach (InventoryItem item in stats.PlayerProfile.StartingInventory)
        {
            Inventory.Add(item);
        }

        foreach (InventoryItem item in stats.PlayerProfile.StartingEquipment)
        {
            EquippedItems.Add(item);
            ApplyEquipedStats(item);
        }
    }

    public void ApplyEquipedStats(InventoryItem ItemToEquip)
    {
        switch (ItemToEquip.InitialEffect)
        {
            case InitialEffect.AddArmour:
                stats.Armor += ItemToEquip.InitialEffectAmount;
                break;
            case InitialEffect.AddDamage:
                stats.Damage += ItemToEquip.InitialEffectAmount;
                break;
        }

        for (int i = 0; i < ItemToEquip.AdditionalItemEffects.Length; i++)
        {
            switch (ItemToEquip.AdditionalItemEffects[i].itemEffect)
            {
                case Effect.BuffHealth:
                    stats.Health += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffStrength:
                    stats.Strength += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffMagic:
                    stats.Magic += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffDefense:
                    stats.Defense += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffSpeed:
                    stats.Speed += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.GiveImmunity:
                    break;
                case Effect.GiveWeakness:
                    break;
                case Effect.AddArmour:
                    stats.Armor += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.AddDamage:
                    stats.Damage += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
            }
        }
    }

    public void TakeDamage(int Amount)
    {
        stats.Health -= Mathf.Clamp((Amount - stats.Armor), 0, int.MaxValue); //Stops the values goes crazy
    }

    public void PingKillQuests(EnemyClass enemyKilled)
    {
        foreach (Quest quest in QuestLog)
        {
            if (quest.questType == QuestType.KillQuest && quest.EnemyToKill == enemyKilled)
            {
                quest.increaseAmount();
            }
        }
    }

    public void PingFetchQuest(InventoryItem itemPickedUp)
    {
        foreach (Quest quest in QuestLog)
        {
            if (quest.questType == QuestType.FetchQuest && quest.ItemNeeded == itemPickedUp)
            {
                quest.increaseAmount();
            }
        }
    }

    public void ClaimQuest(Quest QuestToComplete)//Tie to a button on the quest tab
    {
        foreach (QuestReward reward in QuestToComplete.Reward)
        {
            switch (reward.questReward)
            {
                case Reward.Money:
                    GameState.CurrentPlayer.Money += reward.RewardAmount;
                    break;
                case Reward.Exp:
                    GameState.CurrentPlayer.Experience += reward.RewardAmount;
                    break;
                case Reward.Item:
                    GameState.CurrentPlayer.Inventory.Add(reward.RewardItem);
                    break;
                case Reward.Ability:
                    GameState.CurrentPlayer.Skills.Add(reward.RewardAbility);
                    break;
            }
        }
    }


}
