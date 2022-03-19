terraform {
  backend "azurerm" {
    resource_group_name  = "LOGCORNER-MICROSERVICES-IAC"
    storage_account_name = "tfbackendlogconer"
    container_name       = "backend-tfstate-web-api-resources"
    key                  = "terraform.tfstate"
  }
}  