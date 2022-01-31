# The Azure Active Resource Manager Terraform provider
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~>2.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = "~> 2.0.0"
    }
  }
}
provider "azurerm" {
  features {}
}

# Configure the Azure Active Directory Provider
provider "azuread" {
  client_id     = var.client_id
  client_secret = var.client_secret
  tenant_id     = var.tenant_id
}


# Reference to the current subscription.  Used when creating role assignments
data "azurerm_subscription" "current" {}