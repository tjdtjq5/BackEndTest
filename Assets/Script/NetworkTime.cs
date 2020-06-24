using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class NetworkTime : MonoBehaviour
{
    public string url = "";

    void Start()
    {

        StartCoroutine(WebChk());

    }
    TimeSpan timestamp;
    IEnumerator WebChk()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date");

                if (date.Contains("Mon"))
                {

                }

                DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
              
                timestamp = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);

                int stopwatch =
                    (int)timestamp.TotalSeconds - PlayerPrefs.GetInt("net", (int)timestamp.TotalSeconds);

                Debug.Log(stopwatch + "sec");
              
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("net", (int)timestamp.TotalSeconds);
    }
}
