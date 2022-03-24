using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBitController : MonoBehaviour
{
  [SerializeField]
  private float rotateSpeed = 15f;

  // Start is called before the first frame update
  void Start()
  {
    // ToDo initialize stuff
  }

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
  }

  /// <summary>
  /// OnTriggerEnter is called when the Collider other enters the trigger.
  /// </summary>
  /// <param name="other">The other Collider involved in this collision.</param>
  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      GameController.collectPizza();
      Destroy(gameObject);
    }
  }
}
