using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotatingBalls : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int height = 6;
    //public float speed;

    public float alpha = 3;
    public float beta = 2;
    public float gamma = 7;


   

    List<GameObject> ballList = new List<GameObject>();

    void Start()
    {
        height = 3;
        alpha = 3;
        beta = 2;
        gamma = 7;
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        //sphere.AddComponent<Animation>();
        //sphere.AddComponent<MeshFilter>();
        //sphere.AddComponent<MeshCollider>();
        //sphere.AddComponent<MyCustomScript>();
        //sphere.AddComponent<MeshRenderer>();
        //speed = 5f;

        var sphereRenderer = sphere.GetComponent<Renderer>();

        sphereRenderer.material.SetColor("_Color", Color.red);
        
        for (int y=-3; y <= height; y+=3)
        {
           for (int x=-3; x <= height; x+=3)
           {
                for (int z = -3; z <= height; z+=3)
                {
                    ballList.Add(Instantiate(sphere, new Vector3(x, y, z), Quaternion.identity));
                }
             
           }
        }    

    
    }

    // Update is called once per frame
    void Update()  
    {
        float x, y, z;
        float speed = 0.0001f;
        float factor = Time.deltaTime * speed;
        for (int i = 0; i < ballList.Count; i++)

        {
            x = ballList[i].transform.position.x;
            y = ballList[i].transform.position.y;
            z = ballList[i].transform.position.z;
            for (int j = 0; j < 100; j++)
            {
                x = x + factor * (alpha * y + beta * z);
                y = y + factor * (gamma * z - alpha * x);
                z = z - factor * (gamma * y + beta * x);

            }
            ballList[i].transform.position = new Vector3(x, y, z);

            //ballList[i].transform.position = new Vector3(x + factor * (alpha * y + beta * z),
            //    y + factor * (gamma * z - alpha * x), z - factor * (gamma * y + beta * x));
        }

    }
}
