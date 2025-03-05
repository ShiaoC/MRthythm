using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

public class PoseReceiver : MonoBehaviour
{
    public Transform[] jointTransforms; // ���`Transforms�A�ھڧA���ݨD�]�m

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
        // ��l��UDP�s�u
        udpClient = new UdpClient(12345);
        endPoint = new IPEndPoint(IPAddress.Any, 0);

        // ��X�s�u���\�T��
        Debug.Log("Pose receiver started. Waiting for data...");

        // �}�l��{
        StartCoroutine(ReceiveData());

    }

    private IEnumerator ReceiveData()
    {
        while (true)
        {
            // �����ƾ�
            byte[] data = udpClient.Receive(ref endPoint);

            // �ѪR�ƾ�
            int size = Marshal.SizeOf<Position>();
            int count = data.Length / size;
            Position[] positions = new Position[count];

            for (int i = 0; i < count; i++)
            {
                byte[] bytes = new byte[size];
                Buffer.BlockCopy(data, i * size, bytes, 0, size);
                positions[i] = ByteArrayToStructure<Position>(bytes);
            }

            // ���μƾڨ����`Transforms
            for (int i = 0; i < jointTransforms.Length; i++)
            {
                if (i < positions.Length)
                {
                    Vector3 position = new Vector3(positions[i].x, positions[i].y, positions[i].z);
                    jointTransforms[i].position = position;
                }
            }

            // ���ݤU�@�V
            yield return null;
        }
    }

    // �Nbyte�Ʋ��ഫ�����c��
    static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
    {
        GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        handle.Free();
        return structure;
    }
}
