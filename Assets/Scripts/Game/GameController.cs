using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  // Global variables
  public static int maxAttempts = 3;
  public static int level = 1;
  public static int achievedStars;
  public static int achievedPieces;
  public static int maxLevel = 4;

  // Stage variables
  [SerializeField] public int attemps;
  [SerializeField] private bool isSandbox = false;
  [SerializeField] private int numEnemies = 0;

  private AudioClip wherePizza;
  private AudioSource audioSource;
  private bool started;
  private Game game;
  private bool canTouch;
  private EventBus eventBus;

  // Start is called before the first frame update
  void Start()
  {
    if (!isSandbox)
    {
      attemps = maxAttempts;
    }
    started = false;
    canTouch = false;
    game = new Game(numEnemies);

    eventBus = EventBus.GetInstance();
    eventBus.OnGameStart += onGameStart;
    eventBus.OnPizzaCollected += onCollectPizza;

    audioSource = GetComponent<AudioSource>();
    wherePizza = Resources.Load<AudioClip>("Sounds/where_pizza");
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    eventBus.OnGameStart -= onGameStart;
    eventBus.OnPizzaCollected -= onCollectPizza;
  }

  // Update is called once per frame
  void Update()
  {
    if (!started && Input.GetMouseButtonDown(0) && canTouch)
    {
      eventBus.startGame();
    }

    if (!Input.GetMouseButton(0))
    {
      canTouch = true;
    }
  }

  // Own events implementation
  void onGameStart()
  {
    if (game.hasPizza)
    {
      level++;
      achievedPieces++;
      achievedStars = game.Finish(attemps);
      SceneManager.LoadScene("PostStage");
      return;
    }

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

  void onCollectPizza()
  {
    game.CollectPizza();
  }

  public static void Clean()
  {
    maxAttempts = 3;
    level = 1;
    achievedPieces = 0;
  }
}
