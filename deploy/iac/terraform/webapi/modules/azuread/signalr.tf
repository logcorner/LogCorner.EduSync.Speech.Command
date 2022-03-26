resource "azuread_application" "notification_server_application" {
  display_name     = "LogCorner.EduSync.Notification.Server"
  identifier_uris  = ["https://workshopb2clogcorner.onmicrosoft.com/signalr/hub"]
  sign_in_audience = "AzureADandPersonalMicrosoftAccount"

  api {
    requested_access_token_version = 2

      oauth2_permission_scope {
      admin_consent_description  = "Allow the application to send messages on behalf of the signed-in user."
      admin_consent_display_name = "event-notifier"
      enabled                    = true
      id                         = element(random_uuid.random_id[*].result, 3) 
      type                       = "Admin"
      user_consent_description   = "Allow the application to send messages on your behalf."
      user_consent_display_name  = "event-notifier"
      value                      = "event-notifier"
    }

  }
}

resource "azuread_service_principal" "notification_server_application" {
  application_id = azuread_application.notification_server_application.application_id
}


# Store the password credentials of client application in existing key vault
resource "azurerm_key_vault_secret" "notification_server_application_b2Cclientid" {
  name         = "AzureAdB2C--ClientId"
  value        = azuread_application.notification_server_application.application_id
  key_vault_id = azurerm_key_vault.key_vault_signalr.id
}

resource "azurerm_key_vault_secret" "notification_server_b2cdomain" {
  name         = "AzureAdB2C--Domain"
  value        = "${var.tenantName}.onmicrosoft.com"
  key_vault_id = azurerm_key_vault.key_vault_signalr.id
}

resource "azurerm_key_vault_secret" "notification_server_b2ctenantId" {
  name         = "AzureAdB2C--TenantId"
  value        = data.azuread_client_config.current.tenant_id
  key_vault_id = azurerm_key_vault.key_vault_signalr.id
}


resource "azurerm_key_vault_secret" "notification_server_application_clientid" {
  name         = "AzureAd--ClientId"
  value        = azuread_application.notification_server_application.application_id
  key_vault_id = azurerm_key_vault.key_vault_signalr.id
 
}

resource "azurerm_key_vault_secret" "notification_server_domain" {
  name         = "AzureAd--Domain"
  value        = "${var.tenantName}.onmicrosoft.com"
  key_vault_id = azurerm_key_vault.key_vault_signalr.id
 
}

resource "azurerm_key_vault_secret" "notification_server_tenantId" {
  name         = "AzureAd--TenantId"
  value        = data.azuread_client_config.current.tenant_id
  key_vault_id = azurerm_key_vault.key_vault_signalr.id
 
}
