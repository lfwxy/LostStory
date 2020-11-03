using UnityEngine;
using System.Collections;

public class ScriptData
{
    public int type;
    public string pos;
    public string name;
    public string talk;
    public string picName;

    public ScriptData(int type, string pos, string name, string talk, string picName)
    {
        this.type = type;
        this.pos = pos;
        this.name = name;
        this.talk = talk;
        this.picName = picName;
    }

    public ScriptData(int type, string picName)
    {
        this.type = type;
        this.picName = picName;

    }
}

