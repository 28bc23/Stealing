using UnityEngine;


[RequireComponent(typeof(PlayerBodyComponent))]
public class Player : MonoBehaviour
{

    public IPlayerBody PlayerBody { get; private set; }

    public IInputReader InputReader { get; private set; }

    void Awake()
    {
        InputReader = new InputReader();


        PlayerBody = GetComponent<PlayerBodyComponent>();
        if (PlayerBody == null)
        {
            Debug.LogWarning("Player didnt had PlayerBodyComponent, adding one");
            PlayerBody = gameObject.AddComponent<PlayerBodyComponent>();
        }

        PlayerBody.PlayerComponent.Init(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public interface IPlayerComponent
{
    public void Init(Player player);
}
