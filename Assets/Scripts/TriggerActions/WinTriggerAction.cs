using UnityEngine;

[CreateAssetMenu(fileName = "WinTriggerAction", menuName = "TriggerActions/Win")]
public class WinTriggerAction : ScriptableObject
{
    public void ShowWinScreen()
    {
        WinUI winUI = FindAnyObjectByType<WinUI>(FindObjectsInactive.Include);
        if (winUI == null) Debug.Log("`WinUI` component wasnt found in scene so no win screen will be enabled");
        winUI?.Enable();
    }
}