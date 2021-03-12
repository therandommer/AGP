using UnityEditor;
using UnityEngine;

public class QuestAssetCreator : MonoBehaviour
{
    [MenuItem("Assets/Create/Quest")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Quest>();
    }
}
