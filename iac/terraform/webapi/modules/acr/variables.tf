
variable "resource_group_name" {
  type        = string
  description = "Location of the azure resource group."
}

variable "resource_group_location" {
  type        = string
  description = "Location of the azure resource group."
}

variable "acr_name" {

}

variable "sku" {

}
variable "kubernetes_cluster_identity" {
  
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