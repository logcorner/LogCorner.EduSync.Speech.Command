resource "azurerm_kubernetes_cluster" "default" {
  name                = var.aks-name
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
  dns_prefix          = "${var.dns_prefix}-${var.name}-aks-${var.environment}"


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
  acr_name = "${replace(var.dns_prefix, "-", "")}${replace(var.name, "-", "")}acr"
}
resource "azurerm_container_registry" "default" {
  name                = local.acr_name
  resource_group_name = azurerm_resource_group.default.name
  location            = azurerm_resource_group.default.location
  sku                 = "Standard"
  admin_enabled       = false
}

resource "azurerm_role_assignment" "aks_acr" {
  scope                = azurerm_container_registry.default.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_kubernetes_cluster.default.kubelet_identity[0].object_id
}

output "kubernetes_cluster_name" {
  description = "The name of the azure kubernetes service cluster"
  value       = azurerm_kubernetes_cluster.default.name
}

output "container_registry_name" {
  description = "The name of the azure container registry"
  value       = azurerm_container_registry.default.name
}