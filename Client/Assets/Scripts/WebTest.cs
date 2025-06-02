using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core;
using Kwicrypt.Module.Cryptography;
using Kwicrypt.Module.Cryptography.Interfaces;
using Kwicrypt.Module.Cryptography.Models;
using Kwicrypt.Module.Cryptography.Services;
using Kwicrypt.Module.Dto;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WebTest : MonoBehaviour
{
    public TMP_InputField mailInputField;
    public TMP_InputField passwordInputField;

    private string baseUrl = "http://localhost:5175/api";
    
    private string RsaKeyUrl => $"{baseUrl}/auth/rsa";
    private string RegisterUrl => $"{baseUrl}/auth/register";
    private string LoginUrl => $"{baseUrl}/auth/login";

    private string _serverRsaKey;
    
    private string _accessToken;
    private string _refreshToken;
    
    private ICryptoService _cryptoService;
    private IWebService _webService;
    private IAuthService _authService;
    
    void Start()
    {
        _cryptoService = new CryptoService();
        _webService = new WebService(baseUrl, _cryptoService);
        _authService = new AuthService(_cryptoService, _webService);
    }

    public async void Login()
    {
        var loginRequest = await _authService.LoginUsingMailAsync(mailInputField.text, passwordInputField.text);
        if (loginRequest)
        {
            Debug.Log("Success login");
        }
        else
        {
            Debug.Log("Fail login");
        }
    }

    public async void Register()
    {
        var registerResult = await _authService.RegisterUsingMailAsync(mailInputField.text, passwordInputField.text);
        if (registerResult)
        {
            Debug.Log("Success register");
        }
        else
        {
            Debug.Log("Fail register");
        }
    }
}
