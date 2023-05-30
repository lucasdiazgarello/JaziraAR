using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<int> randonNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    private void Update()
    {
        Debug.Log(OwnerClientId + "; randomNumber: " + randonNumber.Value);

        if (!IsOwner) return;

        
    }

}
