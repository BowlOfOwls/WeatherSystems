using DigitalRuby.WeatherMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InformationPanel;

public class GameInput : MonoBehaviour
{
    private PlayerInput playerInput;


    public event EventHandler<OnScrollPerformedEventArg> OnScrollPerformedEvent;

    private float ScrollValueY;

    [SerializeField] private WeatherMakerConfigurationScript weatherMakerConfigurationScript;

    private WeatherMakerCloudType clouds;

    public class OnScrollPerformedEventArg : EventArgs
    {
        public float scroll;
    }

    public event EventHandler<OnWeatherChangeEventArgs> OnWeatherChangeEvent;

    public class OnWeatherChangeEventArgs : EventArgs
    {
        public string cloud;
        public bool toggleRain;
        public bool toggleThunder;
        public float intensityVal;
    }


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.KeyBoard.Enable();
        playerInput.KeyBoard.Scroll.performed += x => { 
            ScrollValueY = x.ReadValue<float>();
            OnScrollPerformedEvent?.Invoke(this, new OnScrollPerformedEventArg
            {
                scroll = ScrollValueY
            });
        };
    }

    void OnEnable()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Location");
        foreach (GameObject obj in taggedObjects)
        {
            LocationIntelligence locationIntelligence = obj.GetComponent<LocationIntelligence>();
            if (locationIntelligence != null)
            {
                // Subscribe to the event
                locationIntelligence.EnterFieldEvent += LocationIntelligence_EnterFieldEvent;
            }
        }
    }

    private void LocationIntelligence_EnterFieldEvent(object sender, LocationIntelligence.EnterFieldEventArgs e)
    {
        Debug.Log(e.weatherCondition + " at " + e.location);

        switch (e.weatherCondition)
        {
            case WeatherConditions.RAIN:
                OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
                {
                    cloud = "Medium-Heavy-Scattered",
                    toggleRain = true,
                    toggleThunder = false,
                    intensityVal = 0.7f
                }) ;

                break;
            case WeatherConditions.SHOWERS:
                OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
                {
                    cloud = "Light-Medium-Scattered",
                    toggleRain = true,
                    toggleThunder = false,
                    intensityVal = 0.5f
                });
                break;
            case WeatherConditions.THUNDERYSHOWERS:
                OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
                {
                    cloud = "Heavy (Dark)",
                    toggleRain = true,
                    toggleThunder = true,
                    intensityVal = 1f
                });
                break;
            case WeatherConditions.FAIR:
                OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
                {
                    cloud = "Light-Scattered",
                    toggleRain = false,
                    toggleThunder = false,
                    intensityVal = 0f
                }) ;
                break;
            case WeatherConditions.HAZY:
                OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
                {
                    cloud = "Medium-Heavy-Scattered",
                    toggleRain = false,
                    toggleThunder = false,
                    intensityVal = 0f
                });
                break;
            case WeatherConditions.PARTLYCLOUDY:
                OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
                {
                    cloud = "Medium-Heavy-Scattered",
                    toggleRain = false,
                    toggleThunder = false,
                    intensityVal = 0f
                });
                break;
            case WeatherConditions.CLOUDY:
                OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
                {
                    cloud = "Medium-Heavy",
                    toggleRain = false,
                    toggleThunder = false,
                    intensityVal = 0f
                });
                break;
            case WeatherConditions.OVERCAST:
                OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
                {
                    cloud = "Heavy (Dark)",
                    toggleRain = false,
                    toggleThunder = false,
                    intensityVal = 0f
                });
                break;
            default:
                Debug.Log("No Weather Condition detected in GameInput");
                break;

        }
    }

    private void Update()
    {
        TestUpdateCloud();
    }

    public Vector2 GetMovementVectorNormalized()
    {

        Vector2 inputVector = playerInput.KeyBoard.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public void TestUpdateCloud()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
            {
                cloud = "Light-Medium-Scattered",
                toggleRain = false,
                toggleThunder = false
            });
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
            {
                cloud = "Medium-Heavy-Scattered",
                toggleRain = true,
                toggleThunder = false
            });
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnWeatherChangeEvent?.Invoke(this, new OnWeatherChangeEventArgs
            {
                cloud = "Storm",
                toggleRain = true,
                toggleThunder = true
            });
        }
    }
}
