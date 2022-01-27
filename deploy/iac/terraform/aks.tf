resource "azurerm_kubernetes_cluster" "default" {
  name                = "${var.name}-aks"
  location            = "${azurerm_resource_group.default.location}"
  resource_group_name = "${azurerm_resource_group.default.name}"
  dns_prefix          = "${var.dns_prefix}-${var.name}-aks-${var.environment}"
  #depends_on          = [azurerm_role_assignment.aks_network, azurerm_role_assignment.aks_acr]


  default_node_pool  {
    name            = "agentpool"
    node_count            = "${var.node_count}"
    vm_size         = "${var.node_type}"
   
  }
  
  identity {
    type = "SystemAssigned"
  }
  
}

locals {
  acr_name = "${replace(var.dns_prefix, "-", "")}${replace(var.name, "-", "")}acr"
}
resource "azurerm_container_registry" "default" {
  name                     = "${local.acr_name}"
  resource_group_name      = "${azurerm_resource_group.default.name}"
  location                 = "${azurerm_resource_group.default.location}"
  sku                      = "Standard"
  admin_enabled            = false
}

resource "azurerm_role_assignment" "aks_acr" {
  scope                = azurerm_container_registry.default.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_kubernetes_cluster.default.kubelet_identity[0].object_id
}