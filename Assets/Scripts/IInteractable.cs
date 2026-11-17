using UnityEngine.Events;

public interface IInteractable
{
    public void Interact(Player p);

    public event UnityAction OnInteractedEvent;
}
