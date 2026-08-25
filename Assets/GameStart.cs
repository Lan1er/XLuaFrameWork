using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameMode GameMode;   
    void Awake()
    {
        AppConst.GameMode = this.GameMode;
        DontDestroyOnLoad(this);
    }
}
 