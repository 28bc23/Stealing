using UnityEngine;

[CreateAssetMenu(fileName = "GameOverTriggerAction", menuName = "TriggerActions/GameOver")]
public class GameOverTriggerAction : ScriptableObject
{
    public void ShowGameOverScreen()
    {
        GameOverUI gameOverUI = FindAnyObjectByType<GameOverUI>(FindObjectsInactive.Include);
        if (gameOverUI == null) Debug.Log("`GameOverUI` component wasnt found in scene so no gameOver screen will be enabled");
        gameOverUI?.Enable();
    }
}
