using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public int level = 1;
    public int linesCleared = 0;


    void Awake()
    {
        if (instance == null) instance = this; else Destroy(gameObject);
    }


    public void AddLines(int n)
    {
        // Sistema básico de puntuación
        int points = 0;
        switch (n)
        {
            case 1: points = 40 * level; break;
            case 2: points = 100 * level; break;
            case 3: points = 300 * level; break;
            case 4: points = 1200 * level; break;
        }
        score += points;
        linesCleared += n;
        if (linesCleared >= level * 10)
        {
            level++;
            // Puede aumentar velocidad de caída de las piezas
            IncreaseSpeed();
        }
        Debug.Log($"Lines: {linesCleared} Score: {score} Level: {level}");
    }


    void IncreaseSpeed()
    {
        // Encuentra todas las piezas activas y reduce su fallSpeed
        Piece[] pieces = FindObjectsOfType<Piece>();
        foreach (Piece p in pieces)
        {
            p.fallSpeed = Mathf.Max(0.05f, p.fallSpeed * 0.9f);
        }
    }
}