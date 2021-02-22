using UnityEngine;

public class Targeting : MonoBehaviour
{
    public GameObject Target;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseDown");
            Target = null;

            RaycastHit[] hits;
            //Target = null;
            // TODO: raycast here anyway, store the results in 
            hits = Physics.RaycastAll(origin: Camera.main.transform.position, direction:
                (-Camera.main.transform.position + this.transform.position).normalized, maxDistance: 100);

            for(int i = 0; i< hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                GameObject rend = hit.transform.gameObject;

                if (rend)
                {
                    // selected a Player
                    Target = rend;
                    Debug.Log("Targeting " + Target.name);
                }
            }
        }
    }

}
