using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    Move MoveScripts;

    AudioSource VolumeControl;
    AudioSource MenuSound;
    //setting
    public bool isSetting = false;
    public int isSceneLoad = 0;
    public bool isOntouch = false;
    public bool isSetup = false;
    GameObject isSettingObj;
    public int Volume;
    GameObject VolumeText;
    Vector2 MousePostion;

    GameObject canvas;
    GameObject isSetting_1;
    GameObject isSetting_2;
    GameObject isSetting_3;



    GameObject Ready;
    GameObject Go;
    GameObject GameOver;
    GameObject EndGame;
    GameObject CountTextPosition;
    GameObject GameScore;
    GameObject third;
    GameObject two;
    GameObject one;
    GameObject[] MainText;
    GameObject[] LogoLight;
    GameObject StartText;

    GameObject Player;
    TextMeshProUGUI GameScoreText;
    AudioSource BGSound;
    GameObject Rocket_effect;


    // public Text CountText;
    public GameObject CountText_m;
    bool isGameStart = false;
    bool isPlusAlpha = false;
    float mainAlpha = 1.0f;
    float TextAlpha = 0.6f;

    // Start is called before the first frame update
    void Awake()
    {
        


        instance = this;

        MoveScripts = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
        Player = GameObject.FindGameObjectWithTag("PlayerMoveObj").gameObject;
        BGSound = GameObject.FindGameObjectWithTag("PlayerMoveObj").GetComponent<AudioSource>();

        Rocket_effect = GameObject.FindGameObjectWithTag("Rocket");
        if (SceneManager.GetActiveScene().name != "loby")
        {

            Ready = GameObject.FindGameObjectWithTag("Ready").gameObject;
            Go = GameObject.FindGameObjectWithTag("Go").gameObject;
            GameOver = GameObject.FindGameObjectWithTag("GameOver").gameObject;
            EndGame = GameObject.FindGameObjectWithTag("EndGame").gameObject;
            GameScore = GameObject.FindGameObjectWithTag("GameScore").gameObject;
            GameScoreText = GameScore.GetComponent<TextMeshProUGUI>();

            third = GameObject.FindGameObjectWithTag("3").gameObject;
            two = GameObject.FindGameObjectWithTag("2").gameObject;
            one = GameObject.FindGameObjectWithTag("1").gameObject;
            CountText_m = new GameObject();
            CountText_m.AddComponent<TextMeshProUGUI>();

            Ready.SetActive(false);
            Go.SetActive(false);
            GameOver.SetActive(false);
            EndGame.SetActive(false);
            GameScore.SetActive(false);
            third.SetActive(false);
            two.SetActive(false);
            one.SetActive(false);
        }

    }

    void Start()
    {

        canvas = GameObject.FindGameObjectWithTag("Canvas");

        isSettingObj = GameObject.FindGameObjectWithTag("isSetting");
        VolumeControl = GameObject.FindGameObjectWithTag("PlayerMoveObj").GetComponent<AudioSource>();
        MenuSound = GameObject.Find("GameManager").GetComponent<AudioSource>();


        isSetting_1 = GameObject.FindGameObjectWithTag("isSetting1");
        isSetting_2 = GameObject.FindGameObjectWithTag("isSetting2");
        isSetting_3 = GameObject.FindGameObjectWithTag("isSetting3");

        VolumeText = GameObject.FindGameObjectWithTag("Volume");

        Volume = GameObject.Find("VolumeSaver").GetComponent<SoundManager>().Volume;
        VolumeText.GetComponent<TextMeshProUGUI>().text = Volume.ToString();

        VolumeControl.volume = Volume * 0.1f;
        MenuSound.volume = Volume *0.1f;

        Rocket_effect.SetActive(false);
        if (SceneManager.GetActiveScene().name != "loby")
        {
            Ready.SetActive(true);
            Go.SetActive(false);
            GameOver.SetActive(false);

        }
        else
        {
            LogoLight = GameObject.FindGameObjectsWithTag("LogoLight");
            MainText = GameObject.FindGameObjectsWithTag("isMain");
            StartText = GameObject.Find("StartText");
        }


        MenuSound.enabled = false;
        mainAlpha = 1.0f;
        isSetup = false;
        isSetting = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isSetting == false)
        {
            isSettingObj.SetActive(false);
        }
        else
        {
            isSettingObj.SetActive(true);

            if (isSetup==true)
            {
                isSetting_2.SetActive(false);
                isSetting_3.SetActive(true);
            }
            else
            {
                isSetting_2.SetActive(true);
                isSetting_3.SetActive(false);
            }

        }



        if (SceneManager.GetActiveScene().name != "loby")
        {
            var text = CountText_m.GetComponent<TextMeshProUGUI>();

            text.fontSize = 27;
            text.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
            text.color = new Color32(0, 0, 0, 255);
            text.fontStyle = FontStyles.Bold;
            text.font = Resources.Load("BMDOHYEON_ttf SDF") as TMPro.TMP_FontAsset;

            if (Move.instance.isGameStart == false && isSetting == false && isOntouch == true) //(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))
            {
                isGameStart = true;
            }

            if (Move.instance.isGameOver == true)
            {
                BGSound.enabled = false;
                Move.instance.rotateSpeed = 4.2f;
                Move.instance.DOTED.gameObject.SetActive(false);
            }

            if (Move.instance.isRocket == true)
            {
                GameOver.gameObject.SetActive(true);
                GameScore.SetActive(true);
                GameScoreText.text = (Move.instance.CubesIndex * 100 / Move.instance.Cubes.Count).ToString() + "%";
            }

            if (Move.instance.isRocket == true)
            {
                Rocket_effect.SetActive(true);
            }

            if (Move.instance.isGameEnd == true)
            {
                EndGame.gameObject.SetActive(true);
            }

            if (isGameStart == true)
            {
                gameStarter();
            }
        }
        else //Loby
        {
            if (isSetting == true)
            {
                StartText.SetActive(false);
            }

            if (Move.instance.isMainCube == true)
            {
                if (mainAlpha < 1.0f)
                {
                    mainAlpha += 0.01f;
                }

            }
            else if (Move.instance.isMainCube == false)
            {
                if (mainAlpha > 0)
                {
                    mainAlpha -= 0.01f;
                }
            }

            for (int i = 0; i < MainText.Length; i++)
            {
                // MainText[i].SetActive(true);

                MainText[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, mainAlpha);
            }

            if (Move.instance.isMainCube == true)
            {
                StartText.SetActive(true);


                for (int i = 0; i < LogoLight.Length; i++)
                {
                    LogoLight[i].SetActive(true);
                    var RedValue = LogoLight[i].gameObject.GetComponent<SpriteRenderer>().color.r;
                    var BlueValue = LogoLight[i].gameObject.GetComponent<SpriteRenderer>().color.g;
                    var GreenValue = LogoLight[i].gameObject.GetComponent<SpriteRenderer>().color.b;

                    float rAlpha = Random.Range(0.85f, 1.0f);

                    LogoLight[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(RedValue, BlueValue, GreenValue, rAlpha);


                    if (TextAlpha < 1.0f && isPlusAlpha == true)
                    {
                        TextAlpha += 0.004f;
                    }
                    else
                    {
                        isPlusAlpha = false;
                        if (TextAlpha > 0.5f)
                        {
                            TextAlpha -= 0.004f;
                        }
                        else
                        {
                            isPlusAlpha = true;
                        }

                    }
                    StartText.GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255, TextAlpha);
                }


            }
            else
            {
                StartText.SetActive(false);
                LogoLight[0].SetActive(false);
                LogoLight[1].SetActive(false);

            }


        }
        isOnTouch();
    }

    void isOnTouch()
    {
        if (isSetting == false)
        {

            if (Input.GetMouseButtonDown(0))
            {
                MousePostion = Input.mousePosition;
                MousePostion = Camera.main.ScreenToViewportPoint(MousePostion);
              
                if (MousePostion.x < 0.9f && MousePostion.y < 0.9f )
                {
                    isOntouch = true;
                }
                
            }
            else
            {
                isOntouch = false;
            }
        }
    }


    void gameStarter()
    {
        Ready.gameObject.SetActive(false);
        if (third != null)
        {
            third.gameObject.SetActive(true);
            Destroy(third, 0.4f);
        }
        else if (third == null && two != null)
        {
            two.gameObject.SetActive(true);
            Destroy(two, 0.4f);
        }
        else if (third == null && two == null && one != null)
        {
            one.gameObject.SetActive(true);
            Destroy(one, 0.4f);
        }
        else if (third == null && two == null && one == null && Go != null)
        {
            Go.gameObject.SetActive(true);
            Destroy(Go, 1);
            Move.instance.isGameStart = true;
        }
    }

    public void onClickisSetting()
    {
        isSetting = true;
        MenuSound.enabled = true;
        MenuSound.Play();
        VolumeControl.Pause();
    }

    public void OnClickContinu()
    {
        isSetting = false;
        MenuSound.Play();
        VolumeControl.Play();
    }

    public void OnClickSetup()
    {
        isSetup = true;
        MenuSound.Play();
    }

    public void OnClickExit()
    {
        isSceneLoad = 1;
        isSetting = false;
        MenuSound.Play();
    }

    public void InSetupExit()
    {
        isSetup = false;
        MenuSound.Play();
    }

    public void VolumeDown()
    {
        if(Volume>0)
        {
            Volume -= 1;
            VolumeControl.volume -= 0.1f;
            MenuSound.volume -= 0.1f;
        }
        MenuSound.Play();
        VolumeText.GetComponent<TextMeshProUGUI>().text = Volume.ToString();


    }

    public void VolumeUp()
    {
        if (Volume < 10)
        {
            Volume += 1;
            VolumeControl.volume += 0.1f;
            MenuSound.volume += 0.1f;
        }
        MenuSound.Play();
        VolumeText.GetComponent<TextMeshProUGUI>().text = Volume.ToString();
    }

    public void beforeMap()
    {
        isSceneLoad = 2;
        isSetting = false;
        MenuSound.Play();
    }

    public void NextMap()
    {
        isSceneLoad = 3;
        isSetting = false;
        MenuSound.Play();
    }
}
