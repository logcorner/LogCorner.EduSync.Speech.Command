terraform {
  backend "azurerm" {
    resource_group_name  = "newrelic"
    storage_account_name = "terraformnewreliciac"
    container_name       = "tfstate-newrelic"
    key                  = "terraform.tfstate"
  }
}