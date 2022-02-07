resource "azurerm_kubernetes_cluster" "default" {
  name                = var.aks-name
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name
  dns_prefix          = "${var.dns_prefix}-${var.resource_group_name}-aks-${var.environment}"


  default_node_pool {
    name       = "agentpool"
    node_count = var.node_count
    vm_size    = var.node_type

  }

  identity {
    type = "SystemAssigned"
  }

}

locals {
  acr_name = "locornermsacr"
}
resource "azurerm_container_registry" "default" {
  name                = local.acr_name
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  sku                 = "Standard"
  admin_enabled       = false
}

resource "azurerm_role_assignment" "aks_acr" {
  scope                = azurerm_container_registry.default.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_kubernetes_cluster.default.kubelet_identity[0].object_id
}

