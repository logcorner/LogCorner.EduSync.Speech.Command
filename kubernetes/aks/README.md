# LogCorner.EduSync
Building microservices through Event Driven Architecture

# to setup Azure Container Registry:
$resourceGroupName ="LOGCORNER.EDUSYNC.SPEECH.RG"
$resourceGroupLocation ="westeurope"
$servicePrincipalName ="deploy-aks-with-github-action"
$azureContainerRegistryName ="logcorneredusyncregistry"
$aksName="LogCornerEduSyncSpeechCluster"
$subscriptionId="subscriptionId"

az login

az account set --subscription $subscriptionId

# create resource group
az group create --name $resourceGroupName --location $resourceGroupLocation
$resourceGroupId =$(az group show --name $resourceGroupName --query id --output tsv)

kubectl get pods
 kubectl get services

# create a service principal
$servicePrincipal = az ad sp create-for-rbac --name $servicePrincipalName --scopes $resourceGroupId --role Contributor --sdk-auth

#copy output to a text file for use later

$servicePrincipalPassword ="L678Q~7O9t1iad48PQKpau83lftxinDx0lyc9bxq"
$servicePrincipalSecret= $(az ad sp list --display-name $servicePrincipalName --query "[].clientSecret" --output tsv)
                 

$servicePrincipalId= $(az ad sp list --display-name $servicePrincipalName --query "[].appId" --output tsv)

# create azure container registry
az acr create --resource-group $resourceGroupName --name $azureContainerRegistryName --sku Basic
$azureContainerRegistryId=$(az acr show --name $azureContainerRegistryName --resource-group $resourceGroupName --query id --output tsv)

az role assignment create --assignee $servicePrincipalId --scope $azureContainerRegistryId --role AcrPush

# to setup Azure Kubernetes Service cluster

generate a certificate and a key

az role assignment create --assignee $servicePrincipalId --scope $azureContainerRegistryId --role AcrPull

az aks create --resource-group $resourceGroupName --name $aksName --node-count 2 --generate-ssh-keys --attach-acr $azureContainerRegistryName --service-principal $servicePrincipalId --client-secret $servicePrincipalPassword

# Get the kubeconfig to log into the cluster
az aks get-credentials  --resource-group $resourceGroupName   --name $aksName


az login
az acr login --name $azureContainerRegistryName

# build
docker-compose build

# tag
docker tag logcornerhub/logcorner-edusync-speech-command  "$azureContainerRegistryName.azurecr.io/logcorner-edusync-speech-command"

docker tag logcornerhub/logcorner-edusync-speech-mssql-tools  "$azureContainerRegistryName.azurecr.io/logcorner-edusync-speech-mssql-tools"

# push 
docker push "$azureContainerRegistryName.azurecr.io/logcorner-edusync-speech-command"

docker push "$azureContainerRegistryName.azurecr.io/logcorner-edusync-speech-mssql-tools"

kubectl apply -f aks-helloworld-one.yaml --namespace ingress-basic
kubectl apply -f aks-helloworld-two.yaml --namespace ingress-basic
kubectl apply -f hello-world-ingress.yaml --namespace ingress-basic


http://www.ingress-nginx.cloud-devops-craft.com

https://www.ingress-nginx.cloud-devops-craft.com


http://20.126.213.221/speech-command-http-api/swagger/index.html