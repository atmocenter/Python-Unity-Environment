using System;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public class TTSManager : MonoBehaviour
{
    private OpenAIWrapper openAIWrapper;
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private TTSModel model = TTSModel.TTS_1;
    [SerializeField] private TTSVoice voice = TTSVoice.Alloy;
    [SerializeField, Range(0.25f, 4.0f)] private float speed = 1f;
    
    private void OnEnable()
    {
        if (!openAIWrapper) this.openAIWrapper = FindObjectOfType<OpenAIWrapper>();
        if (!audioPlayer) this.audioPlayer = GetComponentInChildren<AudioPlayer>();
    }

    private void OnValidate() => OnEnable();

    
    public async void SynthesizeAndPlay(string text)
    {
        // Regex pattern to detect Python code blocks wrapped in triple backticks
        string pythonCodeBlockPattern = @"```[\s\S]*?```";

        // Check if the text contains a Python code block
        if (Regex.IsMatch(text, pythonCodeBlockPattern))
        {
            UnityEngine.Debug.Log("Text contains a Python code block, skipping TTS: " + text);
            return;
        }

        UnityEngine.Debug.Log("Trying to synthesize " + text);
        //byte[] audioData = await openAIWrapper.RequestTextToSpeech(text, model, voice, speed);
       
        //inumerator //
        //byte[] audioData = Array.Empty<byte>();
        
        //    openAIWrapper.RequestTextToSpeech(text, model, voice, speed);
        //    audioData = openAIWrapper.getByteArray();

        StartCoroutine(GetAudioData(text));


            Debug.Log("after coroutine");
        //if (audioData != null)
        //{
        //    UnityEngine.Debug.Log("Playing audio.");
        //    audioPlayer.ProcessAudioBytes(audioData);
        //}
        //else
        //{
        //    UnityEngine.Debug.LogError("Failed to get audio data from OpenAI.");
        //}
    }

    IEnumerator GetAudioData(string text)
    {
        byte[] audioData = Array.Empty<byte>();
        int dataSuccess = -1;
        openAIWrapper.RequestTextToSpeech(text, model, voice, speed);
        while (dataSuccess == -1)
        {
            
            
            dataSuccess = openAIWrapper.getDataSuccess();
            yield return null;
        }
        if (dataSuccess == 1)
        {
            audioData = openAIWrapper.getByteArray();
            UnityEngine.Debug.Log("Playing audio.");
            audioPlayer.ProcessAudioBytes(audioData);
        }
        else
        {
            UnityEngine.Debug.LogError("Failed to get audio data from OpenAI.");
        }
        Debug.Log("end of Coroutine");
    }

    public void SynthesizeAndPlay(string text, TTSModel model, TTSVoice voice, float speed)
    {
        this.model = model;
        this.voice = voice;
        this.speed = speed;
        SynthesizeAndPlay(text);
    }
}