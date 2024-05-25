using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }
    private GameScenes _scene;
    private bool _calledStartGame;
    public delegate void ChangeScene(string scene);
    private bool _menuActions;
    private bool _gameOverActions;
    private int _puntuation;
    public float WaterInitValue;
    public float LimitWater;
    [SerializeField]
    private float _timeSpeed;
    [SerializeField]
    private float _timeChange;
    private float _timer;
    private TimeSpan _realTime;
    [SerializeField]
    private TimeSpan _startTime;
    public delegate void ChangeDay(DayCicle dayTime);
    public ChangeDay OnChangeDay;
    public DayCicle _currentCycle;
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
    // Start is called before the first frame update
    void Start()
    {
        _calledStartGame = false;
        _menuActions = false;
        _gameOverActions = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBetweenScene();
        if (_scene == GameScenes.GameScene)
        {
            if (!_calledStartGame)
            {
                _realTime = _startTime;
                _calledStartGame = true;
                _currentCycle = DayCicle.Day;
                InitStartClases();
            }
            ControlDay();
        }
        //else if (_scene == EnumLibrary.Scene.GameOverScreen)
        //{
        //    if (!_gameOverActions)
        //    {
        //        _gameOverActions = true;
        //        GameObject.Find("Retry").GetComponent<Button>().onClick.AddListener(UIManager.Instance.GameButton);
        //        GameObject.Find("Menu").GetComponent<Button>().onClick.AddListener(UIManager.Instance.MenuButton);
        //        UIManager.Instance.ShowPuntuationGameOver(_puntuation);
        //    }
        //}
        //else if (_scene == EnumLibrary.Scene.GameMenu)
        //{
        //    if (!_menuActions)
        //    {
        //        _menuActions = true;
        //        GameObject.Find("Escape").GetComponent<Button>().onClick.AddListener(UIManager.Instance.HideSettingsPopUp);
        //        UIManager.Instance.HideSettingsPopUp();
        //        GameObject.Find("Game").GetComponent<Button>().onClick.AddListener(UIManager.Instance.GameButton);
        //        GameObject.Find("Exit").GetComponent<Button>().onClick.AddListener(UIManager.Instance.ExitGame);
        //        GameObject.Find("Settings").GetComponent<Button>().onClick.AddListener(UIManager.Instance.ShowSettingsPopUp);
        //    }
        //}
    }
    void ChangeBetweenScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "GameScene":
            case "demo":
                _scene = GameScenes.GameScene;
                break;
            case "Menú":
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
        if (_timeChange > _timer)
        {
            _timer += Time.deltaTime * _timeSpeed;
        }
        else
        {
            _realTime += TimeSpan.FromMinutes(1);
            UIManager.Instance.ModifyTimeUI(_realTime.ToString("hh:mm"));
            if (_realTime.Hours%12==0)
            {
                switch (_currentCycle)
                {
                    case DayCicle.Day:
                        _currentCycle = DayCicle.Night; 
                        break;
                    case DayCicle.Night:
                        _currentCycle = DayCicle.Day;
                        break;
                }
                //Change Day
                //OnChangeDay(_currentCycle);
                Debug.Log(_currentCycle);
            }
        }
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
        Debug.Log("Game Over");
        LoadScene(GameScenes.GameOverScene);
    }

    public void ExitGameScene()
    {
        //_puntuation = GameObjectLibrary.Instance.PuntuationControllerScript.Value;
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
                SceneManager.LoadScene("Menú");
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
