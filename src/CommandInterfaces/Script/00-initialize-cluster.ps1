Param(
    [parameter(Mandatory=$false)]
    [string]$subscriptionName="Microsoft Azure Sponsorship",
    [parameter(Mandatory=$false)]
    [string]$resourceGroupName="aksLogCornerGroup",
    [parameter(Mandatory=$false)]
    [string]$resourceGroupLocaltion="West Europe",
    [parameter(Mandatory=$false)]
    [string]$clusterName="aksLogCornerCluster",
    [parameter(Mandatory=$false)]
    [int16]$workerNodeCount=3,
    [parameter(Mandatory=$false)]
    [string]$kubernetesVersion="1.11.9"
 )

# Set Azure subscription name
Write-Host "Setting Azure subscription to $subscriptionName"  -ForegroundColor Yellow
az account set --subscription=$subscriptionName

# Create resource group name
Write-Host "Creating resource group $resourceGroupName in region $resourceGroupLocaltion" -ForegroundColor Yellow
az group create `
--name=$resourceGroupName `
--location=$resourceGroupLocaltion `
--output=jsonc

# Create AKS cluster
Write-Host "Creating AKS cluster $clusterName with resource group $resourceGroupName in region $resourceGroupLocaltion" -ForegroundColor Yellow
az aks create `
--resource-group=$resourceGroupName `
--name=$clusterName `
--node-count=$workerNodeCount `
--node-vm-size Standard_D2s_v3 `
--output=jsonc

# Get credentials for newly created cluster
Write-Host "Getting credentials for cluster $clusterName" -ForegroundColor Yellow
az aks get-credentials `
--resource-group=$resourceGroupName `
--name=$clusterName

Write-Host "Successfully created cluster $clusterName with kubernetes version $kubernetesVersion and $workerNodeCount node(s)" -ForegroundColor Green

$exit = read-host "Please press enter to exit"