using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{
    public Text name;
    public Text talk;
    public Image background;
    public Image left;
    public Image right;

    // Use this for initialization
    void Start()
    {
        LoadScripts.instance.loadScripts("123.txt");
        handleData(LoadScripts.instance.loadNext());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            handleData(LoadScripts.instance.loadNext());
        }
    }

    public void setText(Text text, string content)
    {
        print(content);
        text.text = content;
    }

    public void setImage(Image image, string atlasName, string picName)
    {
        SpriteAtlas atlas = Resources.Load("Atlas/" + atlasName) as SpriteAtlas;
        Sprite spr = atlas.GetSprite(picName);
        image.sprite = spr;
    }


    public void handleData(ScriptData data)
    {
        if (data == null)
            return;
        if (data.type == 0)
        {
            setImage(background, "Background", data.picName);
            print(data.picName);
            handleData(LoadScripts.instance.loadNext());
        }
        else
        {

            if (data.pos.CompareTo("left") == 0)
            {
                left.gameObject.SetActive(true);
                setImage(left, "Role", data.picName);
                right.gameObject.SetActive(false);
            }
            else
            {
                right.gameObject.SetActive(true);
                setImage(right, "Role", data.picName);
                left.gameObject.SetActive(false);
            }
            setText(name, data.name);
            setText(talk, data.talk);


        }
    }
}