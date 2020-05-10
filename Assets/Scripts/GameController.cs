using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public GameObject[] levels;
    public BoardRotator rotator;
    public GameObject playerBoom;
    private int curLevel;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        curLevel = PlayerPrefs.GetInt("LEVEL", 0);
        foreach (GameObject l in levels) {
            l.SetActive(false);
        }
        levels[curLevel].SetActive(true);
        rotator.board = levels[curLevel].transform;
    }

    public void NextLevel() {
        levels[curLevel].SetActive(false);
        curLevel++;
        PlayerPrefs.SetInt("LEVEL", curLevel);
        if (curLevel >= levels.Length) {
            PlayerPrefs.SetInt("LEVEL", 0);
            ReloadScene();
        } else {
            levels[curLevel].SetActive(true);
            rotator.board = levels[curLevel].transform;
            rotator.LevelReset();
        }
    }

    public void PlayerDestroyed(GameObject player) {
        player.SetActive(false);
        GameObject deathParticles = Instantiate(playerBoom);
        deathParticles.transform.position = player.transform.position;
        Invoke("ReloadScene", 1f);
    }

    private void ReloadScene() {
        SceneManager.LoadScene(0);
    }
    
}
