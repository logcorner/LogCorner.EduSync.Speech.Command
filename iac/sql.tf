resource "azurerm_mssql_server" "mssql_server" {
  name                         = "logcorner-edusync-speech-mssqlserver"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = "missadministrator"
  administrator_login_password = "MyC0m9l&xP@ssw0rd"
  minimum_tls_version          = "1.2"

  tags = (merge(var.default_tags, tomap({
    type = "sql_server"
    })
  ))
}


resource "azurerm_mssql_database" "mssql_database" {
  name           = "LogCorner.EduSync.Speech.Database"
  server_id      = azurerm_mssql_server.mssql_server.id
  # collation      = "SQL_Latin1_General_CP1_CI_AS"
  # license_type   = "LicenseIncluded"
  # max_size_gb    = 4
  # read_scale     = true
  # sku_name       = "S0"
  # zone_redundant = true

  tags = (merge(var.default_tags, tomap({
    type = "mssql_database"
    })
  ))
}


resource "azurerm_mssql_firewall_rule" "aks_firewall_rule" {
  name                = "aks-firewall-rule"
  server_id        = azurerm_mssql_server.mssql_server.id
  start_ip_address    = "10.2.0.0"
  end_ip_address      = "10.2.255.255"
}


resource "azurerm_mssql_firewall_rule" "client_ip_firewall_rule" {
  name                = "ClientIPAddress"
  server_id        = azurerm_mssql_server.mssql_server.id
  start_ip_address    = "90.26.62.205"
  end_ip_address      = "90.26.62.205"
}

