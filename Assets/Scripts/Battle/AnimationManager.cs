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
        
	}
    public void EnableDamageValues(int damage)
	{
        receivedDamage = damage;
        StartCoroutine("DamageNumbers");
        damageObject.SetActive(false);
	}
    IEnumerator DamageNumbers()
	{
        damageObject.SetActive(true);
        damageText.text = "" + receivedDamage;
        yield return new WaitForSeconds(displayTime);
	}
}
