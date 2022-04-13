using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
  // Event bus stuff
  public static event Action OnGameStart;
  public static event Action OnDragStart;
  public static event Action OnLaunchPek;
  public static event Action OnPizzaCollected;

  // Global variables
  public static int maxAttempts = 3;

  // Stage variables
  [SerializeField]
  public int attemps;

  [SerializeField]
  private bool isSandbox = false;

  private AudioClip wherePizza;
  private AudioSource audioSource;
  private bool started;

  // Start is called before the first frame update
  void Start()
  {
    if (!isSandbox)
    {
      attemps = maxAttempts;
    }
    started = false;

    OnGameStart += onGameStart;

    audioSource = GetComponent<AudioSource>();

    wherePizza = Resources.Load<AudioClip>("Sounds/where_pizza");
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    OnGameStart -= onGameStart;
  }

  // Update is called once per frame
  void Update()
  {
    if (!started && Input.GetMouseButtonDown(0))
    {
      startGame();
    }
  }

  // Event bus invoke methods
  public static void startGame()
  {
    OnGameStart?.Invoke();
  }

  public static void startDrag()
  {
    OnDragStart?.Invoke();
  }

  public static void launchPek()
  {
    OnLaunchPek?.Invoke();
  }

  public static void collectPizza()
  {
    OnPizzaCollected?.Invoke();
  }

  // Own events implementation
  void onGameStart()
  {
    if (started)
    {
      attemps--;
      if (attemps == 0)
      {
        SceneManager.LoadScene("Gameover");
      }
    }
    else
    {
      started = true;
      audioSource.PlayOneShot(wherePizza);
    }
  }
}
