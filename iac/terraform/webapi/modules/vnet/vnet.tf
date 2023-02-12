
resource "azurerm_virtual_network" "apim-aks" {
  name                = "apim-aks-vnet"
  address_space       = ["10.10.0.0/16"]
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name
}

resource "azurerm_subnet" "aks" {
  name                 = "aks-subnet"
  resource_group_name  = var.resource_group_name
  virtual_network_name = azurerm_virtual_network.apim-aks.name
  address_prefixes = ["10.10.1.0/24"]
}


resource "azurerm_subnet" "apim" {
  name                 = "apim-subnet"
  resource_group_name  = var.resource_group_name
  virtual_network_name = azurerm_virtual_network.apim-aks.name
  address_prefixes = ["10.10.2.0/24"]
}
resource "azurerm_subnet" "agic-aks" {
  name                 = "agic-aks-subnet"
  resource_group_name  = var.resource_group_name
  virtual_network_name = azurerm_virtual_network.apim-aks.name
  address_prefixes = ["10.10.3.0/24"]
}

resource "azurerm_role_assignment" "aks" {
  principal_id         = var.kubernetes_cluster_principal
  role_definition_name = "Network Contributor"
  scope                = azurerm_subnet.aks.id # Subnet ID
}



