using UnityEditor;
using UnityEngine;

public class CreateBoostList
{
    [MenuItem("Assets/Create/Boost List")]
    public static BoostList Create()
    {
        BoostList asset = ScriptableObject.CreateInstance<BoostList>();

        AssetDatabase.CreateAsset(asset, "Assets/BoostList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}