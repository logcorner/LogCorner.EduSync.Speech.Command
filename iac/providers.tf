terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.42.0"
    }
  }

  backend "azurerm" {
    resource_group_name  = "WORKSHOP-LOGCORNER-MICROSERVICES"
    storage_account_name = "storagelogornertf"
    container_name       = "tfstate"
    key                  = "LogCorner.EduSync.Speech.Command.tfstate.tfstate"
  }
}

provider "azurerm" {
  features {}
}

