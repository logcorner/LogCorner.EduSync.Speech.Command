resource "random_uuid" "random_id" {
  count = 4
}

resource "azuread_application" "web_api_application" {
  display_name     = "LogCorner.EduSync.Speech.Command.Api"
  identifier_uris  = ["https://workshopb2clogcorner.onmicrosoft.com/command/api"]
  sign_in_audience = "AzureADandPersonalMicrosoftAccount"
  web {
    redirect_uris = ["https://localhost:5002/"]
  }
  app_role {
    allowed_member_types = ["Application"]
    description          = "Apps can query the database"
    display_name         = "Query"
    enabled              = true
    id                   = "00000000-0000-0000-0000-111111111111"
    value                = "Query.All"
  }

  api {
    requested_access_token_version = 2

    oauth2_permission_scope {
      admin_consent_description  = "Allow the application to create speech on behalf of the signed-in user."
      admin_consent_display_name = "Speech.Create"
      enabled                    = true
      id                         = element(random_uuid.random_id[*].result, 0) # unique uuid 
      type                       = "User"
      user_consent_description   = "Allow the application to create speech on your behalf."
      user_consent_display_name  = "Speech.Create"
      value                      = "Speech.Create"
    }
    oauth2_permission_scope {
      admin_consent_description  = "Allow the application to edit speech on behalf of the signed-in user."
      admin_consent_display_name = "Speech.Edit"
      enabled                    = true
      id                         = element(random_uuid.random_id[*].result, 1) # unique uuid 
      type                       = "User"
      user_consent_description   = "Allow the application to edit speech on your behalf."
      user_consent_display_name  = "Speech.Edit"
      value                      = "Speech.Edit"
    }

    oauth2_permission_scope {
      admin_consent_description  = "Allow the application to Delete speech on behalf of the signed-in user."
      admin_consent_display_name = "Speech.Delete"
      enabled                    = true
      id                         = element(random_uuid.random_id[*].result, 2) # unique uuid 
      type                       = "User"
      user_consent_description   = "Allow the application to Delete speech on your behalf."
      user_consent_display_name  = "Speech.Delete"
      value                      = "Speech.Delete"
    }
  }
}

resource "azuread_service_principal" "web_api_application" {
  application_id = azuread_application.web_api_application.application_id
}

# Store the password credentials of client application in existing key vault
resource "azurerm_key_vault_secret" "web_api_application_clientid" {
  name         = "CommandApiAzureAdB2C--ClientId"
  value        = azuread_application.web_api_application.application_id
  key_vault_id = data.azurerm_key_vault.main.id
}


