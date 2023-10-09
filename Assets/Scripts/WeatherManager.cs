using DigitalRuby.WeatherMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{

    private WeatherMakerCloudType clouds;
    private WeatherMakerCloudType lastClouds;

    [SerializeField] GameInput GameInput;


    // Start is called before the first frame update
    void Start()
    {
        GameInput.OnWeatherChangeEvent += GameInput_OnWeatherChangeEvent;
    }

    private void GameInput_OnWeatherChangeEvent(object sender, GameInput.OnWeatherChangeEventArgs e)
    {
        Debug.Log(e.cloud);
        string text = e.cloud;
        text = text.Replace("-", string.Empty).Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);
        clouds = (WeatherMakerCloudType)System.Enum.Parse(typeof(WeatherMakerCloudType), text);
        UpdateClouds();


        if (WeatherMakerPrecipitationManagerScript.Instance != null)
        {
            WeatherMakerPrecipitationManagerScript.Instance.Precipitation = (e.toggleRain ? WeatherMakerPrecipitationType.Rain : WeatherMakerPrecipitationType.None);
            WeatherMakerPrecipitationManagerScript.Instance.PrecipitationIntensity = e.intensityVal;
        }


        if (WeatherMakerThunderAndLightningScript.Instance != null)
        {
            WeatherMakerThunderAndLightningScript.Instance.EnableLightning = e.toggleThunder;
        }


        if (WeatherMakerPrecipitationManagerScript.Instance != null)
        {
            WeatherMakerPrecipitationManagerScript.Instance.PrecipitationIntensity = e.intensityVal;
        }
    }


    private void UpdateClouds()
    {
        if (clouds == lastClouds)
        {
            return;
        }
        lastClouds = clouds;
        if (WeatherMakerLegacyCloudScript2D.Instance != null && WeatherMakerLegacyCloudScript2D.Instance.enabled &&
            WeatherMakerLegacyCloudScript2D.Instance.gameObject.activeInHierarchy)
        {
            if (clouds == WeatherMakerCloudType.None)
            {
                WeatherMakerLegacyCloudScript2D.Instance.RemoveClouds();
            }
            else if (clouds != WeatherMakerCloudType.Custom)
            {
                WeatherMakerLegacyCloudScript2D.Instance.CreateClouds();
            }
        }
        else if (WeatherMakerFullScreenCloudsScript.Instance != null && WeatherMakerFullScreenCloudsScript.Instance.enabled &&
            WeatherMakerFullScreenCloudsScript.Instance.gameObject.activeInHierarchy)
        {
            float duration = WeatherMakerPrecipitationManagerScript.Instance.PrecipitationChangeDuration;
            if (clouds == WeatherMakerCloudType.None)
            {
                WeatherMakerFullScreenCloudsScript.Instance.HideCloudsAnimated(duration);
            }
            else if (clouds != WeatherMakerCloudType.Custom)
            {
                WeatherMakerFullScreenCloudsScript.Instance.ShowCloudsAnimated(duration, "WeatherMakerCloudProfile_" + clouds.ToString());
            }
            else
            {
                // custom clouds, do not modify current cloud script state
            }
        }
    }
}
