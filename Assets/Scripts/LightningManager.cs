using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public float offmin = 10f;
    public float offMax = 20f;
    public float onMin = 0.25f;
    public float onMax = 0.8f;
    public Light lightningLight;
    

    void Start()
    {
        StartCoroutine("Lightning");
    }

    void Update()
    {
        
    }

    IEnumerator Lightning()
    {
        while(true){
            yield return new WaitForSeconds(Random.Range(offmin,offMax));
            lightningLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(onMin, onMax));
            lightningLight.enabled = false;
        }
    }
}
