using UnityEngine;
using UnityEditor;

public class EnemyProfileAssetCreator : MonoBehaviour
{
    [MenuItem("Assets/Create/Enemy Profile")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Enemy>();
    }
}
