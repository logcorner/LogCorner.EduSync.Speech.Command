resource "azuread_application" "swagger_ui_application" {
  display_name     = "Swagger UI"
  sign_in_audience = "AzureADandPersonalMicrosoftAccount"
  web {
    redirect_uris = ["http://localhost:6000/swagger/oauth2-redirect.html",
    "https://localhost:6001/swagger/oauth2-redirect.html"]

    implicit_grant {
      access_token_issuance_enabled = true
      id_token_issuance_enabled     = true
    }
  }
  api {
    requested_access_token_version = 2
  }

  required_resource_access {
    resource_app_id = azuread_application.web_api_application.application_id

    resource_access {
      id   = azuread_service_principal.web_api_application.app_role_ids["Query.All"]
      type = "Role"
    }

    resource_access {
      id   = element(random_uuid.random_id[*].result, 0) # unique uuid //azuread_service_principal.web_api_application.oauth2_permission_scopes["Speech.Create"]
      type = "Scope"

    }

    resource_access {
      id   = element(random_uuid.random_id[*].result, 1) # unique uuid //azuread_service_principal.web_api_application.oauth2_permission_scopes["Speech.Create"]
      type = "Scope"
    }

    resource_access {
      id   = element(random_uuid.random_id[*].result, 2) # unique uuid //azuread_service_principal.web_api_application.oauth2_permission_scopes["Speech.Create"]
      type = "Scope"
    }
  }
}
resource "azuread_application_password" "swagger_ui_application_password" {
  display_name          = "secret"
  application_object_id = azuread_application.swagger_ui_application.object_id
}

resource "azuread_service_principal" "web_api_application" {
  application_id = azuread_application.web_api_application.application_id
}
resource "azuread_service_principal" "swagger_ui_application" {
  application_id = azuread_application.swagger_ui_application.application_id
}

resource "azuread_app_role_assignment" "example" {
  app_role_id         = azuread_service_principal.web_api_application.app_role_ids["Query.All"]
  principal_object_id = azuread_service_principal.swagger_ui_application.object_id
  resource_object_id  = azuread_service_principal.web_api_application.object_id
}



# Store the password credentials of client application in existing key vault
resource "azurerm_key_vault_secret" "swagger_ui_application_clientid" {
  name         = "swagger-ui-application-client-id"
  value        = azuread_application.swagger_ui_application.application_id
  key_vault_id = data.azurerm_key_vault.main.id
}
resource "azurerm_key_vault_secret" "swagger_ui_application_secret" {
  name         = "swagger-ui-application-secret"
  value        = azuread_application_password.swagger_ui_application_password.value
  key_vault_id = data.azurerm_key_vault.main.id
}




output "application_password" {
  sensitive = true
  value     = azuread_application_password.swagger_ui_application_password.value
}

output "api_client_id" {
  description = "API CLIENT_ID"
  value       = azuread_application.web_api_application.application_id
}

output "client_id" {
  description = "client CLIENT_ID"
  value       = azuread_application.swagger_ui_application.application_id
}