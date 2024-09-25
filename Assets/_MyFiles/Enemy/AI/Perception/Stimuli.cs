using UnityEngine;

public class Stimuli : MonoBehaviour
{
    private void Start()
    {
        Sense.RegisterStimuli(this);
    }
    private void OnDestroy()
    {
        Sense.UnRegisterStimuli(this);
    }
    public void SendSoundEvent(float volume) 
    {
        Debug.Log($"Sending sound event");
        HearingSense.SendSoundEvent(volume, this);
    }
}
