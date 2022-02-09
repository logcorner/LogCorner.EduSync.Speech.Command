
resource "azuread_application" "confidential_client_application" {
  display_name     = "LogCorner.EduSync.ConfidentialClient"
  sign_in_audience = "AzureADMultipleOrgs"
 
}


resource "azuread_application_password" "confidential_client_password" {
  display_name          = "secret"
  application_object_id = azuread_application.confidential_client_application.object_id
}


# Store the password credentials of client application in existing key vault
resource "azurerm_key_vault_secret" "confidential_client_clientid" {
  name         = "confidential-client-client-id"
  value        = azuread_application.confidential_client_application.application_id
  key_vault_id = data.azurerm_key_vault.main.id
}
resource "azurerm_key_vault_secret" "confidential_client_secret" {
  name         = "confidential-client-secret"
  value        = azuread_application_password.confidential_client_password.value
  key_vault_id = data.azurerm_key_vault.main.id
}

