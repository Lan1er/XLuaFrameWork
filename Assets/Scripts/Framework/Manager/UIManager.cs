using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //缓存UI
    //Dictionary<string,GameObject> m_UI = new Dictionary<string,GameObject>();
    //ui分组
    Dictionary<string,Transform> m_UIGroups = new Dictionary<string,Transform>();
    private Transform m_UIParent;

    private void Awake()
    {
        m_UIParent = this.transform.parent.Find("UI");
    }
    public void SetUIGroup(List<string> group)
    {
        for (int i = 0; i < group.Count; i++)
        {
            GameObject go = new GameObject("Group-" + group[i]);
            go.transform.SetParent(m_UIParent,false);
            m_UIGroups.Add(group[i],go.transform);
        }
    }
    Transform GetUIGroup(string group)
    {
        if (!m_UIGroups.ContainsKey(group))
            Debug.LogError("Group is not exist");
        return m_UIGroups[group];
    }
    public void OpenUI(string uiName,string group,string luaName)
    {
        GameObject ui = null;
        Transform parent = GetUIGroup(group);
        string uiPath = PathUtil.GetUIPath(uiName);
        Object uiObject = Manager.Pool.Spawn("UI", uiPath);
        if (uiObject != null)
        {
            ui = uiObject as GameObject;
            ui.transform.SetParent(parent, false);
            UILogic uiLogic = ui.GetComponent<UILogic>();
            uiLogic.OnOpen(); 
            return;
        }
        Manager.Resource.LoadUI(uiName, (Object obj) =>
        {
            ui = Instantiate(obj) as GameObject;
            ui.transform.SetParent(parent,false);
            UILogic uiLogic = ui.AddComponent<UILogic>();
            uiLogic.AssetName = uiPath;
            uiLogic.Init(luaName);
            uiLogic.OnOpen();
        });
    }
}
