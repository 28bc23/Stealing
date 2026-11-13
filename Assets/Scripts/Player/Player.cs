using UnityEngine;


[RequireComponent(typeof(PlayerBodyComponent))]
[RequireComponent(typeof(PlayerFPSMovementComponent))]
[RequireComponent(typeof(PlayerInteractionComponent))]
public class Player : MonoBehaviour
{

    public IPlayerBody PlayerBody { get; private set; }
    public IPlayerMovement PlayerMovement { get; private set; }
    public IPlayerInteraction PlayerInteraction { get; private set; }

    public IInputReader InputReader { get; private set; }

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InputReader = new InputReader();
        InputReader.EnablePlayer();
        InputReader.DisableUI();


        PlayerBody = GetComponent<PlayerBodyComponent>();
        PlayerMovement = GetComponent<PlayerFPSMovementComponent>();
        PlayerInteraction = GetComponent<PlayerInteractionComponent>();

        Debug.Assert(PlayerBody != null, "you fucked up (player dont have PlayerBody)");
        Debug.Assert(PlayerMovement != null, "you fucked up (player dont have PlayerMovement)");
        Debug.Assert(PlayerInteraction != null, "you fucked up (player dont have PlayerInteraction)");

        PlayerBody.PlayerComponent.Init(this);
        PlayerMovement.PlayerComponent.Init(this);
        PlayerInteraction.PlayerComponent.Init(this);
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
