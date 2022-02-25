using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FourDim : MonoBehaviour
{
    List<List<float>> balls = new List<List<float>>();
    List<GameObject> ballList = new List<GameObject>();
    List<float> eye = new List<float>() {0, 0, 0, 1};
    List<float> wHat = new List<float>() {0, 0, 0, 1};
    float edge1 = -0.3f;
    float edge2 = 0.3f;
    float inc = 0.3f;
    float fourDR = 0.1f;


    float alpha, beta, gamma, delta, epsilon, nu;
    List<float> xUpdate, yUpdate, zUpdate, wUpdate;

    

    float DotProduct(List<float> left, List<float> right)
    {
        if (left.Count != right.Count)
        {
            throw new ArgumentException("Vectors have mismatched dimension");
        }
        float sum = 0;
        for (int i = 0; i < left.Count; i++)
        {
            sum += left[i] * right[i];
        }
        return sum;
    }

    List<float> VectorsSummed(List<float> left, List<float> right)
    {
        if (left.Count != right.Count)
        {
            throw new ArgumentException("Vectors have mismatched dimension");
        }
        List<float> result = new List<float>();
        for (int i = 0; i < left.Count; i++)
        {
            result.Add(left[i] + right[i]);
        }

        return result;
    }


    List<float> VectorsSubtracted(List<float> left, List<float> right)
    {
        if (left.Count != right.Count)
        {
            throw new ArgumentException("Vectors have mismatched dimension");
        }
        List<float> result = new List<float>() {};
        for (int i = 0; i < left.Count; i++)
        {
            result.Add(left[i] - right[i]);
        }

        return result;
    }

    List<float> VectorScaled(float scale, List<float> toScale)
    {
        List<float> scaled = new List<float>(toScale);
        for (int i = 0; i < toScale.Count; i++)
        {
            scaled[i] *= scale;
        }

        return scaled;
    }

    List<float> Projected(List<float> fourDpoint)
    {
        float pDotDub = DotProduct(fourDpoint, wHat);
        List<float> projected = VectorsSubtracted(fourDpoint, VectorScaled(pDotDub, wHat));
        float scaleDown = 1 / (1 - pDotDub);
        projected = VectorScaled(scaleDown, projected);

        projected.RemoveAt(3);

        return projected;

        //temp just drop 4th dimension
        //List<float> projected = new List<float>(fourDpoint);
        //projected.RemoveAt(3);
        //return projected;
    }

    float ProjectedR(List<float> fourDpoint)
    {
        //g is point minus eye
        List<float> g = VectorsSubtracted(fourDpoint, eye);
        //q is a 4d sphere radial vector that is perpendicular to point
        List<float> q = VectorsSubtracted(VectorScaled(DotProduct(g, g), fourDpoint), VectorScaled(DotProduct(fourDpoint, g), g));

        //Projected Radius Vector = R

        List<float> R = VectorsSubtracted(Projected(VectorsSummed(fourDpoint, q)), Projected(fourDpoint));

        return fourDR * (float)Math.Sqrt((double)DotProduct(R, R) / (double)DotProduct(q, q));
    }

    // Start is called before the first frame update
    void Start()
    {

        alpha = 3;
        beta = 5;
        gamma = 4;
        delta = 3;
        epsilon = 2;
        nu = 7;

        xUpdate = new List<float>() { 0, alpha, beta, gamma };
        yUpdate = new List<float>() { -alpha, 0, delta, epsilon };
        zUpdate = new List<float>() { -beta, -delta, 0, nu };
        wUpdate = new List<float>() { -gamma, -epsilon, -nu, 0 };

        

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var sphereRenderer = sphere.GetComponent<Renderer>();
        sphereRenderer.material.SetColor("_Color", Color.red);
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


        //initialize ball location vectors
        for (float i = edge1; i <= edge2; i += inc)
        {
            for (float j = edge1; j <= edge2; j += inc)
            {
                for (float k = edge1; k <= edge2; k += inc)
                {

                    for (float l = edge1; l <= edge2; l += inc)
                    {
                        List<float> ball = new List<float>() { i, j, k, l};

                        balls.Add(ball);
                        List<float> projected = Projected(ball);
                        float radius = ProjectedR(ball);

                        sphere.transform.localScale = new Vector3(radius, radius, radius);
                        ballList.Add(Instantiate(sphere, new Vector3(projected[0], projected[1], projected[2]), Quaternion.identity));

                    }

                }

            }
        }

    
        



    }

    // Update is called once per frame
    void Update()
    {

        float speed = 0.0005f;
        float factor = Time.deltaTime * speed;


        for (int i = 0; i < balls.Count; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                balls[i][0] += factor * DotProduct(xUpdate, balls[i]); 
                balls[i][1] += factor * DotProduct(yUpdate, balls[i]);
                balls[i][2] += factor * DotProduct(zUpdate, balls[i]);
                balls[i][3] += factor * DotProduct(wUpdate, balls[i]);

            }
            List<float> projected = Projected(balls[i]);
            ballList[i].transform.position = new Vector3(projected[0], projected[1], projected[2]);
            float radius = ProjectedR(balls[i]);
            ballList[i].transform.localScale = new Vector3(radius, radius, radius);



        }
    }
}
