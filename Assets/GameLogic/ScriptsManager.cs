
using UnityEngine;
using System.IO;
using System.Xml;
using System;
using System.Collections.Generic;

public class ScriptsManager : IGameManager
{
    public ScriptsManager(LostStoryGame lostStoryGame) : base(lostStoryGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        loadScripts();
        LoadScence(0, 0, 0);
    }

    int index;

    //存放当前场景index,角色index,谈话顺序index
    int sceneIndex;
    int roleIndex;
    int talkIndex;


    List<string> txt;
    public Dictionary<int, XmlNode> dicScenes;

    private List<XmlNode> roles;


    public void loadScripts()
    {
        TextAsset ta = Resources.Load("Scripts/test", typeof(TextAsset)) as TextAsset;
        ReadXML(new MemoryStream(ta.bytes));
    }

    void ReadXML(Stream stream)
    {
        dicScenes = new Dictionary<int, XmlNode>();
        roles = new List<XmlNode>();
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(stream);
        XmlNode scenes = xmldoc.SelectSingleNode("Scences");
        foreach (XmlNode node in scenes.ChildNodes)
        {
            string id = node.Attributes["id"].Value;
            //string lang = node.Attributes["lang"].Value;
            //string price = node.SelectSingleNode("price").InnerText;
            dicScenes.Add(int.Parse(id), node);
        }
    }

    public void LoadScence(int scIndex, int roIndex = 0, int taIndex = 0)
    {
        sceneIndex = scIndex;
        roleIndex = roIndex;
        talkIndex = taIndex;

        XmlNode scene = dicScenes[scIndex];
        lostStoryGame.ChangeBackground(scene.SelectSingleNode("background").InnerText);

        XmlNode content = scene.SelectSingleNode("content");

        roles.Clear();
        foreach (XmlNode role in content.ChildNodes)
        {
            roles.Add(role);
        }

        LoadRole();
    }

    public void LoadRole()
    {
        
        if (roles[roleIndex].SelectSingleNode("people") != null)
        {
            string people = roles[roleIndex].SelectSingleNode("people").InnerText;
            //显示人物
        }

        string name;
        if (roles[roleIndex].SelectSingleNode("name") != null)
        {
             name = roles[roleIndex].SelectSingleNode("name").InnerText;
            //显示名字
        }
        else
        {
             name = "";
        }
        lostStoryGame.ChangRoleName(name);

        if (roles[roleIndex].SelectSingleNode("talk") != null)
        {
            //在此解析对话内容并存起来
            string talk = roles[roleIndex].SelectSingleNode("talk").InnerText;
            txt = new List<string>(talk.Split(new string[] { "\\r\\n" }, StringSplitOptions.None));
        }
        LoadTalk();

    }

    public void LoadTalk()
    {
        if (txt.Count <= talkIndex)
        {
            roleIndex += 1;
            talkIndex = 0;
            if (roles.Count <= roleIndex)
            {
                roleIndex = 0;
                Debug.Log("进入选择状态，跳关。");
                LoadScence(1, 0, 0);
            }
            else
            {
                LoadRole();
            }
        }
        else
        {
            lostStoryGame.PlayText(txt[talkIndex]);
            Debug.Log(txt[talkIndex]);
            talkIndex += 1;
        }
    }

}