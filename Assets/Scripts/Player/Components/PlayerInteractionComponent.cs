using System;
using UnityEngine;
using UnityEngine.TestTools;

public interface IPlayerInteraction : IPlayerComponent
{
}

public class PlayerInteractionComponent : MonoBehaviour, IPlayerInteraction
{
    Player p;

    [SerializeField]
    float InteractionDistance;

    public IPlayerComponent PlayerComponent => this;

    IInteractable interactable = null;

    public void Init(Player player)
    {
        p = player;

        p.InputReader.OnInteractEvent += OnInteractEventHandle;
    }

    public void Destroy()
    {
        p.InputReader.OnInteractEvent -= OnInteractEventHandle;
    }

    private void OnInteractEventHandle()
    {
        interactable?.Interact(p);
        interactable = null;
    }

    private void FixedUpdate()
    {
        if (p == null || p.PlayerBody == null) return;

        if (p.PlayerBody.CameraTransform == null)
        {
            Debug.LogWarning("No cam??");
            return;
        }
        if (!Physics.Raycast(p.PlayerBody.CameraTransform.position, p.PlayerBody.CameraTransform.forward, out RaycastHit hit, InteractionDistance))
        {
            interactable = null;
            return;
        }

        hit.collider.TryGetComponent(out interactable);
    }
}
