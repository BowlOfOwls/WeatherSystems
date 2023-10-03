using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeatherForecast;
using TMPro;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] private WeatherForecast weatherForecast;
    [SerializeField] private TMP_Text displayText;
    private enum Location {North, South, East, West, Central};
    [SerializeField] private Location location;
    [SerializeField] private string locationName;

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
}
