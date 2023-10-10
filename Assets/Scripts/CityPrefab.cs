using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityPrefab : MonoBehaviour
{
    [SerializeField] private LandMassScriptableObject landMassScriptableObject;
    [SerializeField] private GameObject selected;
    // Start is called before the first frame update
    
    public LandMassScriptableObject GetLandMassScriptableObject()
    {
        return landMassScriptableObject;
    }
}
