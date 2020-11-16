using System;
using System.Collections.Generic;
using System.Xml;

public class LostStoryGame
{
    private static LostStoryGame instance;
    public static LostStoryGame Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new LostStoryGame();
            }
            return instance;
        }
    }

    private GameManager gameManager = null;
    private ScriptsManager scriptsManager = null;
    private UIManager uIManager = null;

    private LostStoryGame() { }

    public void Initinal()
    {
        gameManager = new GameManager(this);
        uIManager = new UIManager(this);
        scriptsManager = new ScriptsManager(this);
        
    }

    public void Update()
    {
        gameManager.Update();
        scriptsManager.Update();
        uIManager.Update();
    }

    public void LoadScence(int scIndex, int roIndex = 0, int taIndex = 0)
    {
        scriptsManager.LoadScence( scIndex,  roIndex , taIndex);
    }

    public void LoadTalk()
    {
        scriptsManager.LoadTalk();
    }

    public void ChangeBackground(string picName)
    {
        uIManager.ChangeBackground(picName);
    }

    public void ChangRoleName(string name)
    {
        uIManager.ChangRoleName(name);
    }

    public void PlayText(string txt)
    {
        uIManager.PlayText(txt);
    }

    public void ShowText(string txt)
    {
        uIManager.ShowText(txt);
    }

    public bool GetTalkPlay()
    {
        return scriptsManager.TalkPlay;
    }

    public void SetTalkPlay(bool playing)
    {
        scriptsManager.TalkPlay = playing;
    }

    public void DrawPeople(XmlNode people) 
    {
        uIManager.DrawPeople(people);
    }

    public void DrawSelection(XmlNode selection)
    {
        uIManager.DrawSelection(selection);
    }

    public void ShowSelect(bool show)
    {
        uIManager.ShowSelect(show);
    }

    public void ChangeScene(XmlNode selection)
    {
        scriptsManager.ChangeScene(selection);
    }
}

