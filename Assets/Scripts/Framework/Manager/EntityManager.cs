using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    Dictionary<string,GameObject> m_Entities = new Dictionary<string,GameObject>();
    Dictionary<string,Transform> m_Groups = new Dictionary<string,Transform>();
    private Transform m_EntityParent;
    private void Awake()
    {
        m_EntityParent = this.transform.parent.Find("Entity");
    }
    public void SetEntityGroup(List<string> group)
    {
        for (int i = 0; i < group.Count; i++)
        {
            GameObject go = new GameObject("Group-" + group[i]);
            go.transform.SetParent(m_EntityParent, false);
            m_Groups.Add(group[i], go.transform);
        }
    }
    Transform GetEntityGroup(string group)
    {
        if (!m_Groups.ContainsKey(group))
            Debug.LogError("Group is not exist");
        return m_Groups[group];
    }
    public void ShowEntity(string name, string group, string luaName)
    {
        GameObject entity = null;
        if (m_Entities.TryGetValue(name, out entity))
        {
            EntityLogic logic = entity.GetComponent<EntityLogic>();
            logic.OnShow();
            return;
        }
        Manager.Resource.LoadPrefab(name, (Object obj) =>
        {
            entity = Instantiate(obj) as GameObject;    
            Transform parent = GetEntityGroup(group);
            entity.transform.SetParent(parent, false);
            m_Entities.Add(name, entity);
            EntityLogic entityLogic = entity.AddComponent<EntityLogic>();
            entityLogic.Init(luaName);
            entityLogic.OnShow();
        });
    }
}
