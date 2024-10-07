using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Vector3 _origLocalPos;
    bool bIsShaking = false;
    [SerializeField] float camShakeMagnitude = 1f;
    [SerializeField] float shakeDuration = 1f;
    private void LateUpdate()
    {
        if (bIsShaking == true)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * camShakeMagnitude;
            transform.localPosition += shakeOffset;
        }
    }

    public void StartShake() 
    {
        Debug.Log("Start Shaking");
        bIsShaking = true;
        transform.localPosition = _origLocalPos;
        Invoke("StopShake", shakeDuration);
    }
    private void StopShake() 
    {
        Debug.Log("stop Shaking");

        bIsShaking = false;
        transform.localPosition = _origLocalPos;
    }
}
