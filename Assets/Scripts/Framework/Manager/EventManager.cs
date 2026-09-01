using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventHandler(object args);
    // 存储所有事件
    Dictionary<int , EventHandler> m_Events = new Dictionary<int , EventHandler>();
    /// <summary>
    /// 事件订阅
    /// </summary>
    /// <param name="id"></param>
    /// <param name="e"></param>
    public void Subscribe(int id,EventHandler e)
    {
        if (m_Events.ContainsKey(id)) 
            m_Events[id] += e;
        else
            m_Events.Add(id, e);
    }
    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="id"></param>
    /// <param name="e"></param>
    public void UnSubscribe(int id, EventHandler e)
    {
        if(m_Events.ContainsKey(id))
        {
            if (m_Events[id] != null)
                m_Events[id] -= e;
            if (m_Events[id] == null)
                m_Events.Remove(id);
        }
    }
    public void Fire(int id,object args = null)
    {
        EventHandler handler;
        if(m_Events.TryGetValue(id, out handler))
        {
            handler(args);
        }
    }
}
