Param(
    [parameter(Mandatory=$false)]
    [string]$resourceGroupName="aksLogCornerGroup",
    [parameter(Mandatory=$false)]
    [string]$clusterName="aksLogCornerCluster"
 )

Write-Host "Starting switch to location of setup files" -ForegroundColor Yellow
Set-Location ~/Source/Repos/LogCorner.EduSync/src/CommandInterfaces/Kubernetes/setup
Write-Host "Successfully switching to location setup files" -ForegroundColor Green

Write-Host "Starting deployment of setup files" -ForegroundColor Yellow
kubectl apply --recursive --filename . 
Write-Host "Successfully deployed setup files" -ForegroundColor Green

$exit = read-host "Please press enter to exit"