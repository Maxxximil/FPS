using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using MiniJSON;
using System.Linq;
using System.Xml.Linq;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public float cloudValue { get; private set; }
    


    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");

        _network = service;
        StartCoroutine(_network.GetWeatherXML(OnXmlDataLoaded));
        //StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded));
        status = ManagerStatus.Initializing;
    }

    public void OnJSONDataLoaded(string data)
    {
        Dictionary<string, object> dict;
        dict = Json.Deserialize(data) as Dictionary<string, object>;

        
        Dictionary<string, object> clouds = (Dictionary<string, object>)dict["cloud"];
        cloudValue = (long)clouds["all"] / 100f;
        Debug.Log("Value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.Started;
    }
    public void OnXmlDataLoaded(string data)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data);

        XmlNode node;
        XmlElement root = doc.DocumentElement;

        node = root.SelectSingleNode("descendant::clouds");
        //Debug.Log(node.OuterXml);

        //XmlNode root = doc.DocumentElement;

        //XmlNode node = root.SelectSingleNode("clouds");
        string value = node.Attributes["all"].Value;
        cloudValue = Convert.ToInt32(value) / 100f;

        Debug.Log("Value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.Started;
    }

    public void LogWeather(string name)
    {
        StartCoroutine(_network.LogWeather(name, cloudValue, OnLogged));
    }

    private void OnLogged(string response)
    {
        Debug.Log(response);
    }
}
