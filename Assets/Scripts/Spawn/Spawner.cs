using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _horizontalSpawnBound = 6.0f;
    private float _verticalSpawnBound = 4.0f;

    protected virtual IEnumerator Spawn(GameObject[] spawnObjects, float delay, float yPos)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            var selectedObject = Random.Range(0, spawnObjects.Length);

            Instantiate(spawnObjects[selectedObject],
                ChooseRandomHorizontalPosition(yPos), spawnObjects[selectedObject].transform.rotation);   
        }      
    }  
    protected Vector3 ChooseRandomHorizontalPosition(float yPos)
    {
        var posX = Random.Range(-_horizontalSpawnBound, _horizontalSpawnBound);
        var posZ = Random.Range(-_verticalSpawnBound,_verticalSpawnBound);

        Vector3 spawnPosition = new Vector3(posX,yPos, posZ);
        return spawnPosition;
    }
    protected float CalculateSpawnDelay(float minValue, float maxValue) => Random.Range(minValue, maxValue);
    
}
