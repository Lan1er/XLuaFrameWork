using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    EditorMode,
    PackageBundle,
    UpdateMode,
}
public class AppConst
{
    public const string BundleExtension = ".ab";
    public const string FileListName = "filelist.txt";
    public static GameMode GameMode = GameMode.EditorMode;
    //热更资源地址
    public const string ResourcesUrl = "http://127.0.0.1/AssetBundles"; 
}
