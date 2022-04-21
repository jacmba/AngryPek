using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  // Stage variables
  [SerializeField] private int attemps;
  [SerializeField] private bool isSandbox = false;
  [SerializeField] private int numEnemies = 0;
  [SerializeField] GameData data;

  public Game game { get; private set; }
  private AudioClip wherePizza;
  private AudioSource audioSource;
  private bool started;
  private bool canTouch;
  private EventBus eventBus;

  // Start is called before the first frame update
  void Start()
  {
    if (!isSandbox)
    {
      attemps = data.attempts;
    }
    started = false;
    canTouch = false;
    game = new Game(numEnemies, attemps);

    eventBus = EventBus.GetInstance();
    eventBus.OnGameStart += OnGameStart;
    eventBus.OnPizzaCollected += OnCollectPizza;
    eventBus.OnFadeOutDone += OnFadeOutDone;
    eventBus.OnExitToMenu += OnExitToMenu;

    audioSource = GetComponent<AudioSource>();
    wherePizza = Resources.Load<AudioClip>("Sounds/where_pizza");
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    eventBus.OnGameStart -= OnGameStart;
    eventBus.OnPizzaCollected -= OnCollectPizza;
    eventBus.OnFadeOutDone -= OnFadeOutDone;
    eventBus.OnExitToMenu -= OnExitToMenu;
  }

  // Update is called once per frame
  void Update()
  {
    if (!started && Input.GetMouseButtonDown(0) && canTouch)
    {
      eventBus.StartGame();
    }

    if (!Input.GetMouseButton(0))
    {
      canTouch = true;
    }
  }

  // Own events implementation
  void OnGameStart()
  {
    if (game.hasPizza)
    {
      int stars = game.Finish();
      data.FinishStage(game);
      eventBus.StartFadeOut();
      return;
    }

    if (started)
    {
      bool dead = game.KillPek();
      if (dead)
      {
        eventBus.StartFadeOut();
        return;
      }
    }
    else
    {
      started = true;
      audioSource.PlayOneShot(wherePizza);
    }
  }

  void OnCollectPizza()
  {
    game.CollectPizza();
  }

  void OnFadeOutDone()
  {
    string scene = game.hasPizza ? "PostStage" : "Gameover";
    SceneManager.LoadScene(scene);
  }

  void OnExitToMenu()
  {
    data.Clean();
    SceneManager.LoadScene(0);
  }
}
