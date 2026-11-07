using UnityEngine;

public interface IPlayerBody
{
    public IPlayerComponent PlayerComponent { get; }
    public CapsuleCollider CapsuleCollider { get; }
}


[RequireComponent(typeof(CapsuleCollider))]
public class PlayerBodyComponent : MonoBehaviour, IPlayerComponent, IPlayerBody
{
    Player player;

    [field: SerializeField, HideInInspector]
    public CapsuleCollider CapsuleCollider { get; private set; }

    public IPlayerComponent PlayerComponent => this;

    private void Reset()
    {
        CapsuleCollider = GetComponent<CapsuleCollider>();

        if (CapsuleCollider != null)
        {
            CapsuleCollider.height = 2.0f;
            CapsuleCollider.radius = 0.5f;
            CapsuleCollider.center = new Vector3(0, 0, 0);
        }
    }

    public void Init(Player player)
    {
        this.player = player;

        if (CapsuleCollider == null)
        {
            CapsuleCollider = GetComponent<CapsuleCollider>();
        }

        Debug.Assert(CapsuleCollider != null, "you fucked up (player dont have collider)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
