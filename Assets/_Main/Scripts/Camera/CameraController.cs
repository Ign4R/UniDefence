using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float duration = 0.1f, float amplitud = 1.5f, float frecuency = 20f)
    {
        StopAllCoroutines();
        StartCoroutine(ApplyNoiseRoutine(duration, amplitud, frecuency));
    }

    IEnumerator ApplyNoiseRoutine(float duration, float amplitud, float frecuency)
    {
        noise.m_AmplitudeGain = amplitud;
        noise.m_FrequencyGain = frecuency;
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
}
