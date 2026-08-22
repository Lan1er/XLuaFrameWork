using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtil
{
    //根目录
    public static readonly string AssetsPath = Application.dataPath;
    //需要打Bundle的目录
    public static readonly string BuildResourcesPath = Application.dataPath + "/BuildResources/";
    //bundle输出目录
    public static readonly string BundleOutPath = Application.streamingAssetsPath;
    //bundle资源路径
    public static string BundleResourcePath
    {
        get { return Application.streamingAssetsPath; }
    }
    /// <summary>
    /// 获取unity的相对路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetUnityPath(string path)
    {
        if(string.IsNullOrEmpty(path))
            return string.Empty;
        return path.Substring(path.IndexOf("Assets"));
    }
    /// <summary>
    /// 获取标准路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetStandardPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;
        return path.Trim().Replace("\\","/");
    }
}
