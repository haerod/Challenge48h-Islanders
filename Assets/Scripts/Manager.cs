using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    [Header("Paramètres du niveau")]
    public Transform cameraPivot;
    public int levelPoints = 100;
    public List<Building> levelBuildings;
    [HideInInspector] public int currentPoints;

    [Header("Paramètres d'interface")]
    public int mainTextSize = 35;
    public Color mainTextColor = Color.yellow;
    public int miniTextSize = 30;
    public Color miniTextColor = Color.white;
    public float textHeightOffset = 1.5f;
    [Space]
    public Texture2D allowedCursor;
    public Texture2D forbiddenCursor;

    [Header("REFERENCES - Zone prog")]
    public Text pointsText;
    public Image pointsFill;
    public AnimationCurve pointsCurve;
    public GameObject panelVictory;
    public GameObject fadeToBlack;
    public AudioSource buildingSound;

    public ButtonConstruction button1;
    public ButtonConstruction button2;
    public ButtonConstruction button3;

    private int index1;
    private int index2;
    private int index3;
    [HideInInspector] public int currentIndex;
    private float currentTime;
    private bool isVictory;

    //===============================================================
    // MONOBEHAVIOUR
    //===============================================================

    private void Awake()
    {
        if(!instance)
            instance = this;
    }

    private void Start()
    {
        pointsText.text = "0/" + levelPoints;
        NewBuilding(1);
        NewBuilding(2);
        NewBuilding(3);
        panelVictory.SetActive(false);
    }

    private void Update()
    {
        if (isVictory) return;

        currentTime += Time.deltaTime;
        float pct = Mathf.Clamp01(currentTime / 2);
        pointsFill.fillAmount = Mathf.Lerp(pointsFill.fillAmount, (float) currentPoints/levelPoints, pointsCurve.Evaluate(pct));

        if (pointsFill.fillAmount >= 1)
            Victory();
    }

    //===============================================================
    // PUBLIC METHODS
    //===============================================================

    public void AddPoints(int value)
    {
        currentPoints += value;
        pointsText.text = currentPoints.ToString()+"/"+levelPoints;
        currentTime = 0;
    }

    public void CreateBuilding(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 1:
                Instantiate(levelBuildings[index1]);
                currentIndex = 1;
                button1.ClickOnButton();
                break;
            case 2:
                Instantiate(levelBuildings[index2]);
                currentIndex = 2;
                button2.ClickOnButton();
                break;
            case 3:
                Instantiate(levelBuildings[index3]);
                currentIndex = 3;
                button3.ClickOnButton();
                break;
            default:
                break;
        }

        SetButtonsActive(false);
    }

    public void NewBuilding(int buttonIndex)
    {
        int newListIndex = Random.Range(0, levelBuildings.Count);

        switch (buttonIndex)
        {
            case 1:
                index1 = newListIndex;
                button1.AddNewBuildingIndex(index1);
                break;
            case 2:
                index2 = newListIndex;
                button2.AddNewBuildingIndex(index2);
                break;
            case 3:
                index3 = newListIndex;
                button3.AddNewBuildingIndex(index3);
                break;
            default:
                break;
        }
    }

    public void SetButtonsActive(bool value)
    {
        button1.gameObject.SetActive(value);
        button2.gameObject.SetActive(value);
        button3.gameObject.SetActive(value);
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel()
    {
        fadeToBlack.SetActive(true);
        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            StartCoroutine(NextLevel_Co(SceneManager.GetActiveScene().buildIndex + 1));
        else
            StartCoroutine(NextLevel_Co(0));
    }

    IEnumerator NextLevel_Co(int index)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }

    //===============================================================
    // PRIVATE METHODS
    //===============================================================

    private void Victory()
    {
        isVictory = true;
        Camera.main.GetComponent<GameCamera>().canMove = false;
        SetButtonsActive(false);
        panelVictory.SetActive(true);
    }
}