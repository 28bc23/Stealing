using UnityEngine;

public interface IPlayerBody
{
    public IPlayerComponent PlayerComponent { get; }
    public CapsuleCollider CapsuleCollider { get; }
    public Transform CameraTransform { get; }
    public Transform Orientation { get; }
}


[RequireComponent(typeof(CapsuleCollider))]
public class PlayerBodyComponent : MonoBehaviour, IPlayerComponent, IPlayerBody
{
    Player player;

    [field: SerializeField]
    public CapsuleCollider CapsuleCollider { get; private set; }
    [field: SerializeField]
    public Transform CameraTransform { get; private set; }
    [field: SerializeField]
    public Transform Orientation { get; private set; }

    public IPlayerComponent PlayerComponent => this;

    public void Init(Player player)
    {
        this.player = player;

        Debug.Assert(CapsuleCollider != null, "you fucked up (player dont have collider)");
        Debug.Assert(CameraTransform != null, "you fucked up (player dont have CameraTransform)");
        Debug.Assert(Orientation != null, "you fucked up (player dont have Orientation)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
