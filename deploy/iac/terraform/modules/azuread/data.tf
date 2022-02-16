data "azuread_client_config" "current" {}


data "azurerm_key_vault" "main" {
  name                = "terraformlogcornervault"
  resource_group_name = "TERRAFORM"
}
