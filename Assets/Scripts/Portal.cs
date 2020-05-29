using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    GameObject Arrow_Up;
    GameObject Arrow_Down;
    GameObject Portal_move;
    GameObject Portal_move_1;

    public bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        Arrow_Up = GameObject.Find("Arrow_Up");
        Arrow_Down = GameObject.Find("Arrow_Down");
        Portal_move = GameObject.FindGameObjectWithTag("Portal");
        Portal_move_1 = GameObject.FindGameObjectWithTag("Portal1");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove == false)
        {

            Portal_move.transform.position = Vector3.Lerp(Portal_move.transform.position, Arrow_Up.transform.position, 0.3f * Time.deltaTime);

            Portal_move_1.transform.position = new Vector3(Portal_move_1.transform.position.x, Portal_move.transform.position.y, Portal_move_1.transform.position.z);


            if ((Portal_move.transform.position - Arrow_Up.transform.position).magnitude <= 0.15f)
            {
                isMove = true;
            }


        }
        else
        {

            Portal_move.transform.position = Vector3.Lerp(Portal_move.transform.position, Arrow_Down.transform.position, 0.3f * Time.deltaTime);

            Portal_move_1.transform.position = new Vector3(Portal_move_1.transform.position.x, Portal_move.transform.position.y, Portal_move_1.transform.position.z);


            if ((Portal_move.transform.position - Arrow_Down.transform.position).magnitude <= 0.15f)
            {
                isMove = false;
            }





        }
    }
}
