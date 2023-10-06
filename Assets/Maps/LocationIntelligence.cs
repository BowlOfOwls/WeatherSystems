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

    public class EnterFieldEventArgs : EventArgs
    {
        public WeatherConditions weatherCondition;
        public Location location;
    }

    private void Start()
    {
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
    }

    private void OnTriggerExit(Collider other)
    {
        EnterFieldEvent?.Invoke(this, new EnterFieldEventArgs
        {

            weatherCondition = WeatherConditions.FAIR,
            location = location
        });
    }

}
