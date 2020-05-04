using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public GameObject[] levels;
    public BoardRotator rotator;
    private int curLevel = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        foreach (GameObject l in levels) {
            l.SetActive(false);
        }
        levels[curLevel].SetActive(true);
        rotator.board = levels[curLevel].transform;
    }

    public void NextLevel() {
        levels[curLevel].SetActive(false);
        curLevel++;
        if (curLevel >= levels.Length) {
            SceneManager.LoadScene(0);
        } else {
            levels[curLevel].SetActive(true);
            rotator.board = levels[curLevel].transform;
            rotator.LevelReset();
        }
    }
    
}
