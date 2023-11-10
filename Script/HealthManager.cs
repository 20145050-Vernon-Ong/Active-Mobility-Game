using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HealthManager : MonoBehaviour
{
    public static int health;
    public static int points;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    void Awake()
    {
        health = 3;
        points = 0;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
        // points = health * 5;
    }

    public int GetPoints()
    {
        return points;
    }
}
