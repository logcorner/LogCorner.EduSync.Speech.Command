# The Azure Active Resource Manager Terraform provider
terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
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

# The Azure Active Directory Terraform provider
/* terraform {
  required_providers {
    azuread = {
      source  = "hashicorp/azuread"
      version = "~> 2.0.0"
    }
  }
} */

# Configure the Azure Active Directory Provider
 provider "azuread" {
  tenant_id = "f12a747a-cddf-4426-96ff-ebe055e215a3"
}
 
# Retrieve domain information
data "azuread_domains" "example" {
  only_initial = true
}
# Reference to the current subscription.  Used when creating role assignments
data "azurerm_subscription" "current" {}

# The main resource group for this deployment
resource "azurerm_resource_group" "default" {
  name     = "${var.name}-${var.environment}-rg"
  location = "${var.location}"
}