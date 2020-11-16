
using UnityEngine;
using System.IO;
using System.Xml;
using System;
using System.Collections.Generic;

public class ScriptsManager : IGameManager
{

    //存放当前场景index,角色index,谈话顺序index
    int sceneIndex;
    int roleIndex;
    int talkIndex;

    string currentTalk;//当前对话内容

    bool scriptBegin = false;//是否正在自动对话
    bool selectBegin = false;//是否处于选择模式

    public bool TalkPlay { get; set; } = false;

    public ScriptsManager(LostStoryGame lostStoryGame) : base(lostStoryGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        loadScripts();

    }


    public override void Update()
    {
        if (scriptBegin == false)
        {
            LoadScence(0, 0, 0);
            scriptBegin = true;
        }
    }

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

        selectBegin = false;
        XmlNode scene = dicScenes[scIndex];
        lostStoryGame.ChangeBackground(scene.SelectSingleNode("background").InnerText);
        //代表本次场景谈话人物以及内容
        XmlNode content = scene.SelectSingleNode("content");
        //代表本次场景结束后的选项内容和进入下一个场景的条件
        XmlNode select = scene.SelectSingleNode("select");
        LoadSelect(select);

        roles.Clear();
        foreach (XmlNode role in content.ChildNodes)
        {
            roles.Add(role);
        }

        LoadRole();
    }

    private void LoadSelect(XmlNode select)
    {
        lostStoryGame.ShowSelect(false);
        foreach (XmlNode selection in select.ChildNodes)
        {
            //在UI增加按钮和添加点击事件
            lostStoryGame.DrawSelection(selection);
        }
    }

    public void ChangeScene(XmlNode selection)
    {
        int sucIndex = int.Parse(selection.SelectSingleNode("success").InnerText);
        LoadScence(sucIndex,0,0);
    }

    public void LoadRole()
    {
        XmlNode people = roles[roleIndex].SelectSingleNode("people");

        //显示人物
        lostStoryGame.DrawPeople(people);


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
        if (selectBegin == true) return;

        if (TalkPlay == false)
        {
            if (txt.Count <= talkIndex)
            {
                roleIndex += 1;
                talkIndex = 0;
                if (roles.Count <= roleIndex)
                {
                    roleIndex = 0;
                    Debug.Log("进入选择状态，跳关。");
                    selectBegin = true;
                    lostStoryGame.ShowSelect(true);
                }
                else
                {
                    LoadRole();
                }
            }
            else
            {
                TalkPlay = true;
                currentTalk = txt[talkIndex];
                lostStoryGame.PlayText(currentTalk);
                Debug.Log(currentTalk);

                talkIndex += 1;
            }
        }
        else
        {
            TalkPlay = false;
            lostStoryGame.ShowText(currentTalk);
        }

    }

}