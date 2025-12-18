using UnityEngine;
using UnityEngine;


public class Piece : MonoBehaviour
{
    float fall = 0;
    public float fallSpeed = 1f; // segundos por paso
    private bool allowRotation = true;
    private bool limitRotation = false; // para la pieza O


    void Start()
    {
        if (!GridManager.IsValidPosition(transform))
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }
    void Update()
    {
        // Movimiento lateral
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!GridManager.IsValidPosition(transform)) transform.position += new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!GridManager.IsValidPosition(transform)) transform.position += new Vector3(-1, 0, 0);
        }


        // Rotación
        if (allowRotation && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            transform.Rotate(0, 0, -90);
            if (!GridManager.IsValidPosition(transform)) transform.Rotate(0, 0, 90);
        }


        // Soft drop
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!GridManager.IsValidPosition(transform))
            {
                transform.position += new Vector3(0, 1, 0);
                Landed();
            }
            else
            {
                fall = Time.time;
            }
        }

        // Rotación
        if (allowRotation && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
{
    transform.Rotate(0, 0, -90);
    if (!GridManager.IsValidPosition(transform)) transform.Rotate(0, 0, 90);
}


// Soft drop
if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
{
    transform.position += new Vector3(0, -1, 0);
    if (!GridManager.IsValidPosition(transform))
    {
        transform.position += new Vector3(0, 1, 0);
        Landed();
    }
    else
    {
        fall = Time.time;
    }
}


// Hard drop
if (Input.GetKeyDown(KeyCode.Space))
{
    while (true)
    {
        transform.position += new Vector3(0, -1, 0);
        if (!GridManager.IsValidPosition(transform))
        {
            transform.position += new Vector3(0, 1, 0);
            Landed();
            break;
        }
    }
}


// Caída automática
if (Time.time - fall >= (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? fallSpeed / 10f : fallSpeed))
{
    transform.position += new Vector3(0, -1, 0);
    if (!GridManager.IsValidPosition(transform))
    {
        transform.position += new Vector3(0, 1, 0);
        Landed();
    }
    fall = Time.time;
}
}


void Landed()
{
    // Almacena en la grilla
    GridManager.StorePiece(transform);
    // Borra filas completas
    int lines = GridManager.DeleteFullRows();
    if (lines > 0) GameManager.instance.AddLines(lines);
    // Spawnea la siguiente pieza
    FindObjectOfType<Spawner>().SpawnNext();
    // Desactiva este objeto
    enabled = false;
}
}
