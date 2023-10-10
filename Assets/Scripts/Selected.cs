using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Selected : MonoBehaviour
{
    [SerializeField] private GameObject selected;

    [SerializeField] private CityPrefab cityPrefab;
    // Start is called before the first frame update
    void Start()
    {
        RayCast.Instance.OnSelectChangeEvent += Instance_OnSelectChangeEvent;
    }

    private void Instance_OnSelectChangeEvent(object sender, RayCast.OnSelectChangeEventArgs e)
    {
        if (e.cityPrefab == cityPrefab)
        {
            selected.SetActive(true);
        }
        else
        {
            selected.SetActive(false);
        }
    }
}
