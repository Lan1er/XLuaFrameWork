using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameMode GameMode;   
    void Start()
    {
        AppConst.GameMode = this.GameMode;
        DontDestroyOnLoad(this);

        Manager.Resource.ParseVersionFile();
        Manager.Lua.Init(
            ()=>
            {
                Manager.Lua.StartLua("Main");
                XLua.LuaFunction func = Manager.Lua.LuaEnv.Global.Get<XLua.LuaFunction>("Main");
                func.Call();
            }
            );
    }
}
  