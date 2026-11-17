using UnityEngine;
using UnityEngine.Events;

public class StealGoalTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onCollideEvent;

    private float value = .0f;

    [SerializeField]
    private float goal = 100.0f;

    public void SetGoal(float goal) => this.goal = goal;

    public void AddStolenValue(float value)
    {
        this.value += value;

        if(this.value >= goal)
        {
            onCollideEvent?.Invoke();
        }
    }
}
