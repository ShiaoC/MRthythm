using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace extOSC
{
    public class OSCMediapipe : MonoBehaviour
    {
        [Header("OSC Settings")]
        public OSCReceiver Receiver;
        [SerializeField] GameObject[] Point;


        void Start()
        {
            Debug.Log(Receiver.LocalHost);
            Receiver.Bind("/tracking/trackers/1/position", (message) => ReceiveVector3(message, 0));
            Receiver.Bind("/tracking/trackers/2/position", (message) => ReceiveVector3(message, 1));
            Receiver.Bind("/tracking/trackers/3/position", (message) => ReceiveVector3(message, 2));
        }

        void changeBodyPartPosition(Vector3 target, GameObject point)
        {
            point.transform.position = target;
        }

        
        public void ReceiveVector3(OSCMessage message, int n)
        {
            if (message.ToVector3(out var vector))
            {
                Point[n].transform.position = vector;

            }
        }
    }
}

