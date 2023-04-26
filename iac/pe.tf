resource "azurerm_private_dns_zone" "private_dns_zone" {
  name                = "privatelink.database.windows.net"
  resource_group_name = azurerm_resource_group.rg.name
  depends_on = [
    azurerm_resource_group.rg,
  ]
}

data "azurerm_network_interface" "example" {
  name                = "sql-pe-nic-log"
  resource_group_name = azurerm_resource_group.rg.name

  depends_on = [
    azurerm_private_endpoint.private_endpoint
  ]
}

output "network_interface_private_ip_address" {
  value = data.azurerm_network_interface.example.private_ip_address
}

resource "azurerm_private_dns_a_record" "private_dns_a_record" {
  name                = "logcorner-edusync-speech-mssqlserver"
  records             = [data.azurerm_network_interface.example.private_ip_address]
  resource_group_name = azurerm_resource_group.rg.name
  tags = {
    creator = "created by private endpoint sql-pe with resource guid f98da0ab-a7d2-45ff-86e4-0ea891b0481c"
  }
  ttl       = 10
  zone_name = "privatelink.database.windows.net"
  depends_on = [
    azurerm_private_dns_zone.private_dns_zone,
  ]
}

# resource "azurerm_network_interface" "res-7" {
#   location            = "westeurope"
#   name                = "sql-pe-nic"
#   resource_group_name = "LOGCORNER.EDUSYNC.SPEECH.RG"
#   ip_configuration {
#     name                          = "sql-pe.nic.5758da49-c742-4345-88aa-444d93d9ea95"
#     private_ip_address_allocation = "Dynamic"
#     subnet_id                     = azurerm_subnet.databasesubnet.id
#   }
#   depends_on = [
#     azurerm_subnet.databasesubnet
#   ]
# }

resource "azurerm_private_dns_zone_virtual_network_link" "dns_zone_virtual_network_link" {
  name                  = "jypehpisk3aui"
  private_dns_zone_name = "privatelink.database.windows.net"
  resource_group_name   = azurerm_resource_group.rg.name
  virtual_network_id    = azurerm_virtual_network.vnet.id
  depends_on = [
    azurerm_private_dns_zone.private_dns_zone,
    azurerm_virtual_network.vnet,
  ]
}
resource "azurerm_private_endpoint" "private_endpoint" {
  custom_network_interface_name = "sql-pe-nic-log"
  
  location            = "westeurope"
  name                = "sql-pe"
  resource_group_name = azurerm_resource_group.rg.name
  subnet_id           = azurerm_subnet.databasesubnet.id
  private_dns_zone_group {
    name                 = "default"
    private_dns_zone_ids = [azurerm_private_dns_zone.private_dns_zone.id]
  }
  private_service_connection {
    is_manual_connection           = false
    name                           = "sql-pe"
    private_connection_resource_id = azurerm_mssql_server.mssql_server.id
    subresource_names              = ["sqlServer"]
  }
  

  depends_on = [
    azurerm_private_dns_zone.private_dns_zone,
    azurerm_subnet.databasesubnet,
    azurerm_mssql_server.mssql_server,
  ]
}
