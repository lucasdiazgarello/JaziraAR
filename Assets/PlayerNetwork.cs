using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<int> randonNumber = new NetworkVariable<int>(1);

    private void Update()
    {
        if (!IsOwner) return;
    }
}
