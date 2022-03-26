data "azurerm_client_config" "current" {}

resource "random_uuid" "random_id" {
  count = 4
}

# command http api
resource "azurerm_key_vault" "key_vault_command" {
  name                        = "logcornervaultcommand"
  location                    = "westeurope"
  resource_group_name         = "LOGCORNER-MICROSERVICES-IAC"
  enabled_for_disk_encryption =  true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = 7
  purge_protection_enabled    = true
  sku_name                    = "standard"

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    key_permissions = [
       "Get","Delete","Delete"
    ]

    secret_permissions = [
      "Get", "Set", "List","Delete","Delete"
    ]

  }
}

# signalr 
resource "azurerm_key_vault" "key_vault_signalr" {
  name                        = "logcornervaultsingalr"
  location                    = "westeurope"
  resource_group_name         = "LOGCORNER-MICROSERVICES-IAC"
  enabled_for_disk_encryption =  true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = 7
  purge_protection_enabled    = true
  sku_name                    = "standard"

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    key_permissions = [
      "Get","Delete","Delete"
    ]

    secret_permissions = [
      "Get", "Set", "List","Delete","Delete"
    ]

  }
}


# signalr 
resource "azurerm_key_vault" "key_vault_daemon" {
  name                        = "logcornervaultdaemon"
  location                    = "westeurope"
  resource_group_name         = "LOGCORNER-MICROSERVICES-IAC"
  enabled_for_disk_encryption =  true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days  = 7
  purge_protection_enabled    = true
  sku_name                    = "standard"

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    key_permissions = [
       "Get","Delete","Delete"
    ]

    secret_permissions = [
      "Get", "Set", "List","Delete","Delete"
    ]

  }
}