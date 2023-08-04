 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

 [System.Serializable]
public class ApiResponse
{
    public List<ClientData> clients;
    public Dictionary<int, ClientDetails> data;
}

[System.Serializable]
public class ClientData
{
    public int id;
    public string label;
    public bool isManager;
}

[System.Serializable]
public class ClientDetails
{
    public string name;
    public int points;
    public string address;
}

public class ApiClient : MonoBehaviour
{
    public List<ClientData> clients = new List<ClientData>();
    public Dictionary<int, ClientDetails> clientDetails = new Dictionary<int, ClientDetails>();
    public GameObject clientItemPrefab;
    public Transform clientList;

    private IEnumerator Start()
    {
        yield return StartCoroutine(FetchDataFromAPI());
        PopulateClientList();
    }

    private IEnumerator FetchDataFromAPI()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                ParseJsonData(json);
            }
            else
            {
                Debug.LogError("Failed to fetch data from the API: " + request.error);
            }
        }
    }

private void ParseJsonData(string json)
{
    ApiResponse response = JsonUtility.FromJson<ApiResponse>(json);

    // Populate clients list
    clients = response.clients;

    // Clear the client details dictionary before repopulating it
    clientDetails.Clear();

    // Check if response.data is not null and contains any entries before iterating through it
    if (response.data != null && response.data.Count > 0)
    {
        // Iterate through the data section and populate the client details dictionary
        foreach (var kvp in response.data)
        {
            int id = kvp.Key;
            ClientDetails details = kvp.Value;
            clientDetails.Add(id, details);
        }
    }
    else
    {
        Debug.LogError("Response data is null or not present in the JSON, or it doesn't contain any entries.");
    }
}



    public void PopulateClientList()
    {
        // Clear the client list before repopulating it
        foreach (Transform child in clientList)
        {
            Destroy(child.gameObject);
        }

        // Iterate through the list of clients and instantiate the client item prefab for each client.
        foreach (ClientData client in clients)
        {
            GameObject clientItemObject = Instantiate(clientItemPrefab, clientList);
            ClientItem clientItem = clientItemObject.GetComponent<ClientItem>();

            // Update the client item UI with the client data.
            clientItem.labelText.text = client.label;
            clientItem.pointsText.text = clientDetails.ContainsKey(client.id) ? clientDetails[client.id].points.ToString() : "N/A";
        }
    }
}

