using UnityEngine;

[CreateAssetMenu(fileName = "ResetKarmaToLevelStartTriggerAction", menuName = "TriggerActions/ResetKarmaToLevelStart")]
public class ResetKarmaToLevelStartTriggerAction : ScriptableObject
{
    public void ResetKarmaToLevelStart()
    {
        if(KarmaCounter.Instance == null)
        {
            Debug.LogWarning("KarmaCounter is not in scene");
            return;
        }

        KarmaCounter.Instance.ResetKarmaToLevelStart();
    }
}
