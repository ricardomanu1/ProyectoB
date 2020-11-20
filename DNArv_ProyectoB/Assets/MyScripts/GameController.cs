using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int difficulty = 0;
    public bool inGame = false;
    public GameObject menu;
    public Text difText;
    public Text pointsText;
    public Text timeText;
    public float timer = 60;
    public float points = 0;
    public GameObject spawner;
    public GameObject puntero;

    // Start is called before the first frame update
    void Start()
    {
        SetInterface(true);
        timeText.text = "Time: " + (int)Mathf.Floor(timer);
        pointsText.text = "Points: " + points;

    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            timer -= Time.deltaTime;
            timeText.text = "Time: " + (int)Mathf.Floor(timer);
            if (timer <= 0)
            {
                timer = 0;
                timeText.text = "Time: " + (int)Mathf.Floor(timer);

                GameOver();
            }
        }
    }

    public void ChangeDifficulty()
    {
        difficulty++;
        if (difficulty > 2)
            difficulty = 0;

        difText.text = "DIFFICULTY: " + difficulty;

    }

    public void StartGame()
    {
        timer = 60;
        points = 0;

        pointsText.text = "Points: " + points;
        timeText.text = "Time: " + (int)Mathf.Floor(timer);

        inGame = true;

        SetInterface(false);

        spawner.GetComponent<ObjectSpawner>().StartSpawning(difficulty);
    }

    public void GameOver()
    {
        SetInterface(true);

        inGame = false;
        spawner.GetComponent<ObjectSpawner>().StopSpawning();
    }

    public void AddPoints(int i)
    {
        points += i;
        pointsText.text = "Points: " + points;
        
    }

    public void SetInterface(bool state)
    {
        menu.SetActive(state);
        puntero.SetActive(state);
    }
}
