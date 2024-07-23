using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    public float maxIntensity = 1f; // Vignette max intensity değeri
    public float maxSmoothness = 1f; // Vignette max smoothness değeri
    public float effectDuration = 1f; // Efektin kaç saniye boyunca uygulanacağı

    private float originalIntensity; // Vignette orijinal intensity değeri
    private float originalSmoothness; // Vignette orijinal smoothness değeri
    private bool isEffectRunning = false; // Efektin çalışıp çalışmadığını belirlemek için flag değişkeni

    public Volume postProcessVolume; // Vignette efektini içeren Post-Processing Volume

    public Vignette vignette;
    private Bloom bloom;

    public bool viggnetteEfect = false;

    void Start()
    {
        // Post-Processing Volume'ü alın
        postProcessVolume = GetComponent<Volume>();

        // Orijinal vignette değerlerini alın
        originalIntensity = 0.55f;
        originalSmoothness = 0.2f;
        VignetteEfect();
    }

    private void Update()
    {
        VignetteEfect();
    }

    private  void VignetteEfect()
    {
        if (viggnetteEfect)
        {
            postProcessVolume.profile.TryGet(out vignette);
            {
                vignette.intensity.value += Time.deltaTime;
                vignette.smoothness.value += Time.deltaTime;
            }
        }
        else
        {
            postProcessVolume.profile.TryGet(out vignette);
            {
                float intensity1, smot1;

                intensity1 = vignette.intensity.value;
                smot1 = vignette.smoothness.value;

                if (intensity1 > originalIntensity) intensity1 -= Time.deltaTime * 3;
                if (smot1 > originalSmoothness) smot1 -= Time.deltaTime * 3;

                vignette.intensity.value = intensity1;
                vignette.smoothness.value = smot1;
            }
        }      
    }
}
