using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayText : MonoBehaviour
{
    private string txt;
    private Text talk;
    public float letterPause = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ShowText(Text text, string word)
    {
        txt = word;
        talk = text;
        talk.text = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in txt.ToCharArray())
        {
            if (LostStoryGame.Instance.GetTalkPlay())
            {
                talk.text += letter;
                yield return new WaitForSeconds(letterPause);
            }
            else 
            {
                StopAllCoroutines();
            }
        }

        LostStoryGame.Instance.SetTalkPlay(false);
        StopAllCoroutines();
    }
}
