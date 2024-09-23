using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense : MonoBehaviour
{
    [SerializeField] private bool bDrawDebug = true;
    [SerializeField] private float forgetTime = 3f;
    private static HashSet<Stimuli> _registeredStimuliSet = new HashSet<Stimuli>();

    private HashSet<Stimuli> _currentSensibleStimuliSet = new HashSet<Stimuli> ();

    private Dictionary<Stimuli, Coroutine> _forgettingCoroutines = new Dictionary<Stimuli, Coroutine> ();

    public static void RegisterStimuli(Stimuli stimuli) 
    { 
        _registeredStimuliSet.Add(stimuli);
    }

    public static void UnRegisterStimuli(Stimuli stimuli)
    { 
        _registeredStimuliSet.Remove(stimuli);
    }

    protected abstract bool IsStimuliSensible(Stimuli stimuli);

    private void Update()
    {
        foreach (Stimuli stimuli in _registeredStimuliSet)
        {
            if (IsStimuliSensible(stimuli))
            {
                HandleSensibleStimuli(stimuli);
            }
            else
            {
                HandleNoSensibleStimuli(stimuli);
            }
        }
    }

    private void HandleSensibleStimuli(Stimuli stimuli)
    {
        //we can sense it now, but we also can sense it before, nothing needs to be done
        if (_currentSensibleStimuliSet.Contains(stimuli)) 
        {
            return;
        }

        _currentSensibleStimuliSet.Add(stimuli);

        if (_forgettingCoroutines.ContainsKey(stimuli)) 
        {
            Debug.Log($"I am forgetting: {stimuli.gameObject.name}");
            StopCoroutine(_forgettingCoroutines[stimuli]);
            _forgettingCoroutines.Remove(stimuli);
            return;
        }

        Debug.Log($"I just sensed: {stimuli.gameObject.name}");
    }

    private void HandleNoSensibleStimuli(Stimuli stimuli)
    {
        //We cant sense it now, but also we did not sense it before, nothing needs to be done
        if (!_currentSensibleStimuliSet.Contains(stimuli))
        {
            return;
        }
        _currentSensibleStimuliSet.Remove(stimuli);


        Coroutine forgettingCoroutine = StartCoroutine(ForgetStimuli(stimuli));
        _forgettingCoroutines.Add(stimuli, forgettingCoroutine);
    }

    private IEnumerator ForgetStimuli(Stimuli stimuli)
    {
        yield return new WaitForSeconds(forgetTime);
        Debug.Log($"I just lost track of: {stimuli.gameObject.name}");
    }

    private void OnDrawGizmos()
    {
        if (bDrawDebug) 
        {
            OnDrawDebug();
        }
    }
    protected virtual void OnDrawDebug() { }

}
