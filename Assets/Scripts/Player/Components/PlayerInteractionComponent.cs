using UnityEngine;

public interface IPlayerInteraction : IPlayerComponent
{
}

public class PlayerInteractionComponent : MonoBehaviour, IPlayerInteraction
{
    Player p;

    [SerializeField]
    float InteractionDistance;

    public IPlayerComponent PlayerComponent => this;

    public void Init(Player player)
    {
        p = player;
    }

    private void FixedUpdate()
    {
        if (p == null || p.PlayerBody == null) return;

        if (p.PlayerBody.CameraTransform == null)
        {
            Debug.LogWarning("No cam??");
            return;
        }
        if (!Physics.Raycast(p.PlayerBody.CameraTransform.position, p.PlayerBody.CameraTransform.forward, out RaycastHit hit, InteractionDistance)) return;

        if (!hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable)) return;

        interactable.Interact(p);
    }
}
