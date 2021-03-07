using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Guanomancer.WebApi
{
    public class WebApiBehaviour : MonoBehaviour
    {
        [SerializeField]
        private string DefaultURL;

        [SerializeField]
        private UnityEvent<string> OnError;

        public void Get<T>(Action<T> andThen, string url = null)
        {
            StartCoroutine(CoGet(andThen, url));
        }

        public void Post<T>(object model, Action<T> andThen, string url = null)
        {
            StartCoroutine(CoPost<T>(model, andThen, url));
        }

        private IEnumerator CoGet<T>(Action<T> andThen, string url = null)
        {
            if (url == null) url = DefaultURL;
            var request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                OnError.Invoke(request.error);
                yield break;
            }

            var json = request.downloadHandler.text;
            var result = JsonUtility.FromJson<T>(json);
            andThen(result);
        }

        private IEnumerator CoPost<T>(object model, Action<T> andThen, string url = null)
        {
            if (url == null) url = DefaultURL;
            var request = UnityWebRequest.Post(url, "");
            request.SetRequestHeader("content-type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(model)));
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                OnError.Invoke(request.error);
                yield break;
            }

            var json = request.downloadHandler.text;
            var result = JsonUtility.FromJson<T>(json);
            andThen(result);
        }
    }
}
