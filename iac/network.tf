resource "azurerm_virtual_network" "vnet" {
  name                = var.virtual_network_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  address_space       = [var.virtual_network_address_prefix]

   tags = (merge(var.default_tags, tomap({
    type = "virtual_network"
    })
  ))
}
resource "azurerm_subnet" "kubesubnet" {
  name                 = var.aks_subnet_name
  address_prefixes     = [var.aks_subnet_address_prefix]
  virtual_network_name = azurerm_virtual_network.vnet.name
  resource_group_name  = azurerm_resource_group.rg.name
}

resource "azurerm_subnet" "acrsubnet" {
  name                 = var.acr_subnet_name
  address_prefixes     = [var.acr_subnet_address_prefix]
  virtual_network_name = azurerm_virtual_network.vnet.name
  resource_group_name  = azurerm_resource_group.rg.name
}

