using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] Transform scanPivot;

    HashSet<GameObject> detectedObject = new HashSet<GameObject>();

    public delegate void OnTargetDetectedDelegate(GameObject newTarget);
    public event OnTargetDetectedDelegate OnTargetDetected;

    public Transform ScanPivot { get => scanPivot; }

    public void StartScan(float scanRadius, float scanDuration)
    {
        StartCoroutine(ScanCoroutine(scanRadius, scanDuration));
    }

    private IEnumerator ScanCoroutine(float scanRadius, float scanDuration)
    {
        float counter = 0;
        float growRate = scanRadius / scanDuration;
        scanPivot.localScale = Vector3.zero;
        while (counter < scanDuration) 
        {
            counter += Time.deltaTime;
            scanPivot.localScale += growRate * Time.deltaTime * Vector3.one;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (detectedObject.Contains(other.gameObject))
            return;

        detectedObject.Add(other.gameObject);
        OnTargetDetected?.Invoke(other.gameObject);
    }
}
