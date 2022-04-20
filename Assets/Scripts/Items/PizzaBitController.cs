using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBitController : MonoBehaviour
{
  [SerializeField] private float rotateSpeed = 15f;

  [SerializeField] private GameObject particles;
  private EventBus eventBus;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    eventBus = EventBus.GetInstance();
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
      eventBus.collectPizza();
      GameObject fireworks = Instantiate(particles, transform);
      fireworks.transform.SetParent(null);
      Destroy(gameObject);
    }
  }
}
