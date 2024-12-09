using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    private Player_Base_Controller player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player_Base_Controller>();

        playerHealthBar.maxValue = player.maxHealth;

        playerHealthBar.value = playerHealthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerHealth(int amount)
    {
        playerHealthBar.value = amount;
    }
}
