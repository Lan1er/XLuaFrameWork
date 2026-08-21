using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class BuildTool : Editor
{
    [MenuItem("Tools/Build Windows Bundle")]
    static void BundleWindowsBuild()
    {
        Build(BuildTarget.StandaloneWindows);
    }
    [MenuItem("Tools/Build Android Bundle")]
    static void BundleAndroidBuild()
    {
        Build(BuildTarget.Android);
    }
    [MenuItem("Tools/Build iPhone Bundle")]
    static void BundleiPhoneBuild()
    {
        Build(BuildTarget.iOS);
    }
    static void Build(BuildTarget target)
    {
        List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();
        string[] files = Directory.GetFiles(PathUtil.BuildResourcesPath,"*",SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".meta"))
                continue;
            AssetBundleBuild assetBundle = new AssetBundleBuild();

            string fileName = PathUtil.GetStandardPath(files[i]);
            Debug.Log("file:" + fileName);
            
            string assetName = PathUtil.GetUnityPath(fileName);
            assetBundle.assetNames = new string[] { assetName };
            string bundleName = fileName.Replace(PathUtil.BuildResourcesPath,"").ToLower();
            assetBundle.assetBundleName = bundleName + ".ab";
            assetBundleBuilds.Add(assetBundle);
        }
        if(Directory.Exists(PathUtil.BundleOutPath))
            Directory.Delete(PathUtil.BundleOutPath,true);
        Directory.CreateDirectory(PathUtil.BundleOutPath);

        BuildPipeline.BuildAssetBundles(PathUtil.BundleOutPath, assetBundleBuilds.ToArray(), BuildAssetBundleOptions.None, target);
    }
}
