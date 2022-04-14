using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostStageController : MonoBehaviour
{
  [SerializeField] private GameObject rootStar;

  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(ShowStars());
  }

  // Update is called once per frame
  void Update()
  {
  }

  IEnumerator ShowStars()
  {
    yield return new WaitForSeconds(1);
    rootStar.SetActive(true);
  }
}
