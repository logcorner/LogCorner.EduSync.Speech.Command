$terraformOutput = "C:\Users\tocan\source\repos\MICROSERVICES\LogCorner.EduSync.Speech.Command\deploy\iac\terraform\powershell\output-a84c5e64-fc55-4bc3-ab1b-eb32c2f5c1c7.json"

$output = Get-Content $($terraformOutput ) | ConvertFrom-Json
Write-Host  $output

$container_registry_name =  $output.container_registry_name.value
$kubernetes_cluster_name =  $output.kubernetes_cluster_name.value

Write-Host "kubernetes_cluster_name ="   $container_registry_name
Write-Host  " kubernetes_cluster_name  =" $kubernetes_cluster_name