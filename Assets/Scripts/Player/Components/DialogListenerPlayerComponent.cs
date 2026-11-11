using UnityEngine;
using TMPro;


public interface IPlayerDialogListener : IDialogListener, IPlayerComponent { }

public class DialogListenerPlayerComponent : MonoBehaviour, IPlayerDialogListener
{
    public IPlayerComponent PlayerComponent => this;

    [SerializeField]
    private GameObject DialogParent;
    [SerializeField]
    private GameObject DialogOptionsParent;
    [SerializeField]
    private GameObject DialogOptionPrefab;
    [SerializeField]
    private TMP_Text DialogText;

    Player p;
    private DialogTrigger currentDialogTrigger = null;

    public void Init(Player player)
    {
        p = player;

        Debug.Assert(DialogParent != null, "PlayerDialogListener doesnt have Dialog praent asigned");
        Debug.Assert(DialogOptionsParent != null, "PlayerDialogListener doesnt have DialogOption parent asigned");
        Debug.Assert(DialogOptionPrefab != null, "PlayerDialogListener doesnt have DialogOption prefab asigned");
        Debug.Assert(DialogText != null, "PlayerDialogListener doesnt have Dialog text asigned");

        DialogParent.SetActive(false);
        DialogOptionsParent.SetActive(false);

        foreach(GameObject o in DialogOptionsParent.transform)
        {
            Destroy(o);
        }
    }

    public void TriggeredDialog(DialogTrigger instigator)
    {
        p.InputReader.DisablePlayer();
        p.InputReader.DisableUI();
        p.InputReader.EnableDialogUI();
        p.PlayerMovement.StopPlayer();

        currentDialogTrigger = instigator;
    }
}
