using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DateDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private GameObject dateDisplay;
    private int timeForward = 0;

    private float accelerate = 1.5f;

    private float delay = 1.5f;

    private DateTime dateTime = DateTime.Now.AddDays(-1);

    public event EventHandler<OnDateChangeEventArgs> OnDateChangeEvent;
    
    public class OnDateChangeEventArgs : EventArgs
    {
        public string date;
        public int period;
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && timeForward < 0)
        {
            timeForward++;
            SendDateUpdate();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            timeForward--;
            SendDateUpdate();
        }
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q)) 
        { 
            accelerate += Time.deltaTime;
            dateDisplay.SetActive(true);
            delay = 1.5f;
        }

        else
        {
            delay -= Time.deltaTime;
            if (delay <= 0f)
            {

                dateDisplay.SetActive(false);
            }
        }

        /*
        if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Q))
        {
            accelerate = 1.5f;
        }
        */



        /*
        if (Input.GetKey(KeyCode.E) && timeForward < 0)
        {
            timeForward += Mathf.Min((int) Mathf.Log( accelerate), 3);
            SendDateUpdate();
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            timeForward -= Mathf.Min((int)Mathf.Log(accelerate), 3);
            SendDateUpdate();
        }
        */


        displayText.SetText("Date forward: " + dateTime.ToString("yyyy-MM-dd") + "\n Time period: " + (ReturnRemainderFromThree(timeForward)));
    }

    private void SendDateUpdate()
    {
        int daysAdded = timeForward / 3;

        dateTime = DateTime.Now.AddDays(daysAdded);

        OnDateChangeEvent?.Invoke(this, new OnDateChangeEventArgs
        {
            date = dateTime.ToString("yyyy-MM-dd"),
            period = ReturnRemainderFromThree(timeForward)
        });
    }

    private int ReturnRemainderFromThree(int total)
    {
        return total % 3 != 0 ? 3 + timeForward % 3 : 0;
    }
}
