using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guanomancer.WebApi;

[RequireComponent(typeof(WebApiBehaviour))]
public class WebApiTestBehaviour : MonoBehaviour
{
    private WebApiBehaviour _webApi;

    private void Awake()
    {
        _webApi = GetComponent<WebApiBehaviour>();
    }

    private void Start()
    {
        _webApi.Get<TestModel>((model) =>
        {
            Debug.Log($"Model identifier: {model.Identifier}");
        });

        var model = new TestModel { Identifier = 50 };
        _webApi.Post<TestModel>(model, (model) =>
        {
            Debug.Log($"Stored model identifier: {model.Identifier}");
        }, "https://localhost:44347/Test/Store");

        model = new TestModel { Identifier = 50 };
        _webApi.Post<TestModel>(model, (model) =>
        {
            Debug.Log($"Validated model identifier: {model.Identifier}");
        }, "https://localhost:44347/Test/Validate");
    }
}
