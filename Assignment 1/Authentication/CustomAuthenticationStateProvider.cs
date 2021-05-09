﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment_1.Data;
using Assignment_1.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Assignment_1.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
    private readonly IJSRuntime jsRuntime;
    private readonly IUserService userService;

    private User cachedUser;

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService userService) {
        this.jsRuntime = jsRuntime;
        this.userService = userService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        var identity = new ClaimsIdentity();
        if (cachedUser == null) {
            string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            if (!string.IsNullOrEmpty(userAsJson)) {
                User tmp = JsonSerializer.Deserialize<User>(userAsJson);
                ValidateLoginAsync(tmp.UserName, tmp.Password);
            }
        } else {
            identity = SetupClaimsForUser(cachedUser);
        }

        ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
        return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
        
    }

    public async Task ValidateLoginAsync(string username, string password) {
        Console.WriteLine("Validating log in");
        if (string.IsNullOrEmpty(username)) throw new Exception("Enter username");
        if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");

        ClaimsIdentity identity = new ClaimsIdentity();
        try {
            User user = await userService.ValidateUserAsync(username, password);
            identity = SetupClaimsForUser(user);
            string serialisedData = JsonSerializer.Serialize(user);
            await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
            cachedUser = user;
        } catch (Exception e) {
            throw e;
        }

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
    }

    public async Task ValidateRegister(string username, string password, string confirmPassword)
    {
        Console.WriteLine("Validating registering");
        Console.WriteLine(username);
        Console.WriteLine(password);
        if (string.IsNullOrEmpty(username)) throw new Exception("Enter username");
        if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");
        if (string.IsNullOrEmpty(confirmPassword)) throw new Exception("Enter password confirmation");
        
        ClaimsIdentity identity = new ClaimsIdentity();

        Task<User> user = userService.RegisterUserAsync(username, password, confirmPassword);
        await ValidateLoginAsync(user.Result.UserName, user.Result.Password);
    }

    public void Logout() {
        cachedUser = null;
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private ClaimsIdentity SetupClaimsForUser(User user) {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        claims.Add(new Claim("Role", user.Role));
        claims.Add(new Claim("SecurityLevel", user.SecurityLevel.ToString()));

        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
        return identity;
    }
}
}