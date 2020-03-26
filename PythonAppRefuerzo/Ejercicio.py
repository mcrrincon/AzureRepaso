import requests
import json

url = 'http://northwind.demos.network/api/products_by_category?type=json'

respuesta = requests.get(url)   #respuesta en JSON
datos = respuesta.json()        #respuesta en formato Object

#Filtrar productos de la categoría Bebidas
#CategoryName -> "Beverages"

print(f"Número de Productos: {len(datos)}")

def filterBeverages(e):
    if e['CategoryName'] == "Beverages":
        return e

bebidas = list(filter(filterBeverages, datos));

bebidas2 = list(filter(lambda e: e['CategoryName'] == "Beverages", datos));

print(f"Número de Productos (Bebidas): {len(bebidas2)}")

def sortByStock(e):
    return e['UnitsInStock']

bebidas2.sort(key=sortByStock,reverse=True)

for b in bebidas2:
    print(f"{b['ProductName']} Stock: {b['UnitsInStock']}")