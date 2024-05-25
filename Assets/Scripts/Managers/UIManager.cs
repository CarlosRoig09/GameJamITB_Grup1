using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IOnStartGame
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is NULL");
            }
            return _instance;
        }
    }
    private TextMeshPro _waterTextValue;
    private GameObject[] _cofees;
    public GameObject[] Cofees { get => _cofees; }
    private int _cofeesCount;
    public int CofesCount { get => _cofeesCount; }
    private bool _firstTime;
    private GameObject _pauseMenu;
    private GameObject _settingPopUp;
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
    }

    public void OnStartGame()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideSettingsPopUp()
    {
        _settingPopUp = GameObject.Find("SettingPopUp");
        _settingPopUp.SetActive(false);
    }

    public void ShowSettingsPopUp()
    {
        _settingPopUp.SetActive(true);
    }

    public void ModifyPunHUD(int puntuation)
    {
        //GameObjectLibrary.Instance.PuntuationText.text = puntuation.ToString();
    }

    public void ShowPuntuationGameOver(int puntuation)
    {
        GameObject.Find("Puntuation").GetComponent<TMP_Text>().text = puntuation.ToString();
    }

    public void ModifyWaterHUD(float water)
    {
        GameObjectLibrary.Instance.WaterValueUI.GetComponent<TextMeshProUGUI>().text = "Water : " + water.ToString();
        //GameObjectLibrary.Instance.EnergySlider.value = energy;
    }

    public void ModifyLimitWaterHUD(float limit)
    {
        GameObjectLibrary.Instance.WaterLimitUI.GetComponent<TextMeshProUGUI>().text = "Limit Water : " + limit.ToString();
    }

    public void ModifyTimeUI(string timer)
    {
        GameObjectLibrary.Instance.WaterLimitUI.GetComponent<TextMeshProUGUI>().text = timer;
    }
    /*No delete could be used for weapons*/
    //public void AddCofeeHUD()
    //{
    //    if (_firstTime)
    //    {
    //        ClickButton(EnumLibrary.ButtonType.Shift);
    //        _firstTime = false;
    //    }
    //    _cofees[_cofeesCount].GetComponent<SpritesMethods>().ChangeSpriteToTheNextOne();
    //    _cofees[_cofeesCount].GetComponent<Animator>().SetBool("Smoke", true);
    //    _cofeesCount += 1;
    //}
    /*No delete, could be used for weapons*/
    //public void RemoveCofeeHUD()
    //{
    //    _cofees[_cofeesCount - 1].GetComponent<SpritesMethods>().ChangeSpriteToTheNextOne();
    //    _cofees[_cofeesCount - 1].GetComponent<Animator>().SetBool("Smoke", false);
    //    _cofeesCount -= 1;
    //}

    //public void CountCofee(int id, int count, int maxCount)
    //{
    //    int limit;
    //    switch (id)
    //    {
    //        case 0:
    //            limit = _cofees.Length - 2;
    //            break;
    //        case 1:
    //            limit = _cofees.Length - 1;
    //            break;
    //        default:
    //            limit = _cofees.Length - 2;
    //            break;
    //    }
    //    _cofees[limit].GetComponent<TMP_Text>().text = count + "/" + maxCount;
    //}

    public void MenuButton()
    {
        GameManager.Instance.LoadScene(GameScenes.TitleScreen);
    }

    public void GameButton()
    {
        GameManager.Instance.LoadScene(GameScenes.GameScene);
    }

    public void PauseButton()
    {
        GameManager.Instance.PauseGame();
    }

    public void ResumeButton()
    {
        GameManager.Instance.ResumeGame();
    }

    public void PauseMenu()
    {
        _pauseMenu.SetActive(true);
    }
    public void ClosePause()
    {
        _pauseMenu.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    
}
