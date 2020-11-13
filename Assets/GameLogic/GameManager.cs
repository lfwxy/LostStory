using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Collections.Generic;

public class GameManager:IGameManager
{
    public Text roleName;
    public Text talk;
    public Image background;
    public Image left;
    public Image right;



    public GameManager(LostStoryGame lostStoryGame):base(lostStoryGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lostStoryGame.LoadTalk();
        }
    }




}