using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenUI : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider loadingSlider;

    private void Awake()
    {
        Disable();
    }

    public void Enable()
    {
        Player p = FindAnyObjectByType<Player>(FindObjectsInactive.Include);
        if(p != null)
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

        if (loadingSlider != null)
        {
            loadingSlider.interactable = false;
            loadingSlider.minValue = 0;
            loadingSlider.maxValue = 1;
            loadingSlider.value = 0;
        }
        loadingScreen.SetActive(true);
    }

    public void Disable() => loadingScreen.SetActive(false);

    public void SetSliderValue(float value)
    {
        if (loadingSlider == null) return;
        float normalized = Mathf.Clamp01(value);
        loadingSlider.value = normalized;
    }
}
