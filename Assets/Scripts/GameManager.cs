using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Animator cameraAnim;
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private ObjectSpawner objectSpawner;
    [SerializeField]
    private List<GameObject> objectsToDelete;
    [SerializeField]
    private List<GameObject> objectsToEnable;

    [SerializeField]
    private List<GameObject> UIObjects;

    [SerializeField]
    private GameObject loveBlurbPrefabs;
    [SerializeField]
    private GameObject chaosBlurbPrefabs;

    private bool pauseGame;
    private bool gameStarted;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted) {
            pauseGame = !pauseGame;

            if (pauseGame) { 
                Time.timeScale = 0f;
                for (int i = 0; i < UIObjects.Count; i++)
                    UIObjects[i].SetActive(true);
            }
            else { 
                Time.timeScale = 1f;
                for (int i = 0; i < UIObjects.Count; i++)
                    UIObjects[i].SetActive(false);
            }
        }

    }

    public void StartGame() {
        gameStarted = true;
        objectSpawner.SpawnObjects();
        playerAnim.SetTrigger("startGame");
        cameraAnim.SetTrigger("startGame");

        for (int i = 0; i < objectsToDelete.Count; i++)
            objectsToDelete[i].SetActive(false);

        for (int i = 0; i < objectsToEnable.Count; i++)
            objectsToEnable[i].SetActive(true);
    }

    public GameObject GetLoveBlurb() {
        return loveBlurbPrefabs;
    }

    public GameObject GetChaosBlurb() {
        return chaosBlurbPrefabs;
    }

    public void PlayerIsDead() {
        for (int i = 0; i < UIObjects.Count; i++)
            UIObjects[i].SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
 }
