using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimationManager : MonoBehaviour
{
    BattleManager bM = null;
    Animator anim = null;
    [SerializeField]
    GameObject damageObject = null; //used for UI enable/disable
    [SerializeField]
    TextMeshProUGUI damageText = null; //used for combat information
    int receivedDamage = 0;
    [SerializeField]
    float displayTime = 3.0f;
    [SerializeField]
    float hitDelay = 0.25f; //length of time character repeats their hit animation
    void Start()
    {
        anim = GetComponent<Animator>();
        bM = FindObjectOfType<BattleManager>();
        damageObject.SetActive(false);
        damageText.text = "I dunno";
    }
    void Update()
    {
    }

    public void UpdateAnimState(string name)
	{
        if(anim.GetBool(name) == true)
		{
            anim.SetBool(name, false);
            Debug.Log("Setting the state of: " + name + " to false.");
        }
        else if(anim.GetBool(name) == false)
		{
            anim.SetBool(name, true);
            Debug.Log("Setting the state of: " + name + " to true.");
        }
        if (name == "isHit" && anim.GetBool(name))
		{
            Invoke("InvertHitState", hitDelay);
		}
        if (name == "AttackType")
		{
            int rnd = Random.Range(1, 3);
            anim.SetInteger("AttackType", rnd);
		}
	}
    //forces player to be in the default animation
    public void IdleThis()
	{
        anim.SetBool("isBattle", true);
        anim.SetBool("isIdle", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDead", false);
        anim.SetBool("isMoving", false);
        anim.SetBool("isHit", false);
        anim.SetInteger("yMove", 0);
        anim.SetBool("xMove", false);

	}
    public void EnableDamageValues(int damage, bool isEffective, bool isNotEffective)
	{
        Debug.Log("Enabling Damage Value display for " + gameObject.name);
        receivedDamage = damage;
        StartCoroutine(DamageNumbers(isEffective, isNotEffective));
	}
    IEnumerator DamageNumbers(bool isEffective, bool isNotEffective)
	{
        damageObject.SetActive(true);
        if(isEffective && !isNotEffective)
		{
            damageText.text = "" + receivedDamage + "!!";
            damageText.color = Color.red;
		}
        else if(!isEffective && isNotEffective)
		{
            damageText.text = "" + receivedDamage + "...";
            damageText.color = Color.gray;
        }
        else
		{
            damageText.text = "" + receivedDamage;
            damageText.color = Color.black;
        }
        if(receivedDamage == 0)
		{
            damageText.text = "Miss";
            damageText.color = Color.gray;
        }
        yield return new WaitForSeconds(displayTime);
        damageObject.SetActive(false);
        Debug.Log("Disabling Damage Value display for " + gameObject.name);
	}
    void DisableDamageNumbers() //used to prevent overlap or enumerator weirdness
	{
        damageObject.SetActive(false);
        damageText.text = "";
        StopAllCoroutines();
	}
    void InvertHitState() //reverse after animation finished
	{
        UpdateAnimState("isHit");
        Debug.Log("Inverted hit state for " + gameObject.name);
	}
}
