using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this script somewhere accessible, fill the sprite lists with things, 
/// then call GenerateRandom() insert an item level if you want a specific power,
/// or insert 0 to generate based on a global value.
/// </summary>
public class ItemGenerator : MonoBehaviour
{
    //sprites for armour randomisation
    [SerializeField]
    List<Sprite> helmetSprites;
    [SerializeField]
    List<Sprite> chestSprites;
    [SerializeField]
    List<Sprite> gloveSprites;
    [SerializeField]
    List<Sprite> bootSprites;
    //sprites for weapon randomisation
    [SerializeField]
    List<Sprite> swordSprites;
    [SerializeField]
    List<Sprite> staffSprites;
    [SerializeField]
    List<Sprite> axeSprites;
    [SerializeField]
    List<Sprite> daggerSprites;
    [SerializeField]
    List<Sprite> hammerSprites;
    //used to help generate the gear with certain power
    int iLevel = 1; // higher = better gear
    float diffModifier = 1; //raw multiplier on stats, higher multipliers on easier difficulties
    //enums for randomisation
    RarityOptions rarity;
    WeaponItem weapon;
    ArmourItems armour;
    InitialEffect initialEffect;
    int initialEffectAmount;
    ItemEffect[] AdditionalItemEffects;
    Effect effect;
    //time for some bools and modifiers
    bool isWeapon = false; //weapon will have a 1/5 chance of dropping
    bool isArmour = false; //armour will have a 4/5 chance of dropping, equal chance for each part
    // WeaponWeights = Flat Damage, Strength, Magic, Speed, Defence
    float[] swordWeights = new float[] { 1, 1.1f, 1, 1.1f, 1.0f };
    float[] staffWeights = new float[] { 0.9f, 0.6f, 1.5f, 1.1f, 1.2f };
    float[] axeWeights = new float[] { 1.2f, 1.1f, 0.6f, 0.7f, 1.0f };
    float[] daggerWeights = new float[] { 1, 1, 1, 1.4f, 0.8f };
    float[] hammerWeights = new float[] { 1.3f, 1.2f, 0.2f, 0.8f, 1.2f };
    // ArmourWeights = Flat Armour, Defence, Strength, Magic, Speed
    float[] headWeights = new float[] { 0.9f, 0.9f, 1.0f, 1.2f, 1.0f };
    float[] bodyWeights = new float[] { 1.2f, 1.2f, 1.2f, 0.7f, 0.7f };
    float[] armWeights = new float[] { 1.2f, 1.2f, 0.7f, 1.2f, 0.7f };
    float[] bootWeights = new float[] { 0.8f, 0.8f, 1.1f, 1.0f, 1.3f };
    //chance for basic, common, rare, epic, legendary
    int[] rarityChance = new int[] { 5, 65, 85, 95, 100 };
    float rarityModifier = 1.0f;

    bool canGenerate = true; //set to false if error detected

    public InventoryItem Item;



    void Start()
    {
        canGenerate = true;
        if(helmetSprites == null)
		{
            Debug.LogError("No Head Sprites Assigned");
		}
        if (chestSprites == null)
        {
            Debug.LogError("No Body Sprites Assigned");
        }
        if (gloveSprites == null)
        {
            Debug.LogError("No Glove Sprites Assigned");
        }
        if (bootSprites == null)
        {
            Debug.LogError("No Boot Sprites Assigned");
        }
        if (swordSprites == null)
        {
            Debug.LogError("No Sword Sprites Assigned");
        }
        if (staffSprites == null)
        {
            Debug.LogError("No Staff Sprites Assigned");
        }
        if (axeSprites == null)
        {
            Debug.LogError("No Axe Sprites Assigned");
        }
        if (daggerSprites == null)
        {
            Debug.LogError("No Dagger Sprites Assigned");
        }
        if (hammerSprites == null)
        {
            Debug.LogError("No Hammer Sprites Assigned");
        }
    }

