
resource "azurerm_container_registry" "default" {
  name                = var.acr_name
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  sku                 = var.sku
  admin_enabled       = false
}

resource "azurerm_role_assignment" "aks_acr" {
  scope                = azurerm_container_registry.default.id
  role_definition_name = "AcrPull"
  principal_id         = var.kubernetes_cluster_identity 
}
