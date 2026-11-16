using UnityEngine;

public class WinUI : MonoBehaviour
{
    [SerializeField]
    private GameObject winScreen;

    private void Awake()
    {
        Disable();
    }

    public void Enable()
    {
        Player p = FindAnyObjectByType<Player>(FindObjectsInactive.Include);
        if (p != null)
        {
            p.InputReader.DisablePlayer();
            p.InputReader.DisableDialogUI();
            p.PlayerMovement.StopPlayer();

            p.InputReader.EnableUI();
        }
        else
        {
            Debug.LogWarning("Player wasnt found");
        }

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        winScreen.SetActive(true);
    }

    public void Disable() => winScreen.SetActive(false);
}
