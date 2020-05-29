using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Move : MonoBehaviour
{
    public static Move instance;
    void Awake()
    {
        instance = this;
    }


    ///
    public float Time_sec = 0;
    public bool isTimer = false;
    public bool isStartRotate = false;
    public bool isEndRotate = false;
    public bool isCameraSlow = false;
    public string CollisionCubename;
    public bool isMainCube = true;
    public bool isCameraDown = false;

    public float CubeAlpha = 0.0f;
    public float MainCube = 0;
    SpriteRenderer YellowTile;

    MoveBall MoveBallScripts;
    MoveBall2 MoveBall2Scripts;


    Image WhiteImage;
    Image RedImage;
    public List<GameObject> TileColor;


    public float rotateSpeed = 3.1f;
    public bool isGameStart = false;
    public bool isGameOver = false;
    public bool isGameEnd = false;
    public bool isMoveBallTurn = true;
    public bool isMoveBallCollNew = false;
    public bool isWhiteStarting = false;
    public bool isRedStarting = false;

    bool isColorChange = false;

    bool isWhiting = false;
    bool isReding = false;
    bool isRotate = false;


    GameObject Cube;
    public List<string> CubeList = new List<string>();
    public List<GameObject> Cubes;
    public int CubesIndex = 1;
    public int LobyToStage = 0;

    Transform MoveBalls;
    Transform MoveBalls2;
    Transform PlayerMoveObj;
    Transform ENDPORTAL;
    Transform ENDPORTAL1;
    Transform ENDPORTAL2;
    public Transform DOTED;
    GameObject BLOCKLight;
    SpriteRenderer DOTED_SPR;

    TextMeshProUGUI Persent_comp;

    GameObject Persent;
    AudioSource Audio;

    public bool isRocket = false;

    GameObject canvas;
    RectTransform canvasTransform;
    float canvasWidth;
    float canvasHeight;

    public Vector2 MoveBallPosition;
    public Vector2 BallCubeLengthVec;

    public string TextValue;

    int childCount;
    // Start is called before the first frame update
    void Start()
    {
    

        CollisionCubename = "";

        Time_sec = 0;
        rotateSpeed = 2.8f;
        TextValue = "Good";
        canvas = GameObject.Find("Canvas");
        canvasTransform = canvas.GetComponent<RectTransform>();
        canvasWidth = canvasTransform.rect.width;
        canvasHeight = canvasTransform.rect.height;
        Audio = GameObject.FindGameObjectWithTag("PlayerMoveObj").GetComponent<AudioSource>();
        Audio.enabled = false;
        if (SceneManager.GetActiveScene().name == "1-X")
        {
            rotateSpeed = 3.0f;
        }



        WhiteImage = GameObject.FindGameObjectWithTag("WhiteBG").GetComponent<Image>();
        RedImage = GameObject.FindGameObjectWithTag("RedBG").GetComponent<Image>();
        BLOCKLight = GameObject.FindGameObjectWithTag("Light");
        MoveBallScripts = GameObject.FindGameObjectWithTag("MoveBall").GetComponent<MoveBall>();
        MoveBall2Scripts = GameObject.FindGameObjectWithTag("MoveBall2").GetComponent<MoveBall2>();

        MoveBalls = GameObject.FindGameObjectWithTag("MoveBall").GetComponent<Transform>();
        MoveBalls2 = GameObject.FindGameObjectWithTag("MoveBall2").GetComponent<Transform>();
        PlayerMoveObj = GameObject.FindGameObjectWithTag("PlayerMoveObj").GetComponent<Transform>();

        var TileMap = GameObject.Find("Tilemap");
        childCount = TileMap.transform.childCount;
        Cubes = new List<GameObject>();



        MoveBallPosition = MoveBalls.position;
        DOTED = GameObject.FindGameObjectWithTag("DOTED").GetComponent<Transform>();
        DOTED_SPR = GameObject.FindGameObjectWithTag("DOTED").GetComponent<SpriteRenderer>();

        for (int i = 0; i < childCount; i++)
        {
            Cubes.Add(TileMap.transform.GetChild(i).gameObject);
        }


        if (SceneManager.GetActiveScene().name != "loby")
        {
            Persent = GameObject.FindGameObjectWithTag("Persent");
            Persent_comp = Persent.GetComponent<TextMeshProUGUI>();
            Persent_comp.gameObject.SetActive(false);

            PlayerMoveObj.position = Cubes[0].transform.position;

            ENDPORTAL = GameObject.FindGameObjectWithTag("ENDPORTAL").GetComponent<Transform>();
            ENDPORTAL.position = Cubes[childCount - 1].transform.position;
            isTimer = false;
        }
        else
        {
            YellowTile = GameObject.FindGameObjectWithTag("Yellow_tile").GetComponent<SpriteRenderer>();
            ENDPORTAL1 = GameObject.FindGameObjectWithTag("ENDPORTAL1").GetComponent<Transform>();
            ENDPORTAL2 = GameObject.FindGameObjectWithTag("ENDPORTAL2").GetComponent<Transform>();

            Audio.enabled = true;
            Audio.pitch = 0.8f;
            Audio.loop = true;
            rotateSpeed = 2.45f;
            isRotate = true;
            isTimer = true;
            TileColor.Add(Cubes[36]);
            Cubes[36].transform.GetComponent<SpriteRenderer>().sprite = YellowTile.sprite;


            for (int CubeNum = 0; CubeNum < childCount; CubeNum++)
            {

                var RedColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.r;
                var GreenColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.g;
                var BlueColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.b;
                Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color = new Color(RedColor, GreenColor, BlueColor, 0);

            }
            for (int CubeNum = 0; CubeNum < childCount; CubeNum++)
            {
                for (int trueCubeNum = 1; trueCubeNum < 9; trueCubeNum++)
                {
                    if ((Cubes[CubeNum].name == "trueCube" + trueCubeNum.ToString() || Cubes[CubeNum].name == "MainCube"))
                    {
                        var RedColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.r;
                        var GreenColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.g;
                        var BlueColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.b;
                        Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color = new Color(RedColor, GreenColor, BlueColor, 255);

                    }
                }
            }
        }

        CubeAlpha = 0.0f;

    }
    
    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetButtonDown("Vertical"))
        {
            Audio.pitch -= (0.03f * -Input.GetAxisRaw("Vertical"));
            rotateSpeed -= (0.15f * -Input.GetAxisRaw("Vertical"));
        }



        if (UiManager.instance.isSetting == true)
            return;

        if (SceneManager.GetActiveScene().name != "loby")
        {
            ENDPORTAL.Rotate(0, 0, 1);
            if (isTimer == true)
            {
                if (Time_sec < 0.3f)
                {
                    Timer();
                    Persent_comp.gameObject.SetActive(true);
                }
                else
                {
                    isTimer = false;
                }
            }
            else
            {
                Time_sec = 0;
                Persent_comp.gameObject.SetActive(false);
            }
        }
        else
        {
            if (isTimer == true)
            {
                if (Time_sec < 0.65f)
                {
                    Timer();
                    isColorChange = true;
                }
                else if (Time_sec < 1.3f)
                {
                    Timer();
                    isColorChange = false;
                }
                else
                {
                    isTimer = false;
                }
            }
            else
            {
                Time_sec = 0;
                isTimer = true;
                isColorChange = true;
            }
        }

        if (isMoveBallTurn == false && isRotate == true) // false라면 MoveBall가 돌아감
        {
            MoveBalls.RotateAround(MoveBalls2.transform.position, -Vector3.forward, Time.deltaTime * 150.0f * rotateSpeed);
            DOTED_SPR.color = new Color32(255, 0, 0, 160);
            DOTED.Rotate(0, 0, -5);
        }
        else if (isMoveBallTurn == true && isRotate == true)// true라면 MoveBall2가 돌아감
        {
            MoveBalls2.RotateAround(MoveBalls.transform.position, -Vector3.forward, Time.deltaTime * 150.0f * rotateSpeed);
            DOTED_SPR.color = new Color32(0, 0, 255, 160);

            DOTED.Rotate(0, 0, -5);
        }

        //로비일때
        if (SceneManager.GetActiveScene().name == "loby")
        {
            if (isMainCube == true)
            {
                if (CubeAlpha > 0)
                {
                    CubeAlpha -= 0.01f;
                }
                
            }
            else if (isMainCube == false)
            {
                if(CubeAlpha< 1.0f )
                {
                    CubeAlpha += 0.01f;
                }
            }

            MainCubeFnc();

            ENDPORTAL1.Rotate(0, 0, 1);
            ENDPORTAL2.Rotate(0, 0, 1);

            if (isGameStart == false)
            {
                if (UiManager.instance.isSetting == false && UiManager.instance.isOntouch == true) //(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))
                {

                    isGameStart = true;
                }
            }

            for (int num = 0; num != childCount; num++)
            {
                if (Cubes[num].transform.childCount == 1)
                {
                    if (Cubes[num].transform.GetChild(0).tag == "Light")
                    {
                        Cubes[num].transform.GetComponent<SpriteRenderer>().sprite = YellowTile.sprite;
                        bool isSame = false;
                        for (int TileColorNum = 0; TileColorNum < TileColor.Count; TileColorNum++)
                        {
                            if (TileColor[TileColorNum].name == Cubes[num].name)
                            {
                                isSame = true;
                                break;
                            }

                        }
                        if (isSame == false)
                        {
                            TileColor.Add(Cubes[num]);
                        }
                    }
                }
            }

            if (TileColor.Count != 0)
            {
                for (int num = 0; num < TileColor.Count; num++)
                {
                    if (isColorChange == true)
                    {
                        if (num % 2 == 0)
                        {
                            var TileColorAlpha = TileColor[num].transform.GetComponent<SpriteRenderer>().color.a;
                            TileColor[num].transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, TileColorAlpha);
                        }
                        else
                        {
                            var TileColorAlpha = TileColor[num].transform.GetComponent<SpriteRenderer>().color.a;
                            TileColor[num].transform.GetComponent<SpriteRenderer>().color = new Color(0, 173, 255, TileColorAlpha);
                        }
                    }
                    else if (isColorChange == false)
                    {
                        if (num % 2 == 0)
                        {
                            var TileColorAlpha = TileColor[num].transform.GetComponent<SpriteRenderer>().color.a;
                            TileColor[num].transform.GetComponent<SpriteRenderer>().color = new Color(0, 173, 255, TileColorAlpha);
                        }
                        else
                        {
                            var TileColorAlpha = TileColor[num].transform.GetComponent<SpriteRenderer>().color.a;
                            TileColor[num].transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, TileColorAlpha);
                        }
                    }
                }
            }

            if (isGameStart == true && UiManager.instance.isSetting == false && UiManager.instance.isOntouch == true)//&& (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))
            {
                if (isMoveBallCollNew == true)
                {
                    if (isMoveBallTurn == true)
                    {
                        isMoveBallTurn = false;

                        for (int num = 0; num != childCount; num++)
                        {
                            if (CollisionCubename == Cubes[num].name)
                            {
                                MoveBallPosition = new Vector3(Cubes[num].transform.position.x, Cubes[num].transform.position.y, 0.5f);

                                if (CollisionCubename == "CameraDown" && isCameraDown == false)
                                {
                                    isCameraDown = true;
                                }
                                else if (CollisionCubename == "CameraDown" && isCameraDown == true)
                                {
                                    isCameraDown = false;
                                }

                                if (CollisionCubename == "MainCube")
                                {
                                    isMainCube = true;
                                   
                                }
                                else
                                {
                                    isMainCube = false;
                                    
                                }

                                if (Cubes[num].transform.childCount == 0)
                                {
                                    var Light = Instantiate(BLOCKLight);
                                    Light.transform.position = Cubes[num].transform.position;
                                    Light.transform.SetParent(Cubes[num].transform);
                                }
                                else if (Cubes[num].transform.name == "Stage1")
                                {
                                    isGameEnd = true;
                                    LobyToStage = 1;
                                }
                                else if (Cubes[num].transform.name == "Stage2")
                                {
                                    isGameEnd = true;
                                    LobyToStage = 2;
                                }
                                break;
                            }
                        }
                        MoveBalls2.position = MoveBallPosition;
                        PlayerMoveObj.position = MoveBallPosition;
                        isMoveBallCollNew = false;
                    }

                    else if (isMoveBallTurn == false)
                    {
                        isMoveBallTurn = true;

                        for (int num = 0; num != childCount; num++)
                        {
                            if (CollisionCubename == Cubes[num].name)
                            {
                                MoveBallPosition = new Vector3(Cubes[num].transform.position.x, Cubes[num].transform.position.y, 0.5f);

                                if(CollisionCubename=="CameraDown" && isCameraDown == false)
                                {
                                    isCameraDown = true;
                                }
                                else if (CollisionCubename == "CameraDown" && isCameraDown == true)
                                {
                                    isCameraDown = false;
                                }

                                if (CollisionCubename == "MainCube")
                                {
                                    isMainCube = true;
                                   
                                }
                                else
                                {
                                    isMainCube = false;
                                   
                                }


                                if (Cubes[num].transform.childCount == 0)
                                {
                                    var Light = Instantiate(BLOCKLight);
                                    Light.transform.position = Cubes[num].transform.position;
                                    Light.transform.SetParent(Cubes[num].transform);
                                }
                                else if (Cubes[num].transform.name == "Stage1")
                                {
                                    isGameEnd = true;
                                    LobyToStage = 1;
                                }
                                else if (Cubes[num].transform.name == "Stage2")
                                {
                                    isGameEnd = true;
                                    LobyToStage = 2;
                                    //   SceneManager.LoadScene("1-1");
                                }
                                break;
                            }
                        }
                        MoveBalls.position = MoveBallPosition;
                        PlayerMoveObj.position = MoveBallPosition;
                        isMoveBallCollNew = true;

                    }
                }

                else if (isMoveBallCollNew == false)
                {
                    isRedStarting = true;
                }


            }

            RedEnding();


        }

        //로비아닐때 ///////////////////////////////////////////////////////////////////
        else if (SceneManager.GetActiveScene().name != "loby")
        {
            if (isGameOver == false)
            {
                if (CubesIndex == childCount)
                {
                    isGameEnd = true;
                }

                if (isGameStart == false)
                {
                    if (UiManager.instance.isSetting == false && UiManager.instance.isOntouch == true)// (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))
                    {
                        Audio.enabled = true;
                        isStartRotate = true;
                        isRotate = true;
                    }
                }

                else if (isGameStart == true && !isGameEnd && UiManager.instance.isSetting == false && UiManager.instance.isOntouch == true)// (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))
                {
                    if (isMoveBallCollNew == true)
                    {
                        BallCubeLength();

                        if (isMoveBallTurn == true)
                        {

                            isMoveBallTurn = false;

                            isTimer = true;
                            Persent.transform.position = Camera.main.WorldToScreenPoint(Cubes[CubesIndex].transform.position + new Vector3(1.5f, 1.5f, 0));
                            Persent_comp.text = TextValue;


                            MoveBallPosition = new Vector3(Cubes[CubesIndex].transform.position.x, Cubes[CubesIndex].transform.position.y, 0.5f);
                            MoveBalls2.position = MoveBallPosition;
                            PlayerMoveObj.position = MoveBallPosition;


                            if (Cubes[CubesIndex].transform.childCount != 0)
                            {
                                if (Cubes[CubesIndex].transform.GetChild(0).name == "Whitning")
                                {
                                    isWhiteStarting = true;

                                    if (Cubes[CubesIndex].transform.childCount != 1)
                                    {
                                        if (Cubes[CubesIndex].transform.GetChild(1).tag == "SpeedLow2")
                                        {
                                            rotateSpeed *= 0.265f;
                                        }
                                    }
                                }

                                if (Cubes[CubesIndex].transform.GetChild(0).tag == "CameraSlow")
                                {
                                    isCameraSlow = true;
                                }
                                else if (Cubes[CubesIndex].transform.GetChild(0).tag == "CameraFast")
                                {
                                    isCameraSlow = false;
                                }

                                if (Cubes[CubesIndex].transform.GetChild(0).tag == "SpeedLow")
                                {
                                    rotateSpeed *= 0.55f;
                                }

                                else if (Cubes[CubesIndex].transform.GetChild(0).tag == "SpeedHigh")
                                {
                                    rotateSpeed *= 1.8181818f;
                                }

                                else if (Cubes[CubesIndex].transform.GetChild(0).tag == "SpeedLow2")
                                {
                                    rotateSpeed *= 0.265f;
                                }

                                else
                                {

                                }

                            }



                            CubesIndex++;
                            var Light = Instantiate(BLOCKLight);
                            Light.transform.position = Cubes[CubesIndex - 1].transform.position;
                            Light.transform.SetParent(Cubes[CubesIndex - 1].transform);

                            isMoveBallCollNew = false;
                        }
                        else if (isMoveBallTurn == false)
                        {
                            isMoveBallTurn = true;

                            isTimer = true;
                            Persent.transform.position = Camera.main.WorldToScreenPoint(Cubes[CubesIndex].transform.position + new Vector3(1.5f, 1.5f, 0));
                            Persent_comp.text = TextValue;

                            MoveBallPosition = new Vector3(Cubes[CubesIndex].transform.position.x, Cubes[CubesIndex].transform.position.y, 0.5f);
                            MoveBalls.position = MoveBallPosition;
                            PlayerMoveObj.position = MoveBallPosition;

                            if (Cubes[CubesIndex].transform.childCount != 0)
                            {
                                if (Cubes[CubesIndex].transform.GetChild(0).tag == "CameraSlow")
                                {
                                    isCameraSlow = true;
                                }
                                else if (Cubes[CubesIndex].transform.GetChild(0).tag == "CameraFast")
                                {
                                    isCameraSlow = false;
                                }



                                if (Cubes[CubesIndex].transform.GetChild(0).tag == "SpeedLow")
                                {
                                    rotateSpeed *= 0.55f;
                                }

                                else if (Cubes[CubesIndex].transform.GetChild(0).tag == "SpeedHigh")
                                {
                                    rotateSpeed *= 1.8181818f;
                                }

                                else
                                {

                                }

                            }



                            CubesIndex++;
                            var Light = Instantiate(BLOCKLight);
                            Light.transform.position = Cubes[CubesIndex - 1].transform.position;
                            Light.transform.SetParent(Cubes[CubesIndex - 1].transform);

                            isMoveBallCollNew = false;
                        }
                    }
                    else if (isMoveBallCollNew == false)
                    {
                        isGameOver = true;
                        isEndRotate = true;
                    }

                }
            }

            if (isStartRotate)
            {
                StartRotate();
            }

            if (isEndRotate)
            {
                EndRotate();
            }

            WhiteEnding();
        }

    }


    //여기부터 함수정의====================================================================================
    void WhiteEnding()
    {
        if (isWhiteStarting == true)
        {
            if (WhiteImage.color.a < 1.0 && isWhiting == false)
            {
                WhiteImage.color += new Color32(0, 0, 0, 20);
            }
            else
            {
                if (WhiteImage.color.a > 0)
                {
                    isWhiting = true;

                    WhiteImage.color -= new Color32(0, 0, 0, 20);
                }
                else
                {
                    isWhiteStarting = false;
                    isWhiting = false;

                }
            }
        }
    }

    void RedEnding()
    {
        if (isRedStarting == true)
        {
            if (RedImage.color.a < 0.2f && isReding == false)
            {
                RedImage.color += new Color(0, 0, 0, 0.1f);
            }
            else
            {
                if (RedImage.color.a > 0)
                {
                    isReding = true;

                    RedImage.color -= new Color(0, 0, 0, 0.1f);
                }
                else
                {
                    isRedStarting = false;
                    isReding = false;

                }
            }
        }
    }

    void StartRotate()
    {
        if (isMoveBallTurn == true)
        {
            Vector3 BallDistance_2;

            BallDistance_2 = MoveBalls2.position - MoveBalls.position;

            if (BallDistance_2.magnitude > 1.2f)
            {
                isStartRotate = false;
            }

            else
            {
                MoveBalls2.transform.Translate(0, 0.007f, 0);
                Vector3 BallDistance_1 = MoveBalls2.position - MoveBalls.position;
                if (BallDistance_1.magnitude > BallDistance_2.magnitude)
                {
                    BallDistance_2 = BallDistance_1;
                }
                else
                {
                    MoveBalls2.transform.Translate(0, -0.013f, 0);
                    BallDistance_2 = MoveBalls2.position - MoveBalls.position;
                }

                MoveBalls2.transform.Translate(0.007f, 0, 0);
                BallDistance_1 = MoveBalls2.position - MoveBalls.position;
                if (BallDistance_1.magnitude > BallDistance_2.magnitude)
                {
                    BallDistance_2 = BallDistance_1;
                }
                else
                {
                    MoveBalls2.transform.Translate(-0.013f, 0, 0);
                    BallDistance_2 = MoveBalls2.position - MoveBalls.position;
                }
            }
        }
    }

    void EndRotate()
    {

        Vector3 BallDistance_2;

        if (isMoveBallTurn == true)
        {
            BallDistance_2 = MoveBalls.position - MoveBalls2.position;

            if (BallDistance_2.magnitude < 0.03)
            {
                isEndRotate = false;
                MoveBalls.gameObject.SetActive(false);
                MoveBalls2.gameObject.SetActive(false);
                DOTED.gameObject.SetActive(false);
                isRocket = true;

            }

            else
            {
                MoveBalls2.transform.Translate(0, 0.05f, 0);
                Vector3 BallDistance_1 = MoveBalls.position - MoveBalls2.position;
                if (BallDistance_1.magnitude < BallDistance_2.magnitude)
                {
                    BallDistance_2 = BallDistance_1;
                }
                else
                {
                    MoveBalls2.transform.Translate(0, -0.055f, 0);
                    BallDistance_2 = MoveBalls.position - MoveBalls2.position;
                }

                MoveBalls2.transform.Translate(0.05f, 0, 0);
                BallDistance_1 = MoveBalls.position - MoveBalls2.position;
                if (BallDistance_1.magnitude < BallDistance_2.magnitude)
                {
                    BallDistance_2 = BallDistance_1;
                }
                else
                {
                    MoveBalls2.transform.Translate(-0.055f, 0, 0);
                    BallDistance_2 = MoveBalls.position - MoveBalls2.position;
                }
            }
        }

        else
        {
            BallDistance_2 = MoveBalls2.position - MoveBalls.position;

            if (BallDistance_2.magnitude < 0.03)
            {
                isEndRotate = false;
                MoveBalls.gameObject.SetActive(false);
                MoveBalls2.gameObject.SetActive(false);
                DOTED.gameObject.SetActive(false);
                isRocket = true;
            }

            else
            {
                MoveBalls.transform.Translate(0, 0.05f, 0);
                Vector3 BallDistance_1 = MoveBalls2.position - MoveBalls.position;
                if (BallDistance_1.magnitude < BallDistance_2.magnitude)
                {
                    BallDistance_2 = BallDistance_1;
                }
                else
                {
                    MoveBalls.transform.Translate(0, -0.055f, 0);
                    BallDistance_2 = MoveBalls2.position - MoveBalls.position;
                }

                MoveBalls.transform.Translate(0.05f, 0, 0);
                BallDistance_1 = MoveBalls2.position - MoveBalls.position;
                if (BallDistance_1.magnitude < BallDistance_2.magnitude)
                {
                    BallDistance_2 = BallDistance_1;
                }
                else
                {
                    MoveBalls.transform.Translate(-0.055f, 0, 0);
                    BallDistance_2 = MoveBalls2.position - MoveBalls.position;
                }
            }
        }
    }



    void Timer()
    {
        Time_sec += Time.deltaTime;
    }

    void BallCubeLength()
    {
        if (isMoveBallTurn == true)
        {
            BallCubeLengthVec = MoveBalls2.position - Cubes[CubesIndex].transform.position;
        }

        else if (isMoveBallTurn == false)
        {
            BallCubeLengthVec = MoveBalls.position - Cubes[CubesIndex].transform.position;
        }

        if (BallCubeLengthVec.magnitude <= 0.3f && BallCubeLengthVec.magnitude >= 0.15f)
        {
            Persent_comp.color = new Color32(255, 0, 0, 255);
            TextValue = "빨라!";
        }
        else if (BallCubeLengthVec.magnitude < 0.15f)
        {
            Persent_comp.color = new Color32(0, 255, 0, 255);
            TextValue = "좋아!";
        }
        else
        {
            Persent_comp.color = new Color32(0, 0, 255, 255);
            TextValue = "느려!";
        }
    }


    void MainCubeFnc()
    {
        if (isMainCube == true)
        {
            for (int CubeNum = 0; CubeNum < childCount; CubeNum++)
            {

                var RedColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.r;
                var GreenColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.g;
                var BlueColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.b;


                Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color = new Color(RedColor, GreenColor, BlueColor, CubeAlpha);


                if (Cubes[CubeNum].transform.childCount != 0)
                {
                    if (Cubes[CubeNum].transform.GetChild(0).tag == "Light")
                    {
                        var LightRedColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.r;
                        var LightGreenColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.g;
                        var LightBlueColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.b;

                        Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(LightRedColor, LightGreenColor, LightBlueColor, CubeAlpha);
                    }
                }
            }
            for (int CubeNum = 0; CubeNum < childCount; CubeNum++)
            {
                for (int trueCubeNum = 1; trueCubeNum < 9; trueCubeNum++)
                {
                    if ((Cubes[CubeNum].name == "trueCube" + trueCubeNum.ToString() || Cubes[CubeNum].name == "MainCube"))
                    {
                        var RedColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.r;
                        var GreenColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.g;
                        var BlueColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.b;

                        Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color = new Color(RedColor, GreenColor, BlueColor, 255);

                        if (Cubes[CubeNum].transform.childCount != 0)
                        {
                            if (Cubes[CubeNum].transform.GetChild(0).tag == "Light")
                            {
                                var LightRedColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.r;
                                var LightGreenColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.g;
                                var LightBlueColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.b;

                                Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(LightRedColor, LightGreenColor, LightBlueColor, 255);
                            }
                        }
                    }
                }
            }
        }

        else
        {
            for (int CubeNum = 0; CubeNum < childCount; CubeNum++)
            {


                var RedColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.r;
                var GreenColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.g;
                var BlueColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.b;


                Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color = new Color(RedColor, GreenColor, BlueColor, CubeAlpha);

                if (Cubes[CubeNum].transform.childCount != 0)
                {
                    if (Cubes[CubeNum].transform.GetChild(0).tag == "Light")
                    {
                        var LightRedColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.r;
                        var LightGreenColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.g;
                        var LightBlueColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.b;

                        Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(LightRedColor, LightGreenColor, LightBlueColor, CubeAlpha);
                    }
                }

            }

            for (int CubeNum = 0; CubeNum < childCount; CubeNum++)
            {
                for (int trueCubeNum = 1; trueCubeNum < 9; trueCubeNum++)
                {
                    if ((Cubes[CubeNum].name == "trueCube" + trueCubeNum.ToString() || Cubes[CubeNum].name == "MainCube"))
                    {
                        var RedColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.r;
                        var GreenColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.g;
                        var BlueColor = Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color.b;

                        Cubes[CubeNum].gameObject.GetComponent<SpriteRenderer>().color = new Color(RedColor, GreenColor, BlueColor, 255);

                        if (Cubes[CubeNum].transform.childCount != 0)
                        {
                            if (Cubes[CubeNum].transform.GetChild(0).tag == "Light")
                            {
                                var LightRedColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.r;
                                var LightGreenColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.g;
                                var LightBlueColor = Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.b;

                                Cubes[CubeNum].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(LightRedColor, LightGreenColor, LightBlueColor, 255);
                            }
                        }
                    }
                }
            }

        }
    }
    

}