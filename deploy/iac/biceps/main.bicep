@description('The name of the Managed Cluster resource.')
param clusterName string = 'akslogcornercluster'
@description('The location of the Managed Cluster resource.')
param location string = resourceGroup().location
@description('Optional DNS prefix to use with hosted Kubernetes API server FQDN.')
param dnsPrefix string = 'logcorner'
@minValue(0)
@maxValue(1023)
@description('Disk size (in GB) to provision for each of the agent pool nodes. This value ranges from 0 to 1023. Specifying 0 will apply the default disk size for that agentVMSize.')
param osDiskSizeGB int = 0
@minValue(1)
@maxValue(50)
@description('The number of nodes for the cluster.')
param agentCount int = 3
@description('The size of the Virtual Machine.')
param agentVMSize string = 'Standard_D2s_v3'

param roleAcrPull string = 'b24988ac-6180-42a0-ab88-20f7382dd24c'
resource clusterName_resource 'Microsoft.ContainerService/managedClusters@2020-09-01' = {
  name: clusterName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
 
  properties: {
    dnsPrefix: dnsPrefix
    enableRBAC: true 
    agentPoolProfiles: [
      {
        name: 'agentpool'
        osDiskSizeGB: osDiskSizeGB
        count: agentCount
        vmSize: agentVMSize
        osType: 'Linux'
        mode: 'System'
      }
    ]
  
  }
}
output controlPlaneFQDN string =clusterName_resource.properties.fqdn


/* CONTAINER REGISTRY */

@minLength(5)
@maxLength(50)
@description('Provide a globally unique name of your Azure Container Registry')
param acrName string = 'acrlogcornerregistry'

@description('Provide a tier of your Azure Container Registry.')
param acrSku string = 'Basic'

resource acrResource 'Microsoft.ContainerRegistry/registries@2021-06-01-preview' = {
  name: acrName
  location: location
  sku: {
    name: acrSku
  }
  properties: {
    adminUserEnabled: true
  }
}

@description('Output the login server property for later use')
output loginServer string = acrResource.properties.loginServer


resource assignAcrPullToAks 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(resourceGroup().id, acrResource.name, 'aksPrincipalId', 'AssignAcrPullToAks')
  scope: acrResource
  properties: {
    description: 'Assign AcrPull role to AKS'
    principalId: clusterName_resource.properties.identityProfile.kubeletidentity.objectId
    principalType: 'ServicePrincipal'
    roleDefinitionId: '/subscriptions/${subscription().subscriptionId}/providers/Microsoft.Authorization/roleDefinitions/${roleAcrPull}'
  }
}
