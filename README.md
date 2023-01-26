# Purpose

This repository contains a Bicep template to setup:
- 1 App Service Linux,
- Azure Front Door in front
- The url will be rewritten: https://xxx/api/hello-world -> https://xxx/something-else/api/hello-world

There is also a very basic API backend to host the "hello-world" endpoint.

# Deploy the infrastructure

```powershell
$subscription = "Training Subscription"

az login
az account set --subscription $subscription

$rgName = "frbar-fd-rr"
$envName = "fb001"
$location = "West Europe"

# Deploy the infrastructure

az group create --name $rgName --location $location
az deployment group create --resource-group $rgName --template-file infra.bicep --mode complete --parameters envName=$envName

# Build and Deploy the API backend

dotnet publish .\api\ -r linux-x64 --self-contained -o publish
Compress-Archive publish\* publish.zip -Force
az webapp deployment source config-zip --src .\publish.zip -n "$($envName)-api-0" -g $rgName

# Test via AFD
$hostname = az afd endpoint list -g $rgName --profile-name "$($envName)-afd" --query [0].hostName -otsv

# should say Hello from HelloWorldWithOtherUrlController!
(curl "https://$($hostname)/api/hello-world" -UseBasicParsing).Content

Remove-Item publish -Recurse
Remove-Item publish.zip

```

# Tear down

```powershell
az group delete --name $rgName
```

