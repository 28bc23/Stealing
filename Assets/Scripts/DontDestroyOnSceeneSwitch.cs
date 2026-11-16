using UnityEngine;

public class DontDestroyOnSceeneSwitch : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
