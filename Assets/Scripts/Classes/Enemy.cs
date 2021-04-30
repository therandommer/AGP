using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    public EnemyClass Class;
    public Sprite EnemySprite;
    public List<AbilityTypes> Elements = new List<AbilityTypes>();

    public Abilities[] StartingSkills;

    public bool isBoss;
    public bool isFinalBoss;
}
