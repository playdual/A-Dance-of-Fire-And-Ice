using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampMove : MonoBehaviour
{
    public bool isMoving = false;
    public float RotationSpeed = 0.30f;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.rotation.eulerAngles.z < 190 && isMoving == false)
        {
            this.transform.Rotate(0, 0, RotationSpeed);
            if (this.transform.eulerAngles.z > 190f)
            {
                isMoving = true;
            }
        }
        else
        {
            this.transform.Rotate(0, 0, -RotationSpeed);
            if (this.transform.eulerAngles.z < 170.0f)
            {
                isMoving = false;
            }
        }
    }
}
