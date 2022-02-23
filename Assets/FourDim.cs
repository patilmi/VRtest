using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FourDim : MonoBehaviour
{
    List<List<float>> balls = new List<List<float>>();
    List<float> eye = new List<float>() {0, 0, 0, 50};
    List<float> wHat = new List<float>() {0, 0, 0, 1};
    int edge1 = -2;
    int edge2 = 2;
    int inc = 2;
    float ballRad = 0.1f;
   

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

    List<float> VectorSubtract(List<float> left, List<float> right)
    {
        if (left.Count != right.Count)
        {
            throw new ArgumentException("Vectors have mismatched dimension");
        }
        List<float> result = new List<float>() {0, 0, 0, 0};
        for (int i = 0; i < left.Count; i++)
        {
            result[i] = left[i] - right[i];
        }

        return result;
    }

    List<float> VectorScale(float scale, List<float> toScale)
    {
        for (int i = 0; i < toScale.Count; i++)
        {
            toScale[i] *= scale;
        }

        return toScale;
    }

    List<float> Project(List<float> fourDpoint)
    {

        List<float> p = VectorSubtract(fourDpoint, eye);
        float pDotDub = DotProduct(p, wHat);
        List<float> projected = VectorSubtract(p, VectorScale(pDotDub, wHat));
        float scaleDown = 1/(1 - pDotDub);
        projected = VectorScale(scaleDown, projected);

        projected.RemoveAt(3);

        return projected;
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize ball location vectors
        for (int i = edge1; i <= edge2; i += inc)
        {
            for (int j = edge1; j < edge2; j += inc)
            {
                for (int k = edge1; k < edge2; k += inc)
                {

                    for (int l = edge1; l < edge2; l += inc)
                    {
                        List<float> ball = new List<float>() { i, j, k, l };
                        balls.Add(ball);
                        
                    }

                }

            }
        }

    
        



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
