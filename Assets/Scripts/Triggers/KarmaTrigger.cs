using UnityEngine;
using UnityEngine.Events;

public class KarmaTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onKarmaGoalHitEvent;

    public float KarmaTriggerGoal = 100.0f;

    public void AddKarma(float karma)
    {
        if (KarmaCounter.Instance == null) return;

        KarmaCounter.Instance.AddKarma(karma);
    }

    public void SetKarmaGoal(float karmaGoal) => KarmaTriggerGoal = karmaGoal;

    private void FixedUpdate()
    {
        if (KarmaCounter.Instance.Karma >= KarmaTriggerGoal) onKarmaGoalHitEvent?.Invoke();
    }
}
