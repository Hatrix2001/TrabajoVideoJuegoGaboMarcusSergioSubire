using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoPrefabs; // arrastra aquí los 7 prefabs


    public void SpawnNext()
    {
        int i = Random.Range(0, tetrominoPrefabs.Length);
        Instantiate(tetrominoPrefabs[i], transform.position, Quaternion.identity);
    }


    void Start()
    {
        SpawnNext();
    }
}