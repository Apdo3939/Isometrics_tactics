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
}
