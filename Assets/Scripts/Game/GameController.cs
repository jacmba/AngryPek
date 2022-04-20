using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  // Stage variables
  [SerializeField] public int attemps;
  [SerializeField] private bool isSandbox = false;
  [SerializeField] private int numEnemies = 0;
  [SerializeField] GameData data;

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
      attemps = Constants.MAX_ATTEMPTS;
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
      data.level++;
      data.pieces++;
      data.stars = game.Finish(attemps);
      data.totalStars += data.stars;
      SceneManager.LoadScene("PostStage");
      return;
    }

    if (started)
    {
      attemps--;
      if (attemps == 0)
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
