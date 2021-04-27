using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MessageType
{
    GainedQuest,
    LeveledUp
}

public class ShowMessage : MonoBehaviour
{
    public GameObject MessagePanel;

    public TMP_Text MessageTitle;

    public TMP_Text MessageText;

    public Image MessageImage;

    public static ShowMessage Instance;

    void Start()
    {
        Instance = this;
        MessagePanel.SetActive(false);
    }

    public void StartCouroutineForMessage(string Title, string Message, Sprite Image, float Duration)
    {
        StartCoroutine(Show(Title, Message, Image, Duration));
    }

    IEnumerator Show(string Title, string Message, Sprite Image, float TimeMessageShownFor)
    {
        MessageTitle.text = Title;
        MessageText.text = Message;
        MessageImage.sprite = Image;
        MessagePanel.SetActive(true);

        yield return new WaitForSeconds(TimeMessageShownFor);

        MessagePanel.SetActive(false);
    }

}
