using UnityEngine;

public class SelectDeselectTab : MonoBehaviour
{
    private Vector3 initialScale = Vector3.one;
    private float scaleMultiplier = 1.2f;
    public void Select()
    {
        transform.localScale = initialScale * scaleMultiplier;
    }

    public void Deselect()
    {
        transform.localScale = initialScale;
    }
}
