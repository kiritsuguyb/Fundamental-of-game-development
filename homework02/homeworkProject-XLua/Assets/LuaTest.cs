using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

public class LuaTest : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    LuaEnv lua;
    LuaFunction update;
    LuaFunction start;
    LuaFunction helloWorld;
    LuaFunction onenable;
    void Start()
    {
        lua = new LuaEnv();
        lua.DoString("require'lua'");
        update = lua.Global.Get<LuaFunction>("Update");
        start = lua.Global.Get<LuaFunction>("Start");
        helloWorld = lua.Global.Get<LuaFunction>("helloWorld");
        start.Call();
    }
    public void LuaHelloWorld()
    {
        helloWorld.Call(text);
    }
    private void Update()
    {
        update.Call();
    }

    private void OnDisable()
    {
        lua.Dispose();
    }
}
