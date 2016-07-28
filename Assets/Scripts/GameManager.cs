using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private int level = 2;
    private List<GameObject> moves = new List<GameObject>(); // List holding music pattern;
    private bool finished = false;
    private bool musicPlaying = false;

    public AudioSource winSound;
    public AudioSource wrongSound;
    public GameObject[] availCubes; // Array of cubes on screen (standart is 4)
    public GameObject cubeParent;
    public Text levelText;
    public GameObject nextLevelButton;

    [HideInInspector]
    public static GameObject lastHit = null;
    [HideInInspector]
    public static int hitCount=-1;

    // Use this for initialization
    void Start() {
        NextLevel();
    }

    
    void InitializeLevel(int lvl)
    {
        moves.Clear();

        levelText.text = "Level:" + (level - 2);
        for (int i = 0; i < lvl; i++)
        {
            int randomIndex = Random.Range(0, availCubes.Length);
            GameObject tempGameObj = availCubes[randomIndex];
            moves.Add(tempGameObj);
        }
    }

    // Starts the pattern playing
    public void StartPattern()
    {
        if (!musicPlaying)
        {
            Restart();
            BlockCubes();
            finished = false;
            StartCoroutine(PlayPattern());
        }
    }

    // Plays preset pattern
    IEnumerator PlayPattern()
    {
        musicPlaying = true;
        for (int i = 0; i < moves.Count; i++)
        {
            moves[i].GetComponent<ClickHandler>().SetPressed();
            yield return new WaitForSeconds(1f);
        }
        UnblockCubes();
        musicPlaying = false;
    }

    // Starts next level
    public void NextLevel()
    {
        nextLevelButton.SetActive(false);
        level++;
        InitializeLevel(level);
        StartPattern();
    }

    // Update is called once per frame
    void Update () {
        if ((lastHit != null) && (moves[hitCount] != lastHit))
        {
            Debug.Log("wrong");
            wrongSound.Play();
            Restart();
        }
        if (hitCount == level -1 )
        {
            FinishLevel();
        }
	}

    // Called once level is finished
    void FinishLevel()
    {
        if (!finished)
        {
            finished = true;
            nextLevelButton.SetActive(true);
            BlockCubes();
            levelText.text = "Level done";
            winSound.Play();
        }
    }

    // Restarts info
    void Restart()
    {
        lastHit = null;
        hitCount = -1;
    }

    // Make cubes unavailable to press
    void BlockCubes()
    {
        foreach (GameObject cube in availCubes)
        {
            cube.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }
    
    // Make cubes available to press
    void UnblockCubes()
    {
        foreach (GameObject cube in availCubes)
        {
            cube.layer = LayerMask.NameToLayer("Default");
        }
    }
}
