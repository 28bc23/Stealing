using UnityEngine;
using UnityEngine.SceneManagement;

public class KarmaCounter : MonoBehaviour
{
    public static KarmaCounter Instance { get; private set; }

    [field: SerializeField]
    public float Karma { get; private set; } = 0;

    [SerializeField]
    private float levelStartKarma = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable() { SceneManager.sceneLoaded += BackupKarma; }
    private void OnDisable() { SceneManager.sceneLoaded -= BackupKarma; }

    private void Start()
    {
        BackupKarma(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    public void AddKarma(float karma) => Karma += karma;

    public void ResetKarmaToLevelStart() => Karma = levelStartKarma;
    private void BackupKarma(Scene scena, LoadSceneMode mode) => levelStartKarma = Karma;
}
