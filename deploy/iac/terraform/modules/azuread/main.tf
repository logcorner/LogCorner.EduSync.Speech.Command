terraform {
  required_version = ">= 0.14"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=2.37.0"
    }

    random = {
      source  = "hashicorp/random"
      version = "~> 3.1.0"
    }

  }
}

provider "azuread" {
  client_id     = "4f651204-1814-4a40-bfbf-9e37df91f71e"
  client_secret = "hMT7Q~i1opoyHXd~3w3wn_GgH8e4Fta2xib.v"
  tenant_id     = "9f36bd04-e5e8-47f0-b89e-36168d55a5f9"
   
}



provider "azurerm" {
  features {}
}

data "azuread_client_config" "current" {}


data "azurerm_key_vault" "main" {
  name                = "terraformlogcornervault"
  resource_group_name = "TERRAFORM"
}
