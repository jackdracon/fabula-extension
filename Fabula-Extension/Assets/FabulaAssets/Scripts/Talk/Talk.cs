using System;
using UnityEngine;
//======JSON VALUES=======================

[Serializable]
public class Talk
{
    public string Speaker;
    public string Speak;
}
//Talk json structure
[Serializable]
public class JsonValues
{
    public Talk[] Talk;

    //return the talk object
    public static JsonValues CreateFromJSON(string _data)
    {
        return JsonUtility.FromJson<JsonValues>(_data);
    }
}
//======================================