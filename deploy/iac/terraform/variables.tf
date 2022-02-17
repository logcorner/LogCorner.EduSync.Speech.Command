variable "resource_group_name" {
  type        = string
  description = "Location of the azure resource group."
}
variable "resource_group_location" {
  type        = string
  description = "Location of the azure resource group."
}

variable "environment" {
  type        = string
  description = "Name of the deployment environment"
}

variable "default_tags" {
  type = object({
    environment = string
    deployed_by = string
  })
}

#---------------   azure kubernetes services ----------------------------------------
variable "aks_name" {
  type        = string
  description = "Location of the azure resource group."
}

variable "node_count" {
  type        = string
  description = "The number of K8S nodes to provision."
}
variable "node_type" {
  type        = string
  description = "The size of each node."
}
variable "dns_prefix" {
  type        = string
  description = "DNS Prefix"
}

#---------------   azure container registry ----------------------------------------
variable "acr_name" {

}

variable "sku" {

}

#---------------   azure active directory application  registration ------------------

variable "client_id" {
  default = "4f651204-1814-4a40-bfbf-9e37df91f71e"
}

variable "client_secret" {
  default = "hMT7Q~i1opoyHXd~3w3wn_GgH8e4Fta2xib.v"
}

variable "tenant_id" {
  default = "9f36bd04-e5e8-47f0-b89e-36168d55a5f9"
}

variable "tenantName" {
  type = string
}

variable "ConfidentialClientDisplayName" {
  
}