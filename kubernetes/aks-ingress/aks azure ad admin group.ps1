#Install kubelogin (this installs both kubectl and kubelogin if you don't have them already)
az aks install-cli
kubectl version --client 
kubelogin --help


#Create a resource group for the serivces we're going to create
az group create --name AKS-AAD --location centralus


#Create a cluster admin group in Azure AD
az ad group create `
    --display-name AKSClusterAdmins `
    --mail-nickname AKSClusterAdmins


#Get the ID for this group
$AAD_GROUP_ID=$(az ad group show --group AKSClusterAdmins  --query id -o tsv)
Write-Host $AAD_GROUP_ID


#Adding a new administrative user to the AKSClusterAdmins group
$AAD_MEMBER_ID=$(az ad user list --query "[?contains(displayName, 'Gora LEYE')].[id]" -o tsv)
Write-Host $AAD_MEMBER_ID
az ad group member add --group AKSClusterAdmins --member-id $AAD_MEMBER_ID


#Get your AAD Tenant ID
$AAD_TENANT_ID=$(az account list --query "[?contains(name, 'Microsoft Azure Sponsorship')].[tenantId]" -o tsv)[0]
Write-Host $AAD_TENANT_ID


#Deploy a Cluster using AAD
# --aad-admin-group-object-ids is a comma seperated list of AAD group object IDs that will be set as cluster admin.
az aks create `
    --resource-group AKS-AAD `
    --name AKSCluster1 `
    --enable-aad `
    --aad-admin-group-object-ids $AAD_GROUP_ID `
    --aad-tenant-id $AAD_TENANT_ID


#Get our new cluster's credentials...don't use the --admin flag here that will download the certificate based credential for the cluster.
#This downloads a config using AAD for authentication
#If you want to disable local logins you can add the --disable-local-accounts to your az aks create command or with az aks update
az aks get-credentials `
    --resource-group AKS-AAD `
    --admin  `
    --name AKSCluster1


#This will launch a web browser interactive login
kubectl get nodes


#You can continue to run commands without authenticating again until your session times out.
kubectl get service -A


#Let's remove access to this user from this cluster by removing it from the group
#Remove a user from the admin group...wait one minute before moving on to the next step while the group membership updates in AAD
az ad group member remove --group AKSClusterAdmins --member-id $AAD_MEMBER_ID


#You'll still have access until your credential cache expires
kubectl get nodes


#So let's delete the credential cache
kubelogin remove-tokens


#Now this should fail, you'll get an interactive authentication, but you will not have access to the cluster's resources
kubectl get nodes


#Let's keep this cluster around for the next demo, but if you need to clean up things here's the code.

#Delete the AAD Group
#az ad group delete --group AKSClusterAdmins 

#Delete the resource group which deletes the cluster
#az group delete --resource-group AKS-AAD 
