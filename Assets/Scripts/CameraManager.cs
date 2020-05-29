using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target; //카메라가 따라갈 대상
    public float moveSpeed;  //카메라 속도
    public float BaseCameraSize = 1.0f;
    public float BaseCameraOffset = 0.0f;
    public float CameraPopTime = 0;
    public bool isCameraPop = false;
    private Vector3 targetPosition;//대상의 현재 위치값
    Move MoveScripts;
    Camera camera;
    SpriteRenderer BG;

    void Awake()
    {
        camera = this.GetComponent<Camera>();
        MoveScripts = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
        BG = GameObject.FindGameObjectWithTag("BG").GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            if (SceneManager.GetActiveScene().name != "loby")
            {
                targetPosition.Set(target.transform.position.x, target.transform.position.y, transform.position.z);

                this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                targetPosition.Set(target.transform.position.x, target.transform.position.y, transform.position.z);

                if (MoveScripts.isCameraDown == true)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(targetPosition.x, targetPosition.y + 2.5f, targetPosition.z), moveSpeed * Time.deltaTime);
                }
                else
                {
                    // this.transform.position = new Vector3(-9.4f, 2.4f, -11.3f);
                    this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(-9.4f, 2.4f, -11.3f), moveSpeed * Time.deltaTime);
                    // this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
                }

                if (UiManager.instance.isSetting == false && UiManager.instance.isOntouch == true) //(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
                {
                    isCameraPop = true;


                }

                CameraPop();
            }
        }
        if (MoveScripts.isTimer == true && MoveScripts.isCameraSlow == false)
        {
            BaseCameraOffset = 0.0f;

            if (SceneManager.GetActiveScene().name == "1-X" && MoveScripts.CubesIndex >= 18)
            {
                camera.orthographicSize = BaseCameraSize * 3.95f;

            }
            else if (!(SceneManager.GetActiveScene().name == "1-X") && !(SceneManager.GetActiveScene().name == "loby"))
            {
                camera.orthographicSize = BaseCameraSize * 3.95f;
            }
        }
        else
        {
            camera.orthographicSize = BaseCameraSize * 4.0f;

        }

        if (MoveScripts.isCameraSlow == true)
        {
            BaseCameraSize = 0.6f;
            camera.orthographicSize = BaseCameraSize * 4.0f - BaseCameraOffset;

            this.transform.rotation = Quaternion.Euler(0, 0, -5);
            BG.color = new Color32(150, 150, 150, 255);
            if (UiManager.instance.isSetting == false && UiManager.instance.isOntouch==true)//(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
            {
                BaseCameraOffset += 0.05f;
            }
        }
        else
        {
            BaseCameraSize = 1.0f;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            BG.color = new Color32(255, 255, 255, 255);
        }

        
    }

    void CameraPop()
    {
        if (CameraPopTime < 0.10f && isCameraPop == true)
        {
            CameraPopTime += Time.deltaTime;
            camera.orthographicSize = 3.97f;
        }
        else if(CameraPopTime >0.10f)
        {
            isCameraPop = false;
            CameraPopTime = 0;
            camera.orthographicSize = 4.00f;
        }

    }
}