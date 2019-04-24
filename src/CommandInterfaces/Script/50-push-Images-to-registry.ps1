Param(
    [parameter(Mandatory=$false)]
    [string]$subscriptionName="Microsoft Azure Sponsorship",
    [parameter(Mandatory=$false)]
    [string]$DataBaseImageName="logcorner-edusync-speech-presentation-data",
    [parameter(Mandatory=$false)]
	
	[string]$ApiImageName="logcorner-edusync-speech-presentation",
    [parameter(Mandatory=$false)]
    [string]$dockerServer="logcorneredusync.azurecr.io",
  
    [parameter(Mandatory=$false)]
    [bool]$stop=$true
    
)

# Set Azure subscription name
Write-Host "Setting Azure subscription to $subscriptionName"  -ForegroundColor Yellow
az account set --subscription=$subscriptionName

# login to regitry
Write-Host "login to regitry" -ForegroundColor Yellow

docker login $dockerServer -u LogCornerEduSync -p guRYbusAKNFfDQCbTKaEjThN=Eyqt4Jl
# Tag and push $ApiImageName
Write-Host "Tag and push $ApiImageName" -ForegroundColor Yellow
docker tag $ApiImageName $dockerServer/$ApiImageName
docker push $dockerServer/$ApiImageName

# Tag and push $DataBaseImageName
Write-Host "Tag and push $DataBaseImageName" -ForegroundColor Yellow
docker tag $DataBaseImageName $dockerServer/$DataBaseImageName
docker push $dockerServer/$DataBaseImageName

if($stop)
{
    $exit = read-host "Please press enter to exit" 
}

