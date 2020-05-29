using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    RectTransform ChangeScene;
    bool isStart = false;
    bool isLoadScene = false;
    public float ChangeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ChangeSpeed = 100;
        ChangeScene = GameObject.FindGameObjectWithTag("Change").GetComponent<RectTransform>();
        ChangeScene.transform.position = new Vector3(1420, 750, 0);
        isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart == true)
        {
            if (ChangeScene.position.x > -3200)
            {
                ChangeScene.transform.Translate(-ChangeSpeed, 0, 0);
            }
            else
            {
                isStart = false;
                ChangeScene.transform.position = new Vector3(4800, 750, 0);
            }
        }

        if (Move.instance.isGameEnd == true)
        {

            if (!(SceneManager.GetActiveScene().name == "loby") && UiManager.instance.isSetting == false && UiManager.instance.isOntouch == true) //(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))//Input.GetButtonDown("Jump") ||
            {
                ChangeScene.transform.position = new Vector3(3200, 750, 0);
                isLoadScene = true;
            }
            else if (SceneManager.GetActiveScene().name == "loby")
            {
               
                isLoadScene = true;
            }

        }

        if(UiManager.instance.isSceneLoad ==1 || UiManager.instance.isSceneLoad == 2 || UiManager.instance.isSceneLoad == 3)
        {
            isLoadScene = true;
        }

        else if (Move.instance.isRocket == true)
        {
            if (UiManager.instance.isSetting == false && UiManager.instance.isOntouch == true) //(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))//Input.GetButtonDown("Jump") ||
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }

        LoadSceneing();
    }


    void LoadSceneing()
    {
        if (isLoadScene == true)
        {

            if (ChangeScene.position.x > 1420)
            {
               
                ChangeScene.transform.Translate(new Vector3(-ChangeSpeed, 0, 0));
            }
            else
            {
                if(UiManager.instance.isSceneLoad == 1)
                {
                    SceneManager.LoadScene("loby");
                    isLoadScene = false;
                }
                else if (UiManager.instance.isSceneLoad == 2)
                {
                    SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex - 1));
                    isLoadScene = false;
                }
                else if (UiManager.instance.isSceneLoad == 3)
                {
                    if(SceneManager.GetActiveScene().name == "1-X")
                    {
                        SceneManager.LoadScene("loby");
                    }
                    else
                    {
                        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
                    }
                    isLoadScene = false;
                }
                else if (SceneManager.GetActiveScene().name == "1-0")
                {
                    SceneManager.LoadScene("1-1");
                    isLoadScene = false;
                }
                else if (SceneManager.GetActiveScene().name == "1-1")
                {

                    SceneManager.LoadScene("1-2");
                    isLoadScene = false;
                }
                else if (SceneManager.GetActiveScene().name == "1-2")
                {

                    SceneManager.LoadScene("1-3");
                    isLoadScene = false;
                }
                else if (SceneManager.GetActiveScene().name == "1-3")
                {

                    SceneManager.LoadScene("1-4");
                    isLoadScene = false;
                }
                else if (SceneManager.GetActiveScene().name == "1-4")
                {

                    SceneManager.LoadScene("1-5");
                    isLoadScene = false;
                }
                else if (SceneManager.GetActiveScene().name == "1-5")
                {

                    SceneManager.LoadScene("1-X");
                    isLoadScene = false;
                }
                else if (SceneManager.GetActiveScene().name == "1-X")
                {

                    SceneManager.LoadScene("loby");
                    isLoadScene = false;
                }
                else if (SceneManager.GetActiveScene().name == "loby")
                {
                    if(Move.instance.LobyToStage == 1)
                    {
                        SceneManager.LoadScene("1-0");
                        isLoadScene = false;
                    }
                    else if (Move.instance.LobyToStage == 2)
                    {
                        SceneManager.LoadScene("1-X");
                        isLoadScene = false;
                    }
                   
                }


            }
            
           
        }
    }
}
