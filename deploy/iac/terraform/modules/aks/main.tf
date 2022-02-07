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


