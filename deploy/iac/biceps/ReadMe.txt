https://docs.microsoft.com/en-us/learn/modules/build-first-bicep-template/4-exercise-define-resources-bicep-template?pivots=cli

az account set --subscription {your subscription ID}

az group create -l westeurope -n aks
az configure --defaults group=aks
az deployment group create --template-file main.bicep
az deployment group create --template-file main.bicep --parameters location=westeurope
az deployment group create --template-file main.bicep --parameters location=westeurope environmentName=Production



sqlServerAdministratorLogin = logcorner
sqlServerAdministratorLoginPassword = LogCornerBiceps1#