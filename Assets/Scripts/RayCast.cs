using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public static RayCast Instance { get; private set; }

    public event EventHandler<OnSelectChangeEventArgs> OnSelectChangeEvent;
    public class OnSelectChangeEventArgs : EventArgs
    {
        public CityPrefab cityPrefab;
    }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        DetectlandArea();
    }

    private void DetectlandArea()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.TryGetComponent(out CityPrefab cityPrefab))
            {
                OnSelectChangeEvent(this, new OnSelectChangeEventArgs
                {
                    cityPrefab = cityPrefab
                });
            }else
            {
                OnSelectChangeEvent(this, new OnSelectChangeEventArgs
                {
                    cityPrefab = null
                });
            }
        }
    }
}
