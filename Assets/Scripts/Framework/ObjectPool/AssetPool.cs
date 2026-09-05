using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

public class AssetPool : PoolBase
{
    public override UObject Spawn(string name)
    {
        return base.Spawn(name);
    }
    public override void UnSpawn(string name, UObject obj)
    {
        base.UnSpawn(name, obj);
    }
    public override void Release()
    {
        base.Release();
        foreach(PoolObject item in m_Objects)
        {
            if(DateTime.Now.Ticks - item.LastUseTime.Ticks >= m_ReleaseTime*10000000)
            {
                Debug.Log("AssetPool release time: " + DateTime.Now + "unload ab:" + item.Name);
                Manager.Resource.UnloadBundle(item.Object);
                m_Objects.Remove(item);
                Release();
                return;
            }
        }
    }
}
