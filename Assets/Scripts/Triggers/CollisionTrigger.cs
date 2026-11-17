using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onCollideEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player player)) return;
        onCollideEvent?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        onCollideEvent?.Invoke();
    }
}
