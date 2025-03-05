using System;
using UnityEngine;

public class LoadDanceByte
{

    public void Load(TextAsset binaryData, int frameCount, float[,,] floatArray)
    {
        if (binaryData != null)
        {
            byte[] bytes = binaryData.bytes;
            int floatSize = 4;  //one float 4 word

            int numRows = 15;  
            int numCols = frameCount;  
            int xyz = 3;  

            floatArray = new float[numRows, numCols, xyz];

            int byteIndex = 0;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    for (int k = 0; k < xyz; k++)
                    {
                        floatArray[i, j, k] = BitConverter.ToSingle(bytes, byteIndex);
                        byteIndex += floatSize;
                    }
                }
            }

            // floatArray
        }
        else
        {
            Debug.LogError("Binary data is null.");
        }
    }
}
