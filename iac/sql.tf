resource "azurerm_mssql_server" "mssql_server" {
  name                          = "logcorner-edusync-speech-mssqlserver"
  resource_group_name           = azurerm_resource_group.rg.name
  location                      = azurerm_resource_group.rg.location
  version                       = "12.0"
  administrator_login           = "missadministrator"
  administrator_login_password  = "MyC0m9l&xP@ssw0rd"
  minimum_tls_version           = "1.2"
  public_network_access_enabled = false
  tags = (merge(var.default_tags, tomap({
    type = "sql_server"
    })
  ))
}


resource "azurerm_mssql_database" "mssql_database" {
  name      = "LogCorner.EduSync.Speech.Database"
  server_id = azurerm_mssql_server.mssql_server.id


  tags = (merge(var.default_tags, tomap({
    type = "mssql_database"
    })
  ))
}





