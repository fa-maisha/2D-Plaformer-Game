using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;

    public GameObject player;
    public GameObject LoadCanvas;
    public List<GameObject> Levels;
    private int currentLevelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        PopQuiz.OnQuizCollect += IncreaseProgressAmount;
        LoadLevel.OnHoldComplete += LoadNextLevel;
        LoadCanvas.SetActive(false);

    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;
        if (progressAmount > 0)
        {
            //Level complete!
            LoadCanvas.SetActive(true);
            Debug.Log("Level Complete");
        }
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex == Levels.Count - 1) ? 0 : currentLevelIndex + 1;
        LoadCanvas.SetActive(false);

        Levels[currentLevelIndex].gameObject.SetActive(false);
        Levels[nextLevelIndex].gameObject.SetActive(true);

        player.transform.position = new Vector3(-6.76f, -1.64f, 0.05157328f);

        currentLevelIndex = nextLevelIndex;
        progressAmount = 0;
        progressSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
