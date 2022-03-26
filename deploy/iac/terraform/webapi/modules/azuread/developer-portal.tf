resource "azuread_application" "deveopper_portal_application" {
  display_name     = "LogCorner.EduSync.Speech.DeveloperPortal"
  sign_in_audience = "AzureADandPersonalMicrosoftAccount"
  web {
    redirect_uris = [
    "https://logcorner-apim-agic-speech.developer.azure-api.net/signin-oauth/code/callback/apim-authorization-server",
    "https://logcorner-apim-agic-speech.developer.azure-api.net/signin-oauth/implicit/callback",
    "https://logcorner-apim-agic-speech.portal.azure-api.net/docs/services/apim-authorization-server/console/oauth2/authorizationcode/callback",
    "https://logcorner-apim-agic-speech.portal.azure-api.net/docs/services/apim-authorization-server/console/oauth2/implicit/callback"
    ]
  }
  api {
    requested_access_token_version = 2
  }

  required_resource_access {
    resource_app_id = azuread_application.web_api_application.application_id

    resource_access {
      id   = element(random_uuid.random_id[*].result, 0) 
      type = "Scope"

    }

    resource_access {
      id   = element(random_uuid.random_id[*].result, 1) 
      type = "Scope"
    }

    resource_access {
      id   = element(random_uuid.random_id[*].result, 2) 
      type = "Scope"
    }
   
  }

}
resource "azuread_application_password" "deveopper_portal_application_password" {
  display_name          = "secret"
  application_object_id = azuread_application.deveopper_portal_application.object_id
}

resource "azuread_service_principal" "deveopper_portal_application" {
  application_id = azuread_application.deveopper_portal_application.application_id
}

/*resource "azurerm_key_vault_secret" "deveopper_portal_application_clientid" {
  name         = "SwaggerUI--OAuthClientId"
  value        = azuread_application.deveopper_portal_application.application_id
  key_vault_id = azurerm_key_vault.key_vault.id
}
resource "azurerm_key_vault_secret" "deveopper_portal_application_secret" {
  name         = "SwaggerUI--OAuthClientSecret"
  value        = azuread_application_password.deveopper_portal_application_password.value
  key_vault_id = azurerm_key_vault.key_vault.id
}

resource "azurerm_key_vault_secret" "deveopper_portal_tenant_name" {
  name         = "SwaggerUI--TenantName"
  value        = var.tenantName
  key_vault_id = azurerm_key_vault.key_vault.id
}*/

# output "application_password" {
#   sensitive = true
#   value     = azuread_application_password.deveopper_portal_application_password.value
# }

# output "api_client_id" {
#   description = "API CLIENT_ID"
#   value       = azuread_application.web_api_application.application_id
# }

# output "client_id" {
#   description = "client CLIENT_ID"
#   value       = azuread_application.deveopper_portal_application.application_id
# }