using UnityEngine;

public class PickupComponent : MonoBehaviour, IInteractable
{
    public void Interact(Player p)
    {
        Destroy(gameObject);
    }
}
