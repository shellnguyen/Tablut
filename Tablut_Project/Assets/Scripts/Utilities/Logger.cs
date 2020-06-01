using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
    //Singleton Init
    private static readonly Logger instance = new Logger();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static Logger()
    {
    }

    private Logger()
    {
        Initialize();
    }

    public static Logger Instance
    {
        get
        {
            return instance;
        }
    }
    //

    private static ILogger m_Logger;

    public void Initialize()
    {
#if UNITY_EDITOR
        m_Logger = Debug.unityLogger;
#endif
    }

    public void PrintLog(string tag, string message)
    {
#if UNITY_EDITOR
        Log(LogType.Log, tag, message);
#endif
    }

    public void PrintError(string tag, string message)
    {
#if UNITY_EDITOR
        Log(LogType.Error, tag, message);
#endif
    }

    public void PrintExc(string tag, string message)
    {
#if UNITY_EDITOR
        Log(LogType.Exception, tag, message);
#endif
    }

    private void Log(LogType type, string tag, string message)
    {
#if UNITY_EDITOR
        m_Logger.Log(type, tag, message);
#endif
    }
}
