resource "azurerm_mssql_server" "mssql_server" {
  name                          = var.mssql_server_name
  resource_group_name           = azurerm_resource_group.rg.name
  location                      = azurerm_resource_group.rg.location
  version                       = var.mssql_server_version
  administrator_login           = var.mssql_server_administrator_login
  administrator_login_password  = var.mssql_server_administrator_login_password
  minimum_tls_version           = "1.2"
  public_network_access_enabled = false

  tags = (merge(var.default_tags, tomap({
    type = "sql_server"
    })
  ))
}

resource "azurerm_mssql_database" "mssql_database" {
  name      = var.mssql_database_name
  server_id = azurerm_mssql_server.mssql_server.id


  tags = (merge(var.default_tags, tomap({
    type = "mssql_database"
    })
  ))
}





