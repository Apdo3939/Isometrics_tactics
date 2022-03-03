using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetCreator
{
    [MenuItem("Assets/Create/ScriptableObjects/Job")]
    public static void CreateJob()
    {
        Job asset = ScriptableObject.CreateInstance<Job>();
        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Jobs/NewJob.asset");
        AssetDatabase.SaveAssets();
        asset.InitStats();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/ScriptableObjects/ModifierCondition/JobCondition")]
    public static void CreateModifierJobCondition()
    {
        ModConditionJob asset = ScriptableObject.CreateInstance<ModConditionJob>();
        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Modifiers/NewJobCondition.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }

    [MenuItem("Assets/Create/ScriptableObjects/ModifierCondition/ElementalCondition")]
    public static void CreateModifierElementalCondition()
    {
        ModConditionElemental asset = ScriptableObject.CreateInstance<ModConditionElemental>();
        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Modifiers/NewElementalCondition.asset");
        AssetDatabase.SaveAssets();

        Selection.activeObject = asset;
    }
}
