using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardButton : MonoBehaviour
{

    public Button mButton;

    // Use this for initialization
    void Start()
    {
        //Gets Button
        Button btnMount = mButton.GetComponent<Button>();
        //add a listener to Button, executing TaskOnClick() when click Button
        btnMount.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Loading Scene1
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}