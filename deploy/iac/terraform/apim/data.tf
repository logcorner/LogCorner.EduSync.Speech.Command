
# Reference to the current subscription.  Used when creating role assignments
data "azurerm_subscription" "current" {}

# The main resource group for this deployment
data "azurerm_resource_group" "resource_group" {
  name = var.resource_group_name
}


data "azurerm_subnet" "apim" {
  name                 = var.subnet_id
  resource_group_name  = var.resource_group_name
  virtual_network_name = var.virtual_network_name
}