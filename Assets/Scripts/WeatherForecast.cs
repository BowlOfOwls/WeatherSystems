using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.WeatherMaker;
using static InformationPanel;

public class WeatherForecast : MonoBehaviour
{
    public event EventHandler<OnUpdateWeatherForecastEEventArgs> updateWeatherForecastEvent;
    public class OnUpdateWeatherForecastEEventArgs : EventArgs
    {
        public WeatherData forecast;
        public int period;
    }

    private int period = 0;

    [SerializeField] private DateDisplay dateDisplay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWeatherForecast("https://api.data.gov.sg/v1/environment/24-hour-weather-forecast"));
        dateDisplay.OnDateChangeEvent += DateDisplay_OnDateChangeEvent;
    }

    private void DateDisplay_OnDateChangeEvent(object sender, DateDisplay.OnDateChangeEventArgs e)
    {

        string api = "https://api.data.gov.sg/v1/environment/24-hour-weather-forecast?date=" + e.date;
        period = e.period;
        StartCoroutine(GetWeatherForecast(api));
    }

    IEnumerator GetWeatherForecast(string api)
    {
        string apiUrl = api;
        WWW webRequest = new WWW(apiUrl);
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            string json = webRequest.text;
            ParseWeatherForecast(json);
        }
        else
        {
            Debug.LogError("Error: " + webRequest.error);
        }
    }

    void ParseWeatherForecast(string json)
    {
        WeatherData weatherData = JsonUtility.FromJson<WeatherData>(json);

        if (weatherData != null && weatherData.items != null)
        {
            foreach (Item item in weatherData.items)
            {
                if (item != null && item.periods != null)
                {
                    updateWeatherForecastEvent?.Invoke(this, new OnUpdateWeatherForecastEEventArgs
                    {
                        forecast = weatherData,
                        period = period
                    });

                    switch (period)
                    {
                        case 0:
                            WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDay = 33500f;
                            break;
                        case 1:
                            WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDay = 50000f;
                            break;
                        case 2:
                            WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDay = 76000f;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.LogError("Item or periods is null");
                }
            }
        }
        else
        {
            Debug.LogError("weatherData or items is null");
        }
    }
}

[System.Serializable]
public class WeatherData
{
    public Item[] items;
    public ApiInfo api_info;
}

[System.Serializable]
public class Item
{
    public Period[] periods;
    public General general;
}

[System.Serializable]
public class Period
{
    public Timing time;
    public Region regions;
}

[System.Serializable]
public class Region
{
    public string west;
    public string east;
    public string central;
    public string south;
    public string north;
}

[System.Serializable]
public class Timing
{
    public string start;
    public string end;
}

[System.Serializable]
public class ApiInfo
{
    public string status;
}


[System.Serializable]
public class General
{
    public string forecast;
}