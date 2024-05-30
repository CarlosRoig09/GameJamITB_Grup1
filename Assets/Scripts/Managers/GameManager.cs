using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public enum GameFinish
{
    Win,
    Lose
}

public enum GameScenes
{
    TitleScreen,
    GameScene,
    GameOverScene
}

public enum DayCicle
{
    Day,
    Night
}
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                UnityEngine.Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }
    //GameManager Values
    private GameScenes _scene;
    private bool _calledStartGame;
    public delegate void ChangeScene(string scene);
    private bool _menuActions;
    private bool _gameOverActions;
    private int _puntuation;

    [Header("WaterValues")]
    //Water Values
    private float _iRPF;
    public float WaterInitValue;
    public float LimitWater;


    [Header("TimeValues")]
    [SerializeField]
    private float _timeSpeed;
    [SerializeField]
    private float _timeChange;
    private float _timer;
    private TimeSpan _realTime;
    public TimeSpan RealTime { get { return _realTime; } }
    private readonly TimeSpan _startTime = new(8, 0, 0);
    private int _days;
    public delegate void ChangeDay(DayCicle dayTime);
    public ChangeDay OnChangeDay;
    [SerializeField]
    private int _vchangeNight;
    [SerializeField]
    private int _vchangeDay;
    public DayCicle CurrentCycle;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }
    [Header("Selectables")]
    public ObjectBehaviour selected;
    // Start is called before the first frame update
    [Header("Fuente")]
    public TMP_FontAsset font;
    void Start()
    {
        _calledStartGame = false;
        _menuActions = false;
        _gameOverActions = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in FindObjectsOfType<TextMeshProUGUI>())
        {
            item.font = font;
        }
        ChangeBetweenScene();
        if (_scene == GameScenes.GameScene)
        {
            if (!_calledStartGame)
            {
                _realTime = _startTime;
                _calledStartGame = true;
                InitStartClases();
                UIManager.Instance.ModifyTimeUI(_realTime.ToString());
            }
            ControlDay();
        }
    }
    public void ChangeScene2(string name)
    {
        if (name == "Exit") Application.Quit();
        SceneManager.LoadScene(name);
    }
    void ChangeBetweenScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainScene":
            case "demo":
                _scene = GameScenes.GameScene;
                break;
            case "MainMenu":
                _scene =GameScenes.TitleScreen;
                break;
            case "GameOver":
                _scene = GameScenes.GameOverScene;
                break;
            default:
                break;
        }
    }
    //InitAllClasesThatNeedToStartInGameScene
    public void InitStartClases()
    {
        IOnStartGame[] clasesToInit = FindObjectsOfType<MonoBehaviour>(true).OfType<IOnStartGame>().ToArray();
        foreach (IOnStartGame classToInit in clasesToInit)
        {
            classToInit.OnStartGame();
        }
    }

    private void ControlDay()
    {
        if (_startTime.Hours == _realTime.Hours)
        {
            _days = _realTime.Days + 1;
            UIManager.Instance.ModifyDays(_days);
            if(_days>1)
             WaterTaxes();
        }

        if (_timeChange > _timer)
        {
            _timer += Time.deltaTime * _timeSpeed;
        }
        else
        {
            _timer = 0;
            _realTime += TimeSpan.FromMinutes(1);
            UIManager.Instance.ModifyTimeUI(_realTime.ToString(@"hh\:mm"));
            if (_realTime.Hours == _vchangeDay || _realTime.Hours == _vchangeNight)
                if(_realTime.Minutes == 1)
                {
                    switch(CurrentCycle)
                    {
                        case DayCicle.Day:
                            CurrentCycle = DayCicle.Night;
                            break;
                        case DayCicle.Night:
                            CurrentCycle = DayCicle.Day;
                            break;
                    }
                    
                    /*Method to call all objects functionals in day or the spawner behaivour.*/
                    OnChangeDay(CurrentCycle);
                }
        }
    }

    private void WaterTaxes()
    {
        GameObjectLibrary.Instance.WaterController.GetComponent<WaterController>().ModWater(_iRPF * -1);
    }

    //public void SubscribeEvent(IWaitTheEvent waitTheEvent)
    //{
    //    IHaveTheEvent[] eventors = FindObjectsOfType<MonoBehaviour>(true).OfType<IHaveTheEvent>().ToArray();
    //    foreach (IHaveTheEvent eventor in eventors)
    //    {
    //        if (eventor.Type == waitTheEvent.Type)
    //        {
    //            eventor.IHTEvent += waitTheEvent.MethodForEvent;
    //        }
    //    }
    //}

    //public void DeSubscribeEvent(IWaitTheEvent waitTheEvent)
    //{
    //    IHaveTheEvent[] eventors = FindObjectsOfType<MonoBehaviour>(true).OfType<IHaveTheEvent>().ToArray();
    //    foreach (IHaveTheEvent eventor in eventors)
    //    {
    //        if (eventor.Type == waitTheEvent.Type)
    //        {
    //            eventor.IHTEvent -= waitTheEvent.MethodForEvent;
    //        }
    //    }
    //}

    public void GameOver()
    {
        UnityEngine.Debug.Log("Game Over");
        LoadScene(GameScenes.TitleScreen);
    }

    public void ExitGameScene()
    {
    }

    public void LoadScene(GameScenes escena)
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            ExitGameScene();
        }
        switch (escena)
        {
            case GameScenes.GameScene:
                _calledStartGame = false;
                _puntuation = 0;
                SceneManager.LoadScene("GameScene");
                break;
            case GameScenes.TitleScreen:
                _menuActions = false;
                SceneManager.LoadScene("MainMenu");
                break;
            case GameScenes.GameOverScene:
                _gameOverActions = false;
                SceneManager.LoadScene("GameOver");
                break;
            default:
                break;
        }

    }

    public void PauseGame()
    {
        UIManager.Instance.PauseMenu();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        UIManager.Instance.ClosePause();
        Time.timeScale = 1;
    }
}
