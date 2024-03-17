# Simple PowerShell Provider
This is the simplest implementation of a PowerShell provider module I was able to come up with.

## Structure

The entry point is [`Provider`](./Provider/Provider.cs), the PowerShell module installed to make a new provider.

[`Drive`](./Provider/Drive.cs) contains singleton data for the drive.

[`Resource`](./Provider/Resource.cs) is a container for drive data.

[`Reader`](./Provider/Reader.cs) exposes resource contents.

## Running and debugging
[`launchSettings`](./Provider/Properties/launchSettings.json) has a profile that enables interactive debugging for the provider.

It imports the provider module and creates a new drive `s:` for it.

## Supported functionality

```powershell
PS C:\simple-powershell-provider\Provider\bin\Debug\netstandard2.0> s:
PS s:\http://example.com/> Get-ChildItem
http://example.com/c1/
http://example.com/c2/
PS s:\http://example.com/> Set-Location c1/
PS s:\http://example.com/c1/> ls
http://example.com/c1/c11/
PS s:\http://example.com/c1/> cd c11/
PS s:\http://example.com/c1/c11/> cd ..
PS s:\http://example.com/c1/> cd /
PS s:\http://example.com/> cd c2/
PS s:\http://example.com/c2/> ls
http://example.com/c2/r1
PS s:\http://example.com/c2/> Get-Content r1
abcdef
PS s:\http://example.com/c2/> Clear-Content r1
PS s:\http://example.com/c2/> cat r1
PS s:\http://example.com/c2/>
```