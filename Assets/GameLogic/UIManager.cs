using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Xml;

public class UIManager : IGameManager
{
    public Text talk;
    public Text roleName;
    public Image background;

    public GameObject Talk;
    public GameObject Name;
    public GameObject Background;
    public GameObject People;
    public GameObject Selection;

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
        People = UITool.FindUIGameObject("People");
        Selection = UITool.FindUIGameObject("Selection");
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

    public void ShowText(string txt)
    {
        SetText(talk, txt);
    }

    public void DrawPeople(XmlNode people)
    {
        int pos;
        float scale;
        string pic;

        foreach (Transform human in People.transform)
        {
            human.gameObject.SetActive(false);
        }

        if (people == null) return;

        foreach (XmlNode human in people.ChildNodes)
        {
            pos = int.Parse(human.SelectSingleNode("pos").InnerText);
            scale = float.Parse(human.SelectSingleNode("scale").InnerText);
            pic = human.SelectSingleNode("pic").InnerText;

            Image role = People.transform.GetChild(pos).GetComponent<Image>();
            SetImage(role, "Role", pic);
            //role.SetNativeSize();
            role.gameObject.SetActive(true);
        }

    }

    public void DrawSelection(int index, XmlNode selection)
    {
        GameObject go;
        Button btn;
        if (Selection.transform.childCount <= index)
        {
            GameObject goPrefab = Resources.Load("Prefabs/select") as GameObject;
            go = UnityEngine.Object.Instantiate(goPrefab);
            btn = go.GetComponent<Button>();
            go.transform.SetParent(Selection.transform);
            go.transform.localScale = Vector3.one;

        }
        else
        {
            go = Selection.transform.GetChild(index).gameObject;
            btn = go.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
        }
        go.SetActive(true);
        go.transform.GetChild(0).GetComponent<Text>().text = selection.SelectSingleNode("text").InnerText;
        btn.onClick.AddListener(delegate { ChangeScene(selection); });

    }

    private void ChangeScene(XmlNode selection)
    {
        lostStoryGame.ChangeScene(selection);
    }

    public void ShowSelect(bool show)
    {
        if (show == false)
        {
            foreach (Transform tr in Selection.transform)
            {
                tr.gameObject.SetActive(false);
            }
        }
        Selection.SetActive(show);
    }
}

