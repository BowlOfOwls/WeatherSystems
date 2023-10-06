using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeatherForecast;
using TMPro;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Windows;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] private WeatherForecast weatherForecast;
    [SerializeField] private TMP_Text displayText;
    public enum Location {North, South, East, West, Central};
    [SerializeField] private Location location;
    [SerializeField] private string locationName;

    public enum WeatherConditions { RAIN, SHOWERS, THUNDERYSHOWERS, FAIR, HAZY, PARTLYCLOUDY, CLOUDY, OVERCAST }

    public event EventHandler<OnAPIUpdateChangeEventArgs> OnAPIUpdateChangeEvent;

    public class OnAPIUpdateChangeEventArgs: EventArgs
    {
        public WeatherConditions weatherConditions;
        public Location location;
    }

    // Start is called before the first frame update
    void Start()
    {
        weatherForecast.updateWeatherForecastEvent += WeatherForecast_updateWeatherForecastEvent;
    }

    private void WeatherForecast_updateWeatherForecastEvent(object sender, WeatherForecast.OnUpdateWeatherForecastEEventArgs e)
    {
        if (e.forecast != null && e.forecast.items != null)
        {
            foreach (Item item in e.forecast.items)
            {
                if (item != null && item.periods != null)
                {
                    string text;
                    switch (location)
                    {
                        case Location.East:
                            text = locationName + "\nWEATHER: " + item.periods[e.period].regions.east + "\n TIMING: " + item.periods[e.period].time.start;
                            break;
                        case Location.North:
                            text = locationName + "\nWEATHER: " + item.periods[e.period].regions.north + "\n TIMING: " + item.periods[e.period].time.start;
                            break;
                        case Location.Central:
                            text = locationName + "\nWEATHER: " + item.periods[e.period].regions.central + "\n TIMING: " + item.periods[e.period].time.start;
                            break;
                        case Location.West:
                            text = locationName + "\nWEATHER: " + item.periods[e.period].regions.west + "\n TIMING: " + item.periods[e.period].time.start;
                            break;
                        case Location.South:
                            text = locationName + "\nWEATHER: " + item.periods[e.period].regions.south + "\n TIMING: " + item.periods[e.period].time.start;
                            break;
                        default:
                            text = locationName + "\n Information unavailable";
                            break;

                    }
                    UpdateWeatherCondition(item.periods[e.period].regions.east);
                    displayText.SetText(text);
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

    private void UpdateWeatherCondition (string weatherConditionString)
    {
        string weatherConditionStringFirstWord = weatherConditionString.Split(new char[] { ' ' })[0];
        switch (weatherConditionStringFirstWord)
        {
            case "Rain":
            {
                    OnAPIUpdateChangeEvent?.Invoke(this, new OnAPIUpdateChangeEventArgs
                    {
                        weatherConditions = WeatherConditions.RAIN,
                        location = location
                    }) ;
                break;
                }
            case "Showers":
                {
                    OnAPIUpdateChangeEvent?.Invoke(this, new OnAPIUpdateChangeEventArgs
                    {
                        weatherConditions = WeatherConditions.SHOWERS,
                        location = location
                    });
                    break;
                }
            case "Thundery":
                {
                    OnAPIUpdateChangeEvent?.Invoke(this, new OnAPIUpdateChangeEventArgs
                    {
                        weatherConditions = WeatherConditions.THUNDERYSHOWERS,
                        location = location
                    });
                    break;
                }
            case "Fair":
                {
                    OnAPIUpdateChangeEvent?.Invoke(this, new OnAPIUpdateChangeEventArgs
                    {
                        weatherConditions = WeatherConditions.FAIR,
                        location = location
                    });
                    break;
                }
            case "Hazy":
                {
                    OnAPIUpdateChangeEvent?.Invoke(this, new OnAPIUpdateChangeEventArgs
                    {
                        weatherConditions = WeatherConditions.HAZY,
                        location = location
                    });
                    break;
                }
            case "Partly":
                {
                    OnAPIUpdateChangeEvent?.Invoke(this, new OnAPIUpdateChangeEventArgs
                    {
                        weatherConditions = WeatherConditions.PARTLYCLOUDY,
                        location = location
                    });
                    break;
                }
            case "Cloudy":
                {
                    OnAPIUpdateChangeEvent?.Invoke(this, new OnAPIUpdateChangeEventArgs
                    {
                        weatherConditions = WeatherConditions.CLOUDY,
                        location = location
                    });
                    break;
                }
            case "Overcast":
                {
                    OnAPIUpdateChangeEvent?.Invoke(this, new OnAPIUpdateChangeEventArgs
                    {
                        weatherConditions = WeatherConditions.OVERCAST,
                        location = location
                    });
                    break;
                }
        }
    }
}
