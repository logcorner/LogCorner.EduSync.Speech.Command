
# deploy kubernetes files to cluster
Write-Host "Starting switch to location of app files" -ForegroundColor Yellow
Set-Location ~/Source/Repos/LogCorner.EduSync/src/CommandInterfaces/Kubernetes/app/dev
Write-Host "Successfully switching to app setup files" -ForegroundColor Green

Write-Host "Starting deployment of setup files" -ForegroundColor Yellow
kubectl apply --recursive --filename . 
Write-Host "Successfully deployed setup files" -ForegroundColor Green

$exit = read-host "Please press enter to exit"