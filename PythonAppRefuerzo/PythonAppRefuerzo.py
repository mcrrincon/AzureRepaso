import requests
import json

############################################################################
# Sintaxís request.get(url, headers = xxxx, data = xxxx)
# Sintaxís request.post(url, headers = xxxx, data = xxxx)
#
# xxxx es el nombre de una variable que contiene las cabeceras o los datos
#      Las cabeceras como los datos son opcionales 
############################################################################

url = 'http://northwind.demos.network/api/customers/BOLID?type=json'
respuesta = requests.get(url)

datos = respuesta.json()

print(f"{datos['CompanyName']}")
print(f"{datos['City']} ({datos['Country']})")
print(f"\n=====================================\n")

############################################################################

url = 'http://northwind.demos.network/api/customers/ANATR'
cab = {
    'Accept' : 'application/json',
}

respuesta = requests.get(url, headers = cab)
datos = respuesta.json()

print(f"{datos['CompanyName']}")
print(f"{datos['City']} ({datos['Country']})")
print(f"\n=====================================\n")

############################################################################

url = 'http://northwind.demos.network/api/invoices?type=json'

respuesta = requests.get(url)   #respuesta en JSON
datos = respuesta.json()        #respuesta en formato Object

# Filtrado utilizando una función
def filterOrderID(e):
    if e['OrderID'] == 10248:
        return e

resultado = list(filter(filterOrderID, datos))

# Filtrado utilizado una lambda
resultado2 = list(filter(lambda e: e['OrderID'] == 10248, datos))

# Ordenar utilizando una funcion
def sortProductName(e):
    return e['ProductName']

resultado2.sort(key=sortProductName)

total = 0
for p in resultado2:
    subtotal = (p['UnitPrice'] * p['Quantity'])
    total += subtotal
    print(f"{p['OrderID']} - {p['ProductID']}#{p['ProductName']} {p['Quantity']} x {p['UnitPrice']} = {subtotal} ")

print(f"Total Pedido: {total}")