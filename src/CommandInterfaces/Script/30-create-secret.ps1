Param(
    [parameter(Mandatory=$false)]
    [string]$subscriptionName="Microsoft Azure Sponsorship",
    [parameter(Mandatory=$false)]
    [string]$secretName="registrysecret",
    [parameter(Mandatory=$false)]
    [string]$dockerServer="logcorneredusync.azurecr.io",
    [parameter(Mandatory=$false)]
    [string]$clusterName="aksLogCornerCluster",
    [parameter(Mandatory=$false)]
    [string]$dockerUsername="LogCornerEduSync",
    [parameter(Mandatory=$false)]
    [string]$dockerPassword="guRYbusAKNFfDQCbTKaEjThN=Eyqt4Jl",
	[parameter(Mandatory=$false)]
    [string]$dockerEmail="admin@azurecr.io",
    [parameter(Mandatory=$false)]
    [string]$namespace="dev",
    [parameter(Mandatory=$false)]
    [bool]$stop=$true
    
)

# Set Azure subscription name
Write-Host "Setting Azure subscription to $subscriptionName"  -ForegroundColor Yellow
az account set --subscription=$subscriptionName

# Get credentials for newly created cluster
Write-Host "Getting credentials for cluster $clusterName" -ForegroundColor Yellow
kubectl create secret docker-registry $secretName `
--docker-server=$dockerServer `
--docker-username=$dockerUsername `
--docker-password=$dockerPassword `
--docker-email=$dockerEmail `
--namespace $namespace

if($stop)
{
    $exit = read-host "Please press enter to exit" 
}

$exit = read-host "Please press enter to exit"