using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationIntelligence : MonoBehaviour
{
    public Material weatherSkyBox;
    public Material defaultSkyBox; 
    private void OnTriggerEnter(Collider other)
    {
        RenderSettings.skybox = weatherSkyBox;
    }

    private void OnTriggerExit(Collider other)
    {
        RenderSettings.skybox = defaultSkyBox;
    }
}
