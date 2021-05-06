using UnityEngine;
using UnityEngine.SceneManagement;

public class MessagingClientBroadcast : MonoBehaviour
{
    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        MessagingManager.Instance.Broadcast();
    }
    */
    public MessagingClientReceiver MCR;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameState.CurrentPlayer.GetComponent<PlayerMovement>().CantMove = true;
        GameState.CurrentPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (Quest quest in GameState.CurrentPlayer.QuestLog)
        {
            if (quest.questType == QuestType.TalkingQuest)
            {
                if (gameObject.GetComponent<Npc>().Name == quest.NpcToTalkTo)
                {
                    quest.increaseAmount();
                }
            }
        }
        GameState.CurrentPlayer.LastSceneName = SceneManager.GetActiveScene().name;

        GameState.CurrentPlayer.LastScenePosition = GameState.CurrentPlayer.gameObject.transform.position;

        MessagingManager.Instance.Subscribe(MCR.StartConvo);

        MessagingManager.Instance.Broadcast();

        MessagingManager.Instance.UnSubscribe(MCR.StartConvo);
    }
}
