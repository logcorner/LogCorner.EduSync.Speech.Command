#
terraform plan -var-file="dev.tfvars"

terraform apply -var-file="dev.tfvars" --auto-approve

$resourceGroupName ="LOGCORNER.EDUSYNC.SPEECH.RG"
$azureContainerRegistryName ="logcorneredusyncregistry"
$aksName="LogCornerEduSyncSpeechCluster"
$subscriptionId="subscriptionId"

az login

az account set --subscription $subscriptionId
az acr login --name $azureContainerRegistryName

# Get the kubeconfig to log into the cluster
az aks get-credentials  --resource-group $resourceGroupName   --name $aksName

# build
docker-compose build

# tag
docker tag logcornerhub/logcorner-edusync-speech-command  "$azureContainerRegistryName.azurecr.io/logcorner-edusync-speech-command:1.0.0"

# push 
docker push "$azureContainerRegistryName.azurecr.io/logcorner-edusync-speech-command:3.0.0"

kubectl apply -f aks-helloworld-one.yaml --namespace ingress-basic

kubectl get pods
kubectl get services


http://20.4.161.165/swagger/index.html

{
  "title": "this is a title",
  "description": "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
  "url": "http://test.com",
  "typeId": 1
}
