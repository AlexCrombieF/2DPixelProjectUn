using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged.AddListener(CharacteTookDamage);
        CharacterEvents.characterHealed.AddListener(CharacterHealed);
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged.RemoveListener(CharacteTookDamage);
        CharacterEvents.characterHealed.RemoveListener(CharacterHealed);
    }
    public void CharacteTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    } 
}
