using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public int Volume;
    UiManager UiScript;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        UiScript = GameObject.Find("GameManager").GetComponent<UiManager>();
      

    }

    // Start is called before the first frame update
    void Start()
    {
        Volume = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (UiScript.Volume >= 0 && UiScript.Volume <= 10)
        {
            Volume = UiScript.Volume;
        }
    }
}
