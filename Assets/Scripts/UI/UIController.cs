using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  [SerializeField] private Text attemptsText;
  [SerializeField] private Text starsText;
  [SerializeField] private GameData data;

  private GameController gameController;

  // Start is called before the first frame update
  void Start()
  {
    gameController = Transform.FindObjectOfType<GameController>();
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
}
