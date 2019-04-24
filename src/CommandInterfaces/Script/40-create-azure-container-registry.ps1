Param(
    [parameter(Mandatory=$false)]
    [string]$resourceGroupName="aksLogCornerGroup",
    [parameter(Mandatory=$false)]
    [string]$registryName="LogCornerEduSync"
 )

# Start creating azure container registry
Write-Host "Start creating azure container registry $registryName"  -ForegroundColor Yellow
az acr create --resource-group $resourceGroupName  --name $registryName --sku Standard --location westeurope
Write-Host "Successfully created azure container registry $registryName" -ForegroundColor Green

$exit = read-host "Please press enter to exit"



