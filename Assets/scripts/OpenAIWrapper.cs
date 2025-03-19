using System.Text;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;
using UnityEngine.Networking;

public class OpenAIWrapper : MonoBehaviour
{
    [SerializeField, Tooltip(("Your OpenAI API key. If you use a restricted key, please ensure that it has permissions for /v1/audio."))] private string openAIKey = "";
    private readonly string outputFormat = "mp3";
    
    [System.Serializable]
    private class TTSPayload
    {
        public string model;
        public string input;
        public string voice;
        public string response_format;
        public float speed;
    }
    byte[] ttsByteArray;

    int ttsDataSuccess = -1;
    //

    public int getDataSuccess ()
    {
        int boolData = ttsDataSuccess;
        ttsDataSuccess = -1;
        return boolData;

    }
    public void setByteArray(byte[] ByteArray)
    {

        ttsByteArray = ByteArray;
        Debug.Log("setting byte array");
        
    }
    public byte[] getByteArray()
    {
        byte[] ttsBytes = ttsByteArray;
        ttsByteArray = Array.Empty<byte>();
        return ttsBytes;
    }





    //changing from task to variable
    public async void RequestTextToSpeech(string text, TTSModel model = TTSModel.TTS_1, TTSVoice voice = TTSVoice.Alloy, float speed = 1f)
    {
        UnityEngine.Debug.Log("Sending new request to OpenAI TTS.");

       



        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAIKey);

        TTSPayload payload = new TTSPayload
        {
            model = model.EnumToString(),
            input = text,
            voice = voice.ToString().ToLower(),
            response_format = this.outputFormat,
            speed = speed
        };

        //string jsonPayload = JsonUtility.ToJson(payload);

        //var httpResponse = await httpClient.PostAsync(
        //    "https://api.openai.com/v1/audio/speech", 
        //    new StringContent(jsonPayload, Encoding.UTF8, "application/json")
        //);

        //byte[] response = await httpResponse.Content.ReadAsByteArrayAsync();

        //using var www = UnityWebRequest.Post("https://api.openai.com/v1/audio/speech",
        //    jsonPayload, "application/json");


        //var operation = www.SendWebRequest();

        //while (!operation.isDone)
        //    await Task.Yield();

        ////if (httpResponse.IsSuccessStatusCode)
        //if(www.result == UnityWebRequest.Result.Success)
        //{
        //    setByteArray(www.downloadHandler.data );
        //    ttsDataSuccess = 1;
        //}
        //else
        //{ 
        //    UnityEngine.Debug.Log("Error: " + www.result);
        //     ttsDataSuccess = 0;

        //}

        string jsonPayload = JsonUtility.ToJson(payload);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonPayload);

        using (UnityWebRequest www = new UnityWebRequest("https://api.openai.com/v1/audio/speech", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonBytes);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + openAIKey);

            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result == UnityWebRequest.Result.Success)
            {
                setByteArray(www.downloadHandler.data);
                ttsDataSuccess = 1;
            }
            else
            {
                UnityEngine.Debug.LogError("Error: " + www.error);
                ttsDataSuccess = 0;
            }
        }



    }

    public void SetAPIKey(string openAIKey) => this.openAIKey = openAIKey;
}