
variable "resource_group_location" {
  type        = string
  description = "Location of the azure resource group."
}

variable "resource_group_name" {
  type        = string
  description = "Location of the azure resource group."
}

variable "service_url" {
  default = "http://10.10.1.5"
}
variable "swaggerurl" {
  default = "http://10.10.1.5/swagger/v1/swagger.json"
}

# variable "kubernetes_cluster_identity" {
  
# }

variable "kubernetes_cluster_principal" {
  
}