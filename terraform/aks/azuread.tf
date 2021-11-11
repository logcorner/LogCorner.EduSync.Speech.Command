resource "azuread_application" "default" {
  display_name = "${var.name}-${var.environment}"
 }

resource "azuread_service_principal" "default" {
  application_id = "${azuread_application.default.application_id}"
}


resource "azuread_service_principal_password" "default" {
  service_principal_id = "${azuread_service_principal.default.id}"
}

resource "azurerm_role_assignment" "aks_network" {
  scope                = "${data.azurerm_subscription.current.id}/resourceGroups/${azurerm_resource_group.default.name}"
  role_definition_name = "Network Contributor"
  principal_id         = "${azuread_service_principal.default.id}"
}

# add the role to the identity the kubernetes cluster was assigned
/* resource "azurerm_role_assignment" "kubweb_to_acr" {
  scope                = "${data.azurerm_subscription.current.id}/resourceGroups/${azurerm_resource_group.default.name}/providers/Microsoft.ContainerRegistry/registries/${azurerm_container_registry.acr.name}"
  role_definition_name = "AcrPull"
  principal_id         = "${azuread_service_principal.default.id}"
} */

# add the role to the identity the kubernetes cluster was assigned
resource "azurerm_role_assignment" "kubweb_to_acr" {
  scope                = azurerm_container_registry.acr.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_kubernetes_cluster.k8s.kubelet_identity[0].object_id
}
