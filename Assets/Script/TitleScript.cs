using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public GameObject hitKey;

    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("SampleScene");
        }

        // タイマーにより文字が点滅
        timer++;
        if(timer %  500 > 300)
        {
            hitKey.SetActive(false);
        }
        else
        {
            hitKey.SetActive(true);
        }
    }
}
