using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "DialogData", menuName = "DialogSystem")]
public class DialogData : ScriptableObject
{
    public List<Message> Messages = new();
    public List<string> Oprions = new();
}

[Serializable]
public class Message
{
    public string Speaker;

    [TextArea(5, 10)]
    public string Text;

    public Message(string speaker, string text)
    {
        Speaker = speaker;
        Text = text;
    }
}
