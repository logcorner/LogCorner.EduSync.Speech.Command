data "azuread_client_config" "current" {}


data "azurerm_key_vault" "main" {
  name                = "terraformlogcornervault"
  resource_group_name = "TERRAFORM"
}


# Configure the Azure Active Directory Provider
provider "azuread" {
  client_id     = var.client_id
  client_secret = var.client_secret
  tenant_id     = var.tenant_id
}