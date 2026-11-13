using UnityEngine;
using TMPro;
using System;
using System.Linq;


public interface IPlayerDialogListener : IDialogListener, IPlayerComponent { }

public class DialogListenerPlayerComponent : MonoBehaviour, IPlayerDialogListener
{
    public IPlayerComponent PlayerComponent => this;

    [SerializeField]
    private GameObject dialogParent;
    [SerializeField]
    private GameObject dialogOptionsParent;
    [SerializeField]
    private GameObject dialogOptionPrefab;
    [SerializeField]
    private TMP_Text dialogText;

    Player p;
    private DialogTrigger currentDialogTrigger = null;
    private int currentDialogIdx = 0;
    private int selectedOption = 0;

    public void Init(Player player)
    {
        p = player;

        Debug.Assert(dialogParent != null, "PlayerDialogListener doesnt have Dialog praent asigned");
        Debug.Assert(dialogOptionsParent != null, "PlayerDialogListener doesnt have DialogOption parent asigned");
        Debug.Assert(dialogOptionPrefab != null, "PlayerDialogListener doesnt have DialogOption prefab asigned");
        Debug.Assert(dialogText != null, "PlayerDialogListener doesnt have Dialog text asigned");
        Debug.Assert(dialogOptionPrefab.TryGetComponent(out IDialogOption o), "Dioalog option prefab doesnt have DialogOption component");

        dialogParent.SetActive(false);
        dialogOptionsParent.SetActive(false);

        dialogText.text = string.Empty;

        p.InputReader.OnNextDialogEvent += OnNextDialogEventHandle;
        p.InputReader.OnOptionUpEvent += OnOptionUpEventHandle;
        p.InputReader.OnOptionDownEvent += OnOptionDownEventHandle;
    }

    public void TriggeredDialog(DialogTrigger instigator)
    {
        EnableDialogInput();

        if (instigator.dialogs.Count <= 0) return;

        currentDialogTrigger = instigator;

        NextDialogText(true);

        foreach(Transform o in dialogOptionsParent.transform) { Destroy(o.gameObject); }

        if(instigator.dialogs.LastOrDefault()?.Options.Count > 0)
        {
            selectedOption = 0;
            DialogData lastDialog = instigator.dialogs.Last();
            for (int i = 0; i < lastDialog.Options.Count; i++)
            {
                GameObject option = Instantiate(dialogOptionPrefab);
                option.transform.SetParent(dialogOptionsParent.transform);

                IDialogOption dialogOption = option.GetComponent<IDialogOption>();
                dialogOption.Text = lastDialog.Options[i];
                if (i == selectedOption) dialogOption.SetHighlight(true);
            }

            if(instigator.dialogs.Count == 1)
                dialogOptionsParent.SetActive(true);
        }
    }


    private void OnOptionDownEventHandle()
    {
        if(dialogOptionsParent.transform.childCount == 0 || selectedOption == 0) return;

        dialogOptionsParent.transform.GetChild(selectedOption--).GetComponent<IDialogOption>().SetHighlight(false);
        dialogOptionsParent.transform.GetChild(selectedOption).GetComponent<IDialogOption>().SetHighlight(true);
    }

    private void OnOptionUpEventHandle()
    {
        if (dialogOptionsParent.transform.childCount == 0 || selectedOption == dialogOptionsParent.transform.childCount - 1) return;

        dialogOptionsParent.transform.GetChild(selectedOption++).GetComponent<IDialogOption>().SetHighlight(false);
        dialogOptionsParent.transform.GetChild(selectedOption).GetComponent<IDialogOption>().SetHighlight(true);
    }

    private void OnNextDialogEventHandle()
    {
        if (currentDialogTrigger == null) return;

        if(currentDialogIdx >= currentDialogTrigger.dialogs.Count-1)
        {
            if (dialogOptionsParent.transform.childCount > 0) currentDialogTrigger.NextDialog(selectedOption);
            else currentDialogTrigger.NextDialog();

            DisableDialogInput();

            selectedOption = 0;
            return;
        }

        NextDialogText();

        if(currentDialogIdx == currentDialogTrigger.dialogs.Count - 1 && dialogOptionsParent.transform.childCount > 0)
        {
            dialogOptionsParent.SetActive(true);
        }
    }

    void NextDialogText(bool start = false)
    {
        if (start)
        {
            currentDialogIdx = 0;
            dialogText.text = $"{currentDialogTrigger.dialogs[currentDialogIdx].Message.Speaker}: {currentDialogTrigger.dialogs[currentDialogIdx].Message.Text}";
            dialogParent.SetActive(true);
            return;
        }

        dialogText.text = $"{currentDialogTrigger.dialogs[++currentDialogIdx].Message.Speaker}: {currentDialogTrigger.dialogs[currentDialogIdx].Message.Text}";
    }

    private void EnableDialogInput()
    {
        p.InputReader.DisablePlayer();
        p.InputReader.DisableUI();
        p.InputReader.EnableDialogUI();
        p.PlayerMovement.StopPlayer();
    }

    private void DisableDialogInput()
    {
        p.InputReader.EnablePlayer();
        p.InputReader.DisableDialogUI();

        dialogParent.SetActive(false);
        dialogOptionsParent.SetActive(false);
    }
}
