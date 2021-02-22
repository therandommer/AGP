using UnityEngine;
using UnityEditor;

public class PlayerProfile : MonoBehaviour
{
    [MenuItem("Assets/Create/Player Profile")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Player>();
    }
}
