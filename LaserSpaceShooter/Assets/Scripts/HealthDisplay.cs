using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    Player sertonHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        sertonHealth = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = sertonHealth.GetHealth().ToString();
    }
}
