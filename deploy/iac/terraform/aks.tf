resource "azurerm_kubernetes_cluster" "default" {
  name                = "${var.name}-aks"
  location            = "${azurerm_resource_group.default.location}"
  resource_group_name = "${azurerm_resource_group.default.name}"
  dns_prefix          = "${var.dns_prefix}-${var.name}-aks-${var.environment}"
  depends_on          = [azurerm_role_assignment.aks_network, azurerm_role_assignment.aks_acr]


  default_node_pool  {
    name            = "agentpool"
    node_count            = "${var.node_count}"
    vm_size         = "${var.node_type}"
   
  }

  service_principal {
    client_id     = "${azuread_application.default.application_id}"
    client_secret = "${azuread_service_principal_password.default.value}"
  }

  role_based_access_control {
    enabled = true
  }
}