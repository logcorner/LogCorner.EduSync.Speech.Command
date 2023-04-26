resource "azurerm_network_interface" "network_interface" {
  name                = var.jumbobox_nic_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  ip_configuration {
    name                          = "internal"
    subnet_id                     = azurerm_subnet.managementSubnet.id
    private_ip_address_allocation = "Dynamic"
  }
}

resource "azurerm_windows_virtual_machine" "windows_virtual_machine" {
  name                = var.jumbobox_vm_name
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  size                = var.jumbobox_vm_size
  admin_username      = var.jumbobox_admin_username
  admin_password      = var.jumbobox_admin_password
  network_interface_ids = [
    azurerm_network_interface.network_interface.id,
  ]

  os_disk {
    caching              = "ReadWrite"
    storage_account_type = "Standard_LRS"
  }

  source_image_reference {
    publisher = "MicrosoftWindowsServer"
    offer     = "WindowsServer"
    sku       = "2016-Datacenter"
    version   = "latest"
  }
}

resource "azurerm_public_ip" "bastion_public_ip" {
  name                = var.bastion_public_ip_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  allocation_method   = "Static"
  sku                 = var.bastion_public_ip_sku
}

resource "azurerm_bastion_host" "bastion_host" {
  name                = var.bastion_host_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  ip_configuration {
    name                 = "configuration"
    subnet_id            = azurerm_subnet.azureBastionSubnet.id
    public_ip_address_id = azurerm_public_ip.bastion_public_ip.id
  }
}