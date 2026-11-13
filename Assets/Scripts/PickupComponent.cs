using UnityEngine;
using UnityEngine.Events;

public class PickupComponent : MonoBehaviour, IInteractable
{
    [SerializeField]
    private UnityEvent onInteractedEvent;

    public event UnityAction OnInteractedEvent
    {
        add => onInteractedEvent.AddListener(value);
        remove => onInteractedEvent.RemoveListener(value);
    }

    public void Interact(Player p)
    {
        onInteractedEvent?.Invoke();
        Destroy(gameObject);
    }

    public void TestMessage(string msg) => Debug.Log(msg);
}
