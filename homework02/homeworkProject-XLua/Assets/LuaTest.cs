using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.DoString("CS.UnityEngine.Debug.Log('Hello world!')");
        luaEnv.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
