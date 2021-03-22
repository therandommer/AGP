using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillPage : MonoBehaviour
{
    public CanvasGroup SkillPageCanvas;
    public CanvasGroup DetailedSkillCanvas;
    public Image SkillImage;
    public Text SkillName;
    public Text SkillDesc;
    public GameObject SkillPrefab;
    
    public void ShowSkillPage()
    {
        SkillPageCanvas.alpha = 1;
        SkillPageCanvas.blocksRaycasts = true;
    }
    public void ShowDetailedSkillCanvas()
    {
        DetailedSkillCanvas.alpha = 1;
        DetailedSkillCanvas.blocksRaycasts = true;
    }

    public void HideSkillPage()
    {
        SkillPageCanvas.alpha = 0;
        SkillPageCanvas.blocksRaycasts = false;
    }

    public void HideDetailedSkillCanvas()
    {
        DetailedSkillCanvas.alpha = 0;
        DetailedSkillCanvas.blocksRaycasts = false;
    }

    public void PopulateSkillList()
    {
        for (int i = 0; i < GameState.CurrentPlayer.Skills.Count; i++)
        {
            ///For as many skills in the list, instantiate the prefab and add to the list
        }
    }
}
