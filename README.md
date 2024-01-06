# Redis Cache Common Repository Package

## Table of Contents
1. [Introduction](#introduction)
2. [Getting Started](#getting-started)
3. [List of Methods](#list-of-methods)
4. [Links](#links)

## Introduction
***
This package provides an easy and quick way to get started working with distributed caching using Redis.

## Getting Started
***
To get started with this library, download the package (```RedisCache.Common.Repository```) from Nuget using your preferred method.
After successful download, in the ```Program.cs``` or ```Startup.cs``` (as the case may be), add these lines to the service collections.
```
builder.Services.ConfigureRedis("localhost:6379")
    .ConfigureCacheRepository();
```
You can then inject the ```ICacheCommonRepository``` interface into the class you wish to use the methods.

## List of Methods
***
* ```T Get<T>(string key);```
* ```bool Set<T>(string key, T value, DateTimeOffset expires);```
* ```object Remove(string key);```
* ```Task<T> GetAsync<T>(string key);```
* ```Task<object> RemoveAsync(string key);```
* ```Task<bool> SetAsync<T>(string key, T value, DateTimeOffset expires);```
* ```Task<bool> KeyExistsAsync(string key);```
* ```bool KeyExists(string key);```

## Links
***
To view the source code or get in touch:
* [Github Repository Link](https://github.com/ojotobar/RedisCache.Common.Repository)
* [Send Me A Mail](mailto:ojotobar@gmail.com)