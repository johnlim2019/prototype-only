using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  public GameObject player;
  private RectTransform rectTransform;
  public Slider healthSlider;
  public LevelManager levelManager;
  public int currentHealth = 100;
  private Vector3 offset = new Vector3(0, 0.6f, 0);

  // Set the initial health when the game starts
  private void Start()
  {
    rectTransform = GetComponent<RectTransform>();
    healthSlider.maxValue = levelManager.playerMaxHealth;
    healthSlider.value = currentHealth;
  }

  void Update()
  {
    rectTransform.position = player.transform.position - offset;
    healthSlider.value = currentHealth;
  }


  public void TakeDamage(int damage)
  {
    currentHealth -= damage;
  }
  public void SetHealth(int currentHealth)
  {
    healthSlider.value = currentHealth;
  }
}
