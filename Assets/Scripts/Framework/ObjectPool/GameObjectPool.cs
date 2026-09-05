using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

public class GameObjectPool : PoolBase
{
    public override UObject Spawn(string name)
    {
        UObject obj = base.Spawn(name);
        if(obj == null)
            return null;
        GameObject go = obj as GameObject;
        go.SetActive(true);
        return obj;
    }
    public override void UnSpawn(string name, UObject obj)
    {
        GameObject go = obj as GameObject;
        go.SetActive(false);
        go.transform.SetParent(this.transform, false);
        base.UnSpawn(name, obj);
    }
    public override void Release()
    {
        base.Release();
        foreach(PoolObject item in m_Objects)
        {
            if(DateTime.Now.Ticks - item.LastUseTime.Ticks >= m_ReleaseTime*10000000)
            {
                Debug.Log("GameObjectPool release time: " + DateTime.Now);
                Destroy(item.Object);
                Manager.Resource.MinusBundleCount(item.Name);
                m_Objects.Remove(item);
                Release();
                return;
            }
        }
    }
}
