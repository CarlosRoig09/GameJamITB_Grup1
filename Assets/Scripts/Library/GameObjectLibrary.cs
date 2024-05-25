using System.Collections.Generic;
using UnityEngine;

public class GameObjectLibrary : MonoBehaviour
{
    private static GameObjectLibrary _instance;

    public static GameObjectLibrary Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameObjectLibrary is NULL");
            }
            return _instance;
        }
    }

    public GameObject TopUIPanel;
    public GameObject WaterValueUI;
    public GameObject WaterLimitUI;
    public GameObject TimerUI;
    public GameObject DaysUI;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
        {
            //DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        //Not in awake, this library have to wait to GameManager Orders.
        TopUIPanel = GameObject.Find("TopPanel");
        WaterValueUI = TopUIPanel.transform.GetChild(0).gameObject;
        WaterLimitUI = TopUIPanel.transform.GetChild(1).gameObject;
        TimerUI = TopUIPanel .transform.GetChild(2).gameObject;
        DaysUI = TopUIPanel.transform .GetChild(3).gameObject;
    }
}