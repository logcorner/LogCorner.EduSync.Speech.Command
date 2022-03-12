terraform init -reconfigure
terraform init
terraform plan -out tfplan
terraform show -json tfplan >> tfplan.json
terraform apply  tfplan




# prepare and push images to azure container registry
az acr login --name locornermsacrtest
docker tag logcornerhub/logcorner-edusync-speech-mssql-tools locornermsacrtest.azurecr.io/logcorner-edusync-speech-mssql-tools
docker push locornermsacrtest.azurecr.io/logcorner-edusync-speech-mssql-tools

docker tag logcornerhub/logcorner-edusync-speech-command  locornermsacrtest.azurecr.io/logcorner-edusync-speech-command
docker push locornermsacrtest.azurecr.io/logcorner-edusync-speech-command


# prepare and deploy to aks

az aks get-credentials --resource-group aks --name akslogcornercluster

kubectl config get-contexts 
kubectl config use-context  akslogcornercluster 

kubectl apply -f . -f CommandDatabase  -f CommandApi
kubectl rollout restart deployment speech-command-http-api-deployment -n aks
kubectl logs speech-command-http-api-deployment-8d494c44c-j7zx6 -n aks




 terraform workspace new test
 terraform workspace select test
 terraform init -var-file="test/test.tfvars"
 terraform validate -var-file="dev/dev.tfvars"
 terraform plan -var-file="test/test.tfvars"
 terraform apply -var-file="test/test.tfvars"


 terraform workspace new dev
 terraform workspace select dev
 terraform init -var-file="dev/dev.tfvars"
 terraform validate -var-file="dev/dev.tfvars"
 terraform plan -var-file="dev/dev.tfvars"
 terraform apply -var-file="dev/dev.tfvars"

https://conferenceapi.azurewebsites.net/?format=json

PUT
https://management.azure.com/subscriptions/023b2039-5c23-44b8-844e-c002f8ed431d/resourceGroups/demo-apim-test/providers/Microsoft.ApiManagement/service/{apimServiceName}?api-version=2021-08-01

$url= 'https://management.azure.com/subscriptions/023b2039-5c23-44b8-844e-c002f8ed431d/providers/Microsoft.ApiManagement/locations/westeurope/deletedservices/logcorner-apim-speech?api-version=2021-08-01-preview'


Invoke-RestMethod -Method Delete -Uri $url



$agicResourceGroup ='demo-agic-test'
$location ='westeurope'
$publicIp ='agicPublicIp'
$agicName ='demo-agic-aks-test'
$clusterName ='demo-apim-aks-test'
$clusterResourceGroup ='demo-apim-test'
$agicVnetName ='agic-aks-vnet'
$aksVnetName ='apim-aks-vnet'
$agicSubnetName ='agic-aks-subnet'

# Create a resource group
az group create --name $agicResourceGroup --location $location

# Deploy a new Application Gateway

az network public-ip create -n $publicIp -g $agicResourceGroup --allocation-method Static --sku Standard
az network vnet create -n $agicVnetName -g $agicResourceGroup --address-prefix 11.0.0.0/8 --subnet-name mySubnet --subnet-prefix 11.1.0.0/16 
az network application-gateway create -n myApplicationGateway -l $location -g $agicResourceGroup --sku Standard_v2 --public-ip-address $publicIp --vnet-name $agicVnetName --subnet mySubnet

# Enable the AGIC add-on in existing AKS cluster through Azure CLI
appgwId=$(az network application-gateway show -n myApplicationGateway -g $agicResourceGroup -o tsv --query "id") 
az aks enable-addons -n myCluster -g $agicResourceGroup -a ingress-appgw --appgw-id $appgwId


nodeResourceGroup=$(az aks show -n myCluster -g $agicResourceGroup -o tsv --query "nodeResourceGroup")
aksVnetName=$(az network vnet list -g $nodeResourceGroup -o tsv --query "[0].name")

aksVnetId=$(az network vnet show -n $aksVnetName -g $nodeResourceGroup -o tsv --query "id")
az network vnet peering create -n AppGWtoAKSVnetPeering -g $agicResourceGroup --vnet-name $agicVnetName --remote-vnet $aksVnetId --allow-vnet-access

appGWVnetId=$(az network vnet show -n $agicVnetName -g $agicResourceGroup -o tsv --query "id")
az network vnet peering create -n AKStoAppGWVnetPeering -g $nodeResourceGroup --vnet-name $aksVnetName --remote-vnet $appGWVnetId --allow-vnet-access

az aks get-credentials -n $clusterName -g $clusterResourceGroup 





<!--
    IMPORTANT:
    - Policy elements can appear only within the <inbound>, <outbound>, <backend> section elements.
    - To apply a policy to the incoming request (before it is forwarded to the backend service), place a corresponding policy element within the <inbound> section element.
    - To apply a policy to the outgoing response (before it is sent back to the caller), place a corresponding policy element within the <outbound> section element.
    - To add a policy, place the cursor at the desired insertion point and select a policy from the sidebar.
    - To remove a policy, delete the corresponding policy statement from the policy document.
    - Position the <base> element within a section element to inherit all policies from the corresponding section element in the enclosing scope.
    - Remove the <base> element to prevent inheriting policies from the corresponding section element in the enclosing scope.
    - Policies are applied in the order of their appearance, from the top down.
    - Comments within policy elements are not supported and may disappear. Place your comments between policy elements or at a higher level scope.
-->
<policies>
    <inbound>
        <base />
        <cors allow-credentials="false">
            <allowed-origins>
                <origin>http://localhost:4200</origin>
                <origin>https://kubernetes.agic.com/</origin>
            </allowed-origins>
            <allowed-methods preflight-result-max-age="300">
                <method>GET</method>
                <method>POST</method>
            </allowed-methods>
            <allowed-headers>
                <header>*</header>
            </allowed-headers>
        </cors>
    </inbound>
    <backend>
        <base />
    </backend>
    <outbound>
        <base />
    </outbound>
    <on-error>
        <base />
    </on-error>
</policies>
















