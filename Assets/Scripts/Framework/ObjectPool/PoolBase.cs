using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

public class PoolBase : MonoBehaviour
{
    //自动释放时间
    protected float m_ReleaseTime;
    //上次释放时间（毫微秒）10^7
    protected long m_LastReleaseTime = 0;
    //真正的对象池
    protected List<PoolObject> m_Objects;
    public void Start()
    {
        m_LastReleaseTime = DateTime.Now.Ticks;
    }
    //初始化对象池
    public void Init(float time)
    {
        m_ReleaseTime = time;
        m_Objects = new List<PoolObject>();
    }
    //取出对象
    public virtual UObject Spawn(string name)
    {
        foreach(PoolObject po in m_Objects)
        {
            if(po.Name == name)
            {
                m_Objects.Remove(po);
                return po.Object;
            }
        }
        return null;
    }
    //回收对象
    public virtual void UnSpawn(string name,UObject obj)
    {
        PoolObject po = new PoolObject(name,obj);
        m_Objects.Add(po);
    }
    public virtual void Release()
    {

    }
    void Update()
    {
        if (DateTime.Now.Ticks - m_LastReleaseTime >= m_ReleaseTime * 10000000)
        {
            m_LastReleaseTime = DateTime.Now.Ticks;
            Release();
        }
    }
}
