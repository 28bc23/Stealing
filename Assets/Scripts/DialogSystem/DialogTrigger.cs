using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogTrigger : MonoBehaviour
{
    [field: SerializeField]
    public DialogData dialog { get; private set; }

    [SerializeField]
    private List<DialogTrigger> nextDialogs = new List<DialogTrigger>();

    [SerializeField]
    private bool destroyUnselectedOptions;

    private void Awake()
    {
        if (dialog == null)
        {
            Debug.LogWarning($"Dialog trigger ({gameObject.name}) doesnt have DialogData");
            return;
        }

        if(dialog.Oprions.Count > 0 && dialog.Oprions.Count != nextDialogs.Count)
        {
            Debug.LogWarning($"Dialog trigger ({gameObject.name}) doesnt have same amount of next dialogs as potions, but it can be right if you know what you are doing, idk.");
            return;
        }
    }

    public void NextDialog()
    {
        if (nextDialogs.Count == 0) return;

        nextDialogs[0].gameObject.SetActive(true);
        Destroy(gameObject);
    }

    public void NextDialog(int optionIdx)
    {
        if (optionIdx < 0) return;
        if (nextDialogs.Count >= (optionIdx+1)) return;

        nextDialogs[optionIdx].gameObject.SetActive(true);

        if (destroyUnselectedOptions)
        {
            for (int i = 0; i < nextDialogs.Count; i++)
            {
                if (i == optionIdx) continue;

                Destroy(nextDialogs[i].gameObject);
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameObject.TryGetComponent(out IDialogListener listener)) return;

        listener.TriggeredDialog(this);
    }
}
