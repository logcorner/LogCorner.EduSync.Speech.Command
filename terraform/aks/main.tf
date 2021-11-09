# The Azure Active Resource Manager Terraform provider
 terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
       version = "=2.46.0"
    }
    azuread = {
     source  = "hashicorp/azuread"
     version = "= 1.6.0"
   }
  }
}
provider "azurerm" {
  features {}
}
# The Azure Active Directory Terraform provider
provider "azuread" {
  tenant_id = "f12a747a-cddf-4426-96ff-ebe055e215a3"
}

# Reference to the current subscription.  Used when creating role assignments
data "azurerm_subscription" "current" {}

# The main resource group for this deployment
resource "azurerm_resource_group" "default" {
  name     = "${var.name}-${var.environment}-rg"
  location = "${var.location}"
}


# add the role to the identity the kubernetes cluster was assigned
resource "azurerm_role_assignment" "kubweb_to_acr" {
  scope                = azurerm_container_registry.acr.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_kubernetes_cluster.k8s.kubelet_identity[0].object_id
}


