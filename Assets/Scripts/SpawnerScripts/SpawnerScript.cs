using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] Transform spawner1;
    [SerializeField] Transform spawner2;
    [SerializeField] Transform spawner3;
    [SerializeField] Transform spawner4;
    [SerializeField] Transform spawner5;

    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] GameObject enemy5;

    void Start()
    {
        StartCoroutine(LevelSpawnPattern());
    }


    IEnumerator LevelSpawnPattern()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 5; i++) 
        {
            Instantiate(enemy1, spawner4.position, Quaternion.Euler(0, 0, 135));
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemy1, spawner4.position, Quaternion.Euler(0, 0, 135));
            yield return new WaitForSeconds(0.3f);
        }

    }
}