    public void GenerateItemImage(bool isArmour = false, bool isWeapon = false)
    {
        int SpriteNumber = 0;
        if (isArmour)
        {
            switch (armour)
            {
                case ArmourItems.Helmet:
                    SpriteNumber = Random.Range(0, helmetSprites.Count);
                    Item.itemImage = helmetSprites[SpriteNumber];
                    break;
                case ArmourItems.Arms:
                    SpriteNumber = Random.Range(0, gloveSprites.Count);
                    Item.itemImage = gloveSprites[SpriteNumber];
                    break;
                case ArmourItems.Chest:
                    SpriteNumber = Random.Range(0, chestSprites.Count);
                    Item.itemImage = chestSprites[SpriteNumber];
                    break;
                case ArmourItems.Boots:
                    SpriteNumber = Random.Range(0, bootSprites.Count);
                    Item.itemImage = bootSprites[SpriteNumber];
                    break;
            }
        }
        else if(isWeapon)
        {
            switch (weapon)
            {
                case WeaponItem.Axe:
                    SpriteNumber = Random.Range(0, axeSprites.Count);
                    Item.itemImage = axeSprites[SpriteNumber];
                    break;
                case WeaponItem.Dagger:
                    SpriteNumber = Random.Range(0, daggerSprites.Count);
                    Item.itemImage = daggerSprites[SpriteNumber];
                    break;
                case WeaponItem.Hammer:
                    SpriteNumber = Random.Range(0, hammerSprites.Count);
                    Item.itemImage = hammerSprites[SpriteNumber];
                    break;
                case WeaponItem.Staff:
                    SpriteNumber = Random.Range(0, staffSprites.Count);
                    Item.itemImage = staffSprites[SpriteNumber];
                    break;
                case WeaponItem.Sword:
                    SpriteNumber = Random.Range(0, swordSprites.Count);
                    Item.itemImage = swordSprites[SpriteNumber];
                    break;
            }
        }
    }

    //if both armour and weapon are false, will be completely random.
    public void GenerateRandom(int _itemLevel, bool _isArmour, bool _isWeapon)
	{
        //Determine item level from global manager here, or input it from other source?
        if(_itemLevel == 0)
		{
            //get ilevel from global manager
		}
        else
		{
            iLevel = _itemLevel;
		}
        DetermineItemType(_isArmour, _isWeapon);
        if(canGenerate)
		{
            AssignRarity();
            SetBaseStats();
        }
        //maybe use a switch to determine some unique item qualities on higher rarities?
        if(_isArmour)
        {
            Item.isArmour = true;
            Item.armourItem = armour;
            GenerateItemImage(_isArmour);
        }
        else if(_isWeapon)
        {
            Item.isWeapon = true;
            Item.weaponItem = weapon;
            GenerateItemImage(_isWeapon);
        }
        Item.rarity = rarity;
        Item.InitialEffect = initialEffect;
        Item.InitialEffectAmount = initialEffectAmount;
    }

