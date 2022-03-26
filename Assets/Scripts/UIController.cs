using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  [SerializeField]
  private Text attemptsText;

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
    attemptsText.text = "x " + gameController.attemps;
  }
}
