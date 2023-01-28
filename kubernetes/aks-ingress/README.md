# LogCorner.EduSync
Building microservices through Event Driven Architecture

# to setup Azure Container Registry:
$resourceGroupName ="rg-aks-ingress-demo"
$resourceGroupLocation ="westeurope"
$servicePrincipalName ="deploy-aks-with-github-action"
$azureContainerRegistryName ="acraksingressdemo"
$aksName="aks-ingress-cluster"

# create resource group
az group create --name $resourceGroupName --location $resourceGroupLocation
$resourceGroupId =$(az group show --name $resourceGroupName --query id --output tsv)


# create a service principal
az ad sp create-for-rbac --name $servicePrincipalName --scopes $resourceGroupId --role Contributor --sdk-auth

#copy output to a text file for use later

$servicePrincipalPassword ="prnSVQ8sSgWuMu7Mnu-~en-v3XjyYz3Uys"

$servicePrincipalId= $(az ad sp list --display-name $servicePrincipalName --query "[].appId" --output tsv)

# create azure container registry
az acr create --resource-group $resourceGroupName --name $azureContainerRegistryName --sku Basic
$azureContainerRegistryId=$(az acr show --name $azureContainerRegistryName --resource-group $resourceGroupName --query id --output tsv)

az role assignment create --assignee $servicePrincipalId --scope $azureContainerRegistryId --role AcrPush


# to setup Azure Kubernetes Service cluster


az role assignment create --assignee $servicePrincipalId --scope $azureContainerRegistryId --role AcrPull

az aks create --resource-group $resourceGroupName --name $aksName --node-count 2 --generate-ssh-keys --attach-acr $azureContainerRegistryName --service-principal $servicePrincipalId --client-secret $servicePrincipalPassword

