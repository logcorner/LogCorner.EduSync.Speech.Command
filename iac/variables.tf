variable "resource_group_name" {
  default = "LOGCORNER.EDUSYNC.SPEECH.RG"
}

# variable "tags" {
#   type    = map(string)
#   default = {
#     Environment = "dev"
#     Owner       = "John Doe"
#   }
# }

variable "default_tags" {
  type = object({
    environment   = string
    deployed_by   = string
    business_unit = string
    owner         = string
    project       = string
  })
}

variable "resource_group_location" {
  default = "westeurope"
}

variable "registry_name" {
  default = "logcorneredusyncregistry"
}

variable "virtual_network_name" {
  default = "LogCorner.EduSync.Speech.Vnet"
}
variable "virtual_network_address_prefix" {
  default = "10.2.0.0/16"
}
variable "aks_name" {
  default = "LogCornerEduSyncSpeechCluster"
}

variable "aks_dns_prefix" {
  description = "Optional DNS prefix to use with hosted Kubernetes API server FQDN."
  default     = "aks-cluster-dns"
}

variable "aks_subnet_name" {
  default = "aksSubnet"
}

variable "aks_subnet_address_prefix" {
  default = "10.2.0.0/24"
}

variable "aks_dns_service_ip" {
  default = "10.4.0.10"
}

variable "aks_docker_bridge_cidr" {
  default = "10.3.0.0/16"
}

variable "aks_service_cidr" {
  default = "10.4.0.0/16"
}


variable "aks_agent_os_disk_size" {
  description = "Disk size (in GB) to provision for each of the agent pool nodes. This value ranges from 0 to 1023. Specifying 0 applies the default disk size for that agentVMSize."
  default     = 40
}

variable "aks_agent_count" {
  description = "The number of agent nodes for the cluster."
  default     = 1
}

variable "aks_agent_vm_size" {
  description = "VM size"
  default     = "Standard_D3_v2"
}

variable "kubernetes_version" {
  description = "Kubernetes version"
  default     = "1.24.3"
}

variable "acr_subnet_name" {
  default = "acrSubnet"
}
variable "acr_subnet_address_prefix" {
  default = "10.2.1.0/24"
}
