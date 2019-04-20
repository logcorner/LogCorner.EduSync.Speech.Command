Param(
    [parameter(Mandatory=$false)]
    [string]$resourceGroupName="aksLogCornerGroup",
    [parameter(Mandatory=$false)]
    [string]$clusterName="aksLogCornerCluster"
 )

# Start the Kubernetes dashboard
Write-Host "Starting the Kubernetes dashboard $clusterName"  -ForegroundColor Yellow
az aks browse --resource-group $resourceGroupName --name $clusterName
Write-Host "Successfully Started the Kubernetes dashboard $clusterName" -ForegroundColor Green

$exit = read-host "Please press enter to exit"