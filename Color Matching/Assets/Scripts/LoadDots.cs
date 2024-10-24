using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDots : MonoBehaviour
{
    public static int offColorNum;
    public static GameObject[] dots;

    public static float colorDiff;

    public static float redRand;
    public static float greenRand;
    public static float blueRand;

    public static Color mainColor;
    public static Color offColor;

    public GameObject dot;
    public GameObject button;

    public static int score;
    public static int highScore;

    public static string gameState;

    public static bool startLoaded;
    public static bool endLoaded;

    private GUIStyle guiStyle;

    private int timer;
    private int slowDown;

    // Start is called before the first frame update
    void Start()
    {
        startLoaded = false;
        endLoaded = false;
        gameState = "Start"; // four modes: Start, Load, Playing, and TimeUp
        timer = 30;
        slowDown = 0;
        guiStyle = new GUIStyle();

        offColorNum = Random.Range(1, 10);

        colorDiff = Random.Range(0.05f, 0.09f);

        redRand = Random.Range(0.1f, 1f);
        greenRand = Random.Range(0.1f, 1f);
        blueRand = Random.Range(0.1f, 1f);

        Color mainColor = new Color(
                redRand,
                greenRand,
                blueRand,
                1
            );
        Color offColor = new Color(
                redRand - colorDiff,
                greenRand - colorDiff,
                blueRand - colorDiff,
                1
            );

    }

    void OnGUI()
    {
        guiStyle.fontSize = 100; //change the font size

        if (gameState == "Start")
        {
            GUI.Label(new Rect(250, 700, 1, 1), "Color Matching", guiStyle);
            GUI.Label(new Rect(430, 800, 1, 1), "Game", guiStyle);

            GUI.Label(new Rect(490, 1730, 1, 1), "Start", guiStyle);

        }

        if (gameState == "Load" || gameState == "Playing")
        {
            GUI.Label(new Rect(80, 160, 1, 1), "Score: " + score.ToString(), guiStyle);
            GUI.Label(new Rect(80, 260, 1, 1), "High Score: " + highScore.ToString(), guiStyle);
            GUI.Label(new Rect(525, 700, 1, 1), timer.ToString(), guiStyle);
        }

        if (gameState == "TimeUp")
        {
            GUI.Label(new Rect(340, 700, 1, 1), "Time's Up!", guiStyle);
            GUI.Label(new Rect(330, 1730, 1, 1), "Play Again?", guiStyle);

        }

        GUI.Label(new Rect(300, 2400, 1, 1), "Baelul Haile", guiStyle);
    }

    void Update()
    {
        if (gameState == "Start" && !startLoaded)
        {
            StartScreen();
            startLoaded = true;
        }

        if (gameState == "Load")
        {
            InstantiateDots();
            gameState = "Playing";
            foreach (GameObject obj in dots)
            {
                ClickedOn(obj);
            }
        }

        if (gameState == "Playing")
        {
            if (timer == 0)
            {
                score = 0;
                timer = 30;
                gameState = "TimeUp";
            }


            if (slowDown == 10)
            {
                timer--;
                slowDown = 0;
            }
            slowDown++;
        }

        if (gameState == "TimeUp" && !endLoaded)
        {
            TimesUpScreen();
            endLoaded = true;
            foreach (GameObject obj in dots)
            {
                Destroy(obj);
            }
        }
        
    }

    public static void ClickedOn(GameObject dot)
    {
        if (dot.name[3].ToString() == offColorNum.ToString())
        {
            // increase score
            score += 50;
            if (score >= highScore) {
                highScore = score;
            }
        }

        Debug.Log("score: " + score);

        // change board
        offColorNum = Random.Range(1, 10);

        colorDiff = Random.Range(0.05f, 0.1f);

        redRand = Random.Range(0.1f, 1f);
        greenRand = Random.Range(0.1f, 1f);
        blueRand = Random.Range(0.1f, 1f);

        mainColor = new Color(
            redRand,
            greenRand,
            blueRand,
            1
        );
        offColor = new Color(
            redRand - colorDiff,
            greenRand - colorDiff,
            blueRand - colorDiff,
            1
        );


        foreach (GameObject obj in dots)
        {
            if (obj.name[3].ToString() == offColorNum.ToString())
            {
                //paint dot off color
                obj.GetComponent<Renderer>().material.color = offColor;
            }
            else
            {
                // paint dot normal color
                obj.GetComponent<Renderer>().material.color = mainColor;
            }
        }

    }

    void StartScreen()
    {
        GameObject StartButton = Instantiate(button,
            new Vector3(.02f, -2.05f, 0),
            new Quaternion(0, 0, 0, 0));

        StartButton.name = "startbutton";
        StartButton.AddComponent<BoxCollider2D>();
        StartButton.AddComponent<MouseDown>();
    }

    void TimesUpScreen()
    {
        GameObject EndButton = Instantiate(button,
            new Vector3(.02f, -2.05f, 0),
            new Quaternion(0, 0, 0, 0));

        EndButton.name = "endbutton";
        EndButton.AddComponent<BoxCollider2D>();
        EndButton.AddComponent<MouseDown>();
    }

    void InstantiateDots()
    {
        GameObject dot1 = Instantiate(dot,
            new Vector3(-1, 1, 0),
            new Quaternion(0, 0, 0, 0));
        GameObject dot2 = Instantiate(dot,
            new Vector3(0, 1, 0),
            new Quaternion(0, 0, 0, 0));
        GameObject dot3 = Instantiate(dot,
            new Vector3(1, 1, 0),
            new Quaternion(0, 0, 0, 0));
        GameObject dot4 = Instantiate(dot,
            new Vector3(-1, 0, 0),
            new Quaternion(0, 0, 0, 0));
        GameObject dot5 = Instantiate(dot,
            new Vector3(0, 0, 0),
            new Quaternion(0, 0, 0, 0));
        GameObject dot6 = Instantiate(dot,
            new Vector3(1, 0, 0),
            new Quaternion(0, 0, 0, 0));
        GameObject dot7 = Instantiate(dot,
            new Vector3(-1, -1, 0),
            new Quaternion(0, 0, 0, 0));
        GameObject dot8 = Instantiate(dot,
            new Vector3(0, -1, 0),
            new Quaternion(0, 0, 0, 0));
        GameObject dot9 = Instantiate(dot,
             new Vector3(1, -1, 0),
             new Quaternion(0, 0, 0, 0));

        dots = new GameObject[] { dot1, dot2, dot3, dot4, dot5, dot6, dot7, dot8, dot9 };

        dot1.name = "dot1";
        dot2.name = "dot2";
        dot3.name = "dot3";
        dot4.name = "dot4";
        dot5.name = "dot5";
        dot6.name = "dot6";
        dot7.name = "dot7";
        dot8.name = "dot8";
        dot9.name = "dot9";

        dot1.AddComponent<CircleCollider2D>();
        dot2.AddComponent<CircleCollider2D>();
        dot3.AddComponent<CircleCollider2D>();
        dot4.AddComponent<CircleCollider2D>();
        dot5.AddComponent<CircleCollider2D>();
        dot6.AddComponent<CircleCollider2D>();
        dot7.AddComponent<CircleCollider2D>();
        dot8.AddComponent<CircleCollider2D>();
        dot9.AddComponent<CircleCollider2D>();

        dot1.AddComponent<MouseDown>();
        dot2.AddComponent<MouseDown>();
        dot3.AddComponent<MouseDown>();
        dot4.AddComponent<MouseDown>();
        dot5.AddComponent<MouseDown>();
        dot6.AddComponent<MouseDown>();
        dot7.AddComponent<MouseDown>();
        dot8.AddComponent<MouseDown>();
        dot9.AddComponent<MouseDown>();

        foreach (GameObject obj in dots)
        {
            obj.GetComponent<Renderer>().enabled = true;
            if (obj.name[3].ToString() == offColorNum.ToString())
            {
                //paint dot off color
                obj.GetComponent<Renderer>().material.color = offColor;
            }
            else
            {
                // paint dot normal color
                obj.GetComponent<Renderer>().material.color = mainColor;
            }
        }

        gameState = "Playing";
    }
}
