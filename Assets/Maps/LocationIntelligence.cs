using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InformationPanel;

public class LocationIntelligence : MonoBehaviour
{

    private WeatherConditions weatherCondition = WeatherConditions.FAIR; 

    [SerializeField] private InformationPanel informationalPanel;

    public event EventHandler<EnterFieldEventArgs> EnterFieldEvent;

    private Location location;

    [SerializeField] private GameObject RainyCloudShadow;
    [SerializeField] private GameObject LightCloudShadow;
    [SerializeField] private GameObject FairCloudShadow;

    public class EnterFieldEventArgs : EventArgs
    {
        public WeatherConditions weatherCondition;
        public Location location;
    }

    private void Start()
    {
        FairCloudShadowSetActive();
        informationalPanel.OnAPIUpdateChangeEvent += InformationalPanel_OnAPIUpdateChangeEvent;
    }

    private void InformationalPanel_OnAPIUpdateChangeEvent(object sender, OnAPIUpdateChangeEventArgs e)
    {
        weatherCondition = e.weatherConditions;
        location = e.location;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnterFieldEvent?.Invoke(this, new EnterFieldEventArgs
        {

            weatherCondition = weatherCondition,
            location = location
        });

        switch (weatherCondition)
        {
            case WeatherConditions.RAIN:
                RainyCloudShadowSetActive();
                break;
            case WeatherConditions.SHOWERS:
                RainyCloudShadowSetActive();
                break;
            case WeatherConditions.THUNDERYSHOWERS:
                RainyCloudShadowSetActive();
                break;
            case WeatherConditions.FAIR:
                FairCloudShadowSetActive();
                break;
            case WeatherConditions.HAZY:
                CloudShadowSetOff();
                break;
            case WeatherConditions.PARTLYCLOUDY:
                LightCloudShadowSetActive();
                break;
            case WeatherConditions.CLOUDY:
                LightCloudShadowSetActive();
                break;
            case WeatherConditions.OVERCAST:
                RainyCloudShadowSetActive();
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnterFieldEvent?.Invoke(this, new EnterFieldEventArgs
        {

            weatherCondition = WeatherConditions.FAIR,
            location = location
        });
    }

    private void RainyCloudShadowSetActive()
    {
        RainyCloudShadow.SetActive(true);
        LightCloudShadow.SetActive(false);
        LightCloudShadow.SetActive(false);
    }

    private void LightCloudShadowSetActive()
    {
        RainyCloudShadow.SetActive(false);
        LightCloudShadow.SetActive(true);
        LightCloudShadow.SetActive(false);
    }

    private void FairCloudShadowSetActive()
    {
        RainyCloudShadow.SetActive(false);
        LightCloudShadow.SetActive(false);
        FairCloudShadow.SetActive(true);
    }

    private void CloudShadowSetOff()
    {
        RainyCloudShadow.SetActive(false);
        LightCloudShadow.SetActive(false);
        FairCloudShadow.SetActive(false);
    }

}
