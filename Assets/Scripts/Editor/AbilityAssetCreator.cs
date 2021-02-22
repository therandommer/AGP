using UnityEditor;
using UnityEngine;

public class AbilityAssetCreator : MonoBehaviour
{
    [MenuItem("Assets/Create/Ability")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Abilities>();
    }
}
