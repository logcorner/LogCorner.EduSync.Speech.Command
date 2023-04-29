resource "azurerm_private_dns_zone" "private_dns_zone" {
  name                = "privatelink.database.windows.net"
  resource_group_name = azurerm_resource_group.rg.name

  tags = (merge(var.default_tags, tomap({
    type = "private_dns_zone"
    })
  ))
  depends_on = [
    azurerm_resource_group.rg,
  ]
}

data "azurerm_network_interface" "example" {
  name                = var.sql_private_endpoint_network_interface_name
  resource_group_name = azurerm_resource_group.rg.name

  depends_on = [
    azurerm_private_endpoint.private_endpoint
  ]
}

# resource "azurerm_private_dns_a_record" "private_dns_a_record" {
#   name                = azurerm_mssql_server.mssql_server.name
#   records             = [data.azurerm_network_interface.example.private_ip_address]
#   resource_group_name = azurerm_resource_group.rg.name

#   ttl       = 10
#   zone_name = azurerm_private_dns_zone.private_dns_zone.name
#   depends_on = [
#     azurerm_private_dns_zone.private_dns_zone,
#   ]

#   tags = (merge(var.default_tags, tomap({
#     type = "private_dns_a_record"
#     })
#   ))
# }

resource "azurerm_private_dns_zone_virtual_network_link" "dns_zone_virtual_network_link" {
  name                  = var.dns_zone_virtual_network_link_name
  private_dns_zone_name = azurerm_private_dns_zone.private_dns_zone.name
  resource_group_name   = azurerm_resource_group.rg.name
  virtual_network_id    = azurerm_virtual_network.vnet.id

  tags = (merge(var.default_tags, tomap({
    type = "dns_zone_virtual_network_link"
    })
  ))

  depends_on = [
    azurerm_private_dns_zone.private_dns_zone,
    azurerm_virtual_network.vnet,
  ]
}
resource "azurerm_private_endpoint" "private_endpoint" {
  custom_network_interface_name = var.sql_private_endpoint_network_interface_name

  location            = azurerm_resource_group.rg.location
  name                = var.sql_private_endpoint_name
  resource_group_name = azurerm_resource_group.rg.name
  subnet_id           = azurerm_subnet.databasesubnet.id
  private_dns_zone_group {
    name                 = "default"
    private_dns_zone_ids = [azurerm_private_dns_zone.private_dns_zone.id]
  }
  private_service_connection {
    is_manual_connection           = false
    name                           = "${var.sql_private_endpoint_name}_private_service_connection"
    private_connection_resource_id = azurerm_mssql_server.mssql_server.id
    subresource_names              = ["sqlServer"]
  }

  tags = (merge(var.default_tags, tomap({
    type = "private_endpoint"
    })
  ))

  depends_on = [
    azurerm_private_dns_zone.private_dns_zone,
    azurerm_subnet.databasesubnet,
    azurerm_mssql_server.mssql_server,
  ]
}

output "network_interface_private_ip_address" {
  value = data.azurerm_network_interface.example.private_ip_address
}
