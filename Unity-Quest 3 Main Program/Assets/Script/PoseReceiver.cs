using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

public class PoseReceiver : MonoBehaviour
{
    public Transform[] jointTransforms; // 關節Transforms，根據你的需求設置

    private UdpClient udpClient;
    private IPEndPoint endPoint;

    [StructLayout(LayoutKind.Sequential)]
    struct Position
    {
        public float x;
        public float y;
        public float z;
    }

    void Start()
    {
        // 初始化UDP連線
        udpClient = new UdpClient(12345);
        endPoint = new IPEndPoint(IPAddress.Any, 0);

        // 輸出連線成功訊息
        Debug.Log("Pose receiver started. Waiting for data...");

        // 開始協程
        StartCoroutine(ReceiveData());

    }

    private IEnumerator ReceiveData()
    {
        while (true)
        {
            // 接收數據
            byte[] data = udpClient.Receive(ref endPoint);

            // 解析數據
            int size = Marshal.SizeOf<Position>();
            int count = data.Length / size;
            Position[] positions = new Position[count];

            for (int i = 0; i < count; i++)
            {
                byte[] bytes = new byte[size];
                Buffer.BlockCopy(data, i * size, bytes, 0, size);
                positions[i] = ByteArrayToStructure<Position>(bytes);
            }

            // 應用數據到關節Transforms
            for (int i = 0; i < jointTransforms.Length; i++)
            {
                if (i < positions.Length)
                {
                    Vector3 position = new Vector3(positions[i].x, positions[i].y, positions[i].z);
                    jointTransforms[i].position = position;
                }
            }

            // 等待下一幀
            yield return null;
        }
    }

    // 將byte數組轉換為結構體
    static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
    {
        GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        handle.Free();
        return structure;
    }
}
