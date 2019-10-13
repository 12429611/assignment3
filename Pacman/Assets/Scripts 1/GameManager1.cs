﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    private static GameManager1 _instance;
    public static GameManager1 Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject pacman;
    public GameObject blinky;
    public GameObject clyde;
    public GameObject inky;
    public GameObject pinky;
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject startCountDownPrefab;
    public GameObject gameoverPrefab;
    public GameObject winPrefab;
    public AudioClip startClip;
    public Text remainText;
    public Text nowText;
    public Text scoreText;

    public bool isSuperPacman = false;
    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int> { 0, 1, 2, 3 };
    private List<GameObject> pacdotGos = new List<GameObject>();
    private int pacdotNum = 0;
    private int nowEat = 0;
    public int score = 0;

    private void Awake()
    {
        _instance = this;
        //Screen set resolution
        Screen.SetResolution(1024, 768, false);
        int tempCount = rawIndex.Count;
        //Each ghost will go random waypoints.
        for (int i = 0; i < tempCount; i++)
        {
            int tempIndex = Random.Range(0, rawIndex.Count);
            usingIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
        }
        //find all pacdots
        foreach (Transform t in GameObject.Find("Maze1").transform)
        {
            pacdotGos.Add(t.gameObject);
        }
        pacdotNum = GameObject.Find("Maze1").transform.childCount;
    }

    private void Start()
    {
        SetGameState(false);
    }

    private void Update()
    {
        //will win if eat all pacdot
        if (nowEat == pacdotNum && pacman.GetComponent<PacmanMove1>().enabled != false)
        {
            gamePanel.SetActive(false);
            Instantiate(winPrefab);
            StopAllCoroutines();
            SetGameState(false);
        }
        if (nowEat == pacdotNum)
        {
            if (Input.anyKeyDown)
            {
                //load new scene
                SceneManager.LoadScene(0);
            }
        }
        if (gamePanel.activeInHierarchy)
        {
            //record scores
            remainText.text = "Remain:\n\n" + (pacdotNum - nowEat);
            nowText.text = "Eaten:\n\n" + nowEat;
            scoreText.text = "Score:\n\n" + score;
        }
    }
    //set Start Button
    public void OnStartButton()
    {
        StartCoroutine(PlayStartCountDown());
        AudioSource.PlayClipAtPoint(startClip, new Vector3(0, 0, -5));
        startPanel.SetActive(false);
    }
    //set Exit Button
    public void OnExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;

    }

    IEnumerator PlayStartCountDown()
    {
        GameObject go = Instantiate(startCountDownPrefab);
        yield return new WaitForSeconds(4f);
        Destroy(go);
        SetGameState(true);
        Invoke("CreateSuperPacdot", 3f);
        gamePanel.SetActive(true);
        GetComponent<AudioSource>().Play();
    }
    //eat pacdot will add score
    public void OnEatPacdot(GameObject go)
    {
        nowEat++;
        score += 100;
        pacdotGos.Remove(go);
    }
    //eat super pacdot, will change superpacman and create super pacdot after 3 second, and freeze enemy.
    public void OnEatSuperPacdot()
    {
        score += 200;
        Invoke("CreateSuperPacdot", 3f);
        isSuperPacman = true;
        FreezeEnemy();
        StartCoroutine(RecoveryEnemy());
    }

    IEnumerator RecoveryEnemy()
    {
        //disfreez enemy after 3s
        yield return new WaitForSeconds(3f);
        DisFreezeEnemy();
        isSuperPacman = false;
    }
    //create super pacdot
    private void CreateSuperPacdot()
    {
        //can not create super pacdot anymore if less 5 pacdots
        if (pacdotGos.Count < 5)
        {
            return;
        }
        int tempIndex = Random.Range(0, pacdotGos.Count);
        pacdotGos[tempIndex].transform.localScale = new Vector3(6, 6, 6);
        pacdotGos[tempIndex].GetComponent<Pacdot1>().isSuperPacdot = true;
    }
    //if eat super pacdot, will freeze enemy and change enemy color
    private void FreezeEnemy()
    {
        blinky.GetComponent<GhostMove1>().enabled = false;
        clyde.GetComponent<GhostMove1>().enabled = false;
        inky.GetComponent<GhostMove1>().enabled = false;
        pinky.GetComponent<GhostMove1>().enabled = false;
        blinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        clyde.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        inky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        pinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    }
    //disfreeze enemy
    private void DisFreezeEnemy()
    {
        blinky.GetComponent<GhostMove1>().enabled = true;
        clyde.GetComponent<GhostMove1>().enabled = true;
        inky.GetComponent<GhostMove1>().enabled = true;
        pinky.GetComponent<GhostMove1>().enabled = true;
        blinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        clyde.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        inky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        pinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    private void SetGameState(bool state)
    {
        pacman.GetComponent<PacmanMove1>().enabled = state;
        blinky.GetComponent<GhostMove1>().enabled = state;
        clyde.GetComponent<GhostMove1>().enabled = state;
        inky.GetComponent<GhostMove1>().enabled = state;
        pinky.GetComponent<GhostMove1>().enabled = state;
    }
}
