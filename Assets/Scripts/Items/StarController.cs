using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
  [SerializeField] private int index;
  [SerializeField] private GameObject nextStar;
  private AudioSource audioSource;

  // Start is called before the first frame update
  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  public void Grown()
  {
    audioSource.Play();
    if (nextStar != null && GameController.achievedStars > index)
    {
      nextStar.SetActive(true);
    }
    else
    {
      PostStageController.ShowPizza();
    }
  }
}
