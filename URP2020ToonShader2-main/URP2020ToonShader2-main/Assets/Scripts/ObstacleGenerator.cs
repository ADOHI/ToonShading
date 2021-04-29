using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject sphere;
    public float radius;
    public float amount;
    public float scale;
    public float maxScale;
    private void Start()
    {
        GenerateLeafs();
    }
    public void GenerateLeafs()
    {
        for (float f = 0f; f < radius; f += radius / amount)
        {

            var randomPos = Random.onUnitSphere * f;
            var randomRadius = (maxScale - (f * maxScale / radius));

            while (!Physics.CheckSphere(randomPos, randomRadius))
            {
                randomPos = Vector3.Lerp(randomPos, this.transform.position, Time.deltaTime);
            }
            var obj = Instantiate(sphere);
            obj.transform.position = randomPos * radius;
            obj.transform.localScale = Vector3.one * (maxScale - (f * maxScale / radius));
          
        }
    }
}
