using UnityEngine;
using UnityEngine.UI;

public class PopulatePlayerParty : MonoBehaviour
{

    public Button CharacterButton;

    public GameObject PartyMemberObject;

    public GameObject ActivePartyMemberObject;

    public Sprite WarriorImage;
    public Sprite MageImage;
    public Sprite RougeImage;
    public Sprite PaladinImage;

    public GameObject SelectedPartyMember;

    public PopulateStatList ReadSelectedMember;
    public PopulateAttackTypeList ReadSelectedMemberElement;

    public PopUpMenu AddToActiveCanvas;

    public PopUpMenu RemoveFromActiveCanvas;

    public PopUpMenu SetMainCharCanvas;
    public void PopulatePartyMemberList()
    {
        foreach (Transform child in PartyMemberObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (GameObject GO in GameState.PartyMembers)
        {
            Button newPM = Instantiate(CharacterButton);
            newPM.transform.SetParent(PartyMemberObject.transform);
            PartyMemberUI UI = newPM.GetComponent<PartyMemberUI>();

            UI.PartyMemberName.text = GO.GetComponent<PlayerController>().stats.PlayerProfile.name;
            switch (GO.GetComponent<PlayerController>().stats.PlayerProfile.Class)
            {
                case PlayerClass.Warrior:
                    UI.PartyMemberImage.sprite = WarriorImage;
                    break;
                case PlayerClass.Mage:
                    UI.PartyMemberImage.sprite = MageImage;
                    break;
                case PlayerClass.Rouge:
                    UI.PartyMemberImage.sprite = RougeImage;
                    break;
                case PlayerClass.Paladin:
                    UI.PartyMemberImage.sprite = PaladinImage;
                    break;
            }
            UI.PartyMemberGameObject = GO;
            newPM.onClick.AddListener(() => SelectPartyMember(UI.PartyMemberGameObject));
        }
    }

    public void PopulateActivePartyMemberList()
    {
        foreach (Transform child in ActivePartyMemberObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (GameObject GO in GameState.ActiveParty)
        {
            Button newPM = Instantiate(CharacterButton, Vector3.zero, Quaternion.identity);
            newPM.transform.SetParent(ActivePartyMemberObject.transform);
            PartyMemberUI UI = newPM.GetComponent<PartyMemberUI>();
            Debug.Log(GO.name);
            UI.PartyMemberName.text = GO.GetComponent<PlayerController>().stats.PlayerProfile.name;
            switch (GO.GetComponent<PlayerController>().stats.PlayerProfile.Class)
            {
                case PlayerClass.Warrior:
                    UI.PartyMemberImage.sprite = WarriorImage;
                    break;
                case PlayerClass.Mage:
                    UI.PartyMemberImage.sprite = MageImage;
                    break;
                case PlayerClass.Rouge:
                    UI.PartyMemberImage.sprite = RougeImage;
                    break;
                case PlayerClass.Paladin:
                    UI.PartyMemberImage.sprite = PaladinImage;
                    break;
            }
            UI.PartyMemberGameObject = GO;
            newPM.onClick.AddListener(() => SelectActivePartyMember(UI.PartyMemberGameObject));
        }
    }

    public void SelectPartyMember(GameObject SelectedPM)
    {
        SelectedPartyMember = SelectedPM;
        ReadSelectedMember.Profile = SelectedPM.GetComponent<PlayerController>().stats.PlayerProfile;
        ReadSelectedMember.populateStat();
        ReadSelectedMemberElement.Profile = SelectedPM.GetComponent<PlayerController>().stats.PlayerProfile;
        ReadSelectedMemberElement.PopAttackTypeList();

        RemoveFromActiveCanvas.DisableTheMenu();
        AddToActiveCanvas.EnableTheMenu();
    }

    public void SelectActivePartyMember(GameObject SelectedPM)
    {
        SelectedPartyMember = SelectedPM;
        ReadSelectedMember.Profile = SelectedPM.GetComponent<PlayerController>().stats.PlayerProfile;
        ReadSelectedMember.populateStat();
        ReadSelectedMemberElement.Profile = SelectedPM.GetComponent<PlayerController>().stats.PlayerProfile;
        ReadSelectedMemberElement.PopAttackTypeList();

        AddToActiveCanvas.DisableTheMenu();
        if(GameState.CurrentPlayer.gameObject != SelectedPM)
        {
            RemoveFromActiveCanvas.EnableTheMenu();
            SetMainCharCanvas.EnableTheMenu();
        }
    }

    public void AddToActiveList()
    {
        if(GameState.ActiveParty.Count < 4)
        {
            for(int i = 0; i < GameState.PartyMembers.Count; i++)
            {
                if(SelectedPartyMember == GameState.PartyMembers[i])
                {
                    GameState.ActiveParty.Add(SelectedPartyMember);
                    GameState.PartyMembers.Remove(SelectedPartyMember);
                }
            }
            PopulateActivePartyMemberList();
            PopulatePartyMemberList();
        }
        else
        {
            ShowMessage.Instance.StartCouroutineForMessage("Error", "You cannot have more than 4 active party members", WarriorImage, 1f);
        }
    }

    public void RemoveFromActiveList()
    {
        if (GameState.ActiveParty.Count >= 2)
        {
            Debug.Log("Trying to remove");
            for (int i = 0; i < GameState.ActiveParty.Count; i++)
            {
                Debug.Log("Trying to remove " + SelectedPartyMember);
                if (SelectedPartyMember == GameState.ActiveParty[i])
                {
                    GameState.PartyMembers.Add(SelectedPartyMember);
                    GameState.ActiveParty.Remove(SelectedPartyMember);
                }
            }
            PopulateActivePartyMemberList();
            PopulatePartyMemberList();
        }
        else
        {
            Debug.Log(ShowMessage.Instance.name);
            ShowMessage.Instance.StartCouroutineForMessage("Error", "You must have at least 1 active party members", WarriorImage, 1f);
        }
    }
    public void MakeMain()
    {
        GameState.ChangeCurrentPlayer(SelectedPartyMember);
    }
}
