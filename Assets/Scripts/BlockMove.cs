using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    GameObject BlockUp;
    GameObject BlockDown;

    public bool isMove = false;
    Move MoveScripts;

    void Awake()
    {
        MoveScripts = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
    }

    // Start is called before the first frame update
    void Start()
    {
        BlockUp = GameObject.Find("BlockUp");
        BlockDown = GameObject.Find("BlockDown");

    }

    // Update is called once per frame
    void Update()
    {
        
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, MoveScripts.CubeAlpha);
   

        if (isMove == false)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, BlockUp.transform.position, 0.4f * Time.deltaTime);
            if ((this.transform.position - BlockUp.transform.position).magnitude <= 0.15f)
            {
                isMove = true;
            }
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, BlockDown.transform.position, 0.4f * Time.deltaTime);
            if ((this.transform.position - BlockDown.transform.position).magnitude <= 0.15f)
            {
                isMove = false;
            }
        }
    }
}
