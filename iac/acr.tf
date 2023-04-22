resource "azurerm_container_registry" "acr" {
  name                          = var.registry_name
  location                      = azurerm_resource_group.rg.location
  resource_group_name           = azurerm_resource_group.rg.name
  public_network_access_enabled = true
  sku                           = "Premium"
  admin_enabled                 = true

  tags = (merge(var.default_tags, tomap({
    type = "container_registry"
    })
  ))
}


