using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

public class PoolObject
{
    //具体对象
    public UObject Object;
    //对象名字
    public string Name;
    //最后一次使用时间
    public DateTime LastUseTime;
    public PoolObject(string name,UObject obj)
    {
        Name = name;
        Object = obj;
        LastUseTime = DateTime.Now;
    }
}
