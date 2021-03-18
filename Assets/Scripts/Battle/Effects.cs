using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//general script that all effects should have assigned to them as function calls will be made using this. Effect specific movements, etc. need their own script
public class Effects : MonoBehaviour
{
    public bool usesDestroy = true; //if false disregards below timer
    [ConditionalHide("usesDestroy")]
    public float destroyTimer = 3.0f; //time before object deletes itself

    public bool isProjectile = false; //true and then handles projectile travel here
    [ConditionalHide("isProjectile")]
    public float projectileSpeed = 2.0f; //time taken to go from player to enemy
    [ConditionalHide("isProjectile")]
    public GameObject hitEffect = null; //spawn this effect on projectile hit
    GameObject hitReference = null;
    //used for travel lerp
    Vector2 posA = Vector2.zero;
    Vector2 posB = Vector2.zero;
    public void InitiateProjectile() //waits for battle manager to call
	{
        if (usesDestroy)
        {
            Invoke("DestroyThis", destroyTimer);
        }
        if (isProjectile)
        {
            StartCoroutine("MoveThis");
        }
    }
    void DestroyThis()
	{
        Debug.Log("Destroying " + gameObject.name);
        Destroy(gameObject);
	}
    IEnumerator MoveThis()
	{
        float tmpTimer = 0.0f;
        while (tmpTimer < projectileSpeed)
        {
            transform.position = Vector2.Lerp(posA, posB, tmpTimer / projectileSpeed);
            tmpTimer += Time.deltaTime;
            yield return null;
         }
        Debug.Log("Projectile: " + gameObject.name + " has reached destination, spawning hit now");
        hitReference = GameObject.Instantiate(hitEffect, new Vector3(posB.x, posB.y, this.transform.position.z), this.transform.rotation);
        hitReference.GetComponent<Effects>().InitiateProjectile();
        DestroyThis();
    }

    public void SetPosA(Vector2 a)
	{
        posA = a;
	}
    public void SetPosB(Vector2 b)
	{
        posB = b;
	}
}
