resource "azurerm_container_registry" "acr" {
  name                          = var.registry_name
  location                      = azurerm_resource_group.rg.location
  resource_group_name           = azurerm_resource_group.rg.name
  public_network_access_enabled = var.public_network_access_enabled
  sku                           = var.registry_sku
  admin_enabled                 = var.admin_enabled

  tags = (merge(var.default_tags, tomap({
    type = "container_registry"
    })
  ))
}


output "acr_login_server" {
  description = "The Login Server of the Azure Container Registry."
  value       = azurerm_container_registry.acr.login_server
}
