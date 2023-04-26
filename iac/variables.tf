variable "resource_group_name" {
  default = "LOGCORNER.EDUSYNC.SPEECH.RG"
}

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

#acr

variable "registry_name" {
  default = "logcorneredusyncregistry"
}

variable "registry_sku" {
  default = "Premium"
}

variable "public_network_access_enabled" {
  default = true
}
variable "admin_enabled" {
  default = true
}

# vnet
variable "virtual_network_name" {
  default = "LogCorner.EduSync.Speech.Vnet"
}
variable "virtual_network_address_prefix" {
  default = "10.2.0.0/16"
}

variable "acr_subnet_name" {
  default = "acrSubnet"
}
variable "acr_subnet_address_prefix" {
  default = "10.2.1.0/24"
}
variable "database_subnet_name" {
  default = "databaseSubnet"
}

variable "database_subnet_address_prefix" {
  default = "10.2.2.0/24"
}

variable "aks_subnet_name" {
  default = "aksSubnet"
}

variable "aks_subnet_address_prefix" {
  default = "10.2.0.0/24"
}

variable "management_subnet_name" {
  default = "managementSubnet"
}

variable "management_subnet_address_prefix" {
  default = "10.2.3.0/24"
}

variable "bastion_subnet_address_prefix" {
  default = "10.2.4.0/27"
}

#aks
variable "aks_name" {
  default = "LogCornerEduSyncSpeechCluster"
}

variable "aks_dns_prefix" {
  description = "Optional DNS prefix to use with hosted Kubernetes API server FQDN."
  default     = "aks-cluster-dns"
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

variable "network_plugin" {
  default = "kubenet"
}

variable "network_policy" {
  default = "calico"
}

variable "load_balancer_sku" {
  default = "standard"
}

#jumbobox

variable "jumbobox_nic_name" {
  default = "windows_vm_nic"
}

variable "jumbobox_vm_name" {
  default = "win-bastion-vm"
}

variable "jumbobox_vm_size" {
  default = "Standard_F2"
}

variable "jumbobox_admin_username" {
  default = "adminuser"
}

variable "jumbobox_admin_password" {
  default = "P@$$w0rd1234!"
}

variable "bastion_public_ip_name" {
  default = "bastion_public_ip"
}

variable "bastion_public_ip_sku" {
  default = "Standard"
}

variable "bastion_host_name" {
  default = "bastion_host"
}

# private endpoint

variable "sql_private_endpoint_network_interface_name" {
  default = "sql-pe-nic"
}

variable "sql_private_endpoint_name" {
  default = "sql-pe"
}
variable "dns_zone_virtual_network_link_name" {
  default = "sql-vnet-link"
}

#sql server
variable "mssql_server_name" {
  default = "logcorner-edusync-speech-mssqlserver"
}

variable "mssql_server_version" {
  default = "12.0"
}

variable "mssql_server_administrator_login" {
  default = "missadministrator"
}

variable "mssql_server_administrator_login_password" {
  default = "MyC0m9l&xP@ssw0rd"
}

variable "mssql_database_name" {
  default = "LogCorner.EduSync.Speech.Database"
}