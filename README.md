# Kexla

## Getting Started 

Kexla is avialbe via [NuGet](https://www.nuget.org/packages/Ofir.Kexla). 

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
