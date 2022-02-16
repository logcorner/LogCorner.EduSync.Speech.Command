# The Azure Active Resource Manager Terraform provider
terraform {
  required_version = ">= 0.14"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~>2.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
       version = "2.6.0"
    }
    random = {
      source  = "hashicorp/random"
      version = "~> 3.1.0"
    }
  }
}
provider "azurerm" {
  features {}
}

