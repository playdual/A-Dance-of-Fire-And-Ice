using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBall2 : MonoBehaviour
{
    Move MoveScripts;

    // Start is called before the first frame update
    void Start()
    {
        MoveScripts = transform.parent.GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (SceneManager.GetActiveScene().name != "loby")
        {
            if (MoveScripts.isGameStart == true && MoveScripts.isMoveBallTurn == true && !MoveScripts.isGameEnd)
            {
                if (col.gameObject.tag == "Map" && col.gameObject.name == MoveScripts.Cubes[MoveScripts.CubesIndex].name)
                {

                    MoveScripts.isMoveBallCollNew = true;

                }
            }
        }
        else
        {
            if (col.gameObject.tag == "Map" && MoveScripts.isMoveBallTurn == true)
            {
                if ((this.transform.position - col.transform.position).magnitude <= 0.35f)
                {
                    MoveScripts.CollisionCubename = col.gameObject.name;
                    MoveScripts.isMoveBallCollNew = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (SceneManager.GetActiveScene().name != "loby")
        {
            if (MoveScripts.isGameStart == true && MoveScripts.isMoveBallTurn == true && !MoveScripts.isGameEnd)
            {
                if (col.gameObject.name == MoveScripts.Cubes[MoveScripts.CubesIndex].name)
                {
                    MoveScripts.isMoveBallCollNew = false;
                    MoveScripts.isGameOver = true;
                    MoveScripts.isEndRotate = true;
                }
            }
        }
        else
        {
            if(MoveScripts.isMoveBallTurn == true)
            {
                MoveScripts.isMoveBallCollNew = false;
            }
        }
    }


}
