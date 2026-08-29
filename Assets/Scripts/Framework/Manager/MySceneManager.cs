using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private string m_LogicName = "[SceneLogic]";
    private void Awake()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }
    /// <summary>
    /// 场景切换回调
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    private void OnActiveSceneChanged(Scene s1, Scene s2)
    {
        if(!s1.isLoaded || !s2.isLoaded)
            return;
        SceneLogic logic1 = GetSceneLogic(s1);
        SceneLogic logic2 = GetSceneLogic(s2);
        logic1?.OnInActive();
        logic2?.OnActive(); 
    }
    /// <summary>
    /// 激活场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void SetActive(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
    }
    /// <summary>
    /// 叠加加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="luaName"></param>
    public void LoadScene(string sceneName,string luaName)
    {
        Manager.Resource.LoadScene(sceneName, (Object obj) =>
        {
            StartCoroutine(StartLoadScene(sceneName,luaName,LoadSceneMode.Additive));
        });
    }
    /// <summary>
    /// 切换加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="luaName"></param>
    public void ChangeScene(string sceneName, string luaName)
    {
        Manager.Resource.LoadScene(sceneName, (Object obj) =>
        {
            StartCoroutine(StartLoadScene(sceneName, luaName, LoadSceneMode.Single));
        });
    }
    /// <summary>
    /// 卸载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void UnLoadSceneAsync(string sceneName)
    {
        StartCoroutine(UnLoadScene(sceneName));
    }
    /// <summary>
    /// 判断场景是否已加载
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private bool isLoadedScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        return scene.isLoaded;
    }
    IEnumerator StartLoadScene(string sceneName,string luaName,LoadSceneMode mode)
    {
        if(isLoadedScene(sceneName)) 
            yield break;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, mode);
        async.allowSceneActivation = true;
        yield return async;

        Scene scene = SceneManager.GetSceneByName(sceneName);
        GameObject go = new GameObject(m_LogicName);
        SceneManager.MoveGameObjectToScene(go, scene);
        SceneLogic logic = go.AddComponent<SceneLogic>();
        logic.SceneName = sceneName;
        logic.Init(luaName);
        logic.OnEnter();
    }
    private IEnumerator UnLoadScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if(!scene.isLoaded)
        {
            Debug.LogError("scene not is loaded");
            yield break;
        }
        SceneLogic logic = GetSceneLogic(scene);
        logic?.OnQuit();
        AsyncOperation async = SceneManager.UnloadSceneAsync(scene);
        yield return async;
    }
    private SceneLogic GetSceneLogic(Scene scene)
    {
        GameObject[] gameObjects = scene.GetRootGameObjects();
        foreach(GameObject go in gameObjects)
        {
            if(go.name.CompareTo(m_LogicName) == 0)
            {
                SceneLogic logic = go.GetComponent<SceneLogic>();
                return logic;
            }
        }
        return null;
    }
}
