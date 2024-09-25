using System;
using UnityEngine;

public class HearingSense : Sense
{
    [SerializeField] float hearingMinVolume = 10;
    public delegate void OnSoundEventSentDelegate(float volume, Stimuli stimuli);
    public static event OnSoundEventSentDelegate OnSoundEventSent;
    private static float _atteniation = 0.05f;

    
    public static void SendSoundEvent(float volume, Stimuli stimuli)
    {
        OnSoundEventSent?.Invoke(volume, stimuli);
    }

    private void Awake()
    {
        OnSoundEventSent += HandleSoundEvent;
    }

    private void HandleSoundEvent(float volume, Stimuli stimuli)
    {
        Debug.Log($"Handling hearing event with volume: {volume} , and stimuli: {stimuli.gameObject.name}");
        float soundTravelDistance = Vector3.Distance(transform.position, stimuli.transform.position);
        float volumeAtOwner = volume - 20 * Mathf.Log(soundTravelDistance, 10) - _atteniation * soundTravelDistance;

        Debug.Log($"volume at owner is: {volumeAtOwner}");

        if (volumeAtOwner < hearingMinVolume) 
        {
            return;
        }

        HandleSensibleStimuli(stimuli);
    }
}
