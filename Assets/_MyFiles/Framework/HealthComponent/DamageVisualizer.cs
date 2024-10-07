using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DamageVisualizer : MonoBehaviour
{
    [SerializeField] Renderer meshRenderer;
    ///first true shows alpha, second true makes it an HDR
    [SerializeField, ColorUsage(true, true)] Color damagedColor;
    [SerializeField] float damageColorDuration = 0.2f;
    [SerializeField] string damageColorMaterialParmName = "_EmissionOffset";//all params from shaderGraph get an "_" put in front
    Color origColor;

    CameraShaker _cameraShaker;
    private void Awake()
    {
        HealthComponent healthComponent = GetComponent<HealthComponent>();
        if (healthComponent)
        {
            healthComponent.OnTakeDamage += TookDamage;
        }
        origColor = meshRenderer.material.GetColor(damageColorMaterialParmName);
        ICameraInterface cameraInterface = GetComponent<ICameraInterface>();
        if (cameraInterface != null)
        {
            _cameraShaker = cameraInterface.GetCamera().AddComponent<CameraShaker>();
        }
    }
    private void TookDamage(float newHealth, float delta, float maxHealth, GameObject instigator)
    {
        if (meshRenderer.material.GetColor(damageColorMaterialParmName) == origColor)
        { 
            meshRenderer.material.SetColor(damageColorMaterialParmName, damagedColor);
            Invoke("ResetColor", damageColorDuration);

            //funtion name and time after waiting
        }
        if (_cameraShaker != null)
        {
            _cameraShaker.StartShake();
        }
    }
    void ResetColor() 
    {
        meshRenderer.material.SetColor(damageColorMaterialParmName, origColor);
    }
}
