terraform {
  backend "azurerm" {
    resource_group_name  = "terraform-rg"
    storage_account_name = "tfbackenglogconer"
    container_name       = "tfbackend"
    key                  = "terraform.tfstate"
  }
}  