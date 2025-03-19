using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Net;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private const bool deleteCachedFile = true;

    private void OnEnable()
    {
        if (!audioSource) this.audioSource = GetComponent<AudioSource>();
    }

    private void OnValidate() => OnEnable();

    public void ProcessAudioBytes(byte[] audioData)
    {
        //android ... builds
        //string filePath = Path.Combine(Application.persistentDataPath, "audio.mp3");
        //File.WriteAllBytes(filePath, audioData);
        
        //StartCoroutine(LoadAndPlayAudio(filePath));

        // for webgl builds
        AudioClip x = ToAudioClip(audioData);
        if (x != null)
        {
            audioSource.clip = x;
            audioSource.Play();
        }
    }
    
    private IEnumerator LoadAndPlayAudio(string filePath)
    {
        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.MPEG);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
            
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else UnityEngine.Debug.LogError("Audio file loading error: " + www.error);
        
        if (deleteCachedFile) File.Delete(filePath);
    }

    private  AudioClip ToAudioClip(byte[] wav)
    {
        int channels = BitConverter.ToInt16(wav, 22); // Channels (stereo or mono)
        int sampleRate = BitConverter.ToInt32(wav, 24); // Sample rate
        int dataStart = BitConverter.ToInt32(wav, 20) + 36; // Start of audio data

        int dataSize = BitConverter.ToInt32(wav, 40); // Size of audio data
        int numSamples = dataSize / (channels * 2); // Number of samples (16-bit PCM)

        float[] audioData = new float[numSamples];

        // Read audio data (16-bit PCM)
        for (int i = 0; i < numSamples; i++)
        {
            short sample = BitConverter.ToInt16(wav, dataStart + i * 2);
            audioData[i] = sample / 32768.0f; // Convert to float in the range of [-1.0, 1.0]
        }

        // Create and return the AudioClip
        AudioClip clip = AudioClip.Create("audioClip", numSamples, channels, sampleRate, false);
        clip.SetData(audioData, 0);
        return clip;
    }
}