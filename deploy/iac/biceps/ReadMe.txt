https://docs.microsoft.com/en-us/learn/modules/build-first-bicep-template/4-exercise-define-resources-bicep-template?pivots=cli

az account set --subscription {your subscription ID}

az group create -l westeurope -n aks
az configure --defaults group=aks
az deployment group create --template-file main.bicep
az deployment group create --template-file main.bicep --parameters location=westeurope
az deployment group create --template-file main.bicep --parameters location=westeurope environmentName=Production



sqlServerAdministratorLogin = logcorner
sqlServerAdministratorLoginPassword = LogCornerBiceps1#

az acr login --name acrlogcornerregistry

# prepare and push images to azure container registry
docker tag logcornerhub/logcorner-edusync-speech-mssql-tools acrlogcornerregistry.azurecr.io/logcorner-edusync-speech-mssql-tools
docker push acrlogcornerregistry.azurecr.io/logcorner-edusync-speech-mssql-tools

docker tag logcornerhub/logcorner-edusync-speech-command  acrlogcornerregistry.azurecr.io/logcorner-edusync-speech-command
docker push acrlogcornerregistry.azurecr.io/logcorner-edusync-speech-command


# prepare and deploy to aks

az aks get-credentials --resource-group aks --name akslogcornercluster

kubectl config get-contexts 
kubectl config use-context  akslogcornercluster 

kubectl apply -f . -f CommandDatabase  -f CommandApi
kubectl rollout restart deployment speech-command-http-api-deployment -n aks
kubectl logs speech-command-http-api-deployment-8d494c44c-j7zx6 -n aks

kubectl apply -f .
kubectl delete -f CommandDatabase
kubectl delete -f CommandApi

kubectl get pods -n aks

http://20.93.199.187/swagger/index.html