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
      eventBus.StartGame();
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
      int stars = game.Finish();
      data.FinishStage(game);
      SceneManager.LoadScene("PostStage");
      return;
    }

    if (started)
    {
      bool dead = game.KillPek();
      if (dead)
      {
        SceneManager.LoadScene("Gameover");
        return;
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
}
