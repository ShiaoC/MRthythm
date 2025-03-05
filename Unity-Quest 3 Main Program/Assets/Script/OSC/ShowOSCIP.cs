using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace extOSC
{
    public class ShowOSCIP : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI address;
        [SerializeField] TextMeshProUGUI port;
        OSCReceiver Receiver;

        
        void Start()
        {
            Receiver = this.gameObject.GetComponent<OSCReceiver>();
            address.text = Receiver.LocalHost;
            port.text = Receiver.LocalPort.ToString();

            Destroy(this, 5f);
        }
        private void Update()
        {
            address.text = Receiver.LocalHost;
        }
    }
}
