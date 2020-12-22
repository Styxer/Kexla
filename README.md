# Kexla

## Getting Started 
![Nuget](https://img.shields.io/nuget/v/Ofir.Kexla)
![Nuget](https://img.shields.io/nuget/dt/Ofir.Kexla)

Kexla is avialbe via [NuGet](https://www.nuget.org/packages/Ofir.Kexla). 

## Whats new 0.5.5
added an option to directly use a string query at ```C# WMISearcher.Query```

for example

```C# 
var networkQuery = CimV2searcher.Query<NetworkAdapter>("SELECT * FROM Win32_NetworkAdapter");
```

## Whats new 0.5
Added new constructors that enables running queries on remore computers
you can now use any combination of 

hostname | Authentication Level | domain | user name | password

to create queries on remote computers

for example

```C# 
var CimV2searcher = new WMISearcher(scope: "root\\CimV2", hostname: "W2019SRV-DEV", username: "Admin", password: "pass123");
```


## How to Use

###### A - Query The Data

1. Add the packge to your project via NuGet package manager

2. Use the libary
```C# 
using Kexla;
```
3. Define your own class based on the property(s) that you need
```C#
[WMIClass(name: "Win32_NetworkAdapter")]
public class NetworkAdapter
{
    [WMIProps(name: "MACAddress")]
    public string MACAddress { get; set; }

    public string Caption { get; set; }

    public override string ToString()
    {
        return "MACAddress " + MACAddress
            + " Caption" + Caption;
    }

}
```

4.Query The Data
```C#
WMISearcher searcher = new WMISearcher(WMIClassTypes.CimV2);
var networkAdapters = searcher.Query<NetworkAdapter>();
```
