using TMPro;
using UnityEngine;

public class OptionDialog : MonoBehaviour, IDialogOption
{
    [SerializeField]
    private string text = string.Empty;

    [SerializeField]
    private TMP_Text dialogText;

    [SerializeField]
    private GameObject highlight;

    public string Text
    {
        get => text;
        set
        {
            text = value ?? string.Empty;
            dialogText.text = text;
        }
    }

    private void Awake()
    {
        Debug.Assert(highlight != null, "dialog option doesnt have highlight asigned");

        SetHighlight(false);
    }

    public void SetHighlight(bool isOn) => highlight.SetActive(isOn);
}
