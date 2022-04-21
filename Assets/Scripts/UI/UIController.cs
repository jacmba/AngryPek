using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  [SerializeField] private Text attemptsText;
  [SerializeField] private Text starsText;
  [SerializeField] private GameData data;
  [SerializeField] private GameObject hud;

  private GameController gameController;
  private EventBus eventBus;
  private Animator animator;

  // Start is called before the first frame update
  void Start()
  {
    gameController = Transform.FindObjectOfType<GameController>();
    animator = GetComponent<Animator>();

    eventBus = EventBus.GetInstance();
    eventBus.OnGameRendered += OnGameRendered;
    eventBus.OnFadeOutStarted += OnFadeOutStarted;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    eventBus.OnGameRendered -= OnGameRendered;
    eventBus.OnFadeOutStarted -= OnFadeOutStarted;
  }

  // Update is called once per frame
  void Update()
  {
    // Draw attempts
    if (gameController.game.attempts > 0)
    {
      attemptsText.text = gameController.game.attempts.ToString();
    }

    // Draw stars
    starsText.text = data.totalStars.ToString();
  }

  void OnGameRendered()
  {
    hud.SetActive(true);
  }

  void OnFadeOutStarted()
  {
    hud.SetActive(false);
    animator.SetTrigger("FadeWhite");
  }

  void OnFadeOutDone()
  {
    eventBus.DoneFadeOut();
  }

  public void OnExitClick()
  {
    eventBus.ExitToMenu();
  }
}