    public void GenerateSpecificItem(bool isArmour = false, ArmourItems armourItem = ArmourItems.None, bool isWeapon = false, WeaponItem weaponItem = WeaponItem.None)
    {
        if(isArmour)
        {
            switch (armourItem)
            {
                case ArmourItems.None:
                    break;
                case ArmourItems.Helmet:
                    armour = ArmourItems.Helmet;
                    break;
                case ArmourItems.Arms:
                    armour = ArmourItems.Arms;
                    break;
                case ArmourItems.Chest:
                    armour = ArmourItems.Chest;
                    break;
                case ArmourItems.Boots:
                    armour = ArmourItems.Boots;
                    break;
            }
        }
        else if(isWeapon)
        {
            switch (weaponItem)
            {
                case WeaponItem.None:
                    break;
                case WeaponItem.Axe:
                    weapon = WeaponItem.Axe;
                    break;
                case WeaponItem.Dagger:
                    weapon = WeaponItem.Dagger;
                    break;
                case WeaponItem.Hammer:
                    weapon = WeaponItem.Hammer;
                    break;
                case WeaponItem.Staff:
                    weapon = WeaponItem.Staff;
                    break;
                case WeaponItem.Sword:
                    weapon = WeaponItem.Sword;
                    break;
            }
        }
    }
    //Item Stats = Flat Damage, Strength, Magic, Defence, Speed
    //GenerateSpecific not yet implemented
    void GenerateSpecific(RarityOptions _rarity, WeaponItem _weapon, ArmourItems _armour, int _itemLevel, int[] ItemStats)
	{
        if(_weapon == WeaponItem.None)
		{
            armour = _armour;
		}
        else if(_armour == ArmourItems.None)
		{
            weapon = _weapon;
		}
        else if(_weapon == WeaponItem.None && _armour == ArmourItems.None)
		{
            canGenerate = false;
            Debug.LogError("Can't generate item, weapon/armour type not specified. Exiting function");
		}
        if(canGenerate) //do things
		{
            rarity = _rarity;
            SetRarityModifier(rarity);
            SetBaseStats();
        }
    }
    void SetBaseStats()
	{
        if (isWeapon && !isArmour)
		{
            
            switch(weapon)
			{
                case WeaponItem.Axe:
                    initialEffect = InitialEffect.AddDamage;
                    if(rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[0].itemEffect = Effect.BuffStrength;
                    }
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[1].itemEffect = Effect.BuffMagic;
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[2].itemEffect = Effect.BuffSpeed;
                    }
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[3].itemEffect = Effect.BuffDefense;
                    }
                    CalculateItemValues(true, 1);
                    break;
                case WeaponItem.Dagger:
                    initialEffect = InitialEffect.AddDamage;
                    if (rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[0].itemEffect = Effect.BuffStrength;
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[1].itemEffect = Effect.BuffMagic;
                    }
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[2].itemEffect = Effect.BuffSpeed;
                    }
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[3].itemEffect = Effect.BuffDefense;
                    }
                    CalculateItemValues(true, 2);
                    break;
                case WeaponItem.Hammer:
                    initialEffect = InitialEffect.AddDamage;
                    if (rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[0].itemEffect = Effect.BuffStrength;
                    }
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[1].itemEffect = Effect.BuffMagic;
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[2].itemEffect = Effect.BuffSpeed;
                    }
                    if (rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[3].itemEffect = Effect.BuffDefense;
                    }
                    CalculateItemValues(true, 3);
                    break;
                case WeaponItem.Staff:
                    initialEffect = InitialEffect.AddDamage;
                    if (rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[0].itemEffect = Effect.BuffStrength;
                    }
                    if(rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[1].itemEffect = Effect.BuffMagic;
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[2].itemEffect = Effect.BuffSpeed;
                    }
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[3].itemEffect = Effect.BuffDefense;
                    }
                    CalculateItemValues(true, 4);
                    break;
                case WeaponItem.Sword:
                    initialEffect = InitialEffect.AddDamage;
                    if (rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[0].itemEffect = Effect.BuffStrength;
                    }
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[1].itemEffect = Effect.BuffMagic;
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[2].itemEffect = Effect.BuffSpeed;
                    }                        
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[3].itemEffect = Effect.BuffDefense;
                    }
                    CalculateItemValues(true, 5);
                    break;
                default:
                    Debug.LogError("Invalid Weapon Type");
                    break;
			}
            
		}
        if(isArmour && !isWeapon)
		{
            
            initialEffect = InitialEffect.AddArmour;
            if(rarity >= RarityOptions.Common)
			{
                Item.AdditionalItemEffects[0].itemEffect = Effect.BuffDefense;
            }
            if(rarity >= RarityOptions.Rare)
			{
                Item.AdditionalItemEffects[1].itemEffect = Effect.BuffStrength;
            }
            if(rarity >= RarityOptions.Epic)
			{
                Item.AdditionalItemEffects[2].itemEffect = Effect.BuffMagic;
            }
            if(rarity >= RarityOptions.Legendary)
			{
                Item.AdditionalItemEffects[3].itemEffect = Effect.BuffSpeed;
            }
            switch (armour)
			{
                case ArmourItems.Helmet:
                    CalculateItemValues(false, 1);
                    break;
                case ArmourItems.Arms:
                    CalculateItemValues(false, 2);
                    break;
                case ArmourItems.Chest:
                    CalculateItemValues(false, 3);
                    break;
                case ArmourItems.Boots:
                    CalculateItemValues(false, 4);
                    break;
                default:
                    Debug.LogError("Invalid Armour Type");
                    break;
			}
            
		}
	}
    //calculates stats based on ilvl ranges
    void CalculateItemValues(bool _isWeapon, int _itemType) //assigns stats based on rarity
	{
        if(_isWeapon)
		{
            switch(_itemType) //can adjust calcs soon(tm)
			{
                case 1: //axe
                    initialEffectAmount = Mathf.RoundToInt( 8 + (iLevel * axeWeights[0]));
                    if(rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[0].EffectAmount = Mathf.RoundToInt(10 + (iLevel * axeWeights[1]));
                    }
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[1].EffectAmount = Mathf.RoundToInt(6 + (iLevel * axeWeights[2]));
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[2].EffectAmount = Mathf.RoundToInt(5 + (iLevel * axeWeights[3]));
                    }
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[3].EffectAmount = Mathf.RoundToInt(7 + (iLevel * axeWeights[4]));
                    }
                    break;
                case 2: //dagger
                    initialEffectAmount = Mathf.RoundToInt(6 + (iLevel * daggerWeights[0]));
                    if(rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[0].EffectAmount = Mathf.RoundToInt(6 + (iLevel * daggerWeights[1]));
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[1].EffectAmount = Mathf.RoundToInt(6 + (iLevel * daggerWeights[2]));
                    }
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[2].EffectAmount = Mathf.RoundToInt(10 + (iLevel * daggerWeights[3]));
                    }
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[3].EffectAmount = Mathf.RoundToInt(8 + (iLevel * daggerWeights[4]));
                    }
                    break;
                case 3: //hammer
                    initialEffectAmount = Mathf.RoundToInt(12 + (iLevel * hammerWeights[0]));
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[0].EffectAmount = Mathf.RoundToInt(11 + (iLevel * hammerWeights[1]));
                    }
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[1].EffectAmount = Mathf.RoundToInt(3 + (iLevel * hammerWeights[2]));
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[2].EffectAmount = Mathf.RoundToInt(5 + (iLevel * hammerWeights[3]));
                    }
                    if(rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[3].EffectAmount = Mathf.RoundToInt(7 + (iLevel * hammerWeights[4]));
                    }
                    break;
                case 4: //staff
                    initialEffectAmount = Mathf.RoundToInt(8 + (iLevel * staffWeights[0]));
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[0].EffectAmount = Mathf.RoundToInt(7 + (iLevel * staffWeights[1]));
                    }
                    if(rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[1].EffectAmount = Mathf.RoundToInt(14 + (iLevel * staffWeights[2]));
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[2].EffectAmount = Mathf.RoundToInt(8 + (iLevel * staffWeights[3]));
                    }
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[3].EffectAmount = Mathf.RoundToInt(10 + (iLevel * staffWeights[4]));
                    }
                    break;
                case 5: //sword
                    initialEffectAmount = Mathf.RoundToInt(10 + (iLevel * swordWeights[0]));
                    if(rarity >= RarityOptions.Common)
					{
                        Item.AdditionalItemEffects[0].EffectAmount = Mathf.RoundToInt(11 + (iLevel * swordWeights[1]));
                    }
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[1].EffectAmount = Mathf.RoundToInt(6 + (iLevel * swordWeights[2]));
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[2].EffectAmount = Mathf.RoundToInt(7 + (iLevel * swordWeights[3]));
                    }
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[3].EffectAmount = Mathf.RoundToInt(6 + (iLevel * swordWeights[4]));
                    }
                    break;
                default:
                    Debug.LogError("Invalid item type passed to calculating stats");
                    break;
			}
		}
        if(!_isWeapon)
		{
            switch(_itemType)
			{
                case 1:
                    initialEffectAmount = Mathf.RoundToInt(6 + (iLevel * headWeights[0]));
                    if(rarity >= RarityOptions.Common)
					{


                        Item.AdditionalItemEffects[0].EffectAmount = Mathf.RoundToInt(6 + (iLevel * headWeights[1]));
                    }
                    if(rarity >= RarityOptions.Rare)
					{
                        Item.AdditionalItemEffects[1].EffectAmount = Mathf.RoundToInt(7 + (iLevel * headWeights[2]));
                    }
                    if(rarity >= RarityOptions.Epic)
					{
                        Item.AdditionalItemEffects[2].EffectAmount = Mathf.RoundToInt(9 + (iLevel * headWeights[3]));
                    }
                    if(rarity == RarityOptions.Legendary)
					{
                        Item.AdditionalItemEffects[3].EffectAmount = Mathf.RoundToInt(7 + (iLevel * headWeights[4]));
                    }
                    break;
                case 2:
                    initialEffectAmount = Mathf.RoundToInt(9 + (iLevel * bodyWeights[0]));
                    if (rarity >= RarityOptions.Common)
                    {


                        Item.AdditionalItemEffects[0].EffectAmount = Mathf.RoundToInt(9 + (iLevel * bodyWeights[1]));
                    }
                    if (rarity >= RarityOptions.Rare)
                    {
                        Item.AdditionalItemEffects[1].EffectAmount = Mathf.RoundToInt(9 + (iLevel * bodyWeights[2]));
                    }
                    if (rarity >= RarityOptions.Epic)
                    {
                        Item.AdditionalItemEffects[2].EffectAmount = Mathf.RoundToInt(4 + (iLevel * bodyWeights[3]));
                    }
                    if (rarity == RarityOptions.Legendary)
                    {
                        Item.AdditionalItemEffects[3].EffectAmount = Mathf.RoundToInt(4 + (iLevel * bodyWeights[4]));
                    }
                    break;
                case 3:
                    initialEffectAmount = Mathf.RoundToInt(9 + (iLevel * armWeights[0]));
                    if (rarity >= RarityOptions.Common)
                    {


                        Item.AdditionalItemEffects[0].EffectAmount = Mathf.RoundToInt(9 + (iLevel * armWeights[1]));
                    }
                    if (rarity >= RarityOptions.Rare)
                    {
                        Item.AdditionalItemEffects[1].EffectAmount = Mathf.RoundToInt(4 + (iLevel * armWeights[2]));
                    }
                    if (rarity >= RarityOptions.Epic)
                    {
                        Item.AdditionalItemEffects[2].EffectAmount = Mathf.RoundToInt(9 + (iLevel * armWeights[3]));
                    }
                    if (rarity == RarityOptions.Legendary)
                    {
                        Item.AdditionalItemEffects[3].EffectAmount = Mathf.RoundToInt(4 + (iLevel * armWeights[4]));
                    }
                    break;
                case 4:
                    initialEffectAmount = Mathf.RoundToInt(5 + (iLevel * headWeights[0]));
                    if (rarity >= RarityOptions.Common)
                    {
                        Debug.Log(Item.AdditionalItemEffects.Length + " " + Item.AdditionalItemEffects[0].itemEffect);

                        Item.AdditionalItemEffects[0].EffectAmount = Mathf.RoundToInt(5 + (iLevel * headWeights[1]));
                    }
                    if (rarity >= RarityOptions.Rare)
                    {
                        Item.AdditionalItemEffects[1].EffectAmount = Mathf.RoundToInt(8 + (iLevel * headWeights[2]));
                    }
                    if (rarity >= RarityOptions.Epic)
                    {
                        Item.AdditionalItemEffects[2].EffectAmount = Mathf.RoundToInt(7 + (iLevel * headWeights[3]));
                    }
                    if (rarity == RarityOptions.Legendary)
                    {
                        Item.AdditionalItemEffects[3].EffectAmount = Mathf.RoundToInt(10 + (iLevel * headWeights[4]));
                    }
                    break;
                default:
                    Debug.LogError("Invalid item type passed to calculating stats");
                    break;
			}
		}
	}
    void DetermineItemType(bool _isArmour, bool _isWeapon)
	{
        isArmour = _isArmour;
        isWeapon = _isWeapon;
        int tmpType = 0;
        if(isArmour == true && isWeapon == false)
		{
            tmpType = Random.Range(1, 4);
            switch (tmpType)
            {
                case 1:
                    armour = ArmourItems.Helmet;
                    break;
                case 2:
                    armour = ArmourItems.Arms;
                    break;
                case 3:
                    armour = ArmourItems.Chest;
                    break;
                case 4:
                    armour = ArmourItems.Boots;
                    break;
                default:
                    Debug.LogError("tmpType out of range in DetermineItemType()");
                    break;
            }
        }
        else if(isArmour == false && isWeapon == true)
		{
            tmpType = Random.Range(1, 5);
            switch (tmpType)
            {
                case 1:
                    weapon = WeaponItem.Axe;
                    break;
                case 2:
                    weapon = WeaponItem.Dagger;
                    break;
                case 3:
                    weapon = WeaponItem.Hammer;
                    break;
                case 4:
                    weapon = WeaponItem.Staff;
                    break;
                case 5:
                    weapon = WeaponItem.Sword;
                    break;
                default:
                    Debug.LogError("tmpType out of range in DetermineItemType()");
                    break;
            }
        }
        else
		{
            canGenerate = false;
            Debug.LogError("Incompatible armour/weapon combination. \n Armour = " + _isArmour + "\n Weapon = " + _isWeapon);
		}
	}
    void AssignRarity()
	{
        int tmpRarity = 0;
        tmpRarity = Random.Range(1, 100);
        if (tmpRarity <= rarityChance[0])
        {
            rarity = RarityOptions.Basic;           
        }
        if (tmpRarity > rarityChance[0] && tmpRarity <= rarityChance[1])
        {
            rarity = RarityOptions.Common;
            Item.AdditionalItemEffects = new ItemEffect[1];
            for (int i = 0; i < Item.AdditionalItemEffects.Length; i++)
            {
                Item.AdditionalItemEffects[i] = new ItemEffect();
            }
        }
        if (tmpRarity > rarityChance[1] && tmpRarity <= rarityChance[2])
        {
            rarity = RarityOptions.Rare;
            Item.AdditionalItemEffects = new ItemEffect[2];
            for (int i = 0; i < Item.AdditionalItemEffects.Length; i++)
            {
                Item.AdditionalItemEffects[i] = new ItemEffect();
            }
        }
        if (tmpRarity > rarityChance[2] && tmpRarity <= rarityChance[3])
        {
            rarity = RarityOptions.Epic;
            Item.AdditionalItemEffects = new ItemEffect[3];
            for (int i = 0; i < Item.AdditionalItemEffects.Length; i++)
            {
                Item.AdditionalItemEffects[i] = new ItemEffect();
            }
        }
        if (tmpRarity > rarityChance[3] && tmpRarity <= rarityChance[4])
        {
            rarity = RarityOptions.Legendary;
            Item.AdditionalItemEffects = new ItemEffect[4];
            for (int i = 0; i < Item.AdditionalItemEffects.Length; i++)
            {
                Item.AdditionalItemEffects[i] = new ItemEffect();
            }
        }
        SetRarityModifier(rarity);
    }
    void SetRarityModifier(RarityOptions _rarity)
	{
        switch(_rarity)
		{
            case RarityOptions.Basic:
                rarityModifier = 0.7f;
                break;
            case RarityOptions.Common:
                rarityModifier = 1.0f;
                break;
            case RarityOptions.Rare:
                rarityModifier = 1.1f;
                break;
            case RarityOptions.Epic:
                rarityModifier = 1.3f;
                break;
            case RarityOptions.Legendary:
                rarityModifier = 1.5f;
                break;
        }
	}
}
