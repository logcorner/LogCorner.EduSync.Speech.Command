
resource "azuread_application" "confidential_client_application" {
  display_name     = "LogCorner.EduSync.Speech.Daemon"
  sign_in_audience = "AzureADMultipleOrgs"

  required_resource_access {
    resource_app_id = azuread_application.notification_server_application.application_id

    resource_access {
      id   = element(random_uuid.random_id[*].result, 3)
      type = "Scope"
    }
  }
 }

resource "azuread_application_password" "confidential_client_password" {
  display_name          = "secret"
  application_object_id = azuread_application.confidential_client_application.object_id
}

resource "azuread_service_principal" "confidential_client_application" {
  application_id = azuread_application.confidential_client_application.application_id
}

resource "azurerm_key_vault_secret" "confidential_client_application_clientid" {
  name         = "AzureAdConfidentialClient--ClientId"
  value        = azuread_application.confidential_client_application.application_id
  key_vault_id = azurerm_key_vault.key_vault_daemon.id
}
resource "azurerm_key_vault_secret" "confidential_application_secret" {
  name         = "AzureAdConfidentialClient--ClientSecret"
  value        = azuread_application_password.confidential_client_password.value
  key_vault_id = azurerm_key_vault.key_vault_daemon.id
}

resource "azurerm_key_vault_secret" "confidential_tenant_id" {
  name         = "AzureAdConfidentialClient--TenantId"
  value        = data.azuread_client_config.current.tenant_id 
  key_vault_id = azurerm_key_vault.key_vault_daemon.id
}

resource "azurerm_key_vault_secret" "confidential_tenant_name" {
  name         = "AzureAdConfidentialClient--TenantName"
  value        = var.tenantName
  key_vault_id = azurerm_key_vault.key_vault_daemon.id
}






 