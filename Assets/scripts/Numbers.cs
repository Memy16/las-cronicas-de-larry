using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    public int Money = 5;
    public int Lives = 200;
    public Text NumOfCoins;
    public Text NumOfLives;

    void Start()
    {
        NumOfLives = GameObject.FindGameObjectWithTag("NumOfLives").GetComponent<Text>();
        UpdateLives();
        NumOfCoins = GameObject.FindGameObjectWithTag("NumOfCoins").GetComponent<Text>();
        UpdateCoins();
    }

    void UpdateCoins()
    {
        NumOfCoins.text = Money.ToString();
    }

    private void OnCollision2DEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Lives -= 5;
            UpdateLives();
        }
    }

    void UpdateLives()
    {
        NumOfLives.text = Lives.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Money")
       {
           Money++;
           Destroy(collision.gameObject);
           UpdateCoins();
       }
    }
    
}