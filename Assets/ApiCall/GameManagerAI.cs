using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public class GameManagerAI : MonoBehaviour
{
    public string username;
    public int maxMessages = 25;
    public GameObject chatPanel, textObj;
    public TMP_InputField chatBox;
    public Color playerMessage, info, botMessage;
    private bool LewisIntro = true;
    public GameObject UI_AI;
    [SerializeField] LewisInteraction LewInteractionScript;
    [SerializeField]
    List<Message> messageList = new List<Message>();

    public TTSManager ttsManager; // Reference to the TTSManager
    public OpenAIWrapper openAIWrapper;

    [SerializeField] private string apiKey = "";
    private static readonly HttpClient client = new HttpClient();

    string gptResponseMsg = "";



    void Start()
    {

    }

    void Update()
    {
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(username + ": " + chatBox.text, Message.MessageType.playerMessage);
                // Call the method directly since it returns a Task<string>
                SendMessageToChatGPT(chatBox.text); // Fire and forget
                chatBox.text = "";
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }

        if (!chatBox.isFocused)
        {
            if (LewisIntro == true)
            {
                SendMessageToChat("Lewis: Hello my name is Lewis, your virtual assistant. I'm here to answer any question you have to the best of my ability. Now how may I help you today?", Message.MessageType.botMessage);
                LewisIntro = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendMessageToChat("You pressed space bar", Message.MessageType.info);
                UnityEngine.Debug.Log("Space");
            }
        }
    }


    public void MessageSendClicked()
    {
        if (chatBox.text != "")
        {
            SendMessageToChat(username + ": " + chatBox.text, Message.MessageType.playerMessage);
            SendMessageToChatGPT(chatBox.text);
        }
        chatBox.text = "";
    }

    public void toggleUI()
    {
        if(UI_AI.activeSelf == true)
        {
            UI_AI.SetActive(false);
            LewInteractionScript.AIUIExit();
        }
        
    }
    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObj.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = text;

        GameObject newText = Instantiate(textObj, chatPanel.transform);
        newMessage.textObj = newText.GetComponent<TextMeshProUGUI>();
        newMessage.textObj.text = newMessage.text;
        newMessage.textObj.color = MessageTypeColor(messageType);

        messageList.Add(newMessage);
    }

    Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color = info;

        switch (messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;
            case Message.MessageType.info:
                color = info;
                break;
            case Message.MessageType.botMessage:
                color = botMessage;
                break;
        }

        return color;
    }


    //changing from Task<string>
    private async void SendMessageToChatGPT(string message)
    {
        string endpoint = "https://api.openai.com/v1/chat/completions";
        Debug.Log("sending msg to gpt");
        var payload = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = message }
            }
        };
        JArray messages = new JArray
            { 
            new JObject
            {
                ["role"] = "user",
                ["content"] = message
            }
            
        };
        
        JObject payloadjson = new JObject
        {
            { "model", "gpt-3.5-turbo" },
            {"messages", messages }
            

        };

        //Debug.Log(payloadjson.ToString());
        //Debug.Log(payloadjson["model"].ToString());
        //Debug.Log(payloadjson["messages"].ToString());
        Debug.Log("after payload created");
        //var jsonPayload = JsonUtility.ToJson(payloadjson);
        Debug.Log("after payload serialized");
        byte[] jsonBytes = Encoding.UTF8.GetBytes(payloadjson.ToString());
        Debug.Log("after payload encoding");
        //var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        using (UnityWebRequest www = new UnityWebRequest(endpoint, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonBytes);
            www.downloadHandler = new DownloadHandlerBuffer();
    
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + apiKey);

            Debug.Log("Before web request"+ www.ToString());
            
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();
            Debug.Log("after requestr has executed and recieved response");

            if (www.result == UnityWebRequest.Result.Success)
            {

                Debug.Log("successful result");
                JObject respObject = JObject.Parse(www.downloadHandler.text);
                //Debug.Log(respObject.ToString());
                string botResponse = respObject["choices"][0]["message"]["content"].ToString();
                Debug.Log(botResponse);
                SendMessageToChat("Lewis: " + botResponse, Message.MessageType.botMessage);
                // Use TTSManager to convert the response to speech

                //doesnt work for webgl build
                //ttsManager.SynthesizeAndPlay(botResponse);

                // Return the bot response for further use if needed
                gptResponseMsg = botResponse.ToString();
            }
            else
            {
                Debug.Log("error result ");
                 //Debug.Log("Error: " + www.error);

                SendMessageToChat("ChatGPT: Error retrieving response.", Message.MessageType.botMessage);

            }
        }


        
    }
}


[System.Serializable]
public class Message
{
    public string text;
    public TextMeshProUGUI textObj;
    public MessageType messageType;

    public enum MessageType
    {
        playerMessage,
        info,
        botMessage
    }
}