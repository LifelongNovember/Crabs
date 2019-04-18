using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;

public class NetworkController : NetworkManager
{
    public int playerId;

    public override void OnClientConnect(NetworkConnection conn) {
        IntegerMessage msg = new IntegerMessage(playerId);
        ClientScene.AddPlayer(conn, 0, msg);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader messageReader) {
        if(messageReader != null) {
            IntegerMessage stream = messageReader.ReadMessage<IntegerMessage>(); 
            playerId  = stream.value;
        }

        GameObject player = Instantiate(playerPrefab, playerPrefab.GetComponent<PlayerController>().spawnPosition.position, Quaternion.identity) as GameObject;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
