using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!gameObject.TryGetComponent<IDialogListener>(out IDialogListener listener)) return;


    }
}
