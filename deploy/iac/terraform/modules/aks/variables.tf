variable "resource_group_location" {
  type        = string
  description = "Location of the azure resource group."
}

variable "resource_group_name" {
  type        = string
  description = "Location of the azure resource group."
}


variable "environment" {
  type        = string
  description = "Name of the deployment environment"
}


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

variable "default_tags" {
  type = object({
    environment       = string
    deployed_by = string
  })
   default = {
   environment = ""
  deployed_by = ""
  }
}

variable "tags" {
  
}

variable "subnet_aks_id" {
  
}

variable "subnet_agic_id" {
  
}