using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public static class ConnectAPI<T>
{
    public static string url = "localhost:5000/api/";
// r    public string myUserID; guardar en player prefs

    public static async Task<T> get(string EP)
    {
        var task = Task.Run(() => internalGet(EP));
        return await task;
    }

    private static T internalGet(string EP)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + EP))
        {
            var request = www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                return JsonUtility.FromJson<T>("{\"info\":" + www.downloadHandler.text + "}");

            } else {
                Debug.Log("Error: " + www.error);
                return default(T);
            }
        }
    }

    
    // public static async Task<T> post(ISerializableJson data, string EP)
    // {
    //     using (UnityWebRequest www = UnityWebRequest.Post(url + EP, data.Serialize()))
    //     {
    //         var request = www.SendWebRequest();

    //         if (www.result == UnityWebRequest.Result.Success) {
    //             //Debug.Log("Response: " + www.downloadHandler.text);
    //             // Compose the response to look like the object we want to extract
    //             // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
    //             string jsonString = data.Serialize();
    //             allplayers = JsonUtility.FromJson<playerList>(jsonString);

    //         } else {
    //             Debug.Log("Error: " + www.error);
    //         }
    //     }
    // }
}
