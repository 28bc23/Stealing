using UnityEngine;


[RequireComponent(typeof(PlayerBodyComponent))]
[RequireComponent(typeof(PlayerFPSMovementComponent))]
public class Player : MonoBehaviour
{

    public IPlayerBody PlayerBody { get; private set; }
    public IPlayerMovement PlayerMovement { get; private set; }

    public IInputReader InputReader { get; private set; }

    void Awake()
    {
        InputReader = new InputReader();
        InputReader.EnablePlayer();
        InputReader.DisableUI();


        PlayerBody = GetComponent<PlayerBodyComponent>();
        PlayerMovement = GetComponent<PlayerFPSMovementComponent>();

        Debug.Assert(PlayerBody != null, "you fucked up (player dont have PlayerBody)");
        Debug.Assert(PlayerMovement != null, "you fucked up (player dont have PlayerMovement)");

        PlayerBody.PlayerComponent.Init(this);
        PlayerMovement.PlayerComponent.Init(this);
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
    public IPlayerComponent PlayerComponent { get; }

    public void Init(Player player);
}
