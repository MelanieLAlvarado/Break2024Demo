using System;
using UnityEngine;

[ExecuteAlways]
public class ViewCamera : MonoBehaviour
{
    [SerializeField] private Transform pitchTransform;
    [SerializeField] private Camera viewCamera;
    [SerializeField] private float armLength = 7f;g

    private Transform _parentTransform;
    
    public void SetFollowParent(Transform parentTransform)
    {
        _parentTransform = parentTransform;
    }

    void Update()
    {
        viewCamera.transform.position = pitchTransform.position - viewCamera.transform.forward * armLength;
    }

    private void LateUpdate()
    {
        if (_parentTransform)
        {
            transform.position = _parentTransform.position;
        }
    }
}
