using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UIManager : IGameManager
{
    public Text talk;
    public Text roleName;
    public Image background;

    public GameObject Talk;
    public GameObject Name;
    public GameObject Background;

    public UIManager(LostStoryGame lostStoryGame) : base(lostStoryGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        Background = UITool.FindUIGameObject("Background");
        background = Background.GetComponent<Image>();
        Talk = UITool.FindUIGameObject("Talk");
        talk = UITool.FindUIGameObject("talk").GetComponent<Text>();
        Name = UITool.FindUIGameObject("Name");
        roleName = UITool.FindUIGameObject("name").GetComponent<Text>();
    }

    private void SetText(Text text, string content)
    {
        text.text = content;
    }

    private void SetImage(Image image, string atlasName, string picName)
    {
        SpriteAtlas atlas = Resources.Load("Atlas/" + atlasName) as SpriteAtlas;
        Sprite spr = atlas.GetSprite(picName);
        image.sprite = spr;
    }

    public void ChangeBackground(string picName)
    {
        SetImage(background, "Background", picName);
    }

    public void ChangRoleName(string name)
    {
        if (name != "")
        {
            Name.SetActive(true);
            SetText(roleName, name);
        }
        else
        {
            Name.SetActive(false);
        }
    }

    public void PlayText(string txt)
    {
        talk.transform.GetComponent<PlayText>().ShowText(talk, txt);
        
    }
}

